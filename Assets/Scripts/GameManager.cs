using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RecipeContainer recipeContainer;
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {
        Invoke("AddRecipeOrder", 2);
    }

    void AddRecipeOrder()
    {
        if (recipeContainer.AddRecipeOrder())
            Invoke("AddRecipeOrder", 8);
    }
    [SerializeField] Text coinText;
    int coid = 0;

    public void FinishOrder(string Recipe)
    {
        if (recipeContainer.ContainsRecipe(Recipe))
        {

            coinText.text = (coid += 50).ToString();
        }
        else
        {

        }
    }
}
