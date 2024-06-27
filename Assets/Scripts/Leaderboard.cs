using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.SceneManagement;


public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private TextMeshProUGUI inputScore;
    [SerializeField]
    private TextMeshProUGUI inputName;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "";

    private void Start()
    {
        publicLeaderboardKey = ScenesStatic.GetPublicKey(SceneManager.GetActiveScene().name);
        string playerName = PlayerStats.PlayerName;
        if (playerName != null)
        {
            SetLeaderboardEntry(playerName, int.Parse(inputScore.text));
        }
        
        //GetLeaderBoard();
    }
    public void GetLeaderBoard()
    {
        Debug.Log("Getting leaderboard");
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopL = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopL; ++i)
            {
                Debug.Log(msg[i].Username);

                names[i].text = (i + 1) + ".   " + msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        Debug.Log("creating new entry");
        Debug.Log(username);
        Debug.Log(score);
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            LeaderboardCreator.ResetPlayer();
            GetLeaderBoard();
            //SceneManager.LoadScene("Level 2");
        }));
    }

    public void LoadNextScene()
    {
        string nextScene = ScenesStatic.GetNextLevel(SceneManager.GetActiveScene().name);
        Debug.Log("Current scene");
        Debug.Log(SceneManager.GetActiveScene().name);
        Debug.Log("Loading scene");
        Debug.Log(nextScene);
        SceneManager.LoadScene(nextScene);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }

}
