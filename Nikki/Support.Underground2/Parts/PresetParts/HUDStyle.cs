﻿using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="HUDStyle"/> used in preset rides.
	/// </summary>
	public class HUDStyle : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string CustomHUD { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string HUDBackingColor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string HUDNeedleColor { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string HUDCharacterColor { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new HUDStyle()
			{
				CustomHUD = this.CustomHUD,
				HUDBackingColor = this.HUDBackingColor,
				HUDNeedleColor = this.HUDNeedleColor,
				HUDCharacterColor = this.HUDCharacterColor
			};

			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.CustomHUD = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.HUDBackingColor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.HUDNeedleColor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.HUDCharacterColor = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.CustomHUD.BinHash());
			bw.Write(this.HUDBackingColor.BinHash());
			bw.Write(this.HUDNeedleColor.BinHash());
			bw.Write(this.HUDCharacterColor.BinHash());
		}
	}
}
