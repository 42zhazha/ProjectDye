using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum DyeType : int
{

    Cloth = 1,
    Red = 10,
    Blue = 100,
    Yellow = 1000,
    Pot,
    Plate
}

public class DyeObject : MonoBehaviour
{
    public List<LogPackage> logData = new List<LogPackage>();

    private Vector3 offset = Vector3.zero;
    [SerializeField] protected RectTransform UIRectTransform;
    [SerializeField] protected Image fillImage;

    public DyeType type;

    protected float processValue = 0;
    public bool IsProcessFinish { get { return processValue >= 1; } }

    public bool CanChop = false;
    public bool Chop(int playerId)
    {
        if (IsProcessFinish)
            return false;
        if (processValue == 0)
            logData.Add(new LogPackage(playerId, "Chop"));
        processValue += Time.deltaTime;
        return true;
    }

    public bool CanPestled = false;
    public bool Pestling(int playerId)
    {
        if (IsProcessFinish)
            return false;
        if (processValue == 0)
            logData.Add(new LogPackage(playerId, "Pestling"));
        processValue += Time.deltaTime;
        return true;
    }

    [SerializeField] new Collider collider;
    [SerializeField] new Rigidbody rigidbody;

    virtual public bool Fusion(DyeObject dye, int playerId = -1)
    {
        return false;
    }

    virtual protected void Update()
    {
        if (fillImage != null)
        {
            if (CanChop || CanPestled)
            {
                fillImage.fillAmount = processValue / 1f;
            }

            if (fillImage.fillAmount > 0 && fillImage.fillAmount < 1)
            {
                UIRectTransform.gameObject.SetActive(true);
                Vector3 target = Camera.main.transform.position;
                target.x = transform.position.x;
                UIRectTransform.LookAt(target);
            }
            else
            {
                UIRectTransform.gameObject.SetActive(false);
            }
        }
        if (tip != null && tip.activeSelf)
        {
            Vector3 target = Camera.main.transform.position;
            target.x = transform.position.x;
            tip.transform.LookAt(target);
        }
    }

    virtual protected void Awake()
    {
        tag = "DyeObject";
        collider.enabled = false;
        rigidbody.isKinematic = true;
        if (fillImage != null)
            fillImage.fillAmount = 0;

        if (CanChop || CanPestled)
            processValue = 0;
        else
            processValue = 1;
    }

    virtual public void Mounting(Transform parent)
    {
        transform.parent = parent;
        if (parent)
        {
            transform.localPosition = Vector3.zero;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            collider.enabled = false;
            rigidbody.isKinematic = true;

        }
        else
        {
            collider.enabled = true;
            rigidbody.isKinematic = false;
        }
    }

    [SerializeField] GameObject tip;
    virtual public void OnTip()
    {
        if (tip != null)
        {
            tip.SetActive(true);
            tip.transform.localScale = Vector3.one;
            tip.transform.DOKill();
            tip.transform.DOShakeScale(0.175f, 1f);
            CancelInvoke("CloseTip");
            Invoke("CloseTip", 3);
        }
    }

    virtual protected void CloseTip()
    {
        tip.SetActive(false);
    }



}
