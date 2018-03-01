using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    /// <summary>
    /// Aktualny kierunek porszania się obietów 
    /// </summary>
    Vector2 currentDirection = Vector2.up;

    /// <summary>
    /// Kierunek względem którego obiekt ma się przemieszczać
    /// </summary>
    Vector2 nextDir = Vector2.up;

    /// <summary>
    /// Zakres prędkości poruszania sie węża
    /// </summary>
    [Range(0.1f, 0.9f)]
    public float speedSnake = 0.5f;

    /// <summary>
    /// Obiekt reprezentujący część składową ogona
    /// </summary>
    public GameObject tailPrefab;

    /// <summary>
    /// Lista pozycji dla poszczególnych czesci ogona
    /// </summary>
    List<Transform> tail = new List<Transform>();

    /// <summary>
    /// Zmienna całkowita pomocnicza służy do zliczania punktów
    /// </summary>
    int score;


    /// <summary>
    /// Flaga ustwiana podczas kontaktu z obiektem Food
    /// </summary>
    bool ate = false;


    void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("Score", score);

        //Metoda odpowiadająca za wyowałonie powtrzenie metody "Movment" w odstępie okreslonego czasu 
        InvokeRepeating("Movement", 0.3f, speedSnake);
        SetInitialTail();
    }


    void Update()
    {
        HandleDir();
    }

    /// <summary>
    /// Odpowiada za przesuniecie obiektów
    /// </summary>
    void Movement()
    { 
        currentDirection = nextDir;
        Vector2 v = transform.position;
        transform.Translate(currentDirection);

        if (ate)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            tail.Insert(0, g.transform);
            ate = false;

        }
        else if (tail.Count > 0)
        {
            tail.Last().position = v;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    /// <summary>
    ///Uruchomienie wyzwalacza podczas kolizji obiketów 
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name.StartsWith("NormalFood"))
        {
            score = PlayerPrefs.GetInt("Score");
            score++;
            PlayerPrefs.SetInt("Score", score);
            ate = true;
            Destroy(coll.gameObject);
        }
        else if (coll.name.StartsWith("SpecialFood"))
        {
            score = PlayerPrefs.GetInt("Score");
            score += 10;
            PlayerPrefs.SetInt("Score", score);
            ate = true;
            Destroy(coll.gameObject);
        }
        else
        {
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("GameOver");
        }
    }

    /// <summary>
    /// Ustawienie początkowej wilekości ogona
    /// </summary>
    void SetInitialTail()
    {
        for (int i = 0; i <= 3; i++)
        {
            Vector2 v = transform.position;
            transform.Translate(currentDirection);
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
            tail.Insert(0, g.transform);
        }
    }
    
    /// <summary>
    /// Ustawienie kierunku przemiszczenia obiektów
    /// </summary>
    void HandleDir()
    {
#if UNITY_EDITOR

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (currentDirection == Vector2.up)
                nextDir = Vector2.right;
            else if (currentDirection == Vector2.right)
                nextDir = Vector2.down;
            else if (currentDirection == Vector2.down)
                nextDir = Vector2.left;
            else
                nextDir = Vector2.up;

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {

            if (currentDirection == Vector2.up)
                nextDir = Vector2.left;
            else if (currentDirection == Vector2.right)
                nextDir = Vector2.up;
            else if (currentDirection == Vector2.down)
                nextDir = Vector2.right;
            else
                nextDir = Vector2.down;
        }

#else
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if ((touch.position.x < Screen.width / 2) && (TouchPhase.Began == touch.phase))
            {
                if (direction == Vector2.up)
                    nextDir = Vector2.left;
                else if (direction == Vector2.right)
                    nextDir = Vector2.up;
                else if (direction == Vector2.down)
                    nextDir = Vector2.right;
                else
                    nextDir = Vector2.down;
            }
            else if ((touch.position.x > Screen.width / 2) && (TouchPhase.Began == touch.phase))
            {
                if (direction == Vector2.up)
                    nextDir = Vector2.right;
                else if (direction == Vector2.right)
                    nextDir = Vector2.down;
                else if (direction == Vector2.down)
                    nextDir = Vector2.left;
                else
                    nextDir = Vector2.up;
            }
        }
#endif
    }
    
}
