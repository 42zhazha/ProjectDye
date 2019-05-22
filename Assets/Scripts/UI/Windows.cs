using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Windows : MonoBehaviour
{
    [SerializeField] GameObject window;
    protected void OnEnable()
    {
        window.transform.localScale = Vector3.one;
        window.transform.DOShakeScale(0.175f, 0.05f);
        Camera.main.GetComponent<CameraContoller>().blur.enabled = true;
    }
    protected void OnDisable()
    {
        Camera.main.GetComponent<CameraContoller>().blur.enabled = false;
    }
}
