using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEnemy : MonoBehaviour
{
    GameObject player;

    Collider2D col;

    Renderer deadEnemyRend;

    float height;

    // Start is called before the first frame update
    void Start()
    {
        

        player = GameObject.FindGameObjectWithTag("Player");

        col = player.GetComponent<Collider2D>();

        height = col.bounds.extents.y;

        deadEnemyRend = GetComponent<Renderer>();

        Destroy(gameObject, 2f);
    }

    void Update()
    {
        DeadEnemyPosition();
    }

    void DeadEnemyPosition ()
    {    
            if ((player.transform.position.y - height) > transform.position.y)
            {
                deadEnemyRend.sortingOrder = 6;
            }
            else
            {
                deadEnemyRend.sortingOrder = 4;
            }
    }
}
