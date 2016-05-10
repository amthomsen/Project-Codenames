using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using FullInspector;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : BaseObject
{

    public static GameManager instance = null;

    public Color startColor, red, blue, grey;

    public List<string> Words;
    public GameObject cardHolder;
    public List<Button> wordCard;

	protected override void LateAwake() {
        instance = this;
	}
	
	void Start() {
        GenerateBoard();
    }

    [InspectorButton]
    public void GenerateBoard()
    {
        if (cardHolder == null)
        {
            cardHolder = GameObject.Find("CardHolder");
        }

        
        wordCard = cardHolder.GetComponentsInChildren<Button>().ToList<Button>();


        List<string> newWords = Words.OrderBy(f => Random.Range(0, 900000)).ToList<string>();
        

        for (int i = 0; i < wordCard.Count; i++)
        {
            Text[] wordCardText = wordCard[i].GetComponentsInChildren<Text>();

            foreach (Text t in wordCardText)
            {
                t.text = newWords[i];
            }
        }

        //foreach (Button x in wordCard)
        //{
        //    Text[] wordCardText = x.GetComponentsInChildren<Text>();
        //    var chosenWord = Words.GetRandomElement<string>();

        //    foreach (Text t in wordCardText)
        //    {
        //        t.text = chosenWord;
        //    }
        //}

    }

    [InspectorButton]
    public void LoadWords()
    {
        var path = EditorUtility.OpenFilePanel("Select file to load", "", "txt");

        if (path.Length != 0)
        {
            Words = System.IO.File.ReadAllLines(path).ToList<string>();

            for(int i = 0; i < Words.Count; i++)
            {
                Words[i] = Words[i].ToUpper();
            }

            //foreach (string s in Words)
            //{
            //    s = "yo";
            //}
        }
    }

}


public static class Extensions
{
    private static System.Random random = new System.Random();

    public static T GetRandomElement<T>(this IEnumerable<T> list)
    {
        // If there are no elements in the collection, return the default value of T
        if (list.Count() == 0)
            return default(T);

        return list.ElementAt(random.Next(list.Count()));
    }
}