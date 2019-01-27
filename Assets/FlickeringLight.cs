using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public float minOnTime;
    public float maxOnTime;

    public float minOffTime;
    public float maxOffTime;

    public float minDelayTime;
    public float maxDelayTime;

    SpriteRenderer sr;
    public GameObject[] symbols;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Flicker());
    }

    public void TurnOffLight()
    {
        StopAllCoroutines();

        sr.enabled = false;

        for (int i = 0; i < symbols.Length; i++)
        {
            symbols[i].SetActive(true);
        }

        gameObject.SetActive(false);
    }

    private IEnumerator Flicker()
    {
        bool on = false;

        while (true)
        {
            if (on)
            {
                on = false;
                sr.enabled = false;

                for (int i = 0; i < symbols.Length; i++)
                {
                    symbols[i].SetActive(true);
                }

                yield return new WaitForSeconds(Random.Range(minOffTime, maxOffTime));
            }
            else
            {
                on = true;
                sr.enabled = true;

                for (int i = 0; i < symbols.Length; i++)
                {
                    symbols[i].SetActive(false);
                }

                yield return new WaitForSeconds(Random.Range(minOnTime, maxOnTime));
            }
            yield return new WaitForSeconds(Random.Range(minDelayTime, maxDelayTime));
        }
    }
}
