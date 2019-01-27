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

    public FMODUnity.StudioEventEmitter humEmitter;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Flicker());

        humEmitter.SetParameter("On/Off", 1);
        humEmitter.Play();
    }

    private void Update()
    {
        if (sr.enabled)
        {
            humEmitter.SetParameter("On/Off", 1);
        }
        else
        {
            humEmitter.SetParameter("On/Off", 0);
        }
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

                // humEmitter.SetParameter("On/Off", 0);
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

                // humEmitter.SetParameter("On/Off", 1);
                yield return new WaitForSeconds(Random.Range(minOnTime, maxOnTime));
            }
            yield return new WaitForSeconds(Random.Range(minDelayTime, maxDelayTime));
        }
    }
}
