using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private const string SceneName = "Game";

    public static void Load()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}
