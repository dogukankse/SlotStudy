using System;
using System.Linq;
using UnityEngine;
using Utils;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "PossibilityTable", menuName = "Custom/Possibility Table", order = 0)]
	public class PossibilityTable : ScriptableObject
	{
		public SlotMatchFloatDictionary Table => _table;

		[SerializeField] private SlotMatchFloatDictionary _table;

		private void OnEnable()
		{
			float sum = _table.Values.Sum();
			if (sum != 100f) throw new Exception("The sum of the values has to be 100");
		}
	}
}