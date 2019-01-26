using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    Transform mTransform;

    public bool usable = true;

    private void OnEnable()
    {
        mTransform = transform;
    }

    public FMODUnity.StudioEventEmitter emitterHit;

    public void PlayHitClip()
    {
        emitterHit.Play();
    }
}
