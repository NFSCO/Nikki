﻿using System;
using System.IO;
using System.ComponentModel;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Reflection.Attributes;
using Nikki.Support.Underground2.Class;
using CoreExtensions.IO;
using CoreExtensions.Conversions;



namespace Nikki.Support.Underground2.Gameplay
{
	/// <summary>
	/// <see cref="Sponsor"/> is a collection of settings related to world challenge events.
	/// </summary>
	public class WorldChallenge : Collectable
	{
		#region Fields

		private string _collection_name;

		[MemoryCastable()]
		private byte _padding0;

		#endregion

		#region Enums

		/// <summary>
		/// Enum of <see cref="WorldChallenge"/> types.
		/// </summary>
		public enum WorldChallengeType : byte
		{
			/// <summary>
			/// Invalid challenge type.
			/// </summary>
			Invalid = 0,

			/// <summary>
			/// Unlocks visual upgrade.
			/// </summary>
			Visual = 1,

			/// <summary>
			/// Unlocks performance upgrade.
			/// </summary>
			Performance = 2,

			/// <summary>
			/// Is a showcase event.
			/// </summary>
			Showcase = 4,
		}

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
		/// GCareer to which the class belongs to.
		/// </summary>
		[Browsable(false)]
		public GCareer Career { get; set; }

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
				if (String.IsNullOrWhiteSpace(value))
				{

					throw new ArgumentNullException("This value cannot be left left empty.");

				}
				if (value.Contains(' '))
				{

					throw new Exception("CollectionName cannot contain whitespace.");

				}
				if (this.Career.GetCollection(value, nameof(this.Career.WorldChallenges)) != null)
				{

					throw new CollectionExistenceException(value);

				}

