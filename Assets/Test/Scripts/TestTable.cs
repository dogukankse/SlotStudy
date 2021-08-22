using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Test.Scripts
{
	public class TestTable : MonoBehaviour
	{
		public TestSegments segments;
		public TestItems items;
		public PossibilityTable possibilityTable;

		private void Start()
		{
			SlotMatchFiller filler = new SlotMatchFiller(100, possibilityTable);
			var arr = filler.Fill();
			string s = "";
			for (int i = 0; i < arr.Length; i++)
			{
				s += $"{i}.{arr[i]} ";
			}

			print(s);

			var itemsList = possibilityTable.Table.Keys;
			items.SetItems(itemsList, items.transform);

			var segmentsEnds = filler.SegmentEndIndexes;
			var segmentBounds = CreateSegmentBounds(segmentsEnds);
			segments.SetData(segmentBounds, arr);
		}

		private Dictionary<SlotMatch, List<(int, int)>> CreateSegmentBounds(Dictionary<SlotMatch, int[]> segmentsEnds)
		{
			var segmentsDict = new Dictionary<SlotMatch, List<(int, int)>>();

			var keys = segmentsEnds.Keys.ToList();
			var values = segmentsEnds.Values.ToList();
			for (int i = 0; i < keys.Count; i++)
			{
				segmentsDict.Add(keys[i], new List<(int, int)>());
				segmentsDict[keys[i]].Add((0, values[i][0]));
				for (int j = 0; j < values[i].Length - 1; j++)
				{
					var range = (values[i][j] + 1, values[i][j + 1]);
					segmentsDict[keys[i]].Add(range);
				}
			}

			return segmentsDict;
		}
	}
}