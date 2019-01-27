using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public FMODUnity.StudioEventEmitter emitterPointerEnter;
    public FMODUnity.StudioEventEmitter emitterPointerClick;

    public void OnPointerEnter(PointerEventData data)
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        emitterPointerEnter.Play();
    }

    public void OnPointerExit(PointerEventData data)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
}
