﻿using System;
using System.IO;
using Nikki.Utils;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.MostWanted.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="ZeroDamage"/> used in preset rides.
	/// </summary>
	public class ZeroDamage : ASubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageFront { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageFrontLeft { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageFrontRight { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageRear { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageRearLeft { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ZeroDamageRearRight { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override ASubPart PlainCopy()
		{
			var result = new ZeroDamage();

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
			this.ZeroDamageFront = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.ZeroDamageFrontLeft = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.ZeroDamageFrontRight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.ZeroDamageRear = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.ZeroDamageRearLeft = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
			this.ZeroDamageRearRight = br.ReadUInt32().BinString(eLookupReturn.EMPTY);
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to read data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.ZeroDamageFront.BinHash());
			bw.Write(this.ZeroDamageFrontLeft.BinHash());
			bw.Write(this.ZeroDamageFrontRight.BinHash());
			bw.Write(this.ZeroDamageRear.BinHash());
			bw.Write(this.ZeroDamageRearLeft.BinHash());
			bw.Write(this.ZeroDamageRearRight.BinHash());
		}
	}
}
