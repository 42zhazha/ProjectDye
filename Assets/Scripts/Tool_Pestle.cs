using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool_Pestle : ToolTable
{
    public override bool Work()
    {
        if (ObjectOnDesk.CanPestled && ObjectOnDesk.Pestling())
        {
            return true;
        }
        return false;
    }
}
