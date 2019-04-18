using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : DyeCube
{
    [SerializeField] DyeObject dyeResource;


    override public DyeObject Take()
    {
        DyeObject obj = base.Take();
        if (obj == null)
        {
            obj = Instantiate(dyeResource);
            obj.name = "Dye";
        }

        return obj;
    }


}
