using System.Collections;
using UnityEngine;


public class Intermidium : MonoBehaviour
{
    [SerializeField] PlayerControls player;
    [SerializeField] GameObject cine;
    [SerializeField] float startCine;

    void Start()
    {
        if (PlayerPrefs.GetInt("Cine") == 0) Cine();
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
        yield return new WaitForSeconds(2.5f);
        player.enabled = true;
    }
    void Update()
    {
        if (Input.anyKey)
        {
            startCine += Time.deltaTime;
        }else startCine = Mathf.Clamp
        if (startCine >= 1.25f && !player.enabled)
        {
            StopCoroutine("Cinematica");
            cine.SetActive(false);
            player.enabled = true;
        }
    }
}
