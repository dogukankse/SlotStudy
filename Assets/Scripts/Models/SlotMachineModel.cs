using System.Collections.Generic;
using Common;
using Controllers;
using UnityEngine;
using Utils;

namespace Models
{
	public sealed class SlotMachineModel : ScriptableObject
	{
		public SlotMachineState SlotMachineState { get; set; } = SlotMachineState.None;
		public List<SlotController> SlotControllers { get; set; } = new List<SlotController>();
	}
}