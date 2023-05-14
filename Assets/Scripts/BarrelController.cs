using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    const float HEALTH_POSITION = 40f;
    [SerializeField] GameObject brokeBarrel;
    [SerializeField] GameObject powerUp;

    GameObject player;

    Collider2D col;

    Renderer barrelRend;

    float height;

    bool nextTo;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        col = player.GetComponent<Collider2D>();

        height = col.bounds.extents.y;

        barrelRend = GetComponent<Renderer>();
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        nextTo = true;
    }

    void OnTriggerExit2D (Collider2D other)
    {
        nextTo = false;
    }

    // Update is called once per frame
    void Update()
    {
        BarrelPosition();

        if (PlayerController.destroyBarrel && nextTo) StartCoroutine("DestroyBarrel");

    }

    void BarrelPosition ()
    {    
            if ((player.transform.position.y - height) > transform.position.y)
            {
                barrelRend.sortingOrder = 6;
            }
            else
            {
                barrelRend.sortingOrder = 4;
            }
    }

    IEnumerator DestroyBarrel()
    {
        
        yield return new WaitForSeconds(.2f);

        PlayerController.destroyBarrel = false;
        
        if (player.transform.localScale.x < 0)
        {
                Instantiate (brokeBarrel, new Vector3 (transform.position.x-1, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else
        {
                Instantiate (brokeBarrel, new Vector3 (transform.position.x+1, transform.position.y, transform.position.z), Quaternion.identity);
        }

        //if (transform.position.x > HEALTH_POSITION)
            Instantiate (powerUp, transform.position, Quaternion.identity);

       
        GameManager.GetInstance().AddScore(gameObject.tag);
        Destroy(gameObject);
    }

}
