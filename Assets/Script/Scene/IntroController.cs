using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    private const string SceneName = "Intro";

    public Text titleText;
    public Button screenButton;

    public string[] TitleList;
    
    private int index;

    private void Start()
    {
        index = 0;
        titleText.text = TitleList[index];

        screenButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        index++;

        if (index < TitleList.Length)
        {
            titleText.text = TitleList[index];
        }
        else
        {
            GameController.Load();
        }
    }


    public static void Load()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}