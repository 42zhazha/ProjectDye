using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RecipeContainer : MonoBehaviour
{
    List<RecipeUI> recipes = new List<RecipeUI>();
    [SerializeField] RecipeUI recipePrefab;


    public bool DeliveryCuisine(string cuisineName)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].cuisine.name == cuisineName)
            {
                Destroy(recipes[i].gameObject);
                recipes.RemoveAt(i);
                Recast();



                return true;
            }
        }
        return false;
    }



    public void AddRecipeOrder(string name)
    {
        CuisineData data = CuisineData.Get(name);

        RecipeUI recipeUI = Instantiate(recipePrefab, transform);
        recipes.Add(recipeUI);
        recipeUI.SetCuisineData(data);
        recipeUI.transform.localPosition = new Vector3(1400, 0);
        Recast();
    }



    void Recast()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            recipes[i].transform.DOLocalMoveX(i * 140, 1f).SetEase(Ease.OutExpo);
        }
    }
}

public class CuisineData
{
    public string name;
    public DyeType[] formula;

    private CuisineData(string name, DyeType[] formula)
    {
        this.name = name;
        this.formula = formula;
    }

    static Dictionary<string, DyeType[]> formulaDict = new Dictionary<string, DyeType[]>(){
                    {"Cloth", new DyeType[]{ DyeType.Cloth} },
            {"Yellow", new DyeType[]{ DyeType.Cloth, DyeType.Yellow} },
            {"Blue", new DyeType[]{ DyeType.Cloth, DyeType.Blue} },
            {"Red", new DyeType[]{ DyeType.Cloth, DyeType.Red} },
            {"Yellow_Blue", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Blue } },
            {"Yellow_Red", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Red } },
            {"Blue_Red", new DyeType[]{ DyeType.Cloth, DyeType.Blue, DyeType.Red } },
            {"Blue_Blue", new DyeType[]{ DyeType.Cloth, DyeType.Blue, DyeType.Blue } },
            {"Red_Red", new DyeType[]{ DyeType.Cloth, DyeType.Red, DyeType.Red } },
            {"Yellow_Yellow", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Yellow } },
            {"Yellow_Yellow_Red", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Yellow, DyeType.Red } },
            {"Yellow_Yellow_Blue", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Yellow, DyeType.Blue } },
            {"Yellow_Red_Red", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Red, DyeType.Red } },
            {"Yellow_Blue_Blue", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Blue, DyeType.Blue } },
            {"Blue_Blue_Red", new DyeType[]{ DyeType.Cloth, DyeType.Blue, DyeType.Blue, DyeType.Red } },
            {"Blue_Red_Red", new DyeType[]{ DyeType.Cloth, DyeType.Blue, DyeType.Red, DyeType.Red } },
            {"Yellow_Blue_Red", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Blue, DyeType.Red } }
    };

    static public CuisineData Get(string name)
    {
        if (formulaDict.ContainsKey(name))
        {
            return new CuisineData(name, formulaDict[name]);
        }
        else
            return null;
    }
}