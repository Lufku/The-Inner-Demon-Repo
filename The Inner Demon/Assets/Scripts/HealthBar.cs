using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;

    public void SetValue(float current, float max)
    {
        fill.fillAmount = current / max;
    }
}
