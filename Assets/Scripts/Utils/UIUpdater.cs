using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    public Image image;
    private void OnValidate()
    {
        image = GetComponent<Image>();
    }
    public void UpdateValue(float value)
    {
        image.fillAmount = value;
    }
}
