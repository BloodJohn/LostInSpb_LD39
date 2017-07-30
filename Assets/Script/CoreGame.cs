using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/*#if UNITY_EDITOR
using System.IO;
#endif*/

public class CoreGame : MonoBehaviour
{
    public static CoreGame Instance;

    /// <summary>Колода ВСЕХ карт в игре</summary>
    public CardData[] desk;
    [HideInInspector]
    public CardData currentCard;
    private readonly List<string> dropList = new List<string>(6);

    /// <summary>уровень счастья</summary>
    public float happy = 1f;
    /// <summary>сытость</summary>
    public float hunger = 1f;
    /// <summary>здоровье</summary>
    public float restRoom = 1f;
    /// <summary>достаточно денег</summary>
    public float money = 1f;

    private const string fileName = "MyFile.txt";
    private const string hipster1 = "hipster1";
    private const string hipster1left = "hipster1left";
    private const string hipster1right = "hipster1right";
    private const string grandma1 = "grandma1";
    private const string grandma1left = "grandma1left";
    private const string grandma1right = "grandma1right";
    private const string win = "win";
    private const string lose = "lose";

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        Random.InitState(DateTime.Now.Millisecond);

        Restart();
        //WriteFile();
    }

    private void Restart()
    {
        happy = Random.Range(0.5f, 0.7f);
        hunger = Random.Range(0.7f, 1f);
        restRoom = Random.Range(0.7f, 1f);
        money = Random.Range(0.1f, 0.3f);

        dropList.Clear();
        dropList.Add(hipster1);
        dropList.Add(grandma1);

        DropCard();
    }

    private void SetCardById(string id)
    {
        foreach (var card in desk)
            if (card.id == id)
            {
                currentCard = card;
                return;
            }
    }

    private void DropCard()
    {
        if (dropList.Count > 0)
        {
            var index = Random.Range(0, dropList.Count);
            SetCardById(dropList[index]);
            dropList.RemoveAt(index);
        }
        else
        {
            SetCardById(win);
        }
    }

    public void SetLeft()
    {
        if (currentCard.id == hipster1) //Спроси у хипстера
        {
            //Где здесь MacDoladls?
            happy = Mathf.Max(0f, happy - 0.1f);
            hunger = Mathf.Max(0f, hunger - 0.1f);
            SetCardById(hipster1left);
        }
        else if (currentCard.id == hipster1right) //Банкомат недалеко. Я провожу тебя.
        {
            //Спасибо!
            happy = Mathf.Min(1f, happy + 0.2f);
            money = Mathf.Min(1f, money + 0.5f);
            DropCard();
        }
        else if (currentCard.id == hipster1left) //Нездоровая пища! Фалафель и смузи - наше все!
        {
            //Я вырос на ней
            happy = Mathf.Max(0f, happy - 0.1f);
            DropCard();
        }
        else if (currentCard.id == grandma1) //Спроси у бабушки
        {
            //Аэропорт?
            happy = Mathf.Min(1f, happy + 0.1f);
            SetCardById(grandma1left);
        }
        else if (currentCard.id == grandma1left) //Пулково? Это тебе на метро надо. ст Московская.
        {
            //Спасибо бабушка!
            happy = Mathf.Min(1f, happy + 0.2f);
            DropCard();
        }
        else if (currentCard.id == grandma1right) //Не понимаю я. Спроси у другого.
        {
            //Бесполезно
            happy = Mathf.Max(0f, happy - 0.2f);
            DropCard();
        }
        else
        {
            Restart();
        }
    }

    public void SetRight()
    {
        if (currentCard.id == hipster1) //Спроси у хипстера
        {
            //Как обналичить кредитку?
            happy = Mathf.Min(1f, happy + 0.1f);
            SetCardById(hipster1right);
        }
        else if (currentCard.id == hipster1right) //Банкомат недалеко. Я провожу тебя.
        {
            //Я передумал
            happy = Mathf.Max(0f, happy - 0.1f);
            DropCard();
        }
        else if (currentCard.id == hipster1left) //Нездоровая пища! Фалафель и смузи - наше все!
        {
            //Ненавижу смузи
            happy = Mathf.Max(0f, happy - 0.2f);
            DropCard();
        }
        else if (currentCard.id == grandma1) //Спроси у бабушки
        {
            //Как обналичить кредитку?
            happy = Mathf.Min(1f, happy + 0.3f);
            SetCardById(grandma1right);
        }
        else if (currentCard.id == grandma1left) //Пулково? Это тебе на метро надо. ст Московская.
        {
            //Метро? Не понимаю.
            happy = Mathf.Min(1f, happy + 0.1f);
            DropCard();
        }
        else if (currentCard.id == grandma1right) //Не понимаю я. Спроси у другого.
        {
            //Извините
            happy = Mathf.Max(0f, happy - 0.1f);
            DropCard();
        }
        else
        {
            Restart();
        }
    }

    /*private void WriteFile()
    {
#if UNITY_EDITOR
        if (File.Exists(fileName))
        {
            Debug.Log(fileName + " already exists.");
            return;
        }
        var sr = File.CreateText(fileName);
        var data = new Desk { desk = desk };
        var jsonText = JsonUtility.ToJson(data);
        sr.Write(jsonText);
        sr.Close();
#endif
    }

    [Serializable]
    class Desk
    {
        public CardData[] desk;
    }*/
}
