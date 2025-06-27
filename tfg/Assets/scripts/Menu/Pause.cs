using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]GameObject pauseuuse;
    public void OpenPause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClosePause()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void USure(GameObject uSure)
    {
        uSure.SetActive(true);
        pauseuuse.SetActive(false);
    }
    public void No(GameObject uSure)
    {
        uSure.SetActive(false);
        pauseuuse.SetActive(true);
    }
    public void NotCoward(GameObject uSure)
    {
        Time.timeScale = 1f;
        uSure.SetActive(false);
    }
    public void Exit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
    public void Retrieve()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Salir", RoomManager.instance.wagonCount);
        SceneManager.LoadScene("Huida");
    }
    public void Options(GameObject options)
    {
        if (options.activeSelf) options.SetActive(false);
        else options.SetActive(true);
    }
}
