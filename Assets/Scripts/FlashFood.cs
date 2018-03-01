using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFood : MonoBehaviour
{
    /// <summary>
    /// Obiekt wymagjący migania przed zniszczeniem
    /// </summary>
    Renderer rendererSpecialFood;

    /// <summary>
    /// Zmiennna pomocnicza. Tempo migania obiektu
    /// </summary>
    private float flashTime = 3.0f;

    void Start()
    {
        rendererSpecialFood = GetComponent<Renderer>();
        StartCoroutine("FlashObject");
    }

    /// <summary>
    /// Metoda odpowiedzialna za miganie obiektu przez zniszczeniem
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlashObject()
    {
        yield return new WaitForSeconds(10);
        YieldInstruction delay = new WaitForSeconds(0.5f);

        for (int i = 5; i >= 0; i--)
        {
            rendererSpecialFood.enabled = false;
            yield return delay;

            rendererSpecialFood.enabled = true;
            if(flashTime == 1.0f)
            {
                yield return delay;
            }
            else
            {
                yield return new WaitForSeconds(flashTime);
                flashTime -= 1.0f;
            }
   
            if(i == 0)
                Destroy(gameObject);
        }
    }
}
