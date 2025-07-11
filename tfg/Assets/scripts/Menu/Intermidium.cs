using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Intermidium : MonoBehaviour
{
    [SerializeField] PlayerControls player;

    void Start()
    {
        AudioManager.PlayMusic(MusicTypes.intermedium);
    }
}
