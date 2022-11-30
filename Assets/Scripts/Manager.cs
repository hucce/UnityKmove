using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;

    public static Manager instance { get { return _instance; } }

    private string currentStage = "Stage1";

    private int zombieCount = 0;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        zombieCount = zombies.Length;
        UIManager.instance.ZombieCountUpdate(zombieCount);
    }

    public string StageNumber()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ZombieDead()
    {
        zombieCount -= 1;
        UIManager.instance.ZombieCountUpdate(zombieCount);
        // ���� äŷ�ϰ� ������� 0���ϰ� �Ǹ�
        if (zombieCount <=0)
        {
            zombieCount = 0;
            UIManager.instance.ZombieCountUpdate(zombieCount);
            // Ŭ���� UI�� ǥ���ϰ� ���� �ð� �Ŀ� ���� ���������� �ε�
            StartCoroutine(CoClear());
        }
    }

    IEnumerator CoClear()
    {
        // �״� ����� ����� ���ö����� ��ٸ���
        yield return new WaitForSeconds(3f);

        // ���� Ŭ���� UI ���
        UIManager.instance.StageClear();

        // ����ϰ�
        yield return new WaitForSeconds(10f);

        // ���� ���������� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Save()
    {
        SaveData saveData = new SaveData(StageNumber());

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/Save.dat");

        binaryFormatter.Serialize(file, saveData);

        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/Save.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/Save.dat", FileMode.Open);

            if(file != null && file.Length > 0)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);

                currentStage = saveData.CurrentStage();

                file.Close();
            }
        }
    }
}
