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
    /// <see cref="SlotType"/> is a collection of settings related to a parts and slot information.
    /// </summary>
    public class SlotType : Shared.Class.SlotType
    {
        #region Fields

        private string _collection_name;
        private static int _index;

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
        public const int BaseClassSize = 0x1C;

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
        public SlotTypeManager Manager { get; set; }

        /// <summary>
        /// Collection name of the variable.
        /// </summary>
        [ReadOnly(true)]
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
        /// Current index of this <see cref="SlotType"/>.
        /// </summary>
        private static int Index
        {
            get => _index;
            set => _index = value > Enum.GetNames(typeof(eSlotCarbon)).Length ? 0 : value;
        }

        /// <summary>
        /// Group 2 slot override entry of this <see cref="SlotType"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SlotOverrideGroup2 { get; set; }

        /// <summary>
        /// Group 3 slot override entry of this <see cref="SlotType"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SlotOverrideGroup3 { get; set; }

        /// <summary>
        /// Group 4 slot override entry of this <see cref="SlotType"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SlotOverrideGroup4 { get; set; }

        /// <summary>
        /// Group 5 slot override entry of this <see cref="SlotType"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SlotOverrideGroup5 { get; set; }

        /// <summary>
        /// Group 6 slot override entry of this <see cref="SlotType"/>.
        /// </summary>
        [AccessModifiable()]
        [MemoryCastable()]
        [Category("Secondary")]
        public string SlotOverrideGroup6 { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="SlotType"/>.
        /// </summary>
        public SlotType() { }

        /// <summary>
        /// Initializes new instance of <see cref="SlotType"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="SlotTypeManager"/> to which this instance belongs to.</param>
        public SlotType(string CName, SlotTypeManager manager)
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="SlotType"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="SlotTypeManager"/> to which this instance belongs to.</param>
        public SlotType(BinaryReader br, SlotTypeManager manager)
        {
            this.Manager = manager;
            this.Disassemble(br);
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~SlotType() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="SlotType"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="SlotType"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            bw.Write(String.IsNullOrEmpty(this.SlotStockOverride)
                ? 0xFFFFFFFF
                : this.SlotStockOverride.BinHash());
            bw.Write(this.SlotMainOverride.BinHash());
            bw.Write(this.SlotOverrideGroup2.BinHash());
            bw.Write(this.SlotOverrideGroup3.BinHash());
            bw.Write(this.SlotOverrideGroup4.BinHash());
            bw.Write(this.SlotOverrideGroup5.BinHash());
            bw.Write(this.SlotOverrideGroup6.BinHash());
        }

        /// <summary>
        /// Disassembles array into <see cref="SlotType"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="SlotType"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            this._collection_name = ((eSlotCarbon)(Index++)).ToString();

            uint key = 0;
            const uint empty = 0xFFFFFFFF;

            key = br.ReadUInt32();
            this.SlotStockOverride = key == empty ? String.Empty : key.BinString(eLookupReturn.EMPTY);

            this.SlotMainOverride = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SlotOverrideGroup2 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SlotOverrideGroup3 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SlotOverrideGroup4 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SlotOverrideGroup5 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
            this.SlotOverrideGroup6 = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new SlotType(CName, this.Manager);
            base.MemoryCast(this, result);
            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="SlotType"/> 
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
                writer.WriteNullTermUTF8(this.SlotStockOverride);
                writer.WriteNullTermUTF8(this.SlotMainOverride);
                writer.WriteNullTermUTF8(this.SlotOverrideGroup2);
                writer.WriteNullTermUTF8(this.SlotOverrideGroup3);
                writer.WriteNullTermUTF8(this.SlotOverrideGroup4);
                writer.WriteNullTermUTF8(this.SlotOverrideGroup5);
                writer.WriteNullTermUTF8(this.SlotOverrideGroup6);
                writer.WriteEnum(this.PrimaryAnimation);

                array = ms.ToArray();

            }

            array = Interop.Compress(array, eLZCompressionType.RAWW);

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
            this.SlotStockOverride = reader.ReadNullTermUTF8();
            this.SlotMainOverride = reader.ReadNullTermUTF8();
            this.SlotOverrideGroup2 = reader.ReadNullTermUTF8();
            this.SlotOverrideGroup3 = reader.ReadNullTermUTF8();
            this.SlotOverrideGroup4 = reader.ReadNullTermUTF8();
            this.SlotOverrideGroup5 = reader.ReadNullTermUTF8();
            this.SlotOverrideGroup6 = reader.ReadNullTermUTF8();
            this.PrimaryAnimation = reader.ReadEnum<eCarAnimLocation>();
        }

        #endregion
    }
}
