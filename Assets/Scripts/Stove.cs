using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : DyeCube
{
    [SerializeField] GameObject fire;
    public override bool Put(DyeObject obj, int playerId )
    {
        if (obj.type == DyeType.Pot)
        {
            if (base.Put(obj, playerId))
            {
                DyePot pot = obj as DyePot;
                fire.SetActive(pot.hasCuisines);
                pot.IsCooking = true;
                return true;
            }
        }
        else if (ObjectOnDesk != null && ObjectOnDesk.type == DyeType.Pot)
        {
            DyePot pot = ObjectOnDesk as DyePot;
            bool flag = pot.Fusion(obj,playerId);

            fire.SetActive(pot.hasCuisines);
            return flag;
        }
        return false;
    }

    override public DyeObject Take(int playerId )
    {
        DyeObject obj = base.Take(playerId);
        if (obj != null)
        {
            ((DyePot)obj).IsCooking = false;
            fire.SetActive(false);
        }
        return obj;
    }

}
