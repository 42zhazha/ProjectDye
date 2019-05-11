using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InitManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        if (GameManager.Instance == null)
            new GameObject("GameManager", typeof(GameManager));
        GameManager.Instance.OnAddPlayer += AddPlayer;
    }

    void AddPlayer(int id)
    {
        GameManager.Instance.NextScene();
    }


}
