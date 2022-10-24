using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public void HpUpdate(int m_currentHP, int m_maxHP)
    {
        Text hpui = GetComponent<Text>();
        //GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        hpui.text = m_currentHP + "/" + m_maxHP;
    }
}
