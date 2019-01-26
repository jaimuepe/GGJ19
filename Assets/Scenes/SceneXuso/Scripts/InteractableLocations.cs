using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLocations : MonoBehaviour
{
    public string tagToDetect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == tagToDetect)
        {
            Debug.Log("Interactable location trigger: Enter");
            ChangeMaterialColor();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == tagToDetect)
        {
            Debug.Log("Interactable location trigger: Stay");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == tagToDetect)
        {
            ChangeMaterialColor();
            Debug.Log("Interactable location trigger: Exit");
        }
    }

    //Prueba
    private void ChangeMaterialColor()
    {
        MeshRenderer _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer.material.color != Color.black)
        {
            _meshRenderer.material.color = Color.black;
        }
        else
        {
            _meshRenderer.material.color = Color.white;
        }
    }
}
