using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Test.Scripts
{
	public class TestItems : MonoBehaviour
	{
		public TestCell cellPrefab;

		public void SetItems(ICollection<SlotMatch> items,Transform parent)
		{
			foreach (var item in items)
			{
				TestCell cell = Instantiate(cellPrefab, parent);
				cell.SetText(item.ToString());
			}
		}
	}
}