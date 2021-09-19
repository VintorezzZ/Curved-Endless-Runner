//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//public struct HighscoreEntry : System.IComparable<HighscoreEntry>
//{
//	public string name;
//	public float score;

//	public int CompareTo(HighscoreEntry other)
//	{
//		// We want to sort from highest to lowest, so inverse the comparison.
//		return other.score.CompareTo(score);
//	}
//}
///// <summary>
///// Save data for the game. This is stored locally in this case, but a "better" way to do it would be to store it on a server
///// somewhere to avoid player tampering with it. Here potentially a player could modify the binary file to add premium currency.
///// </summary>
//public class PlayerData : MonoBehaviour
//{
//    static protected PlayerData m_Instance;
//    static public PlayerData instance { get { return m_Instance; } }

//    protected string saveFile = "";

//	public List<HighscoreEntry> highscores = new List<HighscoreEntry>();

//	public string previousName = "Trash Cat";

//	public float masterVolume = float.MinValue, musicVolume = float.MinValue, masterSFXVolume = float.MinValue;

//	static int s_Version = 12;

//	// High Score management
//	public int GetScorePlace(float score)
//	{
//		HighscoreEntry entry = new HighscoreEntry();
//		entry.score = score;
//		entry.name = "";

//		int index = highscores.BinarySearch(entry);

//		return index < 0 ? (~index) : index;
//	}

//	public void InsertScore(float score, string name)
//	{
//		HighscoreEntry entry = new HighscoreEntry();
//		entry.score = score;
//		entry.name = name;

//		highscores.Insert(GetScorePlace(score), entry);

//		// Keep only the 10 best scores.
//		while (highscores.Count > 10)
//			highscores.RemoveAt(highscores.Count - 1);
//	}

//	// File management

//	static public void Create()
//	{
//		if (m_Instance == null)
//		{
//			m_Instance = new PlayerData();

//			//if we create the PlayerData, mean it's the very first call, so we use that to init the database
//			//this allow to always init the database at the earlier we can, i.e. the start screen if started normally on device
//			//or the Loadout screen if testing in editor		
//		}

//		m_Instance.saveFile = Application.persistentDataPath + "/save.bin";

//		if (File.Exists(m_Instance.saveFile))
//		{
//			// If we have a save, we read it.
//			m_Instance.Read();
//		}
//		else
//		{
//			// If not we create one with default data.
//			NewSave();
//		}		
//	}
//	static public void NewSave()
//	{		
//		m_Instance.Save();
//	}

//	public void Read()
//	{
//		BinaryReader r = new BinaryReader(new FileStream(saveFile, FileMode.Open));

//		int ver = r.ReadInt32();

//		if (ver < 6)
//		{
//			r.Close();

//			NewSave();
//			r = new BinaryReader(new FileStream(saveFile, FileMode.Open));
//			ver = r.ReadInt32();
//		}
		
//		// Added highscores.
//		if (ver >= 3)
//		{
//			highscores.Clear();
//			int count = r.ReadInt32();
//			for (int i = 0; i < count; ++i)
//			{
//				HighscoreEntry entry = new HighscoreEntry();
//				entry.name = r.ReadString();
//				entry.score = r.ReadInt32();

//				highscores.Add(entry);
//			}
//		}				

//		// Added highscore previous name used.
//		if (ver >= 7)
//		{
//			previousName = r.ReadString();
//		}

//		if (ver >= 9)
//		{
//			masterVolume = r.ReadSingle();
//			musicVolume = r.ReadSingle();
//			masterSFXVolume = r.ReadSingle();
//		}	

//		r.Close();
//	}

//	public void Save()
//	{
//		BinaryWriter w = new BinaryWriter(new FileStream(saveFile, FileMode.OpenOrCreate));

//		w.Write(s_Version);
		
//		// Write highscores.
//		w.Write(highscores.Count);
//		for (int i = 0; i < highscores.Count; ++i)
//		{
//			w.Write(highscores[i].name);
//			w.Write(highscores[i].score);
//		}
		
//		// Write name.
//		w.Write(previousName);

//		w.Write(masterVolume);
//		w.Write(musicVolume);
//		w.Write(masterSFXVolume);

//		w.Close();
//	}
//}
