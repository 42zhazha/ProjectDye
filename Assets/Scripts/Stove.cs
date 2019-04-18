using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : DyeCube
{
    public override bool Put(DyeObject obj)
    {
        if (obj.name == "Pot")
        {
            if (base.Put(obj))
            {
                ((DyePot)obj).isCooking = true;
                return true;
            }
        }
        else if (obj.name == "Dye" && ObjectOnDesk != null && ObjectOnDesk.name == "Pot")
        {
            return ((DyePot)ObjectOnDesk).AddCuisine(obj);
        }
        return false;
    }

    override public DyeObject Take()
    {
        DyeObject obj = base.Take();
        if (obj != null)
        {
            ((DyePot)obj).isCooking = false;
        }
        return obj;
    }

}
