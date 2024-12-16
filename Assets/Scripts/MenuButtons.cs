using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PlayButton()
    {
        GameObject.Find("Fade").GetComponent<Animator>().Play("Out");
        Invoke(nameof(LoadIntro), 1f);
    }

    void LoadIntro() => SceneManager.LoadScene("Intro");


    public void IntroButton()
    {
        GameObject.Find("Fade").GetComponent<Animator>().Play("Out");
        Invoke(nameof(LoadGame), 1f);
    }
    void LoadGame() => SceneManager.LoadScene("Game");
}