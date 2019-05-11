using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterBox : DyeCube
{
    public override bool Put(DyeObject obj)
    {
        if (obj.type == DyeType.Plate)
        {
            Plate plate = obj as Plate;
            base.Put(plate);

            // 有料理
            if (plate.data != null)
            {
                StageManager.Instance.DeliveryOrder(((Plate)obj).data.name);
                ((Plate)obj).Clean();
            }

            return true;
        }
        return false;
    }
}
