using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookTutorialWindow : Windows
{

    [SerializeField] GameObject[] stageTutorials;
    public void SetLevel(int level)
    {
        stageTutorials[level - 1].SetActive(true);
    }
}
