using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] AudioClip sfxPowerUp;


   void OnTriggerEnter2D (Collider2D other)
   {
        if (other.tag == "Player")
        {
            GameManager.GetInstance().HealthManager(50);
            AudioSource.PlayClipAtPoint(sfxPowerUp, Camera.main.transform.position, 1);
            Destroy(gameObject);
        }
   }
}
