using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : DyeCube
{
    [SerializeField] DyeObject dyeResource;


    override public DyeObject Take(int playerId)
    {
        DyeObject obj = base.Take(playerId);
        if (obj == null)
        {
            obj = Instantiate(dyeResource);
            obj.name = "Dye";
            obj.logData.Add(new LogPackage(playerId, "Create" + obj.type.ToString()));
        }

        return obj;
    }


}
