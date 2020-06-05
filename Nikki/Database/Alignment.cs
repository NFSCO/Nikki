﻿namespace Nikki.Database
{
	/// <summary>
	/// Represents data alignment type for <see cref="Manager{T}"/>.
	/// </summary>
	public struct Alignment
	{
		/// <summary>
		/// Represents alignment type.
		/// </summary>
		public enum eAlignType : int
		{
			/// <summary>
			/// Alignment of type Z = X % Y
			/// </summary>
			Modular = 1,

			/// <summary>
			/// Alignment of type Z = 0x10 - Y
			/// </summary>
			Actual  = 2,
		}

		/// <summary>
		/// Alignment offset value.
		/// </summary>
		public int Align { get; set; }

		/// <summary>
		/// <see cref="eAlignType"/> of this alignment.
		/// </summary>
		public eAlignType AlignType { get; set; }

		/// <summary>
		/// Default <see cref="Alignment"/> with Modular alignment type of 0x10.
		/// </summary>
		public static readonly Alignment Default = new Alignment(0x10, eAlignType.Modular);

		/// <summary>
		/// Initializes new instance of <see cref="Alignment"/> with align and type provided.
		/// </summary>
		/// <param name="align">Alignment offset value.</param>
		/// <param name="type"><see cref="eAlignType"/> of this alignment.</param>
		public Alignment(int align, eAlignType type)
		{
			this.Align = align;
			this.AlignType = type;

		}

		/// <summary>
		/// Returns alignment type and alignment offset as a string.
		/// </summary>
		/// <returns></returns>
		public override string ToString() => $"Type: {this.AlignType} | Align: 0x{this.Align:X}";
	}
}
