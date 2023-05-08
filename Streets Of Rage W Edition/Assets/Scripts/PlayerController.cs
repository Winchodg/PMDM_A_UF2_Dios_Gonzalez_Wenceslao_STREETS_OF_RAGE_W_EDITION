using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float duration;
    //[SerializeField] GameObject playerGiveUp;
    


    Rigidbody2D rb;

    Vector3 endPosition;
    Vector3 elementPosition;

    Transform cam;

    Animator anim;

    AudioSource sfx;
    
    float moveX, moveY, height;

    bool punch, kick, jumpKick, active, nextTo, nextToBarrel;

    public static bool destroyBarrel;

    const float STAGE_END = 80f;

    const int ENEMY_DAMAGE = -1;

    const int CAR_DAMAGE = -5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        cam = Camera.main.transform;

        anim = GetComponent<Animator>();

        sfx = GetComponent<AudioSource>();
        
        StartCoroutine("StartPlayer");
    }

    void Update()
    {
        // movimiento del jugador
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (rb.velocity.x > 0 && cam.position.x <= transform.position.x && transform.position.x < STAGE_END)
            cam.position = new Vector3 (transform.position.x, cam.position.y, cam.position.z);


        // golpeos
        if(!punch && Input.GetButtonDown("Fire1") )
            punch = true;

        if(!kick && Input.GetButtonDown("Fire2") )
            kick = true;

        if(!jumpKick && Input.GetButtonDown("Fire3") )
            jumpKick = true;
             

        // destrucción barril
        if (nextToBarrel && (punch || kick || jumpKick))
                destroyBarrel = true;

        //PlayerDead();
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        // flags a true que indican cercanía de enemigo y barril.
        if (other.gameObject.tag == "Barrel") nextToBarrel = true;

        
        
    }

    void OnCollisionExit2D (Collision2D other)
    {

        // flags a false que indican que se alejan enemigo y barril.
        if (other.gameObject.tag == "Barrel")
        {
            nextToBarrel = false;
            destroyBarrel = false;
        } 

    }

    

    void FixedUpdate()
    {   
        Walk();
        Flip();
        Punch();
        Kick();
        JumpKick();
    }

    // animacion movimiento jugador.
    void Walk()
    {
        Vector2 velocidad = new Vector2 (moveX * speed * Time.fixedDeltaTime, moveY * speed * Time.fixedDeltaTime);
        rb.velocity = velocidad;

        anim.SetBool ("isWalking", Mathf.Abs(rb.velocity.x) > Mathf.Epsilon || Mathf.Abs(rb.velocity.y) > Mathf.Epsilon); 
    }

    // orientación jugador.
    void Flip ()
    {
        float vx = rb.velocity.x;

        if (Mathf.Abs(vx) > Mathf.Epsilon && active)
        {
            transform.localScale = new Vector2 (Mathf.Sign(vx), 1);
            
        }
    }

    // animación puñetazo.
    void Punch()
    {
        if (!punch) return;

        punch = false;

        anim.SetTrigger("isPunching");
        
    }

    // animación patada.
    void Kick()
    {
        if (!kick) return;

        kick = false;

        anim.SetTrigger("isKicking");

    }

    // animación patada con salto.
    void JumpKick()
    {
        if (!jumpKick) return;

        jumpKick = false;

        anim.SetTrigger("isJumpKicking");

    }

    // animación de daño del jugador.
    void PlayerDamage ()
    {
        if (EnemyController.hitting)
        {
            sfx.Play();
            EnemyController.hitting = false;
            anim.SetTrigger("hitted");
            GameManager.GetInstance().HealthManager(ENEMY_DAMAGE);
        }
        else if (CarController.hitting)
        {
            CarController.hitting = false;
            anim.SetTrigger("hitted");
            GameManager.GetInstance().HealthManager(CAR_DAMAGE);

        }
        
    }


    // movimiento de entrada del jugador en inicio del juego.
    IEnumerator StartPlayer()
    {

        anim.SetBool("start", true);

        //Desactivamos las colisiones.
        BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
        boxCol.enabled = false;

        
        //Capturamos la posición inicial del jugador.
        Vector3 startPosition = transform.position;

        //hacemos la transición del jugador hasta la posición de inicio.
        float t = 0;
        while (t < duration)
        {
            
            t += Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp (startPosition, endPosition, t/duration);
            transform.position = newPosition;
            

            yield return null;
        }

        //activamos de nuevo las colisiones.
        boxCol.enabled = true;
        active = true;
 
    }


    void PlayerDead ()
    {
        if (GameManager.isDead)
        {
            
            active = false;
            //Instantiate (playerGiveUp, transform.position, Quaternion.identity);
            GameManager.isDead = false;
            Destroy(gameObject);
            
        
        }
    }
    
}