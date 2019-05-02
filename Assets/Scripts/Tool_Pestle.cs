using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool_Pestle : ToolTable
{
    public override string Work()
    {
        if (ObjectOnDesk.CanPestled)
        {
            ObjectOnDesk.Pestling();
            return "Pestle";
        }
        return "";
    }
}
