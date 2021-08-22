using System;
using System.Collections.Generic;
using Utils;


public static class SlotMatchParser
{
	public static IEnumerable<TileType> Parse(SlotMatch match)
	{
		switch (match)
		{
			default:
			case SlotMatch.None:
				throw new ArgumentOutOfRangeException(nameof(match), match, null);
			case SlotMatch.AWildBonus:
				return new[] {TileType.A, TileType.Wild, TileType.Bonus};
			case SlotMatch.WildWildSeven:
				return new[] {TileType.Wild, TileType.Wild, TileType.Seven};
			case SlotMatch.JackpotJackpotA:
				return new[] {TileType.Jackpot, TileType.Jackpot, TileType.A};
			case SlotMatch.WildBonusA:
				return new[] {TileType.Wild, TileType.Bonus, TileType.A};
			case SlotMatch.BonusAJackpot:
				return new[] {TileType.Bonus, TileType.A, TileType.Jackpot};
			case SlotMatch.AAA:
				return new[] {TileType.A, TileType.A, TileType.A};
			case SlotMatch.BonusBonusBonus:
				return new[] {TileType.Bonus, TileType.Bonus, TileType.Bonus};
			case SlotMatch.SevenSevenSeven:
				return new[] {TileType.Seven, TileType.Seven, TileType.Seven};
			case SlotMatch.WildWildWild:
				return new[] {TileType.Wild, TileType.Wild, TileType.Wild};
			case SlotMatch.JackpotJackpotJackpot:
				return new[] {TileType.Jackpot, TileType.Jackpot, TileType.Jackpot};
		}
	}
}