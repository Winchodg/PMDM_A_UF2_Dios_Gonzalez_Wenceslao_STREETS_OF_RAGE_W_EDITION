using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    [SerializeField] float speed;
    [SerializeField] GameObject EnemyDead;
    [SerializeField] AudioClip sfxHit;

    GameObject target;

    Collider2D col;

    Collider2D targetCol;

    Renderer enemyRend;

    Animator anim;

    Rigidbody2D rb;

    AudioSource sfx;

    bool nextTo, injured;

    float height;

    float positionX;

    int hitCount;

    public static bool hitting = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        targetCol = target.GetComponent<Collider2D>();;
        height = targetCol.bounds.extents.y;

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        enemyRend = GetComponent<Renderer>();

        anim = GetComponent<Animator>();

        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //el enemigo va a por el jugador.
        Vector3 direction = target.transform.position - transform.position;

        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;

        // daño al enemigo y muerte a los tres golpes.
        if (!injured && nextTo && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3")))
            injured = true;


        if (nextTo) Invoke("EnemyHit", 1f);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyPunch")) 
        {
            hitting = true;
            target.SendMessage("PlayerDamage");
        }
        else
        {
            hitting = false;
        }
        

    }

    void FixedUpdate()
    {
        isInjured();
        Flip();
    }

    // flag a true para indicar jugador cerca.
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            nextTo = true;
        }
        
    }

    // flags a false para dindicar al jugador lejos y que no está golpeando.
    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            nextTo = false;
            
        }
            

        
    }

    // animación de daño al enemigo.
    void isInjured ()
    {
        if (!injured) return;

        injured = false;

        sfx.clip = sfxHit;
        sfx.Play();

        anim.SetTrigger("injured");

        hitCount++;

        if (hitCount == 3)
        {
            Invoke("DeathEnemy", 1f);
        }

    }   

    // animación golpeo enemigo.
    void EnemyHit ()
    {
        
        anim.SetTrigger("enemyPunch");
        
        /*if (hitting) 
            target.SendMessage("PlayerDamage");*/
        
    }

    // muerte enemigo.
    void DeathEnemy ()
    {
        GameManager.GetInstance().AddScore(gameObject.tag);
        Instantiate (EnemyDead, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }

    // orientación enemigo.
    void Flip()
    {
        positionX = target.transform.position.x - transform.position.x;

        if (positionX > 0)
        {
            transform.localScale = new Vector2 (-1, 1);
        }
        else
        {
            transform.localScale = new Vector2 (1, 1);
        }
    }
}