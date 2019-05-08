using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : DyeObject
{
    [SerializeField] GameObject Cloth;
    public string recipeName { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        recipeName = "";
    }
    public void SetRecipe(string recipe)
    {
        recipeName = recipe;
        Cloth.SetActive(true);
        Cloth.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Prefab/Cylinder/" + recipeName);

    }

    public void Clean()
    {
        recipeName = "";
        Cloth.SetActive(false);
    }
}
