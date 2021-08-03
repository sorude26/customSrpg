using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Custom;
/// <summary>
/// セーブデータを扱うテスト
/// </summary>
public class GameDataManager : MonoBehaviour
{
    string m_dataPath;
    private void Awake()
    {
        m_dataPath = Application.dataPath + "/JsonTest.json";
    }
    void Start()
    {
        GameData testData = new GameData();
        string test = JsonUtility.ToJson(testData);
        Debug.Log(test);
    }

    public void SaveData(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        StreamWriter writer = new StreamWriter(m_dataPath, false);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
    }
    public GameData LoadData(string dataPath)
    {
        StreamReader reader = new StreamReader(dataPath);
        string data = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<GameData>(data);
    }
}
