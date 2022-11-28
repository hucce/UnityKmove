using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;

    public static Manager instance { get { return _instance; } }

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
}
