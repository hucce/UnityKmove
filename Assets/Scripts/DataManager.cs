using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance = null;

    public static DataManager instance { get { return _instance; } }

    public List<int> playerDatas = new List<int>();

    private void Awake()
    {
        _instance = this;
        CsvLoad();
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Save(string _stage)
    {
        SaveData saveData = new SaveData(_stage);

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/Save.dat");

        binaryFormatter.Serialize(file, saveData);

        file.Close();
    }

    public string Load()
    {
        string currentStage = "";
        if (File.Exists(Application.persistentDataPath + "/Save.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/Save.dat", FileMode.Open);

            if (file != null && file.Length > 0)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);

                currentStage = saveData.CurrentStage();

                file.Close();
            }
        }
        return currentStage;
    }

    public void CsvLoad()
    {
        TextAsset playerData = Resources.Load<TextAsset>("Player");
        string[] pData = playerData.text.Split(new char[] { '\n' });

        for (int i =1; i< pData.Length-1; i++)
        {
            string rowData = pData[i].Replace("/r", "");
            string[] row = pData[i].Split(',');
            // 0 플레이어 HP, 1 플레이어 공격력
            playerDatas.Add(int.Parse(row[0]));
            playerDatas.Add(int.Parse(row[1]));
        }

        Debug.Log(playerDatas[0]);
    }
}