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
        SceneManager.LoadScene("Intermidium");
    }
    void Start()
    {
        AudioManager.PlayMusic(MusicTypes.menu);
    }
}
