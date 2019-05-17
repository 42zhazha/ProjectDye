using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Customer : MonoBehaviour
{
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
    }

    public void Leave()
    {
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
