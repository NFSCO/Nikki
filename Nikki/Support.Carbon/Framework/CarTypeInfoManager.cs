﻿using System;
using System.IO;
using Nikki.Core;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Exception;
using Nikki.Support.Carbon.Class;
using CoreExtensions.IO;



namespace Nikki.Support.Carbon.Framework
{
	/// <summary>
	/// A <see cref="Manager{T}"/> for <see cref="CarTypeInfo"/> collections.
	/// </summary>
	public class CarTypeInfoManager : Manager<CarTypeInfo>
	{
		/// <summary>
		/// Game to which the class belongs to.
		/// </summary>
		public override GameINT GameINT => GameINT.Carbon;

		/// <summary>
		/// Game string to which the class belongs to.
		/// </summary>
		public override string GameSTR => GameINT.Carbon.ToString();

		/// <summary>
		/// Name of this <see cref="CarTypeInfoManager"/>.
		/// </summary>
		public override string Name => "CarTypeInfos";

		/// <summary>
		/// True if this <see cref="Manager{T}"/> is read-only; otherwise, false.
		/// </summary>
		public override bool IsReadOnly => false;

		/// <summary>
		/// Indicates required alighment when this <see cref="CarTypeInfoManager"/> is being serialized.
		/// </summary>
		public override Alignment Alignment { get; }

		/// <summary>
		/// Initializes new instance of <see cref="CarTypeInfoManager"/>.
		/// </summary>
		/// <param name="db"><see cref="Datamap"/> to which this manager belongs to.</param>
		public CarTypeInfoManager(Datamap db)
		{
			this.Database = db;
			this.Extender = 5;
			this.Alignment = Alignment.Default;
		}

		/// <summary>
		/// Assembles collection data into byte buffers.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		/// <param name="mark">Watermark to put in the padding blocks.</param>
		internal override void Assemble(BinaryWriter bw, string mark)
		{
			if (this.Count == 0) return;

			bw.GeneratePadding(mark, this.Alignment);

			bw.WriteEnum(eBlockID.CarTypeInfos);
			bw.Write(this.Count * CarTypeInfo.BaseClassSize + 8);
			bw.Write(0x11111111);
			bw.Write(0x11111111);

			int count = 0;

			foreach (var collection in this)
			{

				collection.Index = count++;
				collection.Assemble(bw);

			}
		}

		/// <summary>
		/// Disassembles data into separate collections in this <see cref="CarTypeInfoManager"/>.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		/// <param name="block"><see cref="Block"/> with offsets.</param>
		internal override void Disassemble(BinaryReader br, Block block)
		{
			if (Block.IsNullOrEmpty(block)) return;
			if (block.BlockID != eBlockID.CarTypeInfos) return;

			for (int loop = 0; loop < block.Offsets.Count; ++loop)
			{

				br.BaseStream.Position = block.Offsets[loop] + 4;
				var size = br.ReadInt32();
				br.BaseStream.Position += 8;

				int count = (size - 8) / CarTypeInfo.BaseClassSize;
				this.Capacity += count;
				
				for (int i = 0; i < count; ++i)
				{

					var collection = new CarTypeInfo(br, this);

					try { this.Add(collection); }
					catch { } // skip if exists

				}

			}
		}

		/// <summary>
		/// Checks whether CollectionName provided allows creation of a new collection.
		/// </summary>
		/// <param name="cname">CollectionName to check.</param>
		internal override void CreationCheck(string cname)
		{
			if (String.IsNullOrWhiteSpace(cname))
			{

				throw new ArgumentNullException("CollectionName cannot be null, empty or whitespace");

			}

			if (cname.Contains(" "))
			{

				throw new ArgumentException("CollectionName cannot contain whitespace");

			}

			if (cname.Length > CarTypeInfo.MaxCNameLength)
			{

				throw new ArgumentLengthException(CarTypeInfo.MaxCNameLength);

			}

			if (this.Find(cname) != null)
			{

				throw new CollectionExistenceException(cname);

			}
		}

		/// <summary>
		/// Exports collection with CollectionName specified to a filename provided.
		/// </summary>
		/// <param name="cname">CollectionName of a collection to export.</param>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public override void Export(string cname, BinaryWriter bw)
		{
			var index = this.IndexOf(cname);

			if (index == -1)
			{

				throw new CollectionExistenceException($"Collection named {cname} does not exist");

			}
			else
			{

				this[index].Serialize(bw);

			}
		}

		/// <summary>
		/// Imports collection from file provided and attempts to add it to the end of 
		/// this <see cref="Manager{T}"/> in case it does not exist.
		/// </summary>
		/// <param name="type">Type of serialization of a collection.</param>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public override void Import(eSerializeType type, BinaryReader br)
		{
			var position = br.BaseStream.Position;
			var header = new SerializationHeader();
			header.Read(br);

			var collection = new CarTypeInfo();

			if (header.ID != eBlockID.Nikki)
			{

				br.BaseStream.Position = position;
				collection.Disassemble(br);

			}
			else
			{

				if (header.Game != this.GameINT)
				{

					throw new Exception($"Stated game inside collection is {header.Game}, while should be {this.GameINT}");

				}

				if (header.Name != this.Name)
				{

					throw new Exception($"Imported collection is not a collection of type {this.Name}");

				}

				collection.Deserialize(br);

			}

			var index = this.IndexOf(collection);

			if (index == -1)
			{

				this.Add(collection);

			}
			else
			{

				switch (type)
				{
					case eSerializeType.Negate:
						break;

					case eSerializeType.Synchronize:
					case eSerializeType.Override:
						this[index] = collection;
						break;

					default:
						break;
				}

			}
		}
	}
}
