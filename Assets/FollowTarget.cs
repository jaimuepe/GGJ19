using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Vector2 offset;
    public Transform target;
    Transform mTransform;

    private void Start()
    {
        mTransform = transform;
    }

    private void LateUpdate()
    {
        mTransform.position = new Vector3(
            target.position.x + offset.x * mTransform.localScale.x,
            target.position.y + offset.y * mTransform.localScale.y,
            mTransform.position.z);
    }
}
