using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    Vector3 basePosition;
    Transform mTransform;

    public float frequency;
    public float magnitude;

    float phase;

    private void Start()
    {
        mTransform = transform;
        basePosition = mTransform.position;
        phase = Random.Range(0.0f, 2 * Mathf.PI);
    }

    private void OnEnable()
    {
        mTransform = transform;
        basePosition = mTransform.position;
        phase = Random.Range(0.0f, 2 * Mathf.PI);
    }

    private void Update()
    {
        mTransform.position = new Vector3(
            mTransform.parent.position.x,
            mTransform.parent.position.y + magnitude * Mathf.Cos(phase + Time.time / frequency),
            basePosition.z);
    }
}
