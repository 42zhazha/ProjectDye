using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipeUI : MonoBehaviour
{
    [SerializeField] Image recipeImage;
    [SerializeField] Transform resourcesTransform;
    public CuisineData cuisine { get; private set; }
    public void SetCuisineData(CuisineData data)
    {
        this.cuisine = data;
        recipeImage.sprite = Resources.Load<Sprite>("Logo/Recipe/" + this.cuisine.name);
        for (int i = 0; i < this.cuisine.formula.Length; i++)
        {
            GameObject obj = new GameObject("Resources", typeof(Image));
            obj.transform.SetParent(resourcesTransform);
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Logo/Resources/" + this.cuisine.formula[i]);
        }
    }
}
