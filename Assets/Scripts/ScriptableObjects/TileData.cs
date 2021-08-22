using UnityEngine;
using Utils;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "TileData", menuName = "Custom/Tile Data", order = 0)]
	public class TileData : ScriptableObject
	{
		public TileType Type => _type;

		public Sprite Normal => _normal;

		public Sprite Blur => _blur;


		[SerializeField] private TileType _type;
		[SerializeField] private Sprite _normal;
		[SerializeField] private Sprite _blur;
	}
}