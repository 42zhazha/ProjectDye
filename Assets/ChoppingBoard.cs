using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoard : ToolTable
{
    public override void Work()
    {

        if (ObjectOnDesk.CanChop)
        {
            print("Chop");
            ObjectOnDesk.Chop();
        }
    }
}
