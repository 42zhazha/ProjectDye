using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] RectTransform canvas;
    [SerializeField] GameObject happyUI, angre;
    [SerializeField] Transform tipPoint;
    [SerializeField] SpriteRenderer spriteRenderer;
    public string data;
    public Animator animator;

    private void Start()
    {
        animator.SetBool("IsWalk", true);
        transform.DOMove(new Vector3(-7.5f, 0, -0.5f), 2.5F).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOLocalRotate(new Vector3(0, -90f, 0), 0.5f).OnComplete(() =>
            {
                animator.SetBool("IsWalk", false);
                StageManager.Instance.AddOrder(data);
            });
        });
        spriteRenderer.sprite = Resources.Load<Sprite>("Canvas/" + data);
    }

    private void Update()
    {
        if (canvas.gameObject.activeSelf)
        {
            Vector3 target = Camera.main.transform.position;
            target.x = canvas.position.x;
            canvas.LookAt(target);
        }
    }

    public void OnTip()
    {
        canvas.gameObject.SetActive(true);
        angre.SetActive(true);
        angre.transform.localScale = Vector3.one;
        angre.transform.DOKill();
        angre.transform.DOShakeScale(0.175f, 1f);
        int childs = tipPoint.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(tipPoint.GetChild(0).gameObject);
        }
        CuisineData cuisineData = CuisineData.Get(data);
        for (int i = 0; i < cuisineData.formula.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.AddComponent<Image>().sprite = Resources.Load<Sprite>("Logo/Resources/" + cuisineData.formula[i].ToString());
            obj.transform.SetParent(tipPoint, false);
        }
    }

    public void Leave()
    {
        canvas.gameObject.SetActive(true);
        happyUI.SetActive(true);
        happyUI.transform.localScale = Vector3.one;
        happyUI.transform.DOKill();
        happyUI.transform.DOShakeScale(0.175f, 1f);
        angre.SetActive(false);
        animator.SetBool("IsWalk", true);
        transform.DOLocalRotate(new Vector3(0, -0, 0), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {

            transform.DOMove(new Vector3(-7.5f, 0, -17.5f), 4).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
}
