using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipUI : Windows
{
    [SerializeField] GameObject[] levelTips;
    public void SetLevel(int level)
    {
        levelTips[level - 1].SetActive(true);
    }
}
