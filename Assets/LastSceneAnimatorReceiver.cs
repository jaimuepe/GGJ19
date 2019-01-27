using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneAnimatorReceiver : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter emitter;
    public FMODUnity.StudioEventEmitter splashEmitter;

    public void SpawnSand()
    {
        FindObjectOfType<FinalSceneAnimation>().PlaySandParticleSystem();
    }

    public void ClothesOff()
    {
        emitter.Play();
    }

    public void Splash()
    {
        splashEmitter.Play();
    }
}
