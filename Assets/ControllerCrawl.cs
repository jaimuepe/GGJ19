using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerCrawl : MonoBehaviour
{
    Animator mAnimator;
    Vector3 target = new Vector3(1.2f, -2.0f, 0.0f);

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    bool finished = false;

    void Update()
    {
        mAnimator.SetBool("jump", false);

        if (!finished && canMove && Input.GetKeyDown(KeyCode.E))
        {
            mAnimator.SetBool("jump", true);
            StartCoroutine(Move());
        }

        if (!finished && Vector3.Distance(transform.position, target) < 0.01f)
        {
            finished = true;
            StartCoroutine(FadeToWhite());
        }
    }

    bool canMove = true;

    public Image fadeOutPanel;
    public Image endGameImage;

    IEnumerator FadeToWhite()
    {
        yield return new WaitForSeconds(1.0f);

        float duration = 3.0f;

        float start = Time.time;
        float elapsed = 0;
        Color endColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Color startColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        fadeOutPanel.color = startColor;

        while (elapsed < duration)
        {
            // calculate how far through we are
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            fadeOutPanel.color = Color.Lerp(startColor, endColor, normalisedTime);
            yield return null;
        }
        fadeOutPanel.color = endColor;

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(UnFade());
    }

    IEnumerator UnFade()
    {
        endGameImage.gameObject.SetActive(true);

        float duration = 3.0f;

        float start = Time.time;
        float elapsed = 0;
        Color startColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Color endColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        fadeOutPanel.color = startColor;

        while (elapsed < duration)
        {
            // calculate how far through we are
            elapsed = Time.time - start;
            float normalisedTime = Mathf.Clamp(elapsed / duration, 0, 1);
            fadeOutPanel.color = Color.Lerp(startColor, endColor, normalisedTime);
            yield return null;
        }
        fadeOutPanel.color = endColor;
    }

    IEnumerator Move()
    {
        canMove = false;

        float distance = 0.0f;
        float maxDistance = 0.8f;
        float step = 0.5f * Time.deltaTime;

        while (distance < maxDistance && Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                step);

            distance += step;

            yield return null;
        }

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            transform.position = target;
        }

        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }
}
