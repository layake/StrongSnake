using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public List<Transform> highScoreEntryTransforList;



    private void Awake()
    {
        entryContainer = transform.GetChild(6);
        entryTemplate = entryContainer.GetChild(0);
        
        entryTemplate.gameObject.SetActive(false);

        //addHighscoreEntry(10000, "GMK");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScore highScore = JsonUtility.FromJson<HighScore>(jsonString);
        
        //sort entry list by score
        for (int i = 0; i < highScore.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1 ; j < highScore.highScoreEntryList.Count; j++)
            {
                if (highScore.highScoreEntryList[j].score > highScore.highScoreEntryList[i].score)
                {
                    //swap
                    HighScoreEntry tmp = highScore.highScoreEntryList[i];
                    highScore.highScoreEntryList[i] = highScore.highScoreEntryList[j];
                    highScore.highScoreEntryList[j] = tmp;
                }
            }
        }
        
        highScoreEntryTransforList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highScore.highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransforList);
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformsList)
    {
        float templateHeight = 20f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, (-templateHeight - 15) * transformsList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformsList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "th"; break;
            case 1:
                rankString = "1st"; break;
            case 2:
                rankString = "2nd"; break;
            case 3:
                rankString = "3rd"; break;
                
        }
        entryTransform.Find("postext").GetComponent<Text>().text = rankString;
        int score = highScoreEntry.score;
            
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
        string name = highScoreEntry.name;
            
        entryTransform.Find("NameText").GetComponent<Text>().text = name;
        
        transformsList.Add(entryTransform);
    }
    
    private class HighScore
    {
        public List<HighScoreEntry> highScoreEntryList = new List<HighScoreEntry>();
    }

    public void addHighscoreEntry(int score, string name)
    {
        //create highscoreentry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };
        
        //load saved highscore
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScore highScore = JsonUtility.FromJson<HighScore>(jsonString);

        //add new entry to Highscore
        highScore.highScoreEntryList.Add(highScoreEntry);
        
        //saved update Highscores
        string json =  JsonUtility.ToJson(highScore);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    
    [System.Serializable]
    public class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
