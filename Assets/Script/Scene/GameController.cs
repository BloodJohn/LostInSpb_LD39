using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const string SceneName = "Game";
    private Vector2 touchStart;
    private Vector2 touchFinish;

    public GameObject cardHolder;
    public SpriteRenderer cardImage;
    public GameObject cardPanel;

    public Text questionText;
    public Text answerText;
    public Text descriptionText;

    public Image happyFill;
    public Image hungerFill;
    public Image restRoomFill;
    public Image moneyFill;

    public static void Load()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }

    private void Start()
    {
        if (CoreGame.Instance==null) LoadController.Load();
        NextCrad();
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
        if (Mathf.Abs(shift.x) > 0.7f)
        {
            cardHolder.transform.localRotation = Quaternion.identity;

            if (shift.x > 0)
            {
                CoreGame.Instance.SetRight();
            }
            else
            {
                CoreGame.Instance.SetLeft();
            }

            NextCrad();
        }
        else
        {
            touchStart = Vector2.zero;
            touchFinish = Vector2.zero;
            UpdateCardPanel(0f);
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
                answerText.text = CoreGame.Instance.currentCard.AnswerRight;
            }
            else
            {
                answerText.alignment = TextAnchor.UpperLeft;
                answerText.text = CoreGame.Instance.currentCard.AnswerLeft;
            }
        }
  
    }

    private void NextCrad()
    {
        touchStart = Vector2.zero;
        touchFinish = Vector2.zero;
        UpdateCardPanel(0f);

        Debug.LogFormat("NEXT CARD {0}", CoreGame.Instance.currentCard.id);

        cardImage.sprite = CoreGame.Instance.currentCard.Image;
        descriptionText.text = CoreGame.Instance.currentCard.Description;
        questionText.text = CoreGame.Instance.currentCard.Question;

        happyFill.fillAmount = CoreGame.Instance.happy;
        hungerFill.fillAmount = CoreGame.Instance.hunger;
        restRoomFill.fillAmount = CoreGame.Instance.restRoom;
        moneyFill.fillAmount = CoreGame.Instance.money;
    }
}