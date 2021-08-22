using System;
using System.Collections.Generic;
using Utils;

public class SlotMatchProvider
{
	public static SlotMatchProvider Instance => _instance;
	private static SlotMatchProvider _instance;

	private static List<SlotMatch> _matchList;
	private static int _currentIndex;

	public static void Init(List<SlotMatch> matchList, int currentIndex)
	{
		_instance = new SlotMatchProvider();

		_matchList = matchList;
		_currentIndex = currentIndex;
	}

	public SlotMatch GetMatch(bool updateIndex = true)
	{
		SlotMatch item = _matchList[_currentIndex];
		if (updateIndex)
			_currentIndex++;
		return item;
	}

	public List<SlotMatch> GetSaveData()
	{
		return _matchList;
	}

	public int GetCurrentIndex()
	{
		return _currentIndex;
	}
}