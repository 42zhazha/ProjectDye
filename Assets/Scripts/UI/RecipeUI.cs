using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipeUI : MonoBehaviour
{
    [SerializeField] Image recipeImage, headImage;

    public CuisineData cuisine { get; private set; }
    public void SetCuisineData(CuisineData data)
    {
        this.cuisine = data;
        headImage.sprite = Resources.Load<Sprite>("Logo/Recipe/Color/" + this.cuisine.name);
        recipeImage.sprite = Resources.Load<Sprite>("Logo/Recipe/" + this.cuisine.name);
    }
}
