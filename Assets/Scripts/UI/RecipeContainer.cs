using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RecipeContainer : MonoBehaviour
{
    public bool IsComplete { get { return recipes.Count == 0 && cuisinesQueue.Count == 0; } }
    Queue<string> cuisinesQueue = new Queue<string>();
    List<RecipeUI> recipes = new List<RecipeUI>();
    [SerializeField] RecipeUI recipePrefab;

    public void SetCuisines(string[] cuisines)
    {
        for (int i = 0; i < cuisines.Length; i++)
            cuisinesQueue.Enqueue(cuisines[i]);
    }

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

    public bool AddRecipeOrder()
    {
        if (recipes.Count <= 3 && cuisinesQueue.Count > 0)
        {
            CuisineData data = CuisineData.Get(cuisinesQueue.Dequeue());

            if (data == null)
                return false;

            RecipeUI recipeUI = Instantiate(recipePrefab, transform);
            recipes.Add(recipeUI);
            recipeUI.SetCuisineData(data);
            recipeUI.transform.localPosition = new Vector3(1400, 0);
            Recast();
            return true;
        }
        else
            return false;
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