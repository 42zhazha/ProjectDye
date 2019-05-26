using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class ScoreUI : Windows
{
    int countdown = 30;
    [SerializeField] Sprite[] scoreLevelSprites;
    [SerializeField] Sprite[] scoreOSprites;

    [SerializeField] Image scoreLevel, scoreO;
    [SerializeField] Text timeText, countText, replayText;
    [SerializeField] Transform iconPoint;

    public void SetScore(float time, string[] cloths)
    {
        gameObject.SetActive(true);
        timeText.text = Mathf.FloorToInt(time / 60f).ToString() + ":" + (time % 60).ToString("00");
        countText.text = cloths.Length.ToString();
        InvokeRepeating("OnCountDown", 1, 1);
        for (int i = 0; i < cloths.Length; i++)
        {
            GameObject obj = new GameObject("", typeof(Image), typeof(Outline));
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Logo/Recipe/" + cloths[i]);
            obj.transform.SetParent(iconPoint);
        }
        float offset = time - (new float[5] { 130f, 200f, 230f, 260f, 290f }[GameManager.Instance.level - 1]);
        print(offset);
        if (offset < 5)
        {
            //A+
            scoreLevel.sprite = scoreLevelSprites[0];
            scoreO.sprite = scoreOSprites[0];
        }
        else if (offset < 10)
        {
            //A
            scoreLevel.sprite = scoreLevelSprites[0];
            scoreO.sprite = scoreOSprites[1];
        }
        else if (offset < 20)
        {
            //A-
            scoreLevel.sprite = scoreLevelSprites[0];
            scoreO.sprite = scoreOSprites[2];
        }
        else if (offset < 30)
        {
            //B+
            scoreLevel.sprite = scoreLevelSprites[1];
            scoreO.sprite = scoreOSprites[0];
        }
        else if (offset < 40)
        {
            //B
            scoreLevel.sprite = scoreLevelSprites[1];
            scoreO.sprite = scoreOSprites[1];
        }
        else if (offset < 50)
        {
            //B-
            scoreLevel.sprite = scoreLevelSprites[1];
            scoreO.sprite = scoreOSprites[2];
        }
        else if (offset < 60)
        {
            //c+
            scoreLevel.sprite = scoreLevelSprites[2];
            scoreO.sprite = scoreOSprites[0];
        }
        else if (offset < 70)
        {
            //c
            scoreLevel.sprite = scoreLevelSprites[2];
            scoreO.sprite = scoreOSprites[1];
        }
        else if (offset < 80)
        {
            //c-
            scoreLevel.sprite = scoreLevelSprites[2];
            scoreO.sprite = scoreOSprites[2];
        }
        else if (offset < 90)
        {
            //D+
            scoreLevel.sprite = scoreLevelSprites[3];
            scoreO.sprite = scoreOSprites[0];
        }
        else if (offset < 100)
        {
            //D
            scoreLevel.sprite = scoreLevelSprites[3];
            scoreO.sprite = scoreOSprites[1];
        }
        else if (offset < 110)
        {
            //D-
            scoreLevel.sprite = scoreLevelSprites[3];
            scoreO.sprite = scoreOSprites[2];
        }
        else if (offset < 120)
        {
            //E+
            scoreLevel.sprite = scoreLevelSprites[4];
            scoreO.sprite = scoreOSprites[0];
        }
        else if (offset < 130)
        {
            //E
            scoreLevel.sprite = scoreLevelSprites[4];
            scoreO.sprite = scoreOSprites[1];
        }
        else if (offset < 140)
        {
            //E-
            scoreLevel.sprite = scoreLevelSprites[4];
            scoreO.sprite = scoreOSprites[2];
        }
        else if (offset < 150)
        {
            //F+
            scoreLevel.sprite = scoreLevelSprites[5];
            scoreO.sprite = scoreOSprites[0];
        }
        else if (offset < 160)
        {
            //F
            scoreLevel.sprite = scoreLevelSprites[5];
            scoreO.sprite = scoreOSprites[1];
        }
        else
        {
            //F-
            scoreLevel.sprite = scoreLevelSprites[5];
            scoreO.sprite = scoreOSprites[2];
        }


    }

    void OnCountDown()
    {
        countdown -= 1;
        if (countdown <= 0)
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
