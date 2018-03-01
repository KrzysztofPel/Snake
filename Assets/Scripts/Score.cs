using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    /// <summary>
    /// Komponet z UI do wyświtlenia aktualnego wyniku
    /// </summary>
    public Text textScore;

 
    /// <summary>
    /// Zmienna całkowita. Zmienna pomocnicza.
    /// </summary>
    private int score;

    /// <summary>
    /// Zmiena pomocnicza do wyświtlenia ciagu znakwów.
    /// </summary>
    private string finalScore;

    void Update()
    {
        score = PlayerPrefs.GetInt("Score");
        finalScore = "Score: " + score;
        textScore.text = finalScore;
    }
}
