using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const string SceneName = "Game";
    private Vector2 touchStart;
    private Vector2 touchFinish;

    public GameObject cardHolder;
    public GameObject cardPanel;

    public Text questionText;
    public Text answerText;
    public Text descriptionText;

    public static void Load()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            touchFinish = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            OnMouseMove(touchFinish - touchStart);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchFinish = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OnMouseUp(touchFinish - touchStart);
        }
    }

    private void OnMouseMove(Vector2 shift)
    {
        //Debug.LogFormat("shift {0}", shift);

        var newRot= Quaternion.identity;
        newRot.z = Mathf.Atan(-shift.x)/10;

        cardHolder.transform.localRotation = newRot;

        UpdateCardPanel(shift.x);
    }

    private void OnMouseUp(Vector2 shift)
    {
        Debug.LogFormat("UP {0}", shift.x);

        touchStart = Vector2.zero;
        touchFinish = Vector2.zero;
        UpdateCardPanel(0f);

        if (Mathf.Abs(shift.x) > 1)
        {
            cardHolder.transform.localRotation = Quaternion.identity;

            if (shift.x > 0)
            {
                Debug.LogFormat("UP LEFT {0}", shift.x);
            }
            else
            {
                Debug.LogFormat("UP RIGHT {0}", shift.x);
            }
        }
    }

    private void UpdateCardPanel(float shift)
    {
        if (Mathf.Abs(shift) < 0.2f)
        {
            cardPanel.SetActive(false);
        }
        else
        {
            cardPanel.SetActive(true);

            if (shift > 0)
            {
                answerText.alignment = TextAnchor.UpperRight;
                answerText.text = "RIGHT";
            }
            else
            {
                answerText.alignment = TextAnchor.UpperLeft;
                answerText.text = "LEFT";
            }
        }
  
    }
}
