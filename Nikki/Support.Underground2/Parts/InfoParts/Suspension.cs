﻿using System.IO;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Interface;
using Nikki.Reflection.Attributes;



namespace Nikki.Support.Underground2.Parts.InfoParts
{
	/// <summary>
	/// A unit <see cref="Suspension"/> used in car performance.
	/// </summary>
	public class Suspension : ASubPart, ICopyable<Suspension>
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockStiffnessFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockExtStiffnessFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SpringProgressionFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockValvingFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SwayBarFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TrackWidthFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float CounterBiasFront { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockDigressionFront { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockStiffnessRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockExtStiffnessRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SpringProgressionRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockValvingRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float SwayBarRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float TrackWidthRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float CounterBiasRear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		public float ShockDigressionRear { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="Suspension"/>.
		/// </summary>
        public Suspension() { }

        /// <summary>
        /// Creates a plain copy of the objects that contains same values.
        /// </summary>
        /// <returns>Exact plain copy of the object.</returns>
        public Suspension PlainCopy()
        {
            var result = new Suspension();
            var ThisType = this.GetType();
            var ResultType = result.GetType();
            foreach (var ThisProperty in ThisType.GetProperties())
            {
                var ResultField = ResultType.GetProperty(ThisProperty.Name);
                ResultField.SetValue(result, ThisProperty.GetValue(this));
            }
            return result;
        }

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.ShockStiffnessFront = br.ReadSingle();
			this.ShockExtStiffnessFront = br.ReadSingle();
			this.SpringProgressionFront = br.ReadSingle();
			this.ShockValvingFront = br.ReadSingle();
			this.SwayBarFront = br.ReadSingle();
			this.TrackWidthFront = br.ReadSingle();
			this.CounterBiasFront = br.ReadSingle();
			this.ShockDigressionFront = br.ReadSingle();
			this.ShockStiffnessRear = br.ReadSingle();
			this.ShockExtStiffnessRear = br.ReadSingle();
			this.SpringProgressionRear = br.ReadSingle();
			this.ShockValvingRear = br.ReadSingle();
			this.SwayBarRear = br.ReadSingle();
			this.TrackWidthRear = br.ReadSingle();
			this.CounterBiasRear = br.ReadSingle();
			this.ShockDigressionRear = br.ReadSingle();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.Write(this.ShockStiffnessFront);
			bw.Write(this.ShockExtStiffnessFront);
			bw.Write(this.SpringProgressionFront);
			bw.Write(this.ShockValvingFront);
			bw.Write(this.SwayBarFront);
			bw.Write(this.TrackWidthFront);
			bw.Write(this.CounterBiasFront);
			bw.Write(this.ShockDigressionFront);
			bw.Write(this.ShockStiffnessRear);
			bw.Write(this.ShockExtStiffnessRear);
			bw.Write(this.SpringProgressionRear);
			bw.Write(this.ShockValvingRear);
			bw.Write(this.SwayBarRear);
			bw.Write(this.TrackWidthRear);
			bw.Write(this.CounterBiasRear);
			bw.Write(this.ShockDigressionRear);
		}
	}
}
