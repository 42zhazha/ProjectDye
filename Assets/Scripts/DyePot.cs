using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DyePot : DyeObject
{
    [SerializeField] Transform cuisinePoint;
    [SerializeField] Transform additivePoint;
    class Cuisine
    {
        public DyeObject dye;
        public float Ripening = 0;
    }
    //是否放入主材料
    public bool hasChief = false;
    public bool isCharred = false;
    public bool isCooking = true;
    public bool isCookFinish = false;
    float Endure = 0;
    float maxCookTime = 3;
    public string recipeName = "";
    List<Cuisine> cuisines = new List<Cuisine>();

    public void Clean()
    {
        recipeName = "";
        Endure = 0;
        cuisines = new List<Cuisine>();
        hasChief = false;
        isCharred = false;
        isCooking = true;
        isCookFinish = false;
        fillImage.fillAmount = 0;
        int childs = cuisinePoint.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(cuisinePoint.GetChild(0).gameObject);
        }
    }

    override protected void Update()
    {
        if (isCooking && cuisines.Count > 0)
            Cook();



        if (fillImage.fillAmount > 0 && fillImage.fillAmount < 1)
        {
            fillImage.transform.parent.gameObject.SetActive(true);
            Vector3 target = Camera.main.transform.position;
            target.x = transform.position.x;
            UIRectTransform.LookAt(target);
        }
        else
        {
            fillImage.transform.parent.gameObject.SetActive(false);
        }


    }

    public bool AddCuisine(DyeObject dye)
    {
        // if (isCharred)
        //    return false;
        if (dye.type == DyeType.Cloth && hasChief)
            return false;
        if (cuisines.Count >= 4)
            return false;
        if (dye.IsProcessFinish == false)
            return false;

        if (dye.type == DyeType.Cloth)
            hasChief = true;

        cuisines.Add(new Cuisine() { dye = dye, Ripening = 0 });
        Endure = 0;
        Destroy(dye.gameObject);
        RenderRecipe();

        GameObject obj = new GameObject(dye.type.ToString());
        obj.AddComponent<Image>().sprite = Resources.Load<Sprite>("Logo/Resources/" + dye.type.ToString());
        obj.transform.SetParent(additivePoint, false);


        return true;
    }

    void Cook()
    {
        float currectTime = 0;
        isCookFinish = true;
        for (int i = 0; i < cuisines.Count; i++)
        {
            currectTime += cuisines[i].Ripening;
            if (cuisines[i].Ripening >= 3)
                continue;
            else
            {
                cuisines[i].Ripening += Time.deltaTime;
                isCookFinish = false;
                break;
            }
        }
        cuisinePoint.Rotate(Vector3.left * Time.deltaTime);
        fillImage.fillAmount = currectTime / (cuisines.Count * 3);
        if (isCookFinish)
            Endure += Time.deltaTime;
        if (Endure >= maxCookTime)
        {
            //焦了
            isCharred = true;
        }
    }

    public override bool Fusion(DyeObject dye)
    {

        if (dye.type == DyeType.Pot)
            return false;
        if (dye.type == DyeType.Plate)
        {
            Plate plate = (dye as Plate);
            if (isCookFinish && hasChief && plate.recipeName.Equals(""))
            {
                plate.SetRecipe(recipeName);
                Clean();
            }
            return false;
        }
        return AddCuisine(dye);
    }

    void RenderRecipe()
    {
        int recipe = 0;
        for (int i = 0; i < cuisines.Count; i++)
        {
            recipe += (int)cuisines[i].dye.type;
        }
        switch (recipe)
        {
            case 1:
                recipeName = "Cloth";
                break;
            case 10:
            case 11:
                recipeName = "Red";
                break;
            case 20:
            case 21:
                recipeName = "Red_Red";
                break;
            case 100:
            case 101:
                recipeName = "Blue";
                break;
            case 110:
            case 111:
                recipeName = "Blue_Red";
                break;
            case 120:
            case 121:
                recipeName = "Blue_Red_Red";
                break;
            case 200:
            case 201:
                recipeName = "Blue_Blue";
                break;
            case 210:
            case 211:
                recipeName = "Blue_Blue_Red";
                break;
            case 1001:
            case 1000:
                recipeName = "Yellow";
                break;
            case 1010:
            case 1011:
                recipeName = "Yellow_Red";
                break;
            case 1020:
            case 1021:
                recipeName = "Yellow_Red_Red";
                break;
            case 1100:
            case 1101:
                recipeName = "Yellow_Blue";
                break;
            case 1110:
            case 1111:
                recipeName = "Yellow_Blue_Red";
                break;
            case 1200:
            case 1201:
                recipeName = "Yellow_Blue_Blue";
                break;
            case 2000:
            case 2001:
                recipeName = "Yellow_Yellow";
                break;
            case 2100:
            case 2101:
                recipeName = "Yellow_Yellow_Blue";
                break;
            case 2010:
            case 2011:
                recipeName = "Yellow_Yellow_Red";
                break;
        }
        if (recipeName != "")
        {
            int childs = cuisinePoint.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(cuisinePoint.GetChild(0).gameObject);
            }
            GameObject cuisine = Instantiate(Resources.Load<GameObject>("Prefab/Cylinder/" + recipeName), cuisinePoint);
            cuisine.transform.localPosition = Vector3.zero;
        }
    }

}
