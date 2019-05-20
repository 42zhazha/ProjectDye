using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour
{
    [SerializeField] Text time, count;
    [SerializeField] Transform iconPoint;
    int selectButton = 2;
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
                    case 0:

                        break;
                    case 1:
                        break;
                    case 2:
                        GameManager.Instance.NextScene();
                        break;
                }
            }
        }
    }
}
