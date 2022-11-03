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
        // 만약에 연출 중이라면 목표값만 바꾸고 연출은 스킵한다.
        if(isAnimation == false)
        {
            float t = 0;
            // 1. 슬라이드의 벨류 값과 타깃 벨류값을 확인
            // 2. 연출 애니메이션의 값과 현재 애니메이션 시간 확인
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
