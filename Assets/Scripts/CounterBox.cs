using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterBox : DyeCube
{

    public override bool Put(DyeObject obj)
    {
        if (obj.type == DyeType.Plate && ((Plate)obj).recipeName != "")
        {
            base.Put(obj);
            GameManager.Instance.FinishOrder(((Plate)obj).recipeName);
            ((Plate)obj).Clean();

            return true;
        }
        return false;
    }
}
