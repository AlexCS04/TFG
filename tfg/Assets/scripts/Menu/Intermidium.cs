using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Intermidium : MonoBehaviour
{
    [SerializeField] PlayerControls player;
    [SerializeField] GameObject cine;
    [SerializeField] float startCine;
    [SerializeField] Slider skip;

    void Start()
    {
        // if (PlayerPrefs.GetInt("Cine") == 0) Cine();
        AudioManager.PlayMusic(MusicTypes.intermedium);
    }
    private void Cine()
    {
        PlayerPrefs.SetInt("Cine", 1);
        player.enabled = false;
        StartCoroutine("Cinematica");
    }
    IEnumerator Cinematica()
    {
        cine.SetActive(true);
        Debug.Log("Cine");
        yield return new WaitForSeconds(6.5f);
        Debug.Log("Time");
        player.enabled = true;
        cine.SetActive(false);

    }
    void Update()
    {
        // if (Input.anyKey)
        // {
        //     startCine += Time.deltaTime;
        //     skip.value = Mathf.InverseLerp(0, 2.25f, startCine);
        // }
        // else
        // {
        //     startCine = Mathf.Clamp(startCine - Time.deltaTime, 0, 2.25f);
        //     skip.value = Mathf.InverseLerp(0,2.25f, startCine);
        // }
        // if (startCine >= 2.25f && !player.enabled)
        // {
        //     StopCoroutine("Cinematica");
        //     Debug.Log("satsr");

        //     cine.SetActive(false);
        //     player.enabled = true;
        // }
    }
}
