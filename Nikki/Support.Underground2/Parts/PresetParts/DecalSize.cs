﻿using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="DecalSize"/> used in preset rides.
	/// </summary>
	public class DecalSize : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalHood { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalFrontWindow { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalRearWindow { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalLeftDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalRightDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalLeftQuarter { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalRightQuarter { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalWideLeftDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalWideRightDoor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalWideLeftQuarter { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string DecalWideRightQuarter { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new DecalSize();

			foreach (var property in this.GetType().GetProperties())
			{

				property.SetValue(result, property.GetValue(this));

			}

			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.DecalHood = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalFrontWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalRearWindow = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalLeftDoor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalRightDoor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalLeftQuarter = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalRightQuarter = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalWideLeftDoor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalWideRightDoor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalWideLeftQuarter = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.DecalWideRightQuarter = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.DecalHood.BinHash());
			bw.Write(this.DecalFrontWindow.BinHash());
			bw.Write(this.DecalRearWindow.BinHash());
			bw.Write(this.DecalLeftDoor.BinHash());
			bw.Write(this.DecalRightDoor.BinHash());
			bw.Write(this.DecalLeftQuarter.BinHash());
			bw.Write(this.DecalRightQuarter.BinHash());
			bw.Write(this.DecalWideLeftDoor.BinHash());
			bw.Write(this.DecalWideRightDoor.BinHash());
			bw.Write(this.DecalWideLeftQuarter.BinHash());
			bw.Write(this.DecalWideRightQuarter.BinHash());
		}
	}
}
