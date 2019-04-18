using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeObject : MonoBehaviour
{
    public int id;
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

    virtual protected void Awake()
    {
        tag = "DyeObject";
        collider.enabled = false;
        rigidbody.isKinematic = true;
    }

    virtual public void Mounting(Transform parent)
    {
        transform.parent = parent;
        if (parent)
        {
            transform.localPosition = Vector3.zero;
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
