using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar; // El hijo que representa la barra

    public void SetValue(int current, int max)
    {
        if (bar == null)
            return;

        float value = (float)current / max;
        bar.localScale = new Vector3(value, 1f, 1f);
    }
}
