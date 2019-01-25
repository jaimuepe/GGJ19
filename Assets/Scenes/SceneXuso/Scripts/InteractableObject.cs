using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Material m_Material;
    private Coroutine interactAnimationCoroutine;
    [SerializeField]
    private float MaxAlphaValue = 200;
    private float AlphaIntervalChangeTime = 0.001f;
    [SerializeField]
    private float ChangeAlphaValue = 0.0075f;

    void Start()
    {
        m_Material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object trigger activated: Enter");
        if (interactAnimationCoroutine != null)
        {
            StopCoroutine(interactAnimationCoroutine);
        }
        interactAnimationCoroutine = StartCoroutine(InteractAnimation());
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Object trigger activated: Exit");
        if (interactAnimationCoroutine != null)
        {
            StopCoroutine(interactAnimationCoroutine);
        }
        interactAnimationCoroutine = StartCoroutine(InteractAnimation(true));
    }

    IEnumerator InteractAnimation(bool inverse = false)
    {
        Color currentColor = m_Material.GetColor("_OutlineColor");
        if (inverse)
        {
            for (int i = 0; i <= MaxAlphaValue; i++)
            {
                if (currentColor.a != 0)
                {
                    currentColor = m_Material.GetColor("_OutlineColor");
                    m_Material.SetColor("_OutlineColor", new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - ChangeAlphaValue));
                    yield return new WaitForSeconds(AlphaIntervalChangeTime);
                }
            }
        }
        else
        {
            for (int i = 0; i <= MaxAlphaValue; i++)
            {
                currentColor = m_Material.GetColor("_OutlineColor");
                m_Material.SetColor("_OutlineColor", new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + ChangeAlphaValue));
                yield return new WaitForSeconds(AlphaIntervalChangeTime);
            }
        }
    }
}
