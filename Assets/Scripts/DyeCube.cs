using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeCube : MonoBehaviour
{
    /// <summary>
    /// 桌面上的物件
    /// </summary>
    public DyeObject ObjectOnDesk;
    [SerializeField] protected Transform DeskTransform;
    private void Awake()
    {
        tag = "DyeCube";
    }

    virtual public DyeObject Take()
    {
        if (ObjectOnDesk)
        {
            DyeObject obj = ObjectOnDesk;
            ObjectOnDesk = null;
            return obj;
        }
        return null;
    }

    virtual public bool Put(DyeObject obj)
    {
        if (ObjectOnDesk == null)
        {
            ObjectOnDesk = obj;
            obj.Mounting(DeskTransform);
            return true;
        }
        ObjectOnDesk.Fusion(obj);
        return false;
    }
}
