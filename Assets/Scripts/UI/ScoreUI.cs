using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class ScoreUI : Windows
{
    int countdown = 30;

    [SerializeField] Image scoreLevel;
    [SerializeField] Text timeText, countText, replayText;
    [SerializeField] Transform iconPoint;

    public void SetScore(float time, string[] cloths)
    {
        gameObject.SetActive(true);
        timeText.text = Mathf.FloorToInt(time / 60f).ToString() + ":" + (time % 60).ToString("00");
        countText.text = cloths.Length.ToString();
        for (int i = 0; i < cloths.Length; i++)
        {
            GameObject obj = new GameObject("", typeof(Image), typeof(Outline));
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Logo/Recipe/" + cloths[i]);
            obj.transform.SetParent(iconPoint);
        }
        InvokeRepeating("OnCountDown", 1, 1);
    }

    void OnCountDown()
    {
        countdown -= 1;
        if (countdown == 0)
            SceneManager.LoadScene("Init");
        else
            replayText.text = "按下2號鍵繼續(" + countdown.ToString() + ")";
    }

    void Update()
    {
        bool[] isConnects = GameManager.Instance.isConnects;
        for (int i = 0; i < isConnects.Length; i++)
        {
            if (isConnects[i] && Input.GetButtonDown("Player" + (i + 1).ToString() + "Button4"))
                GameManager.Instance.NextScene();
            else if (isConnects[i] && Input.GetButtonDown("Player" + (i + 1).ToString() + "Button3"))
                GameManager.Instance.Replay();
        }
    }
}
