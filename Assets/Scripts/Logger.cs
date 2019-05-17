using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class Logger : MonoBehaviour
{




    public static Logger Instance;

    List<List<List<LogPackage>>> logs = new List<List<List<LogPackage>>>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(List<List<LogPackage>> log)
    {
        logs.Add(log);
    }


    private void OnDestroy()
    {
        string json = "[";
        for (int i = 0; i < logs.Count; i++)
        {
            if (i != 0)
                json += ",";
            json += "[";
            for (int j = 0; j < logs[i].Count; j++)
            {
                if (j != 0)
                    json += ",";
                json += "[";
                for (int k = 0; k < logs[i][j].Count; k++)
                {

                    if (k != 0)
                        json += ",";
                    json += JsonUtility.ToJson(logs[i][j][k]);

                }
                json += "]";
            }
            json += "]";
        }
        json += "]";



        FileStream fs = new FileStream(Application.dataPath + "/" +DateTime.UtcNow.ToString("yyyyMMddhhmmss") + ".txt", FileMode.Create);

        byte[] bytes = new UTF8Encoding().GetBytes(json.ToString());
        fs.Write(bytes, 0, bytes.Length);

        fs.Close();

    }
}


[Serializable]
public class LogPackage
{
    public LogPackage(int playerID, string action)
    {
        this.playerID = playerID;
        this.action = action;
        this.time = DateTime.UtcNow.ToShortDateString();
    }
    [SerializeField]
    public int playerID;
    [SerializeField]
    public string action, time;
}
