using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class EnterTrain : MonoBehaviour
{

    [SerializeField] private GameObject seedUI;
    [SerializeField]private string seed;
    [SerializeField]private string tSeed;
    private int randSeed = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            seedUI.SetActive(true);
            collision.gameObject.GetComponent<PlayerControls>().enabled = false;
        }
    }


    public void EnterSeed(string s)
    {
        seed = s;
        EnteringTrain();
    }
    public void TempSeed(string s)
    {
        tSeed = s;
    }
    private void EnteringTrain()
    {
        if (seed != "") randSeed = 1;
        PlayerPrefs.SetString("Seed", seed);
        PlayerPrefs.SetInt("RandomSeed", randSeed);
        SceneManager.LoadScene("Dungeon");
    }
    public void GoButton() {
        seed = tSeed;
        EnteringTrain();
    }
}
