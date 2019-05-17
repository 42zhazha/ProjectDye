using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterBox : DyeCube
{
    public override bool Put(DyeObject obj, int playerId )
    {
        if (obj.type == DyeType.Plate)
        {
            Plate plate = obj as Plate;
            base.Put(plate, playerId);

            // 有料理
            if (plate.data != null)
            {

                plate.logData.Add(new LogPackage(playerId, "Order"));
                plate.otherLog.Add(plate.logData);
                Logger.Instance.Add(plate.otherLog);
                StageManager.Instance.DeliveryOrder(plate.data.name);
                plate.Clean();
            }

            return true;
        }
        return false;
    }
}
