using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
public class MainStory : MonoBehaviour
{
    public PlayableDirector currentDirector;

    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }
    void Update()
    {
        Debug.Log(currentDirector.time);
    }
    public void setPart(int stage)
    {
        if (stage == 0)
        {
            currentDirector.time = 0.0f;
        }
        if (stage == 1) {
            currentDirector.time = 3.0f;
        }
        else if (stage == 2)
        {
            currentDirector.time = 6.0f;
        }
        else if(stage == 3)
        {
            currentDirector.time = 11.0f;
        }
        else if (stage == 4)
        {
            currentDirector.time = 15.0f;
        }
    }

}
