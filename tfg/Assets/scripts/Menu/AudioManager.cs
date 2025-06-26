using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] public AudioSource musicAmbience;
    [SerializeField] public AudioSource musicBosses;
    [SerializeField] private MusicTypes currentAmbi=MusicTypes.none;
    #region music clips
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;
    [SerializeField] private AudioClip music3;
    [SerializeField] private AudioClip music4;
    [SerializeField] private AudioClip menu;
    [SerializeField] private AudioClip intermedium;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;
    [SerializeField] private AudioClip huir;
    [SerializeField] private AudioClip boss1;
    [SerializeField] private AudioClip boss2;
    [SerializeField] private AudioClip boss3;
    [SerializeField] private AudioClip finalBoss;
    #endregion

    [SerializeField] private List<AudioSource> soundSources;
    private int soundSourceCounter;
    #region sound clips
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;
    [SerializeField] private AudioClip sound3;
    [SerializeField] private AudioClip sword1;
    [SerializeField] private AudioClip sword2;
    [SerializeField] private AudioClip pound;
    [SerializeField] private AudioClip shoot;
    #endregion

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public static void PlaySound(EffectTypes type)
    {
        if (instance == null) return;
        switch (type)
        {
            case EffectTypes.doorOpen:
                instance.soundSources[instance.soundSourceCounter].clip = instance.sound1;
                break;
            case EffectTypes.hit:
                instance.soundSources[instance.soundSourceCounter].clip = instance.sound2;
                break;
            case EffectTypes.hitted:
                instance.soundSources[instance.soundSourceCounter].clip = instance.sound3;
                break;
            case EffectTypes.pound:
                instance.soundSources[instance.soundSourceCounter].clip = instance.pound;
                break;
            case EffectTypes.sword1:
                instance.soundSources[instance.soundSourceCounter].clip = instance.sword1;
                break;
            case EffectTypes.sword2:
                instance.soundSources[instance.soundSourceCounter].clip = instance.sword2;
                break;
            case EffectTypes.shoot:
                instance.soundSources[instance.soundSourceCounter].clip = instance.shoot;
                break;
                
                
                
        }
        instance.soundSources[instance.soundSourceCounter].Play();
        instance.soundSourceCounter += 1;
        if (instance.soundSourceCounter == instance.soundSources.Count) instance.soundSourceCounter = 0;

    }
    public static void PlayMusic(MusicTypes type)
    {
        if (instance == null) return;
        instance.musicBosses.Stop();
        instance.musicAmbience.Pause();
        if (type == instance.currentAmbi)
        {
            instance.musicAmbience.UnPause();
            return;
        }
        switch (type)
        {
            case MusicTypes.music1:
                instance.musicAmbience.clip = instance.music1;
                instance.musicAmbience.Play();
                instance.currentAmbi = type;
                break;
            case MusicTypes.music2:
                instance.musicAmbience.clip = instance.music2;
                instance.musicAmbience.Play();
                instance.currentAmbi = type;
                break;
            case MusicTypes.music3:
                instance.musicAmbience.clip = instance.music3;
                instance.musicAmbience.Play();
                instance.currentAmbi = type;
                break;
            case MusicTypes.music4:
                instance.musicAmbience.clip = instance.music4;
                instance.musicAmbience.Play();
                instance.currentAmbi = type;
                break;
            case MusicTypes.menu:
                instance.musicAmbience.clip = instance.menu;
                instance.musicAmbience.Play();
                instance.currentAmbi = type;
                break;
            case MusicTypes.intermedium:
                instance.musicAmbience.clip = instance.intermedium;
                instance.musicAmbience.Play();
                instance.currentAmbi = type;
                break;
            case MusicTypes.lose:
                instance.musicAmbience.clip = instance.lose;
                instance.musicAmbience.Play();
                instance.currentAmbi = type;
                break;
            case MusicTypes.huir:
                instance.musicAmbience.clip = instance.huir;
                instance.musicAmbience.Play();
                instance.currentAmbi = type;
                break;
            case MusicTypes.boss1:
                instance.musicBosses.clip = instance.boss1;
                instance.musicBosses.Play();
                break;
            case MusicTypes.boss2:
                instance.musicBosses.clip = instance.boss2;
                instance.musicBosses.Play();
                break;
            case MusicTypes.boss3:
                instance.musicBosses.clip = instance.boss3;
                instance.musicBosses.Play();
                break;
            case MusicTypes.finalboss:
                instance.musicBosses.clip = instance.finalBoss;
                instance.musicBosses.Play();
                break;
                

        }

        
    }

}
public enum EffectTypes
{
    doorOpen,
    hit,
    hitted, 
    shoot,
    sword1,sword2,
    pound,

    more

}
public enum MusicTypes
{
    none,
    music1,
    music2,
    music3,
    music4,

    menu,
    intermedium,
    win,
    lose,
    huir,

    boss1,
    boss2,
    boss3,
    finalboss
}
