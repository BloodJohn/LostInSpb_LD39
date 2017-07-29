using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    private const string SceneName = "Intro";

    public static void Load()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}
