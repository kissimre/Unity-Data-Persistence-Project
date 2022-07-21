using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    public BestScore Best { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadBestScore();
    }

    public string GetName()
    {
        return usernameInput.text;
    }

    [Serializable]
    public class BestScore
    {
        public String name;
        public int score;
    }

    public void SaveBestScore(int score)
    {
        Best = new BestScore();
        Best.name = GetName();
        Best.score = score;

        var json = JsonUtility.ToJson(Best);
        File.WriteAllText(GetSaveFileName(), json);
    }

    public void LoadBestScore()
    {
        var file = GetSaveFileName();
        if (File.Exists(file))
        {
            var json = File.ReadAllText(file);
            Best = JsonUtility.FromJson<BestScore>(json);

            bestScoreText.gameObject.SetActive(true);
            bestScoreText.text = $"Best Score : {Best.name} : {Best.score}";
        }
    }

    private string GetSaveFileName()
    {
        return Application.persistentDataPath + "/best.json";
    }

}
