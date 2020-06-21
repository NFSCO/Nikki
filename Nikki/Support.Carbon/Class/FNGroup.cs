﻿using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Utils.EA;
using Nikki.Reflection.Enum;
using Nikki.Support.Carbon.Framework;
using Nikki.Support.Shared.Parts.FNGParts;
using CoreExtensions.IO;
using CoreExtensions.Conversions;
using System.Runtime.CompilerServices;

namespace Nikki.Support.Carbon.Class
{
    /// <summary>
    /// <see cref="FNGroup"/> is a collection of frontend group elements and scripts.
    /// </summary>
    public class FNGroup : Shared.Class.FNGroup
    {
		#region Properties

        /// <summary>
        /// Actual data of this <see cref="FNGroup"/>.
        /// </summary>
		[Browsable(false)]
        public byte[] Data { get; private set; }

        /// <summary>
        /// Game to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override GameINT GameINT => GameINT.Carbon;

        /// <summary>
        /// Game string to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public override string GameSTR => GameINT.Carbon.ToString();

        /// <summary>
        /// Manager to which the class belongs to.
        /// </summary>
        [Browsable(false)]
        public FNGroupManager Manager { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="FNGroup"/>.
        /// </summary>
        public FNGroup() { }

        /// <summary>
        /// Initializes new instance of <see cref="FNGroup"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="FNGroupManager"/> to which this instance belongs to.</param>
        public FNGroup(BinaryReader br, FNGroupManager manager)
        {
            this.Manager = manager;
            this.Disassemble(br);
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~FNGroup() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="FNGroup"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="FNGroup"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            using var ms = new MemoryStream(this.Data);
            using var writer = new BinaryWriter(ms);

            bw.WriteEnum(eBlockID.FEngFiles);
            bw.Write(this.Data.Length);

            foreach (var color in this._colorinfo)
            {
                writer.BaseStream.Position = color.Offset;
                writer.Write((uint)color.Blue);
                writer.Write((uint)color.Green);
                writer.Write((uint)color.Red);
                writer.Write((uint)color.Alpha);
            }

            bw.Write(this.Data);
        }

        /// <summary>
        /// Disassembles array into <see cref="FNGroup"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="FNGroup"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            var ID = br.ReadUInt32();
            var size = br.ReadInt32();

            this.Data = SAT.Decompress(br.ReadBytes(size), ID);

            using var ms = new MemoryStream(this.Data);
            using var reader = new BinaryReader(ms);

            reader.BaseStream.Position = 0x28;
            this.CollectionName = reader.ReadNullTermUTF8().ToUpper();

            if (this.CollectionName.EndsWith(".FNG"))
            {
            
                this.CollectionName.GetFormattedValue("{X}.FNG", out string name);
                this.CollectionName = name;
            
            }

            reader.BaseStream.Position = 0x28;
            
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
            
                byte b1 = reader.ReadByte();
                byte b2 = reader.ReadByte();
                byte b3 = reader.ReadByte();
                byte b4 = reader.ReadByte();

                // SAT, SAD, SA(0x90) or 1111
                if ((b1 == 'S' && b2 == 'A') || (b1 == Byte.MaxValue && b2 == Byte.MaxValue && 
                     b3 == Byte.MaxValue && b4 == Byte.MaxValue))
                {
                
                    uint Offset = (uint)reader.BaseStream.Position;
                    uint Blue = reader.ReadUInt32();
                    uint Green = reader.ReadUInt32();
                    uint Red = reader.ReadUInt32();
                    uint Alpha = reader.ReadUInt32();
                    
                    if (Blue <= Byte.MaxValue && Green <= Byte.MaxValue && 
                        Red <= Byte.MaxValue && Alpha <= Byte.MaxValue)
                    {
                    
                        var color = new FEngColor(this)
                        {
                            Offset = Offset,
                            Blue = (byte)Blue,
                            Green = (byte)Green,
                            Red = (byte)Red,
                            Alpha = (byte)Alpha
                        };
                        
                        this._colorinfo.Add(color);
                    
                    }
                
                }
            
            }
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="FNGroup"/> 
        /// as a string value.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return $"Collection Name: {this.CollectionName} | " +
                   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes instance into a byte array and stores it in the file provided.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
        public override void Serialize(BinaryWriter bw)
        {
            byte[] array;
            var size = this.Data.Length + (this.InfoLength << 3) + this.CollectionName.Length + 0x20;
            using (var ms = new MemoryStream(size))
            using (var writer = new BinaryWriter(ms))
            {

                writer.WriteNullTermUTF8(this.CollectionName);
                writer.Write(this.InfoLength);

                foreach (var color in this._colorinfo)
                {

                    writer.Write(color.Offset);
                    writer.Write(color.Alpha);
                    writer.Write(color.Red);
                    writer.Write(color.Green);
                    writer.Write(color.Blue);

                }

                bw.Write(this.Data.Length);
                bw.Write(this.Data);

                array = ms.ToArray();

            }

            array = Interop.Compress(array, eLZCompressionType.BEST);

            var header = new SerializationHeader(array.Length, this.GameINT, this.Manager.Name);
            header.Write(bw);
            bw.Write(array.Length);
            bw.Write(array);
        }

        /// <summary>
        /// Deserializes byte array into an instance by loading data from the file provided.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        public override void Deserialize(BinaryReader br)
        {
            int size = br.ReadInt32();
            var array = br.ReadBytes(size);

            array = Interop.Decompress(array);

            using var ms = new MemoryStream(array);
            using var reader = new BinaryReader(ms);

            this.CollectionName = reader.ReadNullTermUTF8();
            var count = reader.ReadInt32();
            this._colorinfo.Capacity = count;

            for (int loop = 0; loop < count; ++loop)
			{

				var color = new FEngColor(this)
				{
					Offset = reader.ReadUInt32(),
					Alpha = reader.ReadByte(),
					Red = reader.ReadByte(),
					Green = reader.ReadByte(),
					Blue = reader.ReadByte()
				};

                this._colorinfo.Add(color);

			}

            count = br.ReadInt32();
            this.Data = br.ReadBytes(count);
        }

        #endregion
    }
}