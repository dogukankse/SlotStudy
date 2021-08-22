using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

namespace Components
{
	public class SlotTile : MonoBehaviour
	{
		private TileData _tileData;
		private SpriteRenderer _renderer;
		private TileData _defaultData;

		#region UnityAction

		private void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
		}

		#endregion

		public void SetData(TileData tileData)
		{
			_tileData = tileData;
			if (_defaultData == null)
				_defaultData = tileData;
			UpdateToNormalState();
		}

		public void UpdateToBlurState()
		{
			_renderer.sprite = _tileData.Blur;
		}

		public void UpdateToNormalState()
		{
			_renderer.sprite = _tileData.Normal;
		}
		public void ResetTile()
		{
			SetData(_defaultData);
		}
	}
}