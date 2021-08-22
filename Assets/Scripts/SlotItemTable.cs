using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class SlotItemTable
{
	public Dictionary<SlotMatch, int[]> SegmentEndIndexes => _segmentEndIndexes;

	private readonly SlotMatch[,] _table;
	private readonly float[] _totalCounts;
	private readonly SlotMatch[] _items;
	private readonly int _size;
	private readonly Dictionary<SlotMatch, int[]> _segmentEndIndexes;


	public SlotItemTable(int size, SlotMatchFloatDictionary items)
	{
		_table = new SlotMatch[items.Keys.Count, size];
		_size = size;
		_totalCounts = items.Values.ToArray();
		_items = items.Keys.ToArray();
		_segmentEndIndexes = new Dictionary<SlotMatch, int[]>();

		SeparateToSegments();
		Populate();
	}


	public void ClearSegment(SlotMatch item, int columnIndex)
	{
		int rowIndex = GetRowIndexByItem(item);
		int segmentIndex = GetSegmentIndex(rowIndex, columnIndex);

		int endIndex = _segmentEndIndexes[item][segmentIndex];
		int lenght = segmentIndex == 0 ? 0 : segmentIndex - 1;
		int startIndex = endIndex - _segmentEndIndexes[item][lenght];


		for (int j = startIndex; j <= endIndex; j++)
		{
			_table[rowIndex, j] = SlotMatch.None;
		}
	}

	public Dictionary<SlotMatch, int> GetSegmentItemCounts(int columnIndex)
	{
		Dictionary<SlotMatch, int> counts = new Dictionary<SlotMatch, int>();

		foreach (var item in _items)
		{
			int rowIndex = GetRowIndexByItem(item);
			int segmentIndex = GetSegmentIndex(rowIndex, columnIndex);

			int endIndex = _segmentEndIndexes[item][segmentIndex];
			int lenght = segmentIndex == 0 ? 0 : segmentIndex - 1;
			int startIndex = endIndex - _segmentEndIndexes[item][lenght];
			int count = 0;
			for (int j = startIndex; j <= endIndex; j++)
			{
				if (_table[rowIndex, j] != SlotMatch.None)
				{
					count++;
				}
			}

			counts.Add(item, count);
		}

		return counts;
	}

	private int GetSegmentIndex(int rowIndex, int columnIndex)
	{
		var values = _segmentEndIndexes.Values.ToList();
		for (int i = 0; i < values[rowIndex].Length; i++)
		{
			int currentSegmentBound = values[rowIndex][i];
			if (columnIndex <= currentSegmentBound) return i;
		}

		throw new Exception("Segment Index Not Found");
	}

	private int GetRowIndexByItem(SlotMatch item)
	{
		return _segmentEndIndexes.Keys.ToList().IndexOf(item);
	}

	public void ClearColumn(int columnIndex)
	{
		for (int i = 0; i < _table.GetLength(0); i++)
		{
			_table[i, columnIndex] = SlotMatch.None;
		}
	}

	public List<SlotMatch> GetPossibleColumnItems(int columnIndex)
	{
		List<SlotMatch> items = new List<SlotMatch>();
		for (int i = 0; i < _table.GetLength(0); i++)
		{
			SlotMatch item = _table[i, columnIndex];
			if (item != SlotMatch.None)
				items.Add(item);
		}

		return items;
	}


	private void Populate()
	{
		for (int i = 0; i < _table.GetLength(0); i++)
		{
			for (int j = 0; j < _table.GetLength(1); j++)
			{
				_table[i, j] = _items[i];
			}
		}
	}

	private void SeparateToSegments()
	{
		for (int i = 0; i < _totalCounts.Length; i++)
		{
			int totalCount = (int) _totalCounts[i];
			int everyOf = Mathf.FloorToInt((float) _size / totalCount);
			int[] partLengths = Enumerable.Repeat(everyOf, totalCount).ToArray();
			int extra = _size % totalCount;
			for (int j = 0; j < extra; j++)
			{
				partLengths[j]++;
			}


			for (int j = 0; j < partLengths.Length; j++)
			{
				if (j == 0) partLengths[j]--;
				else partLengths[j] += partLengths[j - 1];
			}

			_segmentEndIndexes[_items[i]] = partLengths;
		}
	}

	public void PrintTable()
	{
		string s = "";
		for (int i = 0; i < _table.GetLength(0); i++)
		{
			string ss = "";
			for (int j = 0; j < _table.GetLength(1); j++)
			{
				ss += _table[i, j] == SlotMatch.None ? "- " : _table[i, j] + " ";
			}

			ss += "\n";
			s += ss;
		}

		Debug.Log(s);
	}
}