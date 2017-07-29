using UnityEngine;

public class CoreGame : MonoBehaviour
{
    public static CoreGame Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