				this._collection_name = value;
			}
		}

		/// <summary>
		/// Binary memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public uint BinKey => this._collection_name.BinHash();

		/// <summary>
		/// Vault memory hash of the collection name.
		/// </summary>
		[Category("Main")]
		[TypeConverter(typeof(HexConverter))]
		public uint VltKey => this._collection_name.VltHash();

		/// <summary>
		/// Event trigger of this <see cref="WorldChallenge"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public string WorldChallengeTrigger { get; set; } = String.Empty;

		/// <summary>
		/// Stage to which this challenge belongs to.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public byte BelongsToStage { get; set; }

		/// <summary>
		/// True if challenge requires specific number of outruns won in order to unlock; 
		/// false otherwise.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public eBoolean UseOutrunsAsReqRaces { get; set; }

		/// <summary>
		/// Required races won to unlock this <see cref="WorldChallenge"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte RequiredRacesWon { get; set; }

		/// <summary>
		/// Label of the SMS sent when challenge is unlocked.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string UnlockableSMS { get; set; } = String.Empty;

		/// <summary>
		/// Parent, or destination in this <see cref="WorldChallenge"/>.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public string Destination { get; set; } = String.Empty;
		
		/// <summary>
		/// Time limit to complete this challenge.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public int TimeLimit { get; set; }

		/// <summary>
		/// Type of the challenge.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Primary")]
		public WorldChallengeType ChallengeType { get; set; }

		/// <summary>
		/// Index of the first unique part that gets unlocked upon completion.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte PartUnlockable1 { get; set; }

		/// <summary>
		/// Index of the second unique part that gets unlocked upon completion.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte PartUnlockable2 { get; set; }

		/// <summary>
		/// Index of the third unique part that gets unlocked upon completion.
		/// </summary>
		[AccessModifiable()]
		[MemoryCastable()]
		[Category("Secondary")]
		public byte PartUnlockable3 { get; set; }

		#endregion

		#region Main

		/// <summary>
		/// Initializes new instance of <see cref="WorldChallenge"/>.
		/// </summary>
		public WorldChallenge() { }

		/// <summary>
		/// Initializes new instance of <see cref="WorldChallenge"/>.
		/// </summary>
		/// <param name="CName">CollectionName of the new instance.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		public WorldChallenge(string CName, GCareer career)
		{
			this.Career = career;
			this.CollectionName = CName;
			CName.BinHash();
		}

		/// <summary>
		/// Initializes new instance of <see cref="WorldChallenge"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="career"><see cref="GCareer"/> to which this instance belongs to.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public WorldChallenge(BinaryReader br, BinaryReader strr, GCareer career)
		{
			this.Career = career;
			this.Disassemble(br, strr);
		}

		/// <summary>
		/// Destroys current instance.
		/// </summary>
		~WorldChallenge() { }

		#endregion

		#region Methods

		/// <summary>
		/// Assembles <see cref="Sponsor"/> into a byte array.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write <see cref="Sponsor"/> with.</param>
		/// <param name="strw"><see cref="BinaryWriter"/> to write strings with.</param>
		public void Assemble(BinaryWriter bw, BinaryWriter strw)
		{
			// CollectionName
			bw.Write((ushort)strw.BaseStream.Position);
			strw.WriteNullTermUTF8(this._collection_name);

			// World Trigger
			bw.Write((ushort)strw.BaseStream.Position);
			strw.WriteNullTermUTF8(this.WorldChallengeTrigger);

			// All settings
			bw.Write(this.BelongsToStage);
			bw.Write(this._padding0);
			bw.Write((byte)((byte)this.UseOutrunsAsReqRaces * 2));
			bw.Write(this.RequiredRacesWon);
			bw.Write(this.UnlockableSMS.BinHash());
			bw.Write(this.Destination.BinHash());
			bw.Write(this.TimeLimit);
			bw.WriteEnum(this.ChallengeType);
			bw.Write(this.PartUnlockable1);
			bw.Write(this.PartUnlockable2);
			bw.Write(this.PartUnlockable3);
		}

		/// <summary>
		/// Disassembles array into <see cref="Sponsor"/> properties.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read <see cref="Sponsor"/> with.</param>
		/// <param name="strr"><see cref="BinaryReader"/> to read strings with.</param>
		public void Disassemble(BinaryReader br, BinaryReader strr)
		{
			ushort position = 0;

			// Collection Name
			position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this._collection_name = strr.ReadNullTermUTF8();

			// Challenge Trigger
			position = br.ReadUInt16();
			strr.BaseStream.Position = position;
			this.WorldChallengeTrigger = strr.ReadNullTermUTF8();

			// Stage and Unlock settings
			this.BelongsToStage = br.ReadByte();
			this._padding0 = br.ReadByte();
			this.UseOutrunsAsReqRaces = (br.ReadByte() == 2) ? eBoolean.True : eBoolean.False;
			this.RequiredRacesWon = br.ReadByte();

			// Hashes
			this.UnlockableSMS = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.Destination = br.ReadUInt32().BinString(eLookupReturn.EMPTY);

			// Time Limit
			this.TimeLimit = br.ReadInt32();

			// Type and Unlockables
			this.ChallengeType = br.ReadEnum<WorldChallengeType>();
			this.PartUnlockable1 = br.ReadByte();
			this.PartUnlockable2 = br.ReadByte();
			this.PartUnlockable3 = br.ReadByte();
		}

		/// <summary>
		/// Casts all attributes from this object to another one.
		/// </summary>
		/// <param name="CName">CollectionName of the new created object.</param>
		/// <returns>Memory casted copy of the object.</returns>
		public override Collectable MemoryCast(string CName)
		{
			var result = new WorldChallenge(CName, this.Career);
			base.MemoryCast(this, result);
			return result;
		}

		/// <summary>
		/// Returns CollectionName, BinKey and GameSTR of this <see cref="GCareerRace"/> 
		/// as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"Collection Name: {this.CollectionName} | " +
				   $"BinKey: {this.BinKey:X8} | Game: {this.GameSTR}";
		}

		#endregion
	}
}