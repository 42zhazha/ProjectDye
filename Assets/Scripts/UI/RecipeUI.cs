using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipeUI : MonoBehaviour
{
    [SerializeField] Image recipeImage;
    [SerializeField] Transform resourcesTransform;
    public string recipeName { get; private set; }
    DyeType[] dyeTypes;
    public void SetRecipe(string recipeName, DyeType[] dyeTypes)
    {
        this.recipeName = recipeName;
        this.dyeTypes = dyeTypes;
        recipeImage.sprite = Resources.Load<Sprite>("Logo/Recipe/" + recipeName);
        for (int i = 0; i < dyeTypes.Length; i++)
        {
            GameObject obj = new GameObject("Resources", typeof(Image));
            obj.transform.SetParent(resourcesTransform);
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Logo/Resources/" + dyeTypes[i]);
        }
    }
}
