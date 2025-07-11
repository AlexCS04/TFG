using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{
    public void ActivarObject(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void DeactivateObj(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void CambiarActivoObj(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void GoToGame(string file)
    {
        int s = PlayerPrefs.GetInt(file);
        if (s != 0) SceneManager.LoadScene("Intermidium");
        else
        {
            PlayerPrefs.SetInt(file, 1);
            SceneManager.LoadScene("Tutorial");
        }
    }
    public void DeleteSave(string file)
    {
        PlayerPrefs.SetInt(file, 0);
    }
    void Start()
    {
        AudioManager.PlayMusic(MusicTypes.menu);
    }
}
