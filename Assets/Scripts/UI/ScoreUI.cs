using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    int selectButton = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool[] isConnects = GameManager.Instance.isConnects;
        for (int i = 0; i < isConnects.Length; i++)
        {
            if (isConnects[i] && Input.GetButtonDown("Player" + (i + 1).ToString() + "Button4"))
            {
                switch (selectButton)
                {
                    case 1:
                        GameManager.Instance.NextScene();
                        break;
                }
            }
        }
    }
}
