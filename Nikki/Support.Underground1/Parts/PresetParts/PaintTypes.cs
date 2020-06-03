﻿using System;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground1.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="PaintTypes"/> used in preset rides.
	/// </summary>
	public class PaintTypes : ASubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string BasePaintType { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string EnginePaintType { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string SpoilerPaintType { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string BrakesPaintType { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string ExhaustPaintType { get; set; } = String.Empty;

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public string RimsPaintType { get; set; } = String.Empty;

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override ASubPart PlainCopy()
		{
			var result = new PaintTypes();

			foreach (var property in this.GetType().GetProperties())
			{

				property.SetValue(result, property.GetValue(this));

			}

			return result;
		}
	}
}
