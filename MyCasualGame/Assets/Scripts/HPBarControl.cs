using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarControl : MonoBehaviour
{
    public Slider hpSlider = null;
    public Gradient hpGradient = null;
    public Image fillImage = null;

    public void SetMaxHp(int maxHP)
    {
        hpSlider.maxValue = maxHP;
        hpSlider.value = maxHP;
        fillImage.color = hpGradient.Evaluate(1.0f);
    }

    public void SetHP(int HP)
    {
        hpSlider.value = HP;
        fillImage.color = hpGradient.Evaluate(hpSlider.normalizedValue);
    }
}
