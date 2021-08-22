using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Common;
using UnityEngine;
using Utils;

namespace Managers
{
	public class SaveManager : Singleton<SaveManager>
	{
		private readonly string _savePath = Application.persistentDataPath + "/save.bin";

		public void Save(List<SlotMatch> matchList, int currentIndex)
		{
			//create data
			SaveData data = new SaveData
			{
				matchList = matchList,
				currentIndex = currentIndex
			};

			//save the data to the file
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(_savePath);
			bf.Serialize(file, data);
			file.Close();
		}

		public bool Load(out SaveData data)
		{
			data = null;
			if (!File.Exists(_savePath)) return false;
			try
			{
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.OpenRead(_savePath);
				data = (SaveData) bf.Deserialize(file);
				file.Close();
				return true;
			}
			catch (Exception)
			{
				data = null;
				return false;
			}
		}
	}
}