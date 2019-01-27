using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneAnimatorReceiver : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter emitter;
    public FMODUnity.StudioEventEmitter splashEmitter;

    public ParticleSystem sandParticleSystem;

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
        sandParticleSystem.Play();
    }

    public void Walk()
    { }

    public void Talk()
    { }
}
