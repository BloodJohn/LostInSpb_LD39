using System;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using System.IO;
#endif

public class CoreGame : MonoBehaviour
{
    public static CoreGame Instance;
    
    /// <summary>Колода ВСЕХ карт в игре</summary>
    public CardData[] desk;
    [HideInInspector]
    public CardData currentCard;

    /// <summary>уровень счастья</summary>
    public float happy = 1f;
    /// <summary>сытость</summary>
    public float hunger = 1f;
    /// <summary>здоровье</summary>
    public float restRoom = 1f;
    /// <summary>достаточно денег</summary>
    public float money = 1f;

    private string fileName = "MyFile.txt";

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        Random.InitState(DateTime.Now.Millisecond);

        Restart();
        //WriteFile();
    }

    public void Restart()
    {
        currentCard = desk[0];
        happy = Random.Range(0.7f, 1f);
        hunger = Random.Range(0.7f, 1f);
        restRoom = Random.Range(0.7f, 1f);
        money = Random.Range(0.7f, 1f);
    }

    public void SetLeft()
    {
        Debug.LogFormat("UP LEFT");

        if (currentCard.id == "1")
        {
            currentCard = desk[1];
            happy = Mathf.Max(0f, happy - 0.5f);
        }
        else
        {
            Restart();
        }
    }

    public void SetRight()
    {
        Debug.LogFormat("UP RIGHT");

        if (currentCard.id == "1")
        {
            currentCard = desk[2];
            happy = Mathf.Min(1f, happy + 0.5f);
        }
        else
        {
            Restart();
        }
    }

    private void WriteFile()
    {
#if UNITY_EDITOR
        if (File.Exists(fileName))
        {
            Debug.Log(fileName + " already exists.");
            return;
        }
        var sr = File.CreateText(fileName);
        var data = new Desk {desk = desk};
        var jsonText = JsonUtility.ToJson(data);
        sr.Write(jsonText);
        /*sr.WriteLine("This is my file.");
        sr.WriteLine("I can write ints {0} or floats {1}, and so on.", 1, 4.2);*/
        sr.Close();
#endif
    }

    [Serializable]
    class Desk
    {
        public CardData[] desk;
    }
}
