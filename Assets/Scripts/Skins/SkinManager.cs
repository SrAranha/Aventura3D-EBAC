using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinTypes
{
    SPEED,
    HEALTH
}
public class SkinManager : Singleton<SkinManager>
{
    public SkinnedMeshRenderer skinRenderer;
    public List<SkinSetup> skins;

    private readonly string _EmissionColorString = "_EmissionColor";
    private Color _defaultColor;
    [SerializeField] private SkinSetup _activeSkin;
    private void Start()
    {
        _defaultColor = skinRenderer.materials[0].GetColor(_EmissionColorString);
    }
    [NaughtyAttributes.Button]
    private void ResetSkin()
    {
        skinRenderer.materials[0].SetColor(_EmissionColorString, _defaultColor);
        _activeSkin = null;
    }
    public void ChangeSkinByType(SkinTypes skin, float duration)
    {
        ResetSkin();
        StartCoroutine(ChangeSkin(skin, duration));
    }
    IEnumerator ChangeSkin(SkinTypes skin, float duration)
    {
        var newSkin = skins.Find(i => i.skinType == skin);
        skinRenderer.materials[0].SetColor(_EmissionColorString, newSkin.newColor);
        _activeSkin = newSkin;
        yield return new WaitForSeconds(duration);
        ResetSkin();
    }
}
[System.Serializable]
public class SkinSetup
{
    public SkinTypes skinType;
    public Color newColor;
}