using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneAnimatorReceiver : MonoBehaviour
{
    public void SpawnSand()
    {
        FindObjectOfType<FinalSceneAnimation>().PlaySandParticleSystem();
    }
}
