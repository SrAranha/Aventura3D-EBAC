using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource musicSource;
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    private void Start()
    {
        PlayRandomMusic();
    }
    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicSetup(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }
    public void PlaySFXByType(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE) return;
        var sfx = GetSFXSetup(sfxType);
        SFXPool.instance.PlayAudioFromPool(sfx.audioClip);
    }
    public MusicSetup GetMusicSetup(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }
    public SFXSetup GetSFXSetup(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }
    [NaughtyAttributes.Button]
    private void PlayRandomMusic()
    {
        var randomMusic = musicSetups[Random.Range(0, musicSetups.Count)];
        PlayMusicByType(randomMusic.musicType);
    }
}

public enum MusicType
{
    MUSIC_1,
    MUSIC_2,
    MUSIC_3
}
[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}
public enum SFXType
{
    NONE,
    COLLECTABLE,
    LIFEPACK,
    SHOOT,
    CHEST,
    CHECKPOINT,
    DESTRUCTABLE
}
[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audioClip;
}