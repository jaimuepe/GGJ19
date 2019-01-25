using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSystem : MonoBehaviour
{
    private LayerMask stairsLayer;

    void Start()
    {
        stairsLayer = LayerMask.GetMask("Stairs");
    }

    public void Calculate()
    {

    }
}
