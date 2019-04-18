using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ToolTable : DyeCube
{
    public bool CanWork { get { return ObjectOnDesk != null; } }
    abstract public void Work();
}
