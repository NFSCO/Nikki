﻿using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground1.Framework;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground1.Class
{
	/// <summary>
	/// <see cref="Track"/> is a collection of settings related to races and events.
	/// </summary>
	public class Track : Shared.Class.Track
	{
		#region Fields

		private string _collection_name;

		/// <summary>
		/// Maximum length of the CollectionName.
		/// </summary>
		public const int MaxCNameLength = 0x02;

		/// <summary>
		/// Offset of the CollectionName in the data.
		/// </summary>
		public const int CNameOffsetAt = 0x6C;

		/// <summary>
		/// Base size of a unit collection.
		/// </summary>
		public const int BaseClassSize = 0xFC;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override GameINT GameINT => GameINT.Underground2;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public override string GameSTR => GameINT.Underground2.ToString();

		/// <summary>
		/// Manager to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public TrackManager Manager { get; set; }

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
				this.Manager.CreationCheck(value);
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
		/// Second race description name.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string RaceDescription2 { get; set; } = String.Empty;

		/// <summary>
		/// Total length of the whole track
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public uint RaceLength { get; set; }

		/// <summary>
		/// Indicates maximum time allowed to complete the race in forward direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TimeLimitToBeatForward { get; set; }

		/// <summary>
		/// Indicates maximum time allowed to complete the race in reverse direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TimeLimitToBeatReverse { get; set; }

		/// <summary>
		/// Indicates score needed to beat the drift race in forward direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int ScoreToBeatDriftForward { get; set; }

		/// <summary>
		/// Indicates score needed to beat the drift race in reverse direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int ScoreToBeatDriftReverse { get; set; }

		/// <summary>
		/// Indicates number of seconds that should pass before opponents can take first shortcut.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short NumSecBeforeShorcutsAllowed { get; set; }

		/// <summary>
		/// Indicates minimum amount of seconds to drift.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short DriftSecondsMin { get; set; }

		/// <summary>
		/// Indicates maximum amount of seconds to drift.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short DriftSecondsMax { get; set; }

		/// <summary>
		/// Indicates configuration settings of the car at the start.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public short CarRaceStartConfig { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapCalibrationOffsetX { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapCalibrationOffsetY { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapCalibrationWidth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapCalibrationRotation { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapStartgridAngle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrackMapFinishlineAngle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_0_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_0_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_1_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_1_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_2_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_2_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_3_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte MaxTrafficCars_3_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte TrafAllowedNearStartgrid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte TrafAllowedNearFinishline { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafMinInitDistFromStart { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafMinInitDistFromFinish { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafMinInitDistInbetweenA { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public float TrafMinInitDistInbetweenB { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="Track"/>.
		/// </summary>
		public Track() { }

		/// <summary>
		/// Initializes new instance of <see cref="Track"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="manager"><see cref="TrackManager"/> to which this instance belongs to.</param>
		public Track(string CName, TrackManager manager)
		{
			this.Manager = manager;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="Track"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="manager"><see cref="TrackManager"/> to which this instance belongs to.</param>
		public Track(BinaryReader br, TrackManager manager)
		{
			this.Manager = manager;
			this.Disassemble(br);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~Track() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="Track"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Track"/> with.</param>
		public override void Assemble(BinaryWriter bw)
		{
			// Write all directories and locations
			bw.WriteNullTermUTF8(this.RaceDescription, 0x20);
			bw.WriteNullTermUTF8(this.TrackDirectory, 0x20);
			bw.WriteNullTermUTF8(this.RegionName, 0x8);
			bw.WriteNullTermUTF8(this.RegionDirectory, 0x20);

			// Write race settings
			bw.WriteEnum(this.IsValid);
			bw.Write(this.IsLoopingRace == eBoolean.True ? (byte)0 : (byte)1);
			bw.Write(this.ReverseVersionExists == eBoolean.True ? (byte)2 : (byte)0);
			bw.Write((byte)0);
			bw.Write(UInt16.Parse(this._collection_name));
			bw.Write(UInt16.Parse(this._collection_name));

			// Write gameplay scores
			bw.Write(this.SunInfoName.BinHash());
			bw.WriteEnum(this.RaceGameplayMode);
			bw.Write(this.RaceLength);
			bw.Write(this.TimeLimitToBeatForward);
			bw.Write(this.TimeLimitToBeatReverse);
			bw.Write(this.ScoreToBeatDriftForward);
			bw.Write(this.ScoreToBeatDriftReverse);

			// Write map calibrations
			bw.Write(this.TrackMapCalibrationOffsetX);
			bw.Write(this.TrackMapCalibrationOffsetY);
			bw.Write(this.TrackMapCalibrationWidth);
			bw.Write((ushort)(this.TrackMapCalibrationRotation / 180 * 32768));
			bw.Write((ushort)(this.TrackMapStartgridAngle / 180 * 32768));
			bw.Write((ushort)(this.TrackMapFinishlineAngle / 180 * 32768));
			bw.Write((short)0);

			// Write difficulties and padding
			bw.Write((int)this.DifficultyForward);
			bw.Write((int)this.DifficultyReverse);
			bw.Write(-1);
			bw.Write(-1);
			bw.Write(-1);
			bw.Write(-1);
			bw.Write(this.NumSecBeforeShorcutsAllowed);
			bw.Write(this.DriftSecondsMin);
			bw.Write(this.DriftSecondsMax);
			bw.Write((short)0);

			// Write traffic settings
			bw.Write(this.MaxTrafficCars_0_0);
			bw.Write(this.MaxTrafficCars_0_1);
			bw.Write(this.MaxTrafficCars_1_0);
			bw.Write(this.MaxTrafficCars_1_1);
			bw.Write(this.MaxTrafficCars_2_0);
			bw.Write(this.MaxTrafficCars_2_1);
			bw.Write(this.MaxTrafficCars_3_0);
			bw.Write(this.MaxTrafficCars_3_1);
			bw.Write(this.TrafAllowedNearStartgrid);
			bw.Write(this.TrafAllowedNearFinishline);
			bw.Write(this.CarRaceStartConfig);
			bw.Write(this.TrafMinInitDistFromStart);
			bw.Write(this.TrafMinInitDistFromFinish);
			bw.Write(this.TrafMinInitDistInbetweenA);
			bw.Write(this.TrafMinInitDistInbetweenB);

			// Write second description name
			bw.WriteNullTermUTF8(this.RaceDescription2, 0x20);
		}

		/// <summary>
		/// Disassembles array into <see cref="Track"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Track"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{
			// Read all directories and locations
			this.RaceDescription = br.ReadNullTermUTF8(0x20);
			this.TrackDirectory = br.ReadNullTermUTF8(0x20);
			this.RegionName = br.ReadNullTermUTF8(0x8);
			this.RegionDirectory = br.ReadNullTermUTF8(0x20);

			// Read race settings
			this.IsValid = br.ReadEnum<eBoolean>();
			this.IsLoopingRace = br.ReadByte() == 0 ? eBoolean.True : eBoolean.False;
			this.ReverseVersionExists = br.ReadByte() == 2 ? eBoolean.True : eBoolean.False;
			br.BaseStream.Position += 1;
			this._collection_name = br.ReadUInt16().ToString();
			br.BaseStream.Position += 2;

			// Read gameplay scores
			this.SunInfoName = br.ReadUInt32().BinString(LookupReturn.EMPTY);
			this.RaceGameplayMode = br.ReadEnum<TrackGameplayMode>();
			this.RaceLength = br.ReadUInt32();
			this.TimeLimitToBeatForward = br.ReadSingle();
			this.TimeLimitToBeatReverse = br.ReadSingle();
			this.ScoreToBeatDriftForward = br.ReadInt32();
			this.ScoreToBeatDriftReverse = br.ReadInt32();

			// Read map calibrations
			this.TrackMapCalibrationOffsetX = br.ReadSingle();
			this.TrackMapCalibrationOffsetY = br.ReadSingle();
			this.TrackMapCalibrationWidth = br.ReadSingle();
			this.TrackMapCalibrationRotation = ((float)br.ReadUInt16()) * 180 / 32768;
			this.TrackMapStartgridAngle = ((float)br.ReadUInt16()) * 180 / 32768;
			this.TrackMapFinishlineAngle = ((float)br.ReadUInt16()) * 180 / 32768;
			br.BaseStream.Position += 2;

			// Read difficulties and padding
			this.DifficultyForward = (TrackDifficulty)(br.ReadInt32());
			this.DifficultyReverse = (TrackDifficulty)(br.ReadInt32());
			br.BaseStream.Position += 0x10;
			this.NumSecBeforeShorcutsAllowed = br.ReadInt16();
			this.DriftSecondsMin = br.ReadInt16();
			this.DriftSecondsMax = br.ReadInt16();
			br.BaseStream.Position += 2;

			// Read traffic settings
			this.MaxTrafficCars_0_0 = br.ReadByte();
			this.MaxTrafficCars_0_1 = br.ReadByte();
			this.MaxTrafficCars_1_0 = br.ReadByte();
			this.MaxTrafficCars_1_1 = br.ReadByte();
			this.MaxTrafficCars_2_0 = br.ReadByte();
			this.MaxTrafficCars_2_1 = br.ReadByte();
			this.MaxTrafficCars_3_0 = br.ReadByte();
			this.MaxTrafficCars_3_1 = br.ReadByte();
			this.TrafAllowedNearStartgrid = br.ReadByte();
			this.TrafAllowedNearFinishline = br.ReadByte();
			this.CarRaceStartConfig = br.ReadInt16();
			this.TrafMinInitDistFromStart = br.ReadSingle();
			this.TrafMinInitDistFromFinish = br.ReadSingle();
			this.TrafMinInitDistInbetweenA = br.ReadSingle();
			this.TrafMinInitDistInbetweenB = br.ReadSingle();

			// Read second race description
			this.RaceDescription2 = br.ReadNullTermUTF8(0x20);
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new Track(CName, this.Manager);
			base.MemoryCast(this, result);
			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="Track"/> 
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
			using (var ms = new MemoryStream(0x122))
			using (var writer = new BinaryWriter(ms))
			{

				// Write all directories and locations
				writer.Write(UInt16.Parse(this._collection_name));
				writer.WriteNullTermUTF8(this.RaceDescription);
				writer.WriteNullTermUTF8(this.RaceDescription2);
				writer.WriteNullTermUTF8(this.TrackDirectory);
				writer.WriteNullTermUTF8(this.RegionName);
				writer.WriteNullTermUTF8(this.RegionDirectory);

				// Write race settings
				writer.WriteEnum(this.IsValid);
				writer.WriteEnum(this.IsLoopingRace);
				writer.WriteEnum(this.ReverseVersionExists);

				// Write gameplay scores
				writer.WriteNullTermUTF8(this.SunInfoName);
				writer.WriteEnum(this.RaceGameplayMode);
				writer.Write(this.RaceLength);
				writer.Write(this.TimeLimitToBeatForward);
				writer.Write(this.TimeLimitToBeatReverse);
				writer.Write(this.ScoreToBeatDriftForward);
				writer.Write(this.ScoreToBeatDriftReverse);

				// Write map calibrations
				writer.Write(this.TrackMapCalibrationOffsetX);
				writer.Write(this.TrackMapCalibrationOffsetY);
				writer.Write(this.TrackMapCalibrationWidth);
				writer.Write(this.TrackMapCalibrationRotation);
				writer.Write(this.TrackMapStartgridAngle);
				writer.Write(this.TrackMapFinishlineAngle);

				// Write difficulties and padding
				writer.WriteEnum(this.DifficultyForward);
				writer.WriteEnum(this.DifficultyReverse);
				writer.Write(this.NumSecBeforeShorcutsAllowed);
				writer.Write(this.DriftSecondsMin);
				writer.Write(this.DriftSecondsMax);

				// Write traffic settings
				writer.Write(this.MaxTrafficCars_0_0);
				writer.Write(this.MaxTrafficCars_0_1);
				writer.Write(this.MaxTrafficCars_1_0);
				writer.Write(this.MaxTrafficCars_1_1);
				writer.Write(this.MaxTrafficCars_2_0);
				writer.Write(this.MaxTrafficCars_2_1);
				writer.Write(this.MaxTrafficCars_3_0);
				writer.Write(this.MaxTrafficCars_3_1);
				writer.Write(this.TrafAllowedNearStartgrid);
				writer.Write(this.TrafAllowedNearFinishline);
				writer.Write(this.CarRaceStartConfig);
				writer.Write(this.TrafMinInitDistFromStart);
				writer.Write(this.TrafMinInitDistFromFinish);
				writer.Write(this.TrafMinInitDistInbetweenA);
				writer.Write(this.TrafMinInitDistInbetweenB);

				array = ms.ToArray();

			}

			array = Interop.Compress(array, LZCompressionType.BEST);

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

			// Write all directories and locations
			this._collection_name = reader.ReadUInt16().ToString();
			this.RaceDescription = reader.ReadNullTermUTF8();
			this.RaceDescription2 = reader.ReadNullTermUTF8();
			this.TrackDirectory = reader.ReadNullTermUTF8();
			this.RegionName = reader.ReadNullTermUTF8();
			this.RegionDirectory = reader.ReadNullTermUTF8();

			// Write race settings
			this.IsValid = reader.ReadEnum<eBoolean>();
			this.IsLoopingRace = reader.ReadEnum<eBoolean>();
			this.ReverseVersionExists = reader.ReadEnum<eBoolean>();

			// Write gameplay scores
			this.SunInfoName = reader.ReadNullTermUTF8();
			this.RaceGameplayMode = reader.ReadEnum<TrackGameplayMode>();
			this.RaceLength = reader.ReadUInt32();
			this.TimeLimitToBeatForward = reader.ReadSingle();
			this.TimeLimitToBeatReverse = reader.ReadSingle();
			this.ScoreToBeatDriftForward = reader.ReadInt32();
			this.ScoreToBeatDriftReverse = reader.ReadInt32();

			// Write map calibrations
			this.TrackMapCalibrationOffsetX = reader.ReadSingle();
			this.TrackMapCalibrationOffsetY = reader.ReadSingle();
			this.TrackMapCalibrationWidth = reader.ReadSingle();
			this.TrackMapCalibrationRotation = reader.ReadSingle();
			this.TrackMapStartgridAngle = reader.ReadSingle();
			this.TrackMapFinishlineAngle = reader.ReadSingle();

			// Write difficulties and padding
			this.DifficultyForward = reader.ReadEnum<TrackDifficulty>();
			this.DifficultyReverse = reader.ReadEnum<TrackDifficulty>();
			this.NumSecBeforeShorcutsAllowed = reader.ReadInt16();
			this.DriftSecondsMin = reader.ReadInt16();
			this.DriftSecondsMax = reader.ReadInt16();

			// Write traffic settings
			this.MaxTrafficCars_0_0 = reader.ReadByte();
			this.MaxTrafficCars_0_1 = reader.ReadByte();
			this.MaxTrafficCars_1_0 = reader.ReadByte();
			this.MaxTrafficCars_1_1 = reader.ReadByte();
			this.MaxTrafficCars_2_0 = reader.ReadByte();
			this.MaxTrafficCars_2_1 = reader.ReadByte();
			this.MaxTrafficCars_3_0 = reader.ReadByte();
			this.MaxTrafficCars_3_1 = reader.ReadByte();
			this.TrafAllowedNearStartgrid = reader.ReadByte();
			this.TrafAllowedNearFinishline = reader.ReadByte();
			this.CarRaceStartConfig = reader.ReadInt16();
			this.TrafMinInitDistFromStart = reader.ReadSingle();
			this.TrafMinInitDistFromFinish = reader.ReadSingle();
			this.TrafMinInitDistInbetweenA = reader.ReadSingle();
			this.TrafMinInitDistInbetweenB = reader.ReadSingle();
		}

		#endregion
	}
}