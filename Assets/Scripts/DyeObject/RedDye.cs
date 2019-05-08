using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDye : DyeObject
{
    [SerializeField] GameObject[] renderObject;

    private void LateUpdate()
    {
        renderObject[0].SetActive(IsProcessFinish == false);
        renderObject[1].SetActive(IsProcessFinish == true);
    }
}
