// Solution:     TopoEdit
// Project:       JackTests01
// File:             Identifiers.cs
// Created:      2023-12-28 (7:54 PM)

using System;
using System.Security.Cryptography;
using System.Text;

namespace SharedCode.ShUtil
{
	public class Identifiers
	{
		internal static readonly char[] chars =
			"abcdefghijklmnopqrstuvwxyz-ABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890".ToCharArray();
		
		internal static readonly char[] numchars =
			"1234567890".ToCharArray();


		public static string GetRandomString(int size)
		{
			byte[] data = new byte[4 * size];

			using (var rg = RandomNumberGenerator.Create())
			{
				rg.GetBytes(data);
			}

			StringBuilder sb = new StringBuilder(size);

			for (var i = 0; i < size; i++)
			{
				var rnd = BitConverter.ToUInt32(data, i * 4);
				var idx = rnd % chars.Length;

				sb.Append(chars[idx]);
			}

			return sb.ToString();
		}

		public static string GetRandomNumericString(int size)
		{
			byte[] data = new byte[4 * size];

			using (var rg = RandomNumberGenerator.Create())
			{
				rg.GetBytes(data);
			}

			StringBuilder sb = new StringBuilder(size);

			for (var i = 0; i < size; i++)
			{
				var rnd = BitConverter.ToUInt32(data, i * 4);
				var idx = rnd % numchars.Length;

				sb.Append(numchars[idx]);
			}

			return sb.ToString();
		}
	}
}