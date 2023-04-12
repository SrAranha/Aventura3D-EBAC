using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image image;
    public void UpdateValue(float value)
    {
        image.fillAmount = value;
    }
}
