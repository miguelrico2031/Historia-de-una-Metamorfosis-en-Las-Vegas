
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeEnd : MonoBehaviour
{
    public void OnFadeEnd()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game Over");

    }
}
