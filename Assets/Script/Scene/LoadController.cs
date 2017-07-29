using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour
{
    private const string SceneName = "Load";

    public static void Load()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }

    private void Start()
    {
        IntroController.Load();
    }
}