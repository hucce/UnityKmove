using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;

    public static UIManager instance { get { return _instance; } }

    public GameObject hpuiObj = null;
    public GameObject hpbarObj = null;
    public GameObject gameOverObj = null;

    private void Awake()
    {
        _instance = this;
    }

    public void HpUpdate(int currentHP, int maxHP)
    {
        hpuiObj.GetComponent<HPUI>().HpUpdate(currentHP, maxHP);
        hpbarObj.GetComponent<HPBar>().HpUpdate(currentHP, maxHP);
    }

    public void GameOver()
    {
        gameOverObj.SetActive(true);
    }
}
