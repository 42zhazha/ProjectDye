using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RecipeContainer : MonoBehaviour
{
    public int level = 1;
    Dictionary<string, DyeType[]> easyLevelcandidateRecipe = new Dictionary<string, DyeType[]>()
        {
            {"Yellow", new DyeType[]{ DyeType.Cloth, DyeType.Yellow} },
            {"Blue", new DyeType[]{ DyeType.Cloth, DyeType.Blue} },
            {"Red", new DyeType[]{ DyeType.Cloth, DyeType.Red} }
        };

    Dictionary<string, DyeType[]> normalLevelcandidateRecipe = new Dictionary<string, DyeType[]>()
        {
            {"Yellow_Blue", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Blue } },
            {"Yellow_Red", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Red } },
            {"Blue_Red", new DyeType[]{ DyeType.Cloth, DyeType.Blue, DyeType.Red } },
            {"Blue_Blue", new DyeType[]{ DyeType.Cloth, DyeType.Blue, DyeType.Blue } },
            {"Red_Red", new DyeType[]{ DyeType.Cloth, DyeType.Red, DyeType.Red } },
            {"Yellow_Yellow", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Yellow } }
        };
    Dictionary<string, DyeType[]> hardLevelcandidateRecipe = new Dictionary<string, DyeType[]>()
        {
            {"Yellow_Yellow_Red", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Yellow, DyeType.Red } },
            {"Yellow_Yellow_Blue", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Yellow, DyeType.Blue } },
            {"Yellow_Red_Red", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Red, DyeType.Red } },
            {"Yellow_Blue_Blue", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Blue, DyeType.Blue } },
            {"Blue_Blue_Red", new DyeType[]{ DyeType.Cloth, DyeType.Blue, DyeType.Blue, DyeType.Red } },
            {"Blue_Red_Red", new DyeType[]{ DyeType.Cloth, DyeType.Blue, DyeType.Red, DyeType.Red } },
            {"Yellow_Blue_Red", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Blue, DyeType.Red } }
        };
    List<RecipeUI> recipes = new List<RecipeUI>();
    [SerializeField] RecipeUI recipePrefab;

    public bool ContainsRecipe(string recipe)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].recipeName == recipe)
            {

                Destroy(recipes[i].gameObject);
                recipes.RemoveAt(i);
                Recast();
                return true;
            }
        }
        return false;
    }

    float normalRate = 0;
    float hardRate = 0;
    public bool AddRecipeOrder()
    {

        // 判斷等級

        if (recipes.Count <= 5)
        {
            if (Random.Range(0f, 1f) < hardRate)
            {
                hardRate = 0;
                normalRate += 0.5f;
                CreateRecipeOrder(hardLevelcandidateRecipe);
            }
            else if (Random.Range(0f, 1f) < normalRate)
            {
                hardRate += 0.1f;
                normalRate = 0;
                CreateRecipeOrder(normalLevelcandidateRecipe);
            }
            else
            {
                hardRate += 0.1f;
                normalRate += 0.5f;
                CreateRecipeOrder(easyLevelcandidateRecipe);
            }
            return true;
        }
        else
            return false;
    }

    void CreateRecipeOrder(Dictionary<string, DyeType[]> candidateRecipe)
    {
        var recipesData = candidateRecipe.ElementAt(Random.Range(0, candidateRecipe.Count));
        RecipeUI recipeUI = Instantiate(recipePrefab, transform);
        recipes.Add(recipeUI);
        recipeUI.SetRecipe(recipesData.Key, recipesData.Value);
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
