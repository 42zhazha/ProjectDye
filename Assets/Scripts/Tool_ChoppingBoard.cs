using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool_ChoppingBoard : ToolTable
{
    public override bool Work(int playerId)
    {
        
        if (ObjectOnDesk.CanChop && ObjectOnDesk.Chop(playerId))
        {
            ObjectOnDesk.Pestling(playerId);
            return true;
        }
        return false;
    }
}
