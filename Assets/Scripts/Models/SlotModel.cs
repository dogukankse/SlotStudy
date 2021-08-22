using System.Collections.Generic;
using Components;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Models
{
	public class SlotModel : ScriptableObject
	{
		public TileDataContainer TileDataContainer { get; set; }
		public SlotPosition SlotPosition { get; set; }
		public bool IsFirstTwoSame { get; set; }
		public List<SlotTile> Tiles { get; set; }
	}
}