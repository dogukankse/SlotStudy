using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

namespace Components
{
	public class SpinStopButton : MonoBehaviour
	{
		public UnityAction ButtonClicked;
		private Button _btn;

		public void SetInteractable(bool status)
		{
			_btn.interactable = status;
		}

		public void UpdateText(SlotMachineState slotMachineState)
		{
			_btn.GetComponentInChildren<Text>().text =
				slotMachineState == SlotMachineState.Spinning ? I18N.En.Stop : I18N.En.Spin;
		}

		private void Awake()
		{
			_btn = GetComponent<Button>();
			_btn.onClick.AddListener(OnButtonClick);
		}

		private void OnButtonClick()
		{
			ButtonClicked?.Invoke();
		}
	}
}