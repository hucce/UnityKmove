using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Slider slider = null;
    [SerializeField]
    float aniTime = 2;
    bool isAnimation = false;
    float targetValue = 0;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void HpUpdate(int m_currentHP, int m_maxHP)
    {
        StartCoroutine(CoHpUdate(m_currentHP, m_maxHP));
    }

    IEnumerator CoHpUdate(int m_currentHP, int m_maxHP)
    {
        targetValue = (float)m_currentHP / (float)m_maxHP;
        // ���࿡ ���� ���̶�� ��ǥ���� �ٲٰ� ������ ��ŵ�Ѵ�.
        if(isAnimation == false)
        {
            float t = 0;
            // 1. �����̵��� ���� ���� Ÿ�� �������� Ȯ��
            // 2. ���� �ִϸ��̼��� ���� ���� �ִϸ��̼� �ð� Ȯ��
            while (t < aniTime)
            {
                isAnimation = true;
                float time = t / aniTime;
                slider.value = Mathf.Lerp(slider.value, targetValue, time);
                yield return new WaitForSeconds(0.1f);
                t += 0.1f;
            }

            slider.value = targetValue;
            isAnimation = false;
        }
    }
}
