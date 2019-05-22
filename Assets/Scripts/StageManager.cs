using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class StageManager : MonoBehaviour
{
    [SerializeField] CookTutorialWindow cookTutorialWindow;
    [SerializeField] TipUI tipUI;
    [SerializeField] GameObject[] PlayerStartUI;

    int TutorialIndex = 0;
    [SerializeField] GameObject ContollerTutorialUI;

    public static StageManager Instance;
    Queue<string> cuisinesQueue = new Queue<string>();
    [SerializeField] Text countText, maxCountText;
    [SerializeField] RecipeContainer recipeContainer;
    Customer currectCustomer;

    List<Player> players = new List<Player>();

    int coid = 0;

    private void Awake()
    {
        Instance = this;
        int level = GameManager.Instance.level;
        SetCuisines(GameManager.Instance.level);
        cookTutorialWindow.SetLevel(level);
        tipUI.SetLevel(level);
    }

    private void Update()
    {
        bool[] isConnects = GameManager.Instance.isConnects;


        if (isGameStart == false)
        {
            for (int i = 0; i < isConnects.Length; i++)
            {
                if (isConnects[i] && Input.GetButtonDown("Player" + (i + 1).ToString() + "Button4"))
                {
                    if (TutorialIndex == 0)
                    {
                        cookTutorialWindow.gameObject.SetActive(true);
                        ContollerTutorialUI.SetActive(false);
                        TutorialIndex++;
                    }
                    else
                        GameStart();
                }
            }
        }
        else
        {
            tipUI.gameObject.SetActive(Input.GetButton("PlayerSelectButton"));
            for (int i = 0; i < isConnects.Length; i++)
            {
                PlayerStartUI[i].SetActive(!isConnects[i]);
            }
        }

    }

    void SetCuisines(int level)
    {
        string[] cuisines = GetCuisines(level);
        for (int i = 0; i < cuisines.Length; i++)
            cuisinesQueue.Enqueue(cuisines[i]);
        maxCountText.text = "/" + cuisines.Length;
    }

    string[] GetCuisines(int level)
    {
        switch (level)
        {
            case 1:
                return new string[6] { "Yellow", "Yellow", "Yellow_Yellow", "Yellow_Yellow", "Yellow", "Yellow_Yellow" };
            case 2:
                return new string[8] { "Blue", "Yellow", "Yellow_Blue", "Blue_Blue", "Yellow_Yellow_Blue", "Yellow_Blue", "Yellow_Blue_Blue", "Yellow_Yellow_Blue" };
            case 3:
                return new string[10] { "Red", "Blue", "Blue_Red", "Red_Red", "Blue_Blue_Red", "Blue_Blue", "Red_Red", "Blue_Red_Red", "Blue_Red", "Blue_Blue_Red" };
            case 4:
                return new string[10] { "Yellow", "Red", "Yellow_Red", "Yellow_Red_Red", "Red_Red", "Yellow_Yellow_Red", "Yellow_Yellow", "Yellow_Red", "Yellow_Red_Red", "Yellow_Yellow_Red" };
            case 5:
                return new string[10] { "Yellow", "Red", "Blue", "Yellow_Red_Red", "Blue_Red_Red", "Yellow_Blue_Red", "Yellow_Yellow_Blue", "Yellow_Yellow_Red", "Yellow_Blue_Blue", "Yellow_Blue_Red" };
            default:
                return new string[0];
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnAddPlayer -= AddPlayer;
    }

    void AddCustomer()
    {
        if (cuisinesQueue.Count > 0)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefab/Customer"));
            currectCustomer = obj.GetComponent<Customer>();
            currectCustomer.data = cuisinesQueue.Dequeue();
            obj.transform.position = new Vector3(-7.5f, 0, 9);
        }
    }

    public void AddOrder(string name)
    {
        recipeContainer.AddRecipeOrder(name);
    }

    void AddPlayer(int id)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefab/Player" + id.ToString()));
        Player player = obj.GetComponent<Player>();
        player.PlayerId = id;
        players.Add(player);
        player.transform.position = new Vector3(id, 2, 0);
        Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(obj.transform, 1, 1, 0, new Vector2(-6f, -15f));
    }

    public void DeliveryOrder(string Recipe)
    {
        if (recipeContainer.DeliveryCuisine(Recipe))
        {
            currectCustomer.Leave();
            countText.text = (System.Convert.ToInt32(countText.text) + 1).ToString();
            if (cuisinesQueue.Count == 0)
            {
                GameEnd();
            }
            else
            {
                Invoke("AddCustomer", 1);
            }
        }
        else
        {
            //送錯菜單
        }
    }

    [SerializeField] Text timeText;
    int time = 0;



    void Tick()
    {
        time += 1;
        timeText.text = Mathf.FloorToInt(time / 60f).ToString() + ":" + (time % 60).ToString("00");
    }

    [SerializeField] GameObject[] GameUIs;
    [SerializeField] ScoreUI ScoreUI;
    [SerializeField] GameObject TutorialUI;
    bool isGameStart = false;
    void GameStart()
    {

        isGameStart = true;

        timeText.text = Mathf.FloorToInt(time / 60f).ToString() + ":" + (time % 60).ToString("00");
        InvokeRepeating("Tick", 1, 1);

        bool[] isConnects = GameManager.Instance.isConnects;
        Camera.main.GetComponent<ProCamera2D>().RemoveAllCameraTargets();
        Camera.main.GetComponent<BlurOptimized>().enabled = false;
        for (int i = 0; i < isConnects.Length; i++)
        {
            if (isConnects[i])
                AddPlayer(i + 1);
        }

        for (int i = 0; i < GameUIs.Length; i++)
        {
            GameUIs[i].SetActive(true);
        }
        TutorialUI.SetActive(false);
        GameManager.Instance.OnAddPlayer += AddPlayer;
        Invoke("AddCustomer", 2);
    }

    void GameEnd()
    {
        for (int i = 0; i < GameUIs.Length; i++)
        {
            GameUIs[i].SetActive(false);
        }
        for (int i = 0; i < players.Count; i++)
        {
            players[i].enabled = false;
        }
        ScoreUI.SetScore(time, GetCuisines(GameManager.Instance.level));
    }
}
