using System;
using UnityEngine;
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

    private string fileName = "MyFile.txt";

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        Restart();
        //WriteFile();
    }

    public void Restart()
    {
        currentCard = desk[0];
    }

    public void SetLeft()
    {
        Debug.LogFormat("UP LEFT");

        if (currentCard.id == "1")
        {
            currentCard = desk[1];
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
