using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;

    public static UIManager instance { get { return _instance; } }

    public GameObject hpuiObj = null;
    public GameObject hpbarObj = null;
    public GameObject gameOverObj = null;
    public GameObject zombieCountObj = null;
    public GameObject stageCountObj = null;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StageCountUpdate();
    }

    public void HpUpdate(int currentHP, int maxHP)
    {
        hpuiObj.GetComponent<HPUI>().HpUpdate(currentHP, maxHP);
        hpbarObj.GetComponent<HPBar>().HpUpdate(currentHP, maxHP);
    }

    public void GameOver()
    {
        gameOverObj.SetActive(true);
        Text textObj = gameOverObj.transform.GetChild(0).GetComponent<Text>();
        textObj.text = "GAME OVER";
        textObj.color = Color.red;
    }

    public void StageClear()
    {
        gameOverObj.SetActive(true);
        Text textObj = gameOverObj.transform.GetChild(0).GetComponent<Text>();
        textObj.text = "STAGE CLEAR";
        textObj.color = Color.blue;
    }

    public void ZombieCountUpdate(int m_zombieCount)
    {
        zombieCountObj.GetComponent<Text>().text = "남은 좀비의 수 : " + m_zombieCount;
    }
    
    public void StageCountUpdate()
    {
        stageCountObj.GetComponent<Text>().text = Manager.instance.StageNumber();
    }
}
