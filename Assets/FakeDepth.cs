using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FakeDepth : MonoBehaviour
{
    Transform mTransform;
    SkinnedMeshRenderer meshRenderer;

    private void Start()
    {
        mTransform = transform;
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (mTransform == null)
        {
            mTransform = transform;
        }
#endif
        // [-3, -2]
        float v = Mathf.Clamp(mTransform.position.y, -3.0f, -2.0f);
        float scale = -1.0f * v - 2.0f;
        mTransform.localScale = scale * Vector3.one;

        float a = Mathf.Clamp(mTransform.position.y, -2.5f, -2.0f);
        float alpha = -2.0f * a - 4.0f;
        meshRenderer.material.SetColor("_Tint", new Color(
            1.0f, 1.0f, 1.0f, alpha));
    }
}
