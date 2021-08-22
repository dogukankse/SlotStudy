using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
	public static class Extensions
	{
		public static T GetRandom<T>(this List<T> enumerable)
		{
			int index = Random.Range(0, enumerable.Count);
			return enumerable[index];
		}

		public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
		{
			return Enumerable.Range(0, matrix.GetLength(0))
				.Select(x => matrix[x, columnNumber])
				.ToArray();
		}

		public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
		{
			return Enumerable.Range(0, matrix.GetLength(1))
				.Select(x => matrix[rowNumber, x])
				.ToArray();
		}
	}
}