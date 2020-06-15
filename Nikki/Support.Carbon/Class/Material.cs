﻿using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Carbon.Framework;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Carbon.Class
{
    /// <summary>
    /// <see cref="Material"/> is a collection of float attributes of shaders and materials.
    /// </summary>
    public class Material : Shared.Class.Material
    {
        #region Fields

        private string _collection_name;

        /// <summary>
        /// Maximum length of the CollectionName.
        /// </summary>
        public const int MaxCNameLength = 0x3F;

        /// <summary>
        /// Offset of the CollectionName in the data.
        /// </summary>
        public const int CNameOffsetAt = 0x1C;

        /// <summary>
        /// Base size of a unit collection.
        /// </summary>
        public const int BaseClassSize = 0xF4;

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
        public MaterialManager Manager { get; set; }

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
        /// Disable value of reflection colors of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float SpecularPower { get; set; }

        /// <summary>
        /// Increment value of reflection colors of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float EnvmapPower { get; set; }

        /// <summary>
        /// Blend value of strong colors of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float EnvmapClamp { get; set; }

        /// <summary>
        /// Disable value of strong colors of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float EnvmapVinylScale { get; set; }

        /// <summary>
        /// Level value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinLevel { get; set; }

        /// <summary>
        /// Red value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinRed { get; set; }

        /// <summary>
        /// Green value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinGreen { get; set; }

        /// <summary>
        /// Blue value of the first bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Diffuse")]
        public float DiffuseMinBlue { get; set; }

        /// <summary>
        /// Level value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [Category("Diffuse")]
        public float DiffuseMaxLevel { get; set; }

        /// <summary>
        /// Red value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [Category("Diffuse")]
        public float DiffuseMaxRed { get; set; }

        /// <summary>
        /// Green value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [Category("Diffuse")]
        public float DiffuseMaxGreen { get; set; }

        /// <summary>
        /// Blue value of the second bright color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [Category("Diffuse")]
        public float DiffuseMaxBlue { get; set; }

        /// <summary>
        /// Main grayscale value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffuseVinylScale { get; set; }

        /// <summary>
        /// Linear negativity of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float VinylLuminanceMinLevel { get; set; }

        /// <summary>
        /// Gradient negativity of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float VinylLuminanceMaxLevel { get; set; }

        /// <summary>
        /// Level value of the reflection color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMaxLevel { get; set; }

        /// <summary>
        /// Red value of the reflection color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMaxRed { get; set; }

        /// <summary>
        /// Green value of the reflection color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMaxGreen { get; set; }

        /// <summary>
        /// Blue value of the reflection color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMaxBlue { get; set; }

        /// <summary>
        /// Outer radius of the shadow fading.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffusePower { get; set; }

        /// <summary>
        /// Value of the optimal light reflection on the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffuseClamp { get; set; }

        /// <summary>
        /// Level value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinLevel { get; set; }

        /// <summary>
        /// Red value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinRed { get; set; }

        /// <summary>
        /// Green value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinGreen { get; set; }

        /// <summary>
        /// Blue value of the first strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMinBlue { get; set; }

        /// <summary>
        /// Level value of the second strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxLevel { get; set; }

        /// <summary>
        /// Red value of the second strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxRed { get; set; }

        /// <summary>
        /// Green value of the second strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxGreen { get; set; }

        /// <summary>
        /// Blue value of the second strong color of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Envmap")]
        public float EnvmapMaxBlue { get; set; }

        /// <summary>
        /// First alpha value of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffuseMinAlpha { get; set; }

        /// <summary>
        /// Second alpha value of the material colors.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffuseMaxAlpha { get; set; }

        /// <summary>
        /// Unknown 1 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float DiffuseFlakes { get; set; }

        /// <summary>
        /// Unknown 2 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float SpecularFlakes { get; set; }

        /// <summary>
        /// Unknown 3 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Primary")]
        public float SpecularVinylScale { get; set; }

        /// <summary>
        /// Unknown 4 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMinLevel { get; set; }

        /// <summary>
        /// Unknown 5 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMinRed { get; set; }

        /// <summary>
        /// Unknown 6 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMinGreen { get; set; }

        /// <summary>
        /// Unknown 7 value of the material.
        /// </summary>
        [AccessModifiable()]
        [StaticModifiable()]
        [MemoryCastable()]
        [Category("Specular")]
        public float SpecularMinBlue { get; set; }

        #endregion

        #region Main

        /// <summary>
        /// Initializes new instance of <see cref="Material"/>.
        /// </summary>
        public Material() { }

        /// <summary>
        /// Initializes new instance of <see cref="Material"/>.
        /// </summary>
        /// <param name="CName">CollectionName of the new instance.</param>
        /// <param name="manager"><see cref="MaterialManager"/> to which this instance belongs to.</param>
        public Material(string CName, MaterialManager manager)
        {
            this.Manager = manager;
            this.CollectionName = CName;
            CName.BinHash();
        }

        /// <summary>
        /// Initializes new instance of <see cref="Material"/>.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
        /// <param name="manager"><see cref="MaterialManager"/> to which this instance belongs to.</param>
        public Material(BinaryReader br, MaterialManager manager)
        {
            this.Manager = manager;
            this.Disassemble(br);
        }

        /// <summary>
        /// Destroys current instance.
        /// </summary>
        ~Material() { }

        #endregion

        #region Methods

        /// <summary>
        /// Assembles <see cref="Material"/> into a byte array.
        /// </summary>
        /// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Material"/> with.</param>
        public override void Assemble(BinaryWriter bw)
        {
            // Write header of the material
            bw.WriteEnum(eBlockID.Materials);
            bw.Write((int)0xEC);
            bw.Write(_Unknown0);
            bw.Write(_Localizer);
            bw.Write(_Localizer);
            bw.Write(this.BinKey);
            bw.Write(_Localizer);

            // Write CollectionName
            bw.WriteNullTermUTF8(this._collection_name, 0x40);

            // Write all settings
            bw.Write(this.DiffusePower);
            bw.Write(this.DiffuseClamp);
            bw.Write(this.DiffuseFlakes);
            bw.Write(this.DiffuseVinylScale);
            bw.Write(this.DiffuseMinLevel);
            bw.Write(this.DiffuseMinRed);
            bw.Write(this.DiffuseMinGreen);
            bw.Write(this.DiffuseMinBlue);
            bw.Write(this.DiffuseMaxLevel);
            bw.Write(this.DiffuseMaxRed);
            bw.Write(this.DiffuseMaxGreen);
            bw.Write(this.DiffuseMaxBlue);
            bw.Write(this.DiffuseMinAlpha);
            bw.Write(this.DiffuseMaxAlpha);
            bw.Write(this.SpecularPower);
            bw.Write(this.SpecularFlakes);
            bw.Write(this.SpecularVinylScale);
            bw.Write(this.SpecularMinLevel);
            bw.Write(this.SpecularMinRed);
            bw.Write(this.SpecularMinGreen);
            bw.Write(this.SpecularMinBlue);
            bw.Write(this.SpecularMaxLevel);
            bw.Write(this.SpecularMaxRed);
            bw.Write(this.SpecularMaxGreen);
            bw.Write(this.SpecularMaxBlue);
            bw.Write(this.EnvmapPower);
            bw.Write(this.EnvmapClamp);
            bw.Write(this.EnvmapVinylScale);
            bw.Write(this.EnvmapMinLevel);
            bw.Write(this.EnvmapMinRed);
            bw.Write(this.EnvmapMinGreen);
            bw.Write(this.EnvmapMinBlue);
            bw.Write(this.EnvmapMaxLevel);
            bw.Write(this.EnvmapMaxRed);
            bw.Write(this.EnvmapMaxGreen);
            bw.Write(this.EnvmapMaxBlue);
            bw.Write(this.VinylLuminanceMinLevel);
            bw.Write(this.VinylLuminanceMaxLevel);
        }

        /// <summary>
        /// Disassembles array into <see cref="Material"/> properties.
        /// </summary>
        /// <param name="br"><see cref="BinaryReader"/> to read <see cref="Material"/> with.</param>
        public override void Disassemble(BinaryReader br)
        {
            br.BaseStream.Position += 0x1C;
            this._collection_name = br.ReadNullTermUTF8(0x40);

            this.DiffusePower = br.ReadSingle();
            this.DiffuseClamp = br.ReadSingle();
            this.DiffuseFlakes = br.ReadSingle();
            this.DiffuseVinylScale = br.ReadSingle();
            this.DiffuseMinLevel = br.ReadSingle();
            this.DiffuseMinRed = br.ReadSingle();
            this.DiffuseMinGreen = br.ReadSingle();
            this.DiffuseMinBlue = br.ReadSingle();
            this.DiffuseMaxLevel = br.ReadSingle();
            this.DiffuseMaxRed = br.ReadSingle();
            this.DiffuseMaxGreen = br.ReadSingle();
            this.DiffuseMaxBlue = br.ReadSingle();
            this.DiffuseMinAlpha = br.ReadSingle();
            this.DiffuseMaxAlpha = br.ReadSingle();
            this.SpecularPower = br.ReadSingle();
            this.SpecularFlakes = br.ReadSingle();
            this.SpecularVinylScale = br.ReadSingle();
            this.SpecularMinLevel = br.ReadSingle();
            this.SpecularMinRed = br.ReadSingle();
            this.SpecularMinGreen = br.ReadSingle();
            this.SpecularMinBlue = br.ReadSingle();
            this.SpecularMaxLevel = br.ReadSingle();
            this.SpecularMaxRed = br.ReadSingle();
            this.SpecularMaxGreen = br.ReadSingle();
            this.SpecularMaxBlue = br.ReadSingle();
            this.EnvmapPower = br.ReadSingle();
            this.EnvmapClamp = br.ReadSingle();
            this.EnvmapVinylScale = br.ReadSingle();
            this.EnvmapMinLevel = br.ReadSingle();
            this.EnvmapMinRed = br.ReadSingle();
            this.EnvmapMinGreen = br.ReadSingle();
            this.EnvmapMinBlue = br.ReadSingle();
            this.EnvmapMaxLevel = br.ReadSingle();
            this.EnvmapMaxRed = br.ReadSingle();
            this.EnvmapMaxGreen = br.ReadSingle();
            this.EnvmapMaxBlue = br.ReadSingle();
            this.VinylLuminanceMinLevel = br.ReadSingle();
            this.VinylLuminanceMaxLevel = br.ReadSingle();
        }

        /// <summary>
        /// Casts all attributes from this object to another one.
        /// </summary>
        /// <param name="CName">CollectionName of the new created object.</param>
        /// <returns>Memory casted copy of the object.</returns>
        public override Collectable MemoryCast(string CName)
        {
            var result = new Material(CName, this.Manager);
            base.MemoryCast(this, result);
            return result;
        }

        /// <summary>
        /// Returns CollectionName, BinKey and GameSTR of this <see cref="Material"/> 
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
        /// <param name="filename">File to write data to.</param>
        public override void Serialize(string filename)
        {

        }

        /// <summary>
        /// Deserializes byte array into an instance by loading data from the file provided.
        /// </summary>
        /// <param name="filename">File to read data from.</param>
        public override void Deserialize(string filename)
        {

        }

        #endregion
    }
}