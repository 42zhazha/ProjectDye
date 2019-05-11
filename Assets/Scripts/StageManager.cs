using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField] Text coinText;
    [SerializeField] RecipeContainer recipeContainer;
    int coid = 0;

    private void Awake()
    {
        Instance = this;
        bool[] isConnects = GameManager.Instance.isConnects;
        for (int i = 0; i < isConnects.Length; i++)
        {
            if (isConnects[i])
                AddPlayer(i + 1);
        }
        GameManager.Instance.OnAddPlayer += AddPlayer;
        SetCuisines(GameManager.Instance.level);
        Invoke("AddRecipeOrder", 2);
    }

    void SetCuisines(int level)
    {
        switch (level)
        {
            case 1:
                recipeContainer.SetCuisines(new string[10] { "Yellow", "Yellow", "Yellow_Yellow", "Yellow_Yellow", "Yellow", "Yellow", "Yellow_Yellow", "Yellow_Yellow", "Yellow", "Yellow_Yellow" });
                break;
            case 2:
                recipeContainer.SetCuisines(new string[10] { "Blue", "Yellow", "Blue", "Yellow_Blue", "Blue_Blue", "Yellow_Yellow_Blue", "Yellow_Blue", "Yellow_Blue_Blue", "Yellow_Blue", "Yellow_Yellow_Blue" });
                break;
            case 3:
                recipeContainer.SetCuisines(new string[10] { "Red", "Blue", "Blue_Red", "Red_Red", "Blue_Blue_Red", "Blue_Blue", "Red_Red", "Blue_Red_Red", "Blue_Red", "Blue_Blue_Red" });
                break;
            case 4:
                recipeContainer.SetCuisines(new string[10] { "Yellow", "Red", "Yellow_Red", "Yellow_Red_Red", "Red_Red", "Yellow_Yellow_Red", "Yellow_Yellow", "Yellow_Red", "Yellow_Red_Red", "Yellow_Yellow_Red" });
                break;
            case 5:
                recipeContainer.SetCuisines(new string[10] { "Yellow", "Red", "Blue", "Yellow_Red_Red", "Blue_Red_Red", "Yellow_Blue_Red", "Yellow_Yellow_Blue", "Yellow_Yellow_Red", "Yellow_Blue_Blue", "Yellow_Blue_Red" });
                break;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnAddPlayer -= AddPlayer;
    }

    void AddRecipeOrder()
    {
        if (recipeContainer.AddRecipeOrder())
            Invoke("AddRecipeOrder", 16);
        else
            Invoke("AddRecipeOrder", 8);
    }

    void AddPlayer(int id)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefab/Player"));
        obj.GetComponent<Player>().PlayerId = id;
        Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(obj.transform, 1, 1, 0, new Vector2(-6f, -15f));
    }

    public void DeliveryOrder(string Recipe)
    {
        if (recipeContainer.DeliveryCuisine(Recipe))
        {
            coinText.text = (coid += 50).ToString();
            if (recipeContainer.IsComplete)
            {
                GameManager.Instance.NextScene();
            }
        }
        else
        {
            //送錯菜單
        }

    }

    [SerializeField] Text timeText;
    int time = 0;
    // Use this for initialization
    void Start()
    {
        timeText.text = Mathf.FloorToInt(time / 60f).ToString() + ":" + (time % 60).ToString("00");
        InvokeRepeating("Tick", 1, 1);
    }

    void Tick()
    {
        time += 1;
        timeText.text = Mathf.FloorToInt(time / 60f).ToString() + ":" + (time % 60).ToString("00");
    }
}
