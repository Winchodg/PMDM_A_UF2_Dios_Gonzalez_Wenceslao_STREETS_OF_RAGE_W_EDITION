using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] float speed;

    GameObject player;

    Collider2D col;

    Renderer carRend;

    float height;

    public static bool hitting = false;

    const float DESTROY_LENGTH = -20.0f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        col = player.GetComponent<Collider2D>();

        height = col.bounds.extents.y;

        carRend = GetComponent<Renderer>();
    }
    
    // Update is called once per frame
    void Update()
    { 
        CarPosition();

        // movemos el coche.
        transform.Translate (Vector3.left * speed * Time.deltaTime);

        // destruimos el coche.
        if ((Camera.main.transform.position.x - player.transform.position.x) < DESTROY_LENGTH) Destroy(gameObject);
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            hitting = true;
            player.SendMessage("PlayerDamage");
        }
    }

    void CarPosition ()
    {    
            if ((player.transform.position.y - height) > transform.position.y)
            {
                carRend.sortingOrder = 6;
            }
            else
            {
                carRend.sortingOrder = 4;
            }
    }
    
}