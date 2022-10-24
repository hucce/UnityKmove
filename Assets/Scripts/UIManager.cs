using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject hpuiObj = null;
    public GameObject gameOverObj = null;

    public void HpUpdate(int currentHP, int maxHP)
    {
        hpuiObj.GetComponent<HPUI>().HpUpdate(currentHP, maxHP);
    }

    public void GameOver()
    {
        gameOverObj.SetActive(true);
    }
}
