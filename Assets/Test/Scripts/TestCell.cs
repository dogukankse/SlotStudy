using UnityEngine;
using UnityEngine.UI;

namespace Test.Scripts
{
	public class TestCell : MonoBehaviour
	{
		private Text _txt;
		private Image _bg;

		public void SetText(string text)
		{
			_txt.text = text;
		}

		public void SetText(string text, Color color)
		{
			_txt.text = text;
			_txt.color = color;
		}

		public void SetColor(Color color)
		{
			_bg.color = color;
		}

		private void Awake()
		{
			_txt = GetComponentInChildren<Text>();
			_bg = GetComponent<Image>();
		}
	}
}