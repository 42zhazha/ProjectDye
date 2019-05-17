using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool_Pestle : ToolTable
{
    public override bool Work(int playerId)
    {
        if (ObjectOnDesk.CanPestled && ObjectOnDesk.Pestling(playerId))
        {
            return true;
        }
        return false;
    }
}
