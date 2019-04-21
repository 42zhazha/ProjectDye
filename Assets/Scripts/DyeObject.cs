using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] protected RectTransform UIRectTransform;
    [SerializeField] protected Image fillImage;


    public DyeType type;
    public bool CanChop = false;
    float chopValue = 0;
    public bool ChopFinish { get { return chopValue >= 1; } }
    public void Chop()
    {
        if (ChopFinish)
            return;
        chopValue += Time.deltaTime;
    }

    [SerializeField] new Collider collider;
    [SerializeField] new Rigidbody rigidbody;

    virtual public bool Fusion(DyeObject dye)
    {
        return false;
    }

    virtual protected void Update()
    {
        if (fillImage != null)
        {
            if (CanChop)
            {
                fillImage.fillAmount = chopValue / 1f;
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
    }

    virtual protected void Awake()
    {
        tag = "DyeObject";
        collider.enabled = false;
        rigidbody.isKinematic = true;
        if (fillImage != null)
            fillImage.fillAmount = 0;
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
}
