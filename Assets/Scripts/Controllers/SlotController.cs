using Models;
using ScriptableObjects;
using UnityEngine;
using Utils;
using Views;
using Yell;

namespace Controllers
{
	public class SlotController
	{
		public int DataCount => _model.TileDataContainer.DataList.Count;
		public bool IsFirstTwoSame => _model.IsFirstTwoSame;
		public SlotPosition Position => _model.SlotPosition;

		private readonly SlotModel _model;
		private readonly SlotView _view;

		public SlotController(SlotView view, SlotPosition slotPosition)
		{
			YellManager.Instance.Listen(YellType.OnFirstTwoSame, new YellAction(this, OnFirstTwoSame));
			YellManager.Instance.Listen(YellType.OnLastSlotAnimComplete, new YellAction(this, OnLastAnimCompleted));

			_model = ScriptableObject.CreateInstance<SlotModel>();
			_model.SlotPosition = slotPosition;
			_view = view;

			_view.OnTilesCreated += tiles =>
			{
				for (int i = 0; i < tiles.Count; i++)
				{
					tiles[i].SetData(_model.TileDataContainer.DataList[i]);
				}

				_model.Tiles = tiles;
			};
		}


		public void SetData(TileDataContainer data)
		{
			_model.TileDataContainer = data;
		}

		/// <summary>
		/// Stops the spine action and finds top and bottom by given tile
		/// </summary>
		/// <param name="matchItem">Centre tile</param>
		public void StopSpine(TileType matchItem)
		{
			int maxIndex = DataCount - 1;
			
			int index = _model.TileDataContainer.GetIndexByTile(matchItem);

			int topIndex = index - 1;
			if (topIndex < 0) topIndex = 0;
			else if (topIndex > maxIndex) topIndex = maxIndex;

			int bottomIndex = index + 1;
			if (bottomIndex > maxIndex) bottomIndex = 0;

			int lastIndex = _model.Tiles.Count - 1;
			_model.Tiles[lastIndex].SetData(_model.TileDataContainer.DataList[topIndex]);
			_model.Tiles[lastIndex - 1].SetData(_model.TileDataContainer.DataList[index]);
			_model.Tiles[lastIndex - 2].SetData(_model.TileDataContainer.DataList[bottomIndex]);


			_view.StopSpine();
		}

		private void OnFirstTwoSame(YellData yellData)
		{
			_model.IsFirstTwoSame = true;
		}

		private void OnLastAnimCompleted(YellData yellData)
		{
			_model.IsFirstTwoSame = false;
		}
	}
}