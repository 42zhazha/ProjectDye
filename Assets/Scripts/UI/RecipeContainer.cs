using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RecipeContainer : MonoBehaviour
{
    Dictionary<string, DyeType[]> easyLevelcandidateRecipe = new Dictionary<string, DyeType[]>()
        {
            {"Yellow", new DyeType[]{ DyeType.Cloth, DyeType.Yellow} },
            {"Blue", new DyeType[]{ DyeType.Cloth, DyeType.Blue} },
        };

    Dictionary<string, DyeType[]> normalLevelcandidateRecipe = new Dictionary<string, DyeType[]>()
        {
            {"Green", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Blue } },
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
    public bool AddRecipeOrder()
    {
        if (recipes.Count <= 5)
        {
            if (Random.Range(0, 1) < normalRate)
            {
                normalRate = 0;
                CreateRecipeOrder(normalLevelcandidateRecipe);
            }
            else
            {
                normalRate += (1 / 4f);
                CreateRecipeOrder(easyLevelcandidateRecipe);
            }
            return true;
        }
        else
            return false;
    }

    void CreateRecipeOrder(Dictionary<string, DyeType[]> candidateRecipe)
    {
        var recipesData = easyLevelcandidateRecipe.ElementAt(Random.Range(0, candidateRecipe.Count));
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
