using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RecipeContainer : MonoBehaviour
{


    Dictionary<string, DyeType[]> candidateRecipe = new Dictionary<string, DyeType[]>()
        {
            {"Yellow", new DyeType[]{ DyeType.Cloth, DyeType.Yellow} },
            {"Green", new DyeType[]{ DyeType.Cloth, DyeType.Yellow, DyeType.Blue } },
            {"Blue", new DyeType[]{ DyeType.Cloth, DyeType.Blue} },
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

    public bool AddRecipeOrder()
    {
        if (recipes.Count <= 5)
        {
            RecipeUI recipeUI = Instantiate(recipePrefab, transform);
            recipes.Add(recipeUI);
            var recipesData = candidateRecipe.ElementAt(Random.Range(0, candidateRecipe.Count));
            recipeUI.SetRecipe(recipesData.Key, recipesData.Value);
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
