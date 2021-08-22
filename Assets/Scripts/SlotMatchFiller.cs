using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Utils;

public class SlotMatchFiller
{
	public Dictionary<SlotMatch, int[]> SegmentEndIndexes => _slotItemTable.SegmentEndIndexes;

	
	private readonly SlotItemTable _slotItemTable;
	private readonly SlotMatch[] _rndArr;


	public SlotMatchFiller(int size, PossibilityTable possibilityTable)
	{
		_slotItemTable = new SlotItemTable(size, possibilityTable.Table);
		_rndArr = new SlotMatch[size];
	}


	public SlotMatch[] Fill()
	{
		for (int i = 0; i < _rndArr.Length; i++)
		{
			var segmentCounts = _slotItemTable.GetSegmentItemCounts(i);
			var selectedItem = GetMinItem(segmentCounts);
			_slotItemTable.ClearColumn(i);
			_slotItemTable.ClearSegment(selectedItem, i);
			_rndArr[i] = selectedItem;
		}

		return _rndArr;
	}

	private SlotMatch GetMinItem(Dictionary<SlotMatch, int> segmentCounts)
	{
		var ordered = segmentCounts
			.OrderBy(x => x.Value)
			.Where(x => x.Value != 0)
			.ToDictionary(x => x.Key, x => x.Value);

		return ordered.Where(x => x.Value == ordered.Values.First())
			.ToDictionary(x => x.Key, x => x.Value).Keys.ToList().GetRandom();
	}
}