using UnityEngine;
using UnityEngine.SceneManagement;

public class Huida : MonoBehaviour
{
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject leave;
    void Start()
    {
        int h = PlayerPrefs.GetInt("Salir");

        if (h == 0) Exit(lose);
        else if (h == -1) Exit(leave);
        else Exit(win);
    }
    private void Exit(GameObject titulo)
    {
        titulo.SetActive(true);
    }

    public void Back()
    {
        SceneManager.LoadScene("Intermidium");
    }
}