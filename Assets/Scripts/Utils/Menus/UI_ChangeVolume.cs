using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_ChangeVolume : MonoBehaviour
{
    [Header("Audio Name")]
    [SerializeField] private string audioName;
    private TMP_Text audioNameText;
    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeExposedParameter;
    [SerializeField] private float thresholdVolume = 80f;
    [Header("Volume")]
    [SerializeField] private float minVolume = 0f;
    [SerializeField] private float maxVolume = 100f;
    [Header("Input")]
    private TMP_InputField inputField;
    private TMP_Text inputPlaceHolder;
    [Header("Slider")]
    private Slider slider;

    private float currentVolume;

    private void OnValidate()
    {
        audioNameText = GetComponentInChildren<TMP_Text>();
        audioNameText.text = audioName + " Audio";
        slider = GetComponentInChildren<Slider>();
        inputField = GetComponentInChildren<TMP_InputField>();
        inputPlaceHolder = inputField.placeholder.GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.minValue = minVolume;
        slider.maxValue = maxVolume;
        slider.value = GetCurrentVolume();
    }

    // Update is called once per frame
    void Update()
    {
        inputPlaceHolder.text = slider.value.ToString();
    }
    private float GetCurrentVolume()
    {
        audioMixer.GetFloat(volumeExposedParameter, out currentVolume);
        return currentVolume += thresholdVolume;
    }
    public void ClearText()
    {
        inputField.text = string.Empty;
    }
    public void ChangeVolumeInputToSlider(string value)
    {
        float.TryParse(value, out currentVolume);
        if (currentVolume > 100f)
        {
            currentVolume = 100f;
        }
        else if (currentVolume < 0f)
        {
            currentVolume = 0f;
        }
        slider.value = currentVolume;
        ClearText();
    }
    public void SetVolumeLevel(float volume)
    {
        audioMixer.SetFloat(volumeExposedParameter, volume - thresholdVolume);
    }
}
