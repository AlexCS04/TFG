using UnityEngine;

public class EnterTrain : MonoBehaviour
{

    [SerializeField] private GameObject seedUI;
    private string seed;
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
    private void EnteringTrain()
    {
        if (seed != "") randSeed = 1;
        PlayerPrefs.SetString("Seed", seed);
        PlayerPrefs.SetInt("RandomSeed", randSeed);
    }
}
