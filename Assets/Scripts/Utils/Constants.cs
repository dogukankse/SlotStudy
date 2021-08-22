namespace Utils
{
	public enum SlotMachineState
	{
		None,
		Spinning,
		Stopped
	}

	public enum TileType
	{
		A,
		Bonus,
		Jackpot,
		Seven,
		Wild
	}

	public enum SlotMatch
	{
		None,
		AWildBonus,
		WildWildSeven,
		JackpotJackpotA,
		WildBonusA,
		BonusAJackpot,
		AAA,
		BonusBonusBonus,
		SevenSevenSeven,
		WildWildWild,
		JackpotJackpotJackpot
	}

	public enum SlotPosition
	{
		Left,
		Centre,
		Right
	}


	public enum StopAnimType
	{
		Slow,
		Normal,
		Fast
	}

	public enum YellType
	{
		OnSlotCreated,
		OnTileDataContainerFetched,
		OnSlotMachineCreated,
		PlayCoinParticle,
		OnFirstTwoSame,
		OnLastSlotAnimComplete
	}


	public static class Addresses
	{
		public const string TileDataContainer = "TileDataContainer";
	}

	public static class I18N
	{
		public class En
		{
			public const string Spin = "Spin";
			public const string Stop = "Stop";
		}
	}

	public static class ParticleCount
	{
		public const int JackpotCount = 200;
		public const int WildCount = 150;
		public const int SevenCount = 100;
		public const int BonusCount = 75;
		public const int ACount = 50;
	}
}