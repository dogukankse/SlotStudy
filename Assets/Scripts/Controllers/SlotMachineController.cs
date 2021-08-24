using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Utils;
using Views;
using Yell;

namespace Controllers
{
	public class SlotMachineController
	{
		public SlotMachineState State => _model.SlotMachineState;

		private readonly SlotMachineModel _model;
		private readonly SlotMachineView _view;

		private AsyncOperationHandle<TileDataContainer> _handle;

		private SlotMatch _slotMatch;

		public SlotMachineController(SlotMachineView view)
		{
			YellManager.Instance.Listen(YellType.OnSlotCreated, new YellAction(this, OnSlotCreated));
			YellManager.Instance.Listen(YellType.OnLastSlotAnimComplete, new YellAction(this, OnLastSlotAnimCompleted));

			_model = ScriptableObject.CreateInstance<SlotMachineModel>();
			_view = view;

			SetData();
		}


		public void StartSpine()
		{
			_model.SlotMachineState = SlotMachineState.Spinning;
			_view.StartSpine();
		}

		/// <summary>
		/// Gets next match from local save from provider and 
		/// </summary>
		public void StopSpine()
		{
			_model.SlotMachineState = SlotMachineState.Stopped;
			_slotMatch = SlotMatchProvider.Instance.GetMatch();
			Debug.Log(_slotMatch + " " + SlotMatchProvider.Instance.GetCurrentIndex());
			List<TileType> matchItems = SlotMatchParser.Parse(_slotMatch).ToList();

			//if first element is same, trigger an action for last slot animation
			if (matchItems[0] == matchItems[1])
				YellManager.Instance.Yell(YellType.OnFirstTwoSame);

			foreach (var slot in _model.SlotControllers)
			{
				switch (slot.Position)
				{
					case SlotPosition.Left:
						slot.StopSpine(matchItems[0]);
						break;
					case SlotPosition.Centre:
						slot.StopSpine(matchItems[1]);
						break;
					case SlotPosition.Right:
						slot.StopSpine(matchItems[2]);
						break;
				}
			}
		}

		private async void SetData()
		{
			var data = await FetchTileDataContainer();

			foreach (var slot in _model.SlotControllers)
			{
				slot.SetData(data);
			}

			YellManager.Instance.Yell(YellType.OnTileDataContainerFetched);
		}

		private async Task<TileDataContainer> FetchTileDataContainer()
		{
			_handle = Addressables.LoadAssetAsync<TileDataContainer>(Addresses.TileDataContainer);
			await _handle.Task;

			switch (_handle.Status)
			{
				case AsyncOperationStatus.Succeeded:
					return _handle.Result;
				case AsyncOperationStatus.Failed:
					throw new Exception(Addresses.TileDataContainer + " loading failed");
			}

			return null;
		}

		private void OnSlotCreated(YellData yell)
		{
			SlotController controller = (SlotController) yell.data;
			_model.SlotControllers.Add(controller);
		}

		private void OnLastSlotAnimCompleted(YellData yellData)
		{
			YellManager.Instance.Yell(YellType.PlayCoinParticle, new YellData(_slotMatch));
		}
	}
}