using System;
using System.Collections.Generic;
using Components;
using Controllers;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Yell;
using Random = UnityEngine.Random;

namespace Views
{
	public class SlotView : MonoBehaviour
	{
		public UnityAction<List<SlotTile>> OnTilesCreated;
		[SerializeField] private SlotPosition _slotPosition;
		[SerializeField] private Transform _tileParent;
		[SerializeField] private SlotTile _tilePrefab;

		private List<Vector3> _positions;
		private List<SlotTile> _tiles;
		private List<Tween> _tweenList;

		private SlotController _controller;

		#region UnityEvents

		private void Awake()
		{
			_tweenList = new List<Tween>();
			_controller = new SlotController(this, _slotPosition);

			YellManager.Instance.Listen(YellType.OnTileDataContainerFetched,
				new YellAction(this, CalculateAndCreateTiles));
		}

		private void Start()
		{
			YellManager.Instance.Yell(YellType.OnSlotCreated, new YellData(_controller));
		}

		#endregion

		private void CreateTiles()
		{
			_tiles = new List<SlotTile>();
			for (var i = 0; i < _controller.DataCount; i++)
			{
				SlotTile tile = Instantiate(_tilePrefab, _tileParent);
				tile.transform.localPosition = _positions[i];
				_tiles.Add(tile);
			}

			OnTilesCreated(_tiles);
		}

		public void Spine()
		{
			if (_tweenList.Count > 0)
			{
				_tweenList.Clear();
				ResetTiles();
			}

			foreach (var tile in _tiles)
			{
				tile.UpdateToBlurState();
			}

			_tileParent.transform.DOLocalMoveY(0, 0);
			Tween tween = _tileParent.DOLocalMoveY(-2.55f * 2, .1f)
				.SetEase(Ease.Linear);
			tween.OnComplete(() => tween.Restart());
			_tweenList.Add(tween);
		}

		public void StopSpine()
		{
			foreach (var tile in _tiles)
			{
				tile.UpdateToNormalState();
			}

			foreach (var tween in _tweenList)
			{
				tween.OnComplete(Snap);
				tween.SetAutoKill(true);
			}
		}

		private void Snap()
		{
			_tileParent.transform.DOLocalMoveY(0, 0);
			switch (_slotPosition)
			{
				case SlotPosition.Left:
				case SlotPosition.Centre:
					SlowStopAnim();
					break;
				case SlotPosition.Right:
					if (!_controller.IsFirstTwoSame)
						SlowStopAnim()
							.OnComplete(() => { YellManager.Instance.Yell(YellType.OnLastSlotAnimComplete); });
					else
					{
						if (Random.value < .5)
							NormalStopAnim()
								.OnComplete(() => { YellManager.Instance.Yell(YellType.OnLastSlotAnimComplete); });
						else
							FastStopAnim()
								.OnComplete(() => { YellManager.Instance.Yell(YellType.OnLastSlotAnimComplete); });
					}
					break;
			}
		}

		private Tween SlowStopAnim(float duration = 0, float delay = 0)
		{
			if (duration > .1f || duration <= 0) duration = .1f;
			if (delay >= .5f || delay < 0) delay = .35f;

			return _tileParent.DOLocalMoveY(-2.55f * 2, duration)
				.SetEase(Ease.OutBack)
				.SetDelay(delay);
		}

		private Tween NormalStopAnim(float delay = 0.45f)
		{
			return _tileParent.DOLocalMoveY(-2.55f * 2, 1f)
				.SetDelay(delay)
				.SetEase(Ease.OutBack);
		}


		private Tween FastStopAnim(float delay = 0.45f)
		{
			return _tileParent.DOLocalMoveY(-2.55f * 2, 2.25f)
				.SetDelay(delay)
				.SetEase(Ease.OutBack)
				.OnComplete(() => { YellManager.Instance.Yell(YellType.OnLastSlotAnimComplete); });
		}

		private void ResetTiles()
		{
			for (var i = 0; i < _tiles.Count; i++)
			{
				var tile = _tiles[i];
				tile.ResetTile();
				tile.transform.localPosition = _positions[i];
			}
		}

		private void CalculateAndCreateTiles(YellData yellData)
		{
			float firstY = (Mathf.RoundToInt(_controller.DataCount / 2f) - 1) * -2.55f;
			_positions = new List<Vector3>();

			for (int i = 0; i <= _controller.DataCount; i++)
			{
				_positions.Add(new Vector3(0, firstY + (2.55f * i)));
			}

			CreateTiles();
		}
	}
}