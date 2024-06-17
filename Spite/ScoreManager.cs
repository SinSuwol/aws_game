using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    public InputField nicknameInput;
    public Text scoreText;
    public Text leaderboardText;
    private int score = 0;
    private string apiUrl = ""; // API Gateway URL

    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public int GetScore()  // GetScore 메서드 추가
    {
        return score;
    }

    public void SubmitScore()
    {
        string nickname = nicknameInput.text;
        StartCoroutine(SendScoreToServer(nickname, score));
    }

    IEnumerator SendScoreToServer(string nickname, int score)
    {
        var json = new
        {
            nickname = nickname,
            score = score
        };

        string jsonData = JsonUtility.ToJson(json);
        UnityWebRequest request = new UnityWebRequest(apiUrl + "/score", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Score submitted successfully");
            StartCoroutine(GetLeaderboard());
        }
    }

    IEnumerator GetLeaderboard()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl + "/leaderboard");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            Leaderboard leaderboard = JsonUtility.FromJson<Leaderboard>(json);
            DisplayLeaderboard(leaderboard);
        }
    }

    void DisplayLeaderboard(Leaderboard leaderboard)
    {
        leaderboardText.text = "Leaderboard:\n";
        foreach (var entry in leaderboard.entries)
        {
            leaderboardText.text += entry.nickname + ": " + entry.score + "\n";
        }
    }
}

[System.Serializable]
public class Leaderboard
{
    public LeaderboardEntry[] entries;
}

[System.Serializable]
public class LeaderboardEntry
{
    public string nickname;
    public int score;
}
