﻿using System.Collections.Generic;



namespace Nikki.Utils
{
    /// <summary>
    /// Collection of DDS palette compression and decompression functions.
    /// </summary>
    public static class Palette
    {
        /// <summary>
        /// Convert PAL8 .dds format into 32-bit RGBA.
        /// </summary>
        /// <param name="data">Binary data block to be converted.</param>
        /// <returns>32-bit RGBA byte array converted based on PAL8 palette.</returns>
        public static unsafe byte[] P8toRGBA(byte[] data)
        {
            var palette = new uint[0x100]; // palette size is 0x400 bytes = 0x100 ints
            var result = new byte[(data.Length - 0x400) * 4]; // 8 bpp * 4 bytes per color

            fixed (byte* byteptr_t = &data[0])
            {
                // Load palette into memory for access
                for (int a1 = 0; a1 < 0x100; ++a1)
                    palette[a1] = *(uint*)(byteptr_t + a1 * 4);
            }
            fixed (byte* byteptr_t = &result[0])
            {
                // Write result data based on palette
                for (int a1 = 0, a2 = 0; a1 < data.Length - 0x400; ++a1, a2 += 4)
                {
                    var color = palette[data[a1 + 0x400]];
                    *(uint*)(byteptr_t + a2) = color;
                }
            }
            return result;
        }

        /// <summary>
        /// Convert RGBA .dds format to 8-bit PAL8.
        /// </summary>
        /// <param name="data">Binary data of the .dds file including header.</param>
        /// <returns>PAL8 formatted byte array with palette and compressed data.</returns>
        public static unsafe byte[] RGBAtoP8(byte[] data)
        {
            int size = 0x400 + data.Length / 4;
            var result = new byte[size];
            var map = new Dictionary<uint, byte>(0x100);
            int used = 0; // num of palette colors used; max = 0x100

            fixed (byte* byteptr_t = &data[0])
            {
                // Marshal through RGBA data
                for (int offset = 0, mapoff = 0, dataoff = 0x400; offset < data.Length; offset += 4)
                {
                    uint color = *(uint*)(byteptr_t + offset);
                    if (map.TryGetValue(color, out byte index))
                        result[dataoff++] = index;
                    else
                    {
                        if (used == 0x100) return null; // return null if palette is over
                        map[color] = (byte)used; // add palette to the dictionary
                        result[mapoff++] = data[offset + 0];
                        result[mapoff++] = data[offset + 1];
                        result[mapoff++] = data[offset + 2];
                        result[mapoff++] = data[offset + 3];
                        result[dataoff++] = (byte)used++;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Convert PAL4 .dds format into 32-bit RGBA.
        /// </summary>
        /// <param name="data">Binary data block to be converted.</param>
        /// <returns>32-bit RGBA byte array converted based on PAL4 palette.</returns>
        public static unsafe byte[] P4toRGBA(byte[] data)
        {
            var palette = new uint[0x10]; // palette size is 0x400 bytes = 0x100 ints
            var result = new byte[(data.Length - 0x40) * 8]; // 4 bpp * 4 bytes per color

            fixed (byte* byteptr_t = &data[0])
            {
                // Load palette into memory for access
                for (int a1 = 0; a1 < 0x10; ++a1)
                    palette[a1] = *(uint*)(byteptr_t + a1 * 4);
            }
            fixed (byte* byteptr_t = &result[0])
            {
                // Write result data based on palette
                for (int a1 = 0, a2 = 0; a1 < data.Length - 0x40; ++a1, a2 += 8)
                {
                    var bit1 = data[a1 + 0x40] >> 4; // first 4 bits in the byte being read
                    var bit2 = data[a1 + 0x40] & 0x0F; // second 4 bits in the byte being read

                    *(uint*)(byteptr_t + a2) = palette[bit1];
                    *(uint*)(byteptr_t + a2 + 4) = palette[bit2];
                }
            }
            return result;
        }

        /// <summary>
        /// Convert RGBA .dds format to 4-bit PAL4.
        /// </summary>
        /// <param name="data">Binary data of the .dds file including header.</param>
        /// <returns>PAL4 formatted byte array with palette and compressed data.</returns>
        public static unsafe byte[] RGBAtoP4(byte[] data)
        {
            int size = 0x40 + data.Length / 8;
            var result = new byte[size];
            var map = new Dictionary<uint, byte>(0x10);
            byte used = 0; // num of palette colors used; max = 0x10

            fixed (byte* byteptr_t = &data[0])
            {
                // Marshal through RGBA data
                for (int offset = 0, mapoff = 0, dataoff = 0x40; offset < data.Length; offset += 8)
                {
                    uint color1 = *(uint*)(byteptr_t + offset);
                    uint color2 = *(uint*)(byteptr_t + offset + 4);

                    if (!map.TryGetValue(color1, out byte bit1))
                    {
                        if (used == 0x10) return null; // return null if palette is over
                        bit1 = used; // assign current color index
                        map[color1] = used++; // add palette to the dictionary

                        // Write color to the palette
                        result[mapoff++] = data[offset + 0];
                        result[mapoff++] = data[offset + 1];
                        result[mapoff++] = data[offset + 2];
                        result[mapoff++] = data[offset + 3];
                    }
                    if (!map.TryGetValue(color2, out byte bit2))
                    {
                        if (used == 0x10) return null; // return null if palette is over
                        bit2 = used; // assign current color index
                        map[color2] = used++; // add palette to the dictionary

                        // Write color to the palette
                        result[mapoff++] = data[offset + 4];
                        result[mapoff++] = data[offset + 5];
                        result[mapoff++] = data[offset + 6];
                        result[mapoff++] = data[offset + 7];
                    }

                    result[dataoff++] = (byte)((bit1 << 4) | bit2);
                }
            }
            return result;
        }
    }
}