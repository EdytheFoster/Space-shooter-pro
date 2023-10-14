using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Lives_Bar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;


    public void UpdateBoss_Lives_Bar(float currentValue, float maxValue)
    { 
        _slider.value = currentValue / maxValue;
    }

}
