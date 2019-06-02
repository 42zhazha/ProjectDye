using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckScoreWindow : Windows
{

    [SerializeField] Sprite[] scoreLevelSprites, scoreOSprites;
    [SerializeField] Image[] scoreLevel, scoreO;
    protected override void OnEnable()
    {
        base.OnEnable();
        for (int i = 1; i <= 5; i++)
        {
            float offset = PlayerPrefs.GetFloat(i.ToString(), -99f);
            if (offset == -99f)
            {
                scoreLevel[i - 1].sprite = scoreLevelSprites[6];
                scoreO[i - 1].sprite = scoreOSprites[1];
                continue;
            }
            if (offset < 5)
            {
                //A+
                scoreLevel[i - 1].sprite = scoreLevelSprites[0];
                scoreO[i - 1].sprite = scoreOSprites[0];
            }
            else if (offset < 10)
            {
                //A
                scoreLevel[i - 1].sprite = scoreLevelSprites[0];
                scoreO[i - 1].sprite = scoreOSprites[1];
            }
            else if (offset < 20)
            {
                //A-
                scoreLevel[i - 1].sprite = scoreLevelSprites[0];
                scoreO[i - 1].sprite = scoreOSprites[2];
            }
            else if (offset < 30)
            {
                //B+
                scoreLevel[i - 1].sprite = scoreLevelSprites[1];
                scoreO[i - 1].sprite = scoreOSprites[0];
            }
            else if (offset < 40)
            {
                //B
                scoreLevel[i - 1].sprite = scoreLevelSprites[1];
                scoreO[i - 1].sprite = scoreOSprites[1];
            }
            else if (offset < 50)
            {
                //B-
                scoreLevel[i - 1].sprite = scoreLevelSprites[1];
                scoreO[i - 1].sprite = scoreOSprites[2];
            }
            else if (offset < 60)
            {
                //c+
                scoreLevel[i - 1].sprite = scoreLevelSprites[2];
                scoreO[i - 1].sprite = scoreOSprites[0];
            }
            else if (offset < 70)
            {
                //c
                scoreLevel[i - 1].sprite = scoreLevelSprites[2];
                scoreO[i - 1].sprite = scoreOSprites[1];
            }
            else if (offset < 80)
            {
                //c-
                scoreLevel[i - 1].sprite = scoreLevelSprites[2];
                scoreO[i - 1].sprite = scoreOSprites[2];
            }
            else if (offset < 90)
            {
                //D+
                scoreLevel[i - 1].sprite = scoreLevelSprites[3];
                scoreO[i - 1].sprite = scoreOSprites[0];
            }
            else if (offset < 100)
            {
                //D
                scoreLevel[i - 1].sprite = scoreLevelSprites[3];
                scoreO[i - 1].sprite = scoreOSprites[1];
            }
            else if (offset < 110)
            {
                //D-
                scoreLevel[i - 1].sprite = scoreLevelSprites[3];
                scoreO[i - 1].sprite = scoreOSprites[2];
            }
            else if (offset < 120)
            {
                //E+
                scoreLevel[i - 1].sprite = scoreLevelSprites[4];
                scoreO[i - 1].sprite = scoreOSprites[0];
            }
            else if (offset < 130)
            {
                //E
                scoreLevel[i - 1].sprite = scoreLevelSprites[4];
                scoreO[i - 1].sprite = scoreOSprites[1];
            }
            else if (offset < 140)
            {
                //E-
                scoreLevel[i - 1].sprite = scoreLevelSprites[4];
                scoreO[i - 1].sprite = scoreOSprites[2];
            }
            else if (offset < 150)
            {
                //F+
                scoreLevel[i - 1].sprite = scoreLevelSprites[5];
                scoreO[i - 1].sprite = scoreOSprites[0];
            }
            else if (offset < 160)
            {
                //F
                scoreLevel[i - 1].sprite = scoreLevelSprites[5];
                scoreO[i - 1].sprite = scoreOSprites[1];
            }
            else
            {
                //F-
                scoreLevel[i - 1].sprite = scoreLevelSprites[5];
                scoreO[i - 1].sprite = scoreOSprites[2];
            }
        }
    }
}
