﻿using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Reflection.Enum.SlotID;
using Nikki.Support.Carbon.Framework;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Carbon.Class
{
    /// <summary>
    /// <see cref="SlotOverride"/> is a collection of settings related to car's slot overrides.
    /// </summary>
    public class SlotOverride : Shared.Class.SlotOverride
    {
        #region Fields

        private string _collection_name;

        /// <summary>
        /// Maximum length of the CollectionName.
        /// </summary>
        public const int MaxCNameLength = -1;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = -1;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0x24;

        #endregion

        #region Properties

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
        public SlotOverrideManager Manager { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [AccessModifiable()]
        [Category("Main")]
        public override string CollectionName
        {
            get => this._collection_name;
            set
            {
                this.Manager?.CreationCheck(value);
                this._collection_name = value;
            }
        }

        /// <summary>
        /// Binary memory hash of the collection name.
        /// </summary>
        [Category("Main")]
        [TypeConverter(typeof(HexConverter))]
        public override uint BinKey => this._collection_name.BinHash();

        /// <summary>
        /// Vault memory hash of the collection name.
        /// </summary>
        [Category("Main")]
        [TypeConverter(typeof(HexConverter))]
        public override uint VltKey => this._collection_name.VltHash();

        /// <summary>
        /// Group 2 info override entry of this <see cref="SlotOverride"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string InfoOverrideGroup2 { get; set; }

        /// <summary>
        /// Group 3 info override entry of this <see cref="SlotOverride"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string InfoOverrideGroup3 { get; set; }

        /// <summary>
        /// Group 4 info override entry of this <see cref="SlotOverride"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string InfoOverrideGroup4 { get; set; }

        /// <summary>
        /// Group 5 info override entry of this <see cref="SlotOverride"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string InfoOverrideGroup5 { get; set; }

        /// <summary>
        /// Group 6 info override entry of this <see cref="SlotOverride"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string InfoOverrideGroup6 { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="SlotOverride"/>.
        /// </summary>
        public SlotOverride() { }

        /// <summary>
        /// Initializes new instance of <see cref="SlotOverride"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="SlotOverrideManager"/> to which this instance belongs to.</param>
        public SlotOverride(string CName, SlotOverrideManager manager)
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="SlotOverride"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="SlotOverrideManager"/> to which this instance belongs to.</param>
        public SlotOverride(BinaryReader br, SlotOverrideManager manager)
        {
            this.Manager = manager;
            this.Disassemble(br);
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~SlotOverride() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="SlotType"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SlotType"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            var keys = this._collection_name.Split("_PART_", 2, StringSplitOptions.None);
            var id = (eSlotCarbon)Enum.Parse(typeof(eSlotCarbon), keys[1]);

            bw.Write(keys[0].BinHash());
            bw.Write((int)id);
            bw.Write(keys[0].BinHash());
            bw.Write(this.InfoMainOverride.BinHash());
            bw.Write(this.InfoOverrideGroup2.BinHash());
            bw.Write(this.InfoOverrideGroup3.BinHash());
            bw.Write(this.InfoOverrideGroup4.BinHash());
            bw.Write(this.InfoOverrideGroup5.BinHash());
            bw.Write(this.InfoOverrideGroup6.BinHash());
        }

        /// <summary>
        /// Disassembles array into <see cref="SlotType"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="SlotType"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            var key = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            var id = (eSlotCarbon)br.ReadInt32();
            br.BaseStream.Position += 4;
            this._collection_name = $"{key}_PART_{id}";

            this.InfoMainOverride = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.InfoOverrideGroup2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.InfoOverrideGroup3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.InfoOverrideGroup4 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.InfoOverrideGroup5 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.InfoOverrideGroup6 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new SlotOverride(CName, this.Manager);
            base.MemoryCast(this, result);
            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="SlotOverride"/> 
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
            using (var ms = new MemoryStream(0x60))
            using (var writer = new BinaryWriter(ms))
            {

                writer.WriteNullTermUTF8(this._collection_name);
                writer.WriteNullTermUTF8(this.InfoMainOverride);
                writer.WriteNullTermUTF8(this.InfoOverrideGroup2);
                writer.WriteNullTermUTF8(this.InfoOverrideGroup3);
                writer.WriteNullTermUTF8(this.InfoOverrideGroup4);
                writer.WriteNullTermUTF8(this.InfoOverrideGroup5);
                writer.WriteNullTermUTF8(this.InfoOverrideGroup6);

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

            this._collection_name = reader.ReadNullTermUTF8();
            this.InfoMainOverride = reader.ReadNullTermUTF8();
            this.InfoOverrideGroup2 = reader.ReadNullTermUTF8();
            this.InfoOverrideGroup3 = reader.ReadNullTermUTF8();
            this.InfoOverrideGroup4 = reader.ReadNullTermUTF8();
            this.InfoOverrideGroup5 = reader.ReadNullTermUTF8();
            this.InfoOverrideGroup6 = reader.ReadNullTermUTF8();
        }

        #endregion
    }
}
