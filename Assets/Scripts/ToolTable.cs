using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ToolTable : DyeCube
{
    [SerializeField] Transform tool;
    public int workerCount = 0;
    public bool CanWork { get { return ObjectOnDesk != null; } }
    abstract public bool Work(int PlayerId );

    public GameObject PickUpTool()
    {
        workerCount++;

        tool.gameObject.SetActive(false);
        GameObject obj = Instantiate(tool.gameObject);
        obj.SetActive(true);
        return obj;
    }

    public void PutBackTool(GameObject tool)
    {
        workerCount--;
        Destroy(tool);
        if (workerCount == 0)
            this.tool.gameObject.SetActive(true);
    }
}
