using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DyePot : DyeObject
{
    [SerializeField] Transform cuisinePoint;
    [SerializeField] Transform additivePoint;
    [SerializeField] GameObject smoshObject;
    class Cuisine
    {
        public DyeObject dye;
        public float Ripening = 0;
    }

    /// <summary>
    /// 是否放入主材料
    /// </summary>
    public bool hasChief = false;
    /// <summary>
    /// 是否燒焦
    /// </summary>
    public bool isCharred = false;



    public bool IsCooking = true;


    public bool isCookFinish = false;
    float Endure = 0;
    float maxCookTime = 3;
    public CuisineData CuisineData = null;
    List<Cuisine> cuisines = new List<Cuisine>();
    public bool hasCuisines { get { return cuisines.Count > 0; } }
    [SerializeField] GameObject clothPoint;

    public void Clean()
    {
        CuisineData = null;
        Endure = 0;
        cuisines = new List<Cuisine>();
        hasChief = false;
        clothPoint.SetActive(false);
        clothPoint.transform.DOKill();
        isCharred = false;
        IsCooking = true;
        isCookFinish = false;
        fillImage.fillAmount = 0;
        int childs = cuisinePoint.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(cuisinePoint.GetChild(0).gameObject);
        }
        childs = additivePoint.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(additivePoint.GetChild(0).gameObject);
        }
    }

    override protected void Update()
    {
        if (IsCooking && hasCuisines)
        {
            Cook();
            smoshObject.SetActive(true);
        }
        else
            smoshObject.SetActive(false);


        if (fillImage.fillAmount > 0 && fillImage.fillAmount < 1)
        {
            fillImage.transform.parent.gameObject.SetActive(true);

        }
        else
        {
            fillImage.transform.parent.gameObject.SetActive(false);
        }
        Vector3 target = Camera.main.transform.position;
        target.x = transform.position.x;
        UIRectTransform.LookAt(target);


    }

    /// <summary>
    /// 添加原材料
    /// </summary>
    /// <param name="dye"></param>
    /// <returns></returns>
    public bool AddCuisine(DyeObject dye)
    {
        // if (isCharred)
        //    return false;
        if (dye.type == DyeType.Cloth && hasChief)
            return false;
        if (cuisines.Count >= 4)
            return false;
        if (dye.IsProcessFinish == false)
        {
            dye.OnTip();
            return false;
        }
        if (dye.type == DyeType.Cloth)
        {
            clothPoint.SetActive(true);
            hasChief = true;
        }

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
            if (cuisines[i].Ripening >= 6)
                continue;
            else
            {
                cuisines[i].Ripening += Time.deltaTime;
                isCookFinish = false;
                break;
            }
        }
        fillImage.fillAmount = currectTime / (cuisines.Count * 6);
        clothPoint.transform.eulerAngles += new Vector3(0, 100) * Time.deltaTime;
        if (isCookFinish)
            Endure += Time.deltaTime;
        if (Endure >= maxCookTime)
        {
            //焦了
            isCharred = true;
        }
    }

    public override bool Fusion(DyeObject dye, int playerId = -1)
    {
        if (dye.type == DyeType.Pot)
            return false;
        if (dye.type == DyeType.Plate)
        {
            Plate plate = (dye as Plate);
            if (isCookFinish && hasChief && plate.data == null)
            {
                List<List<LogPackage>> logs = new List<List<LogPackage>>();
                for (int i = 0; i < cuisines.Count; i++)
                {
                    logs.Add(cuisines[i].dye.logData);
                }
                plate.SetRecipe(CuisineData, playerId, logs);
                Clean();
            }
            return false;
        }

        if (AddCuisine(dye))
        {

            if (playerId != -1)
                dye.logData.Add(new LogPackage(playerId, "Cook"));
            return true;
        }
        return false;
    }

    void RenderRecipe()
    {
        int recipe = 0;
        for (int i = 0; i < cuisines.Count; i++)
        {
            recipe += (int)cuisines[i].dye.type;
        }
        string recipeName = "";
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
        CuisineData cuisineData = CuisineData.Get(recipeName);
        if (cuisineData != null)
        {
            this.CuisineData = cuisineData;
            int childs = cuisinePoint.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(cuisinePoint.GetChild(0).gameObject);
            }
            GameObject cuisine = Instantiate(Resources.Load<GameObject>("Prefab/Cylinder/" + CuisineData.name), cuisinePoint);
            cuisine.transform.localPosition = Vector3.zero;
            cuisine.transform.localEulerAngles = Vector3.zero;
        }
    }

}
