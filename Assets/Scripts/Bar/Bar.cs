using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMax(int val)
    {
        slider.maxValue = val;
        slider.value = val;
    }

    public void UpdateVal(int val)
    {
        slider.value = val;
    }
}
