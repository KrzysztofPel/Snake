using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    
    /// <summary>
    /// Obiekt reprezentujący zwyczajne jedzenie
    /// </summary>
    public GameObject normalFoodPrefab;
    
    /// <summary>
    /// Obiekt rezperzentujący specjalne jedzenie
    /// </summary>
    public GameObject specjalFoodPrefab;

    //Przypisanie danego komponetu wcelu uzyskania pozycji umieszczenia na scenie
    public Transform borderRight;
    public Transform borderLeft;
    public Transform borderTop;
    public Transform borderBottom;

    //Zmienne pomocnicze do odrodzenia się jedzenia
    float minTime = 8.0f;
    float maxTime = 25.0f;
    float respawnTime;
    float time;


    void Start()
    {
        time = 0;
    }

    void Update()
    {
        RespawnNormalFood();

        time += Time.deltaTime;

        if (time >= respawnTime)
        {
            RespawnSpecialFood();
        }
    }

    /// <summary>
    /// Metoda sprawdza czy obiekt o nazwie NormalFood istnieje
    /// </summary>
    void RespawnNormalFood()
    {
        if (GameObject.Find("NormalFood(Clone)") == null)
        {
            SpawnFood(normalFoodPrefab);
        }
    }

    
    /// <summary>
    /// Metoda sprawdza czy obiekt o nazwie SpecialFood istnieje
    /// </summary>
    void RespawnSpecialFood()
    {
        if (GameObject.Find("SpecialFood(Clone)") == null)
        {

            SpawnFood(specjalFoodPrefab);
            time = 0;
            SetRandomTime();
        }
    }


    /// <summary> 
    /// Metoda losuje odstęp czasu przed kolejnym odrodzeniem specjalnego jedzenia
    /// </summary>
    void SetRandomTime()
    {
        respawnTime = Random.Range(minTime, maxTime);
    }

    /// <summary> 
    /// Metoda odpowiada za umieszczenie jedzenia w randowmwym miejscu wyznaczonej powierzchni
    /// </summary>
    /// <param name="gameObject"></param>
    void SpawnFood(GameObject gameObject)
    {
        //Ustalenie pozycji x i y dla obiektu
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);
        //Wywołanie metody odpowiedzialeej za sklonowoanie i umieszczennie obiektu wyznaczonym miescu o domyślniej rotacji
        Instantiate(gameObject, new Vector2(x, y), Quaternion.identity);
    }
}

