using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Test.Scripts
{
	public class TestSegments : MonoBehaviour
	{
		public TestSegmentItem segmentItemPrefab;

		public void SetData(Dictionary<SlotMatch, List<(int, int)>> segmentBounds, SlotMatch[] slotMatches)
		{
			foreach (var segmentBound in segmentBounds)
			{
				TestSegmentItem item = Instantiate(segmentItemPrefab, transform);
				item.SetData(segmentBound.Key, segmentBound.Value, slotMatches);
			}
		}
	}
}