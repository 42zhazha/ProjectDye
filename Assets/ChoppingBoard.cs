using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoppingBoard : ToolTable
{

    public override void Work()
    {
        if (ObjectOnDesk.CanChop)
        {
            ObjectOnDesk.Chop();
        }
    }
}
