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

        if (h == 0) Exit(lose, MusicTypes.lose);
        else if (h == -1) Exit(leave, MusicTypes.huir);
        else Exit(win, MusicTypes.win);
    }
    private void Exit(GameObject titulo, MusicTypes type)
    {
        titulo.SetActive(true);
        AudioManager.PlayMusic(type);
    }

    public void Back()
    {
        SceneManager.LoadScene("Intermidium");
    }
}