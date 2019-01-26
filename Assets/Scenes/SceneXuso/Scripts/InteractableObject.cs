using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Material m_Material;
    private Coroutine interactAnimationCoroutine;
    private float MaxAlphaValue = 100;
    private float AlphaIntervalChangeTime = 0.0001f;
    private float ChangeAlphaValue = 0.005f;
    private Animator m_Animator;

    void Start()
    {
        m_Material = GetComponent<MeshRenderer>().material;
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                Debug.Log("Object trigger activated: Enter");
                if (interactAnimationCoroutine != null)
                {
                    StopCoroutine(interactAnimationCoroutine);
                }
                //interactAnimationCoroutine = StartCoroutine(InteractAnimation());
                m_Animator.Play("InteractAnimation");
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                Debug.Log("Object trigger activated: Exit");
                if (interactAnimationCoroutine != null)
                {
                    StopCoroutine(interactAnimationCoroutine);
                }
                //interactAnimationCoroutine = StartCoroutine(InteractAnimation(true));
                m_Animator.Play("InteractAnimation_R");
                break;
            default:
                break;
        }
    }

    IEnumerator InteractAnimation(bool inverse = false)
    {
        Color currentColor = m_Material.GetColor("_OutlineColor");
        if (inverse)
        {
            for (int i = 0; i <= MaxAlphaValue; i++)
            {
                if (currentColor.a > 0)
                {
                    currentColor = m_Material.GetColor("_OutlineColor");
                    m_Material.SetColor("_OutlineColor", new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - ChangeAlphaValue));
                    yield return new WaitForSeconds(AlphaIntervalChangeTime);
                }
                else
                {
                    break;
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
