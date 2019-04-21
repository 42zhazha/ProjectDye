using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : DyeObject
{
    [SerializeField] Transform recipePoint;
    public string recipeName { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        recipeName = "";
    }
    public void SetRecipe(string recipe)
    {
        recipeName = recipe;
        if (recipeName != "")
        {
            GameObject cuisine = Instantiate(Resources.Load<GameObject>("Prefab/Cylinder/" + recipeName), recipePoint);
            cuisine.transform.localPosition = Vector3.zero;
        }
    }

    public void Clean()
    {
        recipeName = "";
        int childs = recipePoint.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(recipePoint.GetChild(0).gameObject);
        }
    }
}
