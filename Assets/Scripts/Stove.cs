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
                if (pot.hasCuisines)
                    fire.SetActive(true);
                pot.isCooking = true;
                return true;
            }
        }
        else if (ObjectOnDesk != null && ObjectOnDesk.type == DyeType.Pot)
        {
            if (((DyePot)ObjectOnDesk).Fusion(obj))
            {
                fire.SetActive(true);
                return true;
            }
            else
                return false;
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
