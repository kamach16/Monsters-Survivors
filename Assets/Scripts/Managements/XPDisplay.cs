using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPDisplay : MonoBehaviour
{
    [SerializeField] private Slider xpSlider;

    public void UpdateXP(float currentXP, float xpToLevelUp) // execute when enemy is killed, xp value is changing
    {
        xpSlider.maxValue = xpToLevelUp;
        xpSlider.value = currentXP;
    }
}
