using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool_ChoppingBoard : ToolTable
{
    public override string Work()
    {
        if (ObjectOnDesk.CanChop)
        {
            ObjectOnDesk.Chop();
            return "Chop";
        }
        return "";
    }
}
