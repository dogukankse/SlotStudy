using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "TileDataContainer", menuName = "Custom/Tile Data Container", order = 0)]
	public class TileDataContainer : ScriptableObject
	{
		public List<TileData> DataList => _dataList;

		[SerializeField] private List<TileData> _dataList;


		public int GetIndexByTile(TileType matchItem)
		{
			TileData tileData = _dataList.First(x => x.Type == matchItem);
			return _dataList.IndexOf(tileData);
		}
	}
}