using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrantController : MonoBehaviour
{
    GameObject player;

    Collider2D col;

    Renderer hydrantRend;

    float height;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        col = player.GetComponent<Collider2D>();

        height = col.bounds.extents.y;

        hydrantRend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HydrantPosition();
    }

    void HydrantPosition ()
    {    
            if ((player.transform.position.y - height) > transform.position.y)
            {
                hydrantRend.sortingOrder = 6;
            }
            else
            {
                hydrantRend.sortingOrder = 4;
            }
    }
}
