﻿using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Prostreet.Class
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
		public const int MaxCNameLength = 0x3F;

		/// <summary>
		/// Offset of the CollectionName in the data.
		/// </summary>
		public const int CNameOffsetAt = 0x40;

		/// <summary>
		/// Base size of a unit collection.
		/// </summary>
		public const int BaseClassSize = 0x198;

		#endregion

		#region Properties

		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Prostreet;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Prostreet.ToString();

		/// <summary>
		/// Database to which the class belongs to.
		/// </summary>
		public Database.Prostreet Database { get; set; }

		/// <summary>
		/// Collection name of the variable.
		/// </summary>
		[AccessModifiable()]
		public override string CollectionName
		{
			get => this._collection_name;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("This value cannot be left empty.");
				if (value.Contains(" "))
					throw new Exception("CollectionName cannot contain whitespace.");
				if (!UInt16.TryParse(value, out ushort id))
					throw new Exception("Unable to parse TrackID from the collection name provided.");
				if (this.Database.Tracks.FindCollection(value) != null)
					throw new CollectionExistenceException(value);
				this._collection_name = value;
				this.TrackID = id;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		public override uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		public override uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// Unique ID value of the track that is used ingame.
		/// </summary>
		public ushort TrackID { get; private set; }

		/// <summary>
		/// Location index of the track.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public int LocationIndex { get; set; }

		/// <summary>
		/// Location directory of the track.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public string LocationDirectory { get; set; } = String.Empty;

		/// <summary>
		/// Indicates whether the race is used in performance tuning events.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public eBoolean IsPerformanceTuning { get; set; }

		/// <summary>
		/// Location type of the track.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public eLocationType LocationType { get; set; }

		/// <summary>
		/// Drift type of the track.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public eDriftType DriftType { get; set; }

		/// <summary>
		/// Total length of the whole track
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		public uint RaceLength { get; set; }

		/// <summary>
		/// Indicates maximum time allowed to complete the race in forward direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TimeLimitToBeatForward { get; set; }

		/// <summary>
		/// Indicates maximum time allowed to complete the race in reverse direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TimeLimitToBeatReverse { get; set; }

		/// <summary>
		/// Indicates score needed to beat the drift race in forward direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public int ScoreToBeatDriftForward { get; set; }

		/// <summary>
		/// Indicates score needed to beat the drift race in reverse direction.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public int ScoreToBeatDriftReverse { get; set; }

		/// <summary>
		/// Indicates number of seconds that should pass before opponents can take first shortcut.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short NumSecBeforeShorcutsAllowed { get; set; }

		/// <summary>
		/// Indicates minimum amount of seconds to drift.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short DriftSecondsMin { get; set; }

		/// <summary>
		/// Indicates maximum amount of seconds to drift.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short DriftSecondsMax { get; set; }

		/// <summary>
		/// Indicates configuration settings of the car at the start.
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public short CarRaceStartConfig { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrackMapCalibrationOffsetX { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrackMapCalibrationOffsetY { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrackMapCalibrationWidth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrackMapCalibrationRotation { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrackMapCalibrationZoomIn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrackMapStartgridAngle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrackMapFinishlineAngle { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float MenuMapZoomOffsetX { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float MenuMapZoomOffsetY { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float MenuMapZoomWidth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public int MenuMapStartZoomed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxTrafficCars_0_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxTrafficCars_0_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxTrafficCars_1_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxTrafficCars_1_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxTrafficCars_2_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxTrafficCars_2_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxTrafficCars_3_0 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte MaxTrafficCars_3_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte TrafAllowedNearStartgrid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public byte TrafAllowedNearFinishline { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrafMinInitDistFromStart { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrafMinInitDistFromFinish { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrafMinInitDistInbetweenA { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrafMinInitDistInbetweenB { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrafOncomingFraction1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrafOncomingFraction2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrafOncomingFraction3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[StaticModifiable()]
		[MemoryCastable()]
		public float TrafOncomingFraction4 { get; set; }

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
		/// <param name="db"><see cref="Database.Prostreet"/> to which this instance belongs to.</param>
		public Track(string CName, Database.Prostreet db)
		{
			this.Database = db;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="Track"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="db"><see cref="Database.Prostreet"/> to which this instance belongs to.</param>
		public Track(BinaryReader br, Database.Prostreet db)
		{
			this.Database = db;
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
			bw.WriteNullTermUTF8(this.RaceDescription, 0x40);
			bw.WriteNullTermUTF8(this.TrackDirectory, 0x40);
			bw.WriteNullTermUTF8(this.RegionName, 0x20);
			bw.WriteNullTermUTF8(this.RegionDirectory, 0x40);
			bw.Write(this.LocationIndex);
			bw.WriteNullTermUTF8(this.LocationDirectory, 0x20);

			// Write race settings
			bw.WriteEnum(this.LocationType);
			bw.WriteEnum(this.DriftType);
			bw.WriteEnum(this.IsValid);
			bw.Write(this.IsLoopingRace == eBoolean.True ? (byte)0 : (byte)1);
			bw.WriteEnum(this.ReverseVersionExists);
			bw.Write((byte)0);
			bw.WriteEnum(this.IsPerformanceTuning);
			bw.Write((byte)0);
			bw.Write(this.TrackID);
			bw.Write(this.TrackID);
			bw.Write((short)0);

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
			bw.Write(this.TrackMapCalibrationZoomIn);

			// Write difficulties
			bw.Write((int)this.DifficultyForward);
			bw.Write((int)this.DifficultyReverse);
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
			bw.Write(this.TrafOncomingFraction1);
			bw.Write(this.TrafOncomingFraction2);
			bw.Write(this.TrafOncomingFraction3);
			bw.Write(this.TrafOncomingFraction4);

			// Write menu map settings
			bw.Write(this.MenuMapZoomOffsetX);
			bw.Write(this.MenuMapZoomOffsetY);
			bw.Write(this.MenuMapZoomWidth);
			bw.Write(this.MenuMapStartZoomed);
		}

		/// <summary>
		/// Disassembles array into <see cref="Track"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Track"/> with.</param>
		public override void Disassemble(BinaryReader br)
		{
			// Read all directories and locations
			this.RaceDescription = br.ReadNullTermUTF8(0x40);
			this.TrackDirectory = br.ReadNullTermUTF8(0x40);
			this.RegionName = br.ReadNullTermUTF8(0x20);
			this.RegionDirectory = br.ReadNullTermUTF8(0x40);
			this.LocationIndex = br.ReadInt32();
			this.LocationDirectory = br.ReadNullTermUTF8(0x20);

			// Read race settings
			this.LocationType = br.ReadEnum<eLocationType>();
			this.DriftType = br.ReadEnum<eDriftType>();
			this.IsValid = br.ReadEnum<eBoolean>();
			this.IsLoopingRace = br.ReadByte() == 0 ? eBoolean.True : eBoolean.False;
			this.ReverseVersionExists = br.ReadEnum<eBoolean>();
			br.BaseStream.Position += 1;
			this.IsPerformanceTuning = br.ReadEnum<eBoolean>();
			br.BaseStream.Position += 1;
			this._collection_name = br.ReadUInt16().ToString();
			this.TrackID = br.ReadUInt16();
			br.BaseStream.Position += 2;

			// Read gameplay scores
			this.SunInfoName = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.RaceGameplayMode = br.ReadEnum<eRaceGameplayMode>();
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
			this.TrackMapCalibrationZoomIn = br.ReadSingle();

			// Read difficulties and padding
			this.DifficultyForward = (eTrackDifficulty)(br.ReadInt32());
			this.DifficultyReverse = (eTrackDifficulty)(br.ReadInt32());
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
			this.TrafOncomingFraction1 = br.ReadSingle();
			this.TrafOncomingFraction2 = br.ReadSingle();
			this.TrafOncomingFraction3 = br.ReadSingle();
			this.TrafOncomingFraction4 = br.ReadSingle();

			// Read menu map settings
			this.MenuMapZoomOffsetX = br.ReadSingle();
			this.MenuMapZoomOffsetY = br.ReadSingle();
			this.MenuMapZoomWidth = br.ReadSingle();
			this.MenuMapStartZoomed = br.ReadInt32();
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override ACollectable MemoryCast(string CName)
		{
			var result = new Track(CName, this.Database);
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
				   $"BinKey: {this.BinKey.ToString("X8")} | Game: {this.GameSTR}";
		}

		#endregion
	}
}