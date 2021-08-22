using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Test.Scripts
{
	public class TestSegmentItem : MonoBehaviour
	{
		public TestCell cellPrefab;

		public void SetData(SlotMatch match, List<(int, int)> segmentBound, SlotMatch[] slotMatches)
		{
			foreach (var bound in segmentBound)
			{
				TestCell cell = Instantiate(cellPrefab, transform);
				cell.SetText($"{bound.Item1} - {bound.Item2}", Color.black);

				int count = 0;
				for (int i = bound.Item1; i < bound.Item2 + 1; i++)
				{
					if (match == slotMatches[i])
					{
						count++;
					}
				}

				cell.SetColor(count == 1 ? Color.green : Color.red);
			}
		}
	}
}