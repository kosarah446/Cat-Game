using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public string[] levelScenes;
    public Sprite[] levelImages;
    public GameObject imageSrc;
    private int indexScene;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TMP_InputField playerName;

    // Start is called before the first frame update
    void Start()
    {
        indexScene = 0;
        if (levelImages.Length >0 && levelScenes.Length > 0)
        {
            Debug.Log("setting images");
            imageSrc.GetComponent<Image>().sprite = levelImages[indexScene];
            levelText.text = levelScenes[indexScene];
        }
    }



    // Update is called once per frame
    public void NextScene()
    {
        Debug.Log(" next");
        if (indexScene +1  == levelImages.Length)
        {
            Debug.Log("No next");
            return;
        }
        indexScene = (indexScene + 1) % levelScenes.Length;
        imageSrc.GetComponent<Image>().sprite = levelImages[indexScene];
        levelText.text = levelScenes[indexScene];
    }
    public void PrevScene()
    {
        Debug.Log("prev");
        if (indexScene == 0)
        {
            Debug.Log("No first");
            return;
        }
        indexScene = (indexScene - 1) % levelScenes.Length;
        imageSrc.GetComponent<Image>().sprite = levelImages[indexScene];
        levelText.text = levelScenes[indexScene];
    }
    public void StartScene()
    {
        Debug.Log("loading Scene");
        PlayerStats.PlayerName = playerName.text;
        if (indexScene == 0)
        {
            Debug.Log("Enters mainstory");
            SceneManager.LoadScene("MainStory", LoadSceneMode.Single);
        }
        else 
        {
            SceneManager.LoadScene(levelScenes[indexScene]);
        }
    }
}
