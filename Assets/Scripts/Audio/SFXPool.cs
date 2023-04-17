using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPool : Singleton<SFXPool>
{
    public int poolAmount;
    public AudioMixerGroup sfxMixerGroup;

    private List<AudioSource> _audioSouceList;
    private int _index = 0;
    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
    }
    private void CreatePool()
    {
        _audioSouceList = new();
        while (_audioSouceList.Count < poolAmount)
        {
            GameObject newObject = new("SFXPool");
            newObject.transform.SetParent(transform);
            AudioSource audioSource = newObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = sfxMixerGroup;
            _audioSouceList.Add(newObject.GetComponent<AudioSource>());
        }
    }
    public void PlayAudioFromPool(AudioClip newClip)
    {
        _audioSouceList[_index].clip = newClip;
        _audioSouceList[_index].Play();

        _index++;
        if (_index >= _audioSouceList.Count) _index = 0;
    }
}
