using System.Collections.Generic;
using System.Linq;
using Common;
using Controllers;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Random = UnityEngine.Random;

namespace Views
{
	public class SlotMachineView : MonoBehaviour
	{
		[SerializeField] private List<SlotView> _slots;

		private SlotMachineController _controller;

		#region UnityEvents

		private void Awake()
		{
			_controller = new SlotMachineController(this);
		}

		#endregion

		public void StartSpine()
		{
			float delay = Random.Range(0f, .5f);
			foreach (var slot in _slots)
			{
				DOVirtual.DelayedCall(delay, slot.Spine);
				delay += Random.Range(.15f, .25f);
			}
		}
	}
}