using System;
using System.Linq;
using System.Net.NetworkInformation;
using Components;
using Controllers;
using ScriptableObjects;
using UnityEngine;
using Utils;
using Views;
using Yell;

namespace Managers
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private SpinStopButton _button;
		[SerializeField] private SlotMachineView _slotMachineView;
		[SerializeField] private PossibilityTable _possibilityTable;
		[SerializeField] private CoinParticle _coinParticle;

		private SlotMatchProvider _matchProvider;

		private SlotMachineController _slotMachineController;

		#region UnityEvents

		private void Awake()
		{
			YellManager.Instance.Listen(YellType.OnSlotMachineCreated, new YellAction(this, OnSlotMachineCreated));
			YellManager.Instance.Listen(YellType.PlayCoinParticle, new YellAction(this, OnPlayParticle));
			YellManager.Instance.Listen(YellType.MaxIndexReached, new YellAction(this, CreateNewMatches));

			LoadOrCreateData();
			_button.ButtonClicked += OnSpinButtonClicked;
		}


		private void OnDestroy()
		{
			//save current match list state to machine
			SaveManager.Instance.Save(_matchProvider.GetSaveData(), _matchProvider.GetCurrentIndex());
			print("Saved");
		}

		#endregion


		private void LoadOrCreateData()
		{
			//if there is a save file load it, otherwise create new data 
			if (SaveManager.Instance.Load(out SaveData data))
			{
				print("Loading");
				SlotMatchProvider.Init(data.matchList, data.currentIndex);
			}
			else
			{
				CreateNewMatches();
			}

			_matchProvider = SlotMatchProvider.Instance;
			print(_matchProvider.GetMatch(false) + " " + _matchProvider.GetCurrentIndex());
		}

		private void CreateNewMatches(YellData yellData = null)
		{
			print("Creating");
			SlotMatchFiller matchFiller = new SlotMatchFiller(100, _possibilityTable);
			var slotMatches = matchFiller.Fill();
			SlotMatchProvider.Init(slotMatches.ToList(), 0);
		}

		private void OnSlotMachineCreated(YellData yellData)
		{
			_slotMachineController = (SlotMachineController) yellData.data;
		}

		//play particle anim if all slots are same
		private void OnPlayParticle(YellData yellData)
		{
			SlotMatch match = (SlotMatch) yellData.data;
			int count = 0;

			switch (match)
			{
				case SlotMatch.AAA:
					count = ParticleCount.ACount;
					break;
				case SlotMatch.BonusBonusBonus:
					count = ParticleCount.BonusCount;
					break;
				case SlotMatch.SevenSevenSeven:
					count = ParticleCount.SevenCount;
					break;
				case SlotMatch.WildWildWild:
					count = ParticleCount.WildCount;
					break;
				case SlotMatch.JackpotJackpotJackpot:
					count = ParticleCount.JackpotCount;
					break;
			}

			if (count > 0)
				_coinParticle.Play(count, () => _button.SetInteractable(true));
			else
				_button.SetInteractable(true);
		}

		private void OnSpinButtonClicked()
		{
			if (_slotMachineController.State == SlotMachineState.Spinning)
			{
				_button.SetInteractable(false);
				_slotMachineController.StopSpine();
			}
			else
			{
				_slotMachineController.StartSpine();
			}

			_button.UpdateText(_slotMachineController.State);
		}
	}
}