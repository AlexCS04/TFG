using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tendero : MonoBehaviour, IInteractable
{
    private bool speak;
    [SerializeField] private List<string> dialogos;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject highLight;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            speak = true;
            highLight.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            speak = false;
            highLight.SetActive(false);
        }
    }

    IEnumerator Hablar()
    {
        text.text = "";
        yield return null;
        text.text = dialogos[Random.Range(0, dialogos.Count)];
        yield return new WaitForSeconds(3.3f);
        text.text = "";
    }

    public bool Interact()
    {
        StopCoroutine("Hablar");
        StartCoroutine("Hablar");
        return false;
    }
}
