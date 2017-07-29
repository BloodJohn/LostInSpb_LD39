using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseController : MonoBehaviour
{
    private const string SceneName = "Lose";

    public static void Load()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}
