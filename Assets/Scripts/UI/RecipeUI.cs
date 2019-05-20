using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipeUI : MonoBehaviour
{
    [SerializeField] Image recipeImage,headImage;
    [SerializeField] Transform resourcesTransform;
    public CuisineData cuisine { get; private set; }
    public void SetCuisineData(CuisineData data)
    {
        this.cuisine = data;
        headImage.sprite = Resources.Load<Sprite>("Logo/Recipe/" + this.cuisine.name);
        recipeImage.sprite = Resources.Load<Sprite>("Logo/Recipe/" + this.cuisine.name);

        for (int i = 0; i < this.cuisine.formula.Length; i++)
        {
            GameObject obj = new GameObject("Resources", typeof(Image),typeof(Outline));
            obj.transform.SetParent(resourcesTransform);
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Logo/Resources/" + this.cuisine.formula[i]);
        }
    }
}
