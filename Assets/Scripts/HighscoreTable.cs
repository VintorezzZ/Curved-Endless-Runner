using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : SingletonBehaviour<HighScoreTable>
{
    private Transform _entryContainer;
    private Transform _entryTemplate;
    private List<Transform> _highscoreEntryTransformList;

    private void Awake() 
    {
        InitializeSingleton();
        
        _entryContainer = transform.Find("HighscoreEntryContainer");
        _entryTemplate = _entryContainer.Find("HighscoreEntryTemplate");

        _entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++) 
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++) 
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score) 
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        _highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) 
        {
            CreateHighscoreEntryTransform(highscoreEntry, _entryContainer, _highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) 
    {
        float templateHeight = 31f;
        Transform entryTransform = Instantiate(_entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) 
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

        float score = highscoreEntry.score;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString("0") + "m";

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;

        int coins = highscoreEntry.coins;
        entryTransform.Find("CoinsText").GetComponent<Text>().text = coins.ToString();

        // Set background visible odds and evens, easier to read
        entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);
        
        // Highlight First
        if (rank == 1) 
        {
            entryTransform.Find("PosText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("NameText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("CoinsText").GetComponent<Text>().color = Color.green;
        }

        //// Set tropy
        //switch (rank) 
        //{
        //    default:
        //        entryTransform.Find("trophy").gameObject.SetActive(false);
        //        break;
        //    case 1:
        //        entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("FFD200");
        //        break;
        //    case 2:
        //        entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("C6C6C6");
        //        break;
        //    case 3:
        //        entryTransform.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("B76F56");
        //        break;
        //}

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(float score, int coins, string name) 
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, coins = coins, name = name };
        
        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) 
        {
            // There's no stored table, initialize
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
            //highscores.highscoreEntryList.Clear();
        }

        //highscores.highscoreEntryList.Clear();

        bool userExist = false;
        foreach (var highscore in highscores.highscoreEntryList)
        {
            if (highscore.name == highscoreEntry.name)
            {
                userExist = true;
                if (highscore.score < highscoreEntry.score)
                {
                    highscore.score = highscoreEntry.score;
                    Debug.Log("Score updated!");
                }
                if (highscore.coins < highscoreEntry.coins)
                {
                    highscore.coins = highscoreEntry.coins;
                    Debug.Log("Coins updated!");
                }
            }
        }

        if (!userExist)
        {
            highscores.highscoreEntryList.Add(highscoreEntry);
            Debug.Log("New score added!");
        }

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores 
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] 
    private class HighscoreEntry 
    {
        public float score;
        public int coins;
        public string name;
    }
}
