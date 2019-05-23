using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : DyeCube
{

    public override bool Put(DyeObject obj, int playerId)
    {
        print("Put");
        if (obj.type == DyeType.Pot)
            (obj as DyePot).Clean();
        else if (obj.type == DyeType.Plate)
            (obj as Plate).Clean();
        else
        {
            Destroy(obj.gameObject);
            return true;
        }
        return false;
    }
}
