using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource musicAmbience;
    [SerializeField] private AudioSource musicBosses;
    #region music clips
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;
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

    }
    public static void PlayMusic(MusicTypes type)
    {
        if (instance == null) return;
        instance.musicBosses.Stop();
        switch (type)
        {
            case MusicTypes.music1:
                instance.musicAmbience.clip = instance.music1;
                break;
            case MusicTypes.music2:
                instance.musicAmbience.clip = instance.music2;
                break;

        }

        
    }

}
public enum EffectTypes
{
    doorOpen
}
public enum MusicTypes
{
    music1,
    music2
}
