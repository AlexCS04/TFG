using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Huida : MonoBehaviour
{
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject leave;
    [SerializeField] private TextMeshProUGUI score;
    void Start()
    {
        int h = PlayerPrefs.GetInt("Salir");

        if (h == -1) Exit(lose, MusicTypes.lose, 0);
        else if (h <= 050) Exit(leave, MusicTypes.huir, h);
        else Exit(win, MusicTypes.win, h);
        PlayerPrefs.SetInt("Salir", 0);
    }
    private void Exit(GameObject titulo, MusicTypes type, int s)
    {
        titulo.SetActive(true);
        AudioManager.PlayMusic(type);
        score.text = "Score:" + s.ToString();
    }

    public void Back()
    {
        SceneManager.LoadScene("Intermidium");
    }
}