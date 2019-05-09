using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : DyeCube
{
    [SerializeField] GameObject fire;
    public override bool Put(DyeObject obj)
    {
        if (obj.type == DyeType.Pot)
        {
            if (base.Put(obj))
            {
                DyePot pot = obj as DyePot;
                fire.SetActive(pot.hasCuisines);
                pot.isCooking = true;
                return true;
            }
        }
        else if (ObjectOnDesk != null && ObjectOnDesk.type == DyeType.Pot)
        {
            DyePot pot = ObjectOnDesk as DyePot;
            bool flag = pot.Fusion(obj);
            fire.SetActive(pot.hasCuisines);
            return flag;
        }
        return false;
    }

    override public DyeObject Take()
    {
        DyeObject obj = base.Take();
        if (obj != null)
        {
            ((DyePot)obj).isCooking = false;
            fire.SetActive(false);
        }
        return obj;
    }

}
