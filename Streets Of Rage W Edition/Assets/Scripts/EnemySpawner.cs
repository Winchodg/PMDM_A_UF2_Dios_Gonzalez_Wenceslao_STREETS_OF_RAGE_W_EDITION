using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    const float MORE_ENEMIES = 40f;
    const float MIN_Y = -2.9f;
    const float MAX_Y = 2.6f;

    [SerializeField] float delay;

    [SerializeField] GameObject enemy;

    float posicionY;

    float interval = 5;

    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine ("ThrowEnemies");
    }

    // Update is called once per frame
    void update()
    {
        if (gameObject.transform.position.x > MORE_ENEMIES)
            interval = 3;
    }

    IEnumerator ThrowEnemies ()
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {

            //Generamos una nueva nave, calculando aleatoriamente la posicin en x.
            posicionY = Random.Range (MIN_Y, MAX_Y);
            Instantiate(enemy, new Vector3(transform.position.x, posicionY, transform.position.z), Quaternion.identity);

            //retrasamos el tiempo de interval la generación de una nueva nave.
            yield return new WaitForSeconds(interval);
        }
       
    }
}
