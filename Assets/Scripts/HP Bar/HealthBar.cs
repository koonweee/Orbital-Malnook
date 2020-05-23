using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxHP(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }

    public void UpdateHP(int hp)
    {
        slider.value = hp;
    }
}
