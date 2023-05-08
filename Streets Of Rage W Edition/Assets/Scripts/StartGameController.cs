using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text startMsg;
    [SerializeField] float duration;
    [SerializeField] int numParpadeos;

    Material mat;

    Color color;

    float alpha, newAlpha;

    AudioSource sfx;

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        color = mat.color;
        alpha = color.a;

        sfx = GetComponent<AudioSource>(); 

        StartCoroutine ("ChangeColor");
        StartCoroutine ("StartGame");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) 
        {
            sfx.Play();
            startMsg.text = "";
            StartCoroutine ("FadeToBlack");
        }
    }

    IEnumerator ChangeColor()
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            title.color = Color.Lerp(Color.red, Color.yellow, time/duration);
            yield return null;
        }

        while (time > 0)
        {
            time -= Time.deltaTime;
            title.color = Color.Lerp(Color.red, Color.yellow, time/duration);
            yield return null;
        }

        StartCoroutine ("ChangeColor");
    }

    IEnumerator StartGame()
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            startMsg.color = Color.Lerp(Color.white, Color.black, time/duration);
            yield return null;
        }

        while (time > 0)
        {
            time -= Time.deltaTime;
            startMsg.color = Color.Lerp(Color.white, Color.black, time/duration);
            yield return null;
        }
        StartCoroutine ("StartGame");
    }

    IEnumerator FadeToBlack()
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            mat.color = Color.Lerp(Color.white, Color.black, time/duration);
            title.color = Color.Lerp(title.color, Color.black, time/duration);
            yield return null;
        }

        SceneManager.LoadScene(1);
    }

}
