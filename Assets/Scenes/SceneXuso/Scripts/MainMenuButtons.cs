using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public FMODUnity.StudioEventEmitter emitterPointerEnter;
    public FMODUnity.StudioEventEmitter emitterPointerClick;

    public void OnPointerEnter()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        emitterPointerEnter.Play();
    }

    public void OnPointerExit()
    {

        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
}
