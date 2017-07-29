using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    private const string SceneName = "Win";

    public static void Load()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}
