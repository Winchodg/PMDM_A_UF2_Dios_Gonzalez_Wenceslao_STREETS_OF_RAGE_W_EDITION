using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    const float LIMIT_POSITION = 70f;
    [SerializeField] float interval;

    [SerializeField] float delay;

    [SerializeField] GameObject car;

    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine ("ThrowCar");
    }

    // Update is called once per frame
    void update()
    {
        
    }

    IEnumerator ThrowCar ()
    {
        yield return new WaitForSeconds(delay);

        while (Camera.main.transform.position.x < LIMIT_POSITION)
        {

            //Generamos una nueva nave, calculando aleatoriamente la posicin en x.
            Instantiate(car, transform.position, Quaternion.identity);



            //retrasamos el tiempo de interval la generación de una nueva nave.
            yield return new WaitForSeconds(interval);
        }
       
    }
}
