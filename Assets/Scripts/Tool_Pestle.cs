using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool_Pestle : ToolTable
{
    public override void Work()
    {
        if (ObjectOnDesk.CanPestled)
        {
            ObjectOnDesk.Pestling();
        }
    }
}
