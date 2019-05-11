using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : DyeObject
{
    [SerializeField] GameObject Cloth;
    [SerializeField] Transform additivePoint;
    public CuisineData data { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        data = null;
    }
    public void SetRecipe(CuisineData data)
    {
        this.data = data;
        Cloth.SetActive(true);
        Cloth.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Prefab/Cylinder/" + data.name);

        for (int i = 0; i < data.formula.Length; i++)
        {
            GameObject obj = new GameObject(data.formula[i].ToString());
            obj.AddComponent<Image>().sprite = Resources.Load<Sprite>("Logo/Resources/" + data.formula[i].ToString());
            obj.transform.SetParent(additivePoint, false);
        }
    }
    override protected void Update()
    {
        Vector3 target = Camera.main.transform.position;
        target.x = transform.position.x;
        UIRectTransform.LookAt(target);
    }

    public void Clean()
    {
        data = null;
        Cloth.SetActive(false);
        int childs = additivePoint.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(additivePoint.GetChild(0).gameObject);
        }
    }
}
