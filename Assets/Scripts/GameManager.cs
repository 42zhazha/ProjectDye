using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int level;
    public System.Action<int> OnAddPlayer;
    public bool[] isConnects = new bool[2] { false, false };
    public static GameManager Instance;

    private void Awake()
    {
        level = 0;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        for (int i = 0; i < isConnects.Length; i++)
        {
            if (isConnects[i] == false && Input.GetButtonDown("Player" + (i + 1).ToString() + "StartButton"))
            {
                isConnects[i] = true;
                if (OnAddPlayer != null)
                    OnAddPlayer(i + 1);
            }
        }
    }

    void OnGUI()
    {
        if (Input.anyKeyDown)
        {

            Debug.Log(Event.current.keyCode);
        }
    }



    public void NextScene()
    {
        if (level < 5)
            level += 1;
        else
            level = 0;
        if (level == 0)
            SceneManager.LoadScene("Init");
        else
        {
            SceneManager.LoadScene("Stage" + level.ToString());
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }

}
