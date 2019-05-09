using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;
public class GameManager : MonoBehaviour
{
    bool[] isConnect = new bool[2] { false, false };
    public static GameManager Instance;
    public RecipeContainer recipeContainer;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Invoke("AddRecipeOrder", 2);
        isConnect[0] = true;
        AddPlayer(1);
    }

    private void Update()
    {
        if (isConnect[1] == false && Input.GetButtonDown("Player2StartButton"))
        {
            isConnect[1] = true;
            AddPlayer(2);
        }
    }

    void AddPlayer(int id)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefab/Player"));
        obj.GetComponent<Player>().PlayerId = id;
        Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(obj.transform, 1, 1, 0, new Vector2(-6f, -15f));
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
            //送錯菜單
        }
    }
}
