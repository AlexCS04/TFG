using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tendero : MonoBehaviour
{
    private bool speak;
    [SerializeField] private List<string> dialogos;
    [SerializeField] private TextMeshProUGUI text;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            speak = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            speak = false;
        }
    }
    void Update()
    {
        if (speak && Input.GetKeyDown(KeyCode.F))
        {
            StopCoroutine("Hablar");
            StartCoroutine("Hablar");
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
}
