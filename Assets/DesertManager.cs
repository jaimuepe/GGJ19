using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertManager : MonoBehaviour
{
    public SpriteRenderer bg0;
    public SpriteRenderer light0;

    public SpriteRenderer bg1;
    public SpriteRenderer light1;

    public SpriteRenderer bg2;
    public SpriteRenderer light2;

    ExitDoorDesert door;

    Color[] originalColor;

    void Start()
    {
        door = FindObjectOfType<ExitDoorDesert>();
        originalColor = new Color[] { bg0.color, light0.color, bg1.color, light1.color, bg2.color, light2.color };

        bg1.color = new Color(bg1.color.r, bg1.color.g, bg1.color.b, 0);
        light1.color = new Color(light1.color.r, light1.color.g, light1.color.b, 0);
        bg2.color = new Color(bg2.color.r, bg2.color.g, bg2.color.b, 0);
        light2.color = new Color(light2.color.r, light2.color.g, light2.color.b, 0);

        GameObject bgMusic = GameObject.FindGameObjectWithTag("BackgroundMusic");
        if (bgMusic)
        {
            FMODUnity.StudioEventEmitter emitter =
                GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<FMODUnity.StudioEventEmitter>();
            emitter.SetParameter("room2", 1.0f);
        }
    }

    public void Transition()
    {
        if (door.numberTimesUsed == 0)
        {
            StartCoroutine(FadeImage(true, bg0, 0));
            StartCoroutine(FadeImage(true, light0, 1));

            StartCoroutine(FadeImage(false, bg1, 2));
            StartCoroutine(FadeImage(false, light1, 3));
        }
        else if (door.numberTimesUsed == 1)
        {
            StartCoroutine(FadeImage(true, bg1, 2));
            StartCoroutine(FadeImage(true, light1, 3));

            StartCoroutine(FadeImage(false, bg2, 4));
            StartCoroutine(FadeImage(false, light2, 5));

        }
    }

    IEnumerator FadeImage(bool fadeAway, SpriteRenderer sr, int index)
    {
        float duration = 2.0f;

        // fade from opaque to transparent
        if (fadeAway)
        {
            float start = Time.time;
            float elapsed = 0;
            Color startColor = originalColor[index];
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

            while (elapsed < duration)
            {
                // calculate how far through we are
                elapsed = Time.time - start;
                float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
                sr.color = Color.Lerp(startColor, endColor, normalisedTime);
                yield return null;
            }
            sr.color = endColor;
        }
        // fade from transparent to opaque
        else
        {
            float start = Time.time;
            float elapsed = 0;
            Color endColor = originalColor[index];
            Color startColor = new Color(endColor.r, endColor.g, endColor.b, 0);

            sr.color = startColor;

            while (elapsed < duration)
            {
                // calculate how far through we are
                elapsed = Time.time - start;
                float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
                sr.color = Color.Lerp(startColor, endColor, normalisedTime);
                yield return null;
            }
            sr.color = endColor;
        }
    }
}
