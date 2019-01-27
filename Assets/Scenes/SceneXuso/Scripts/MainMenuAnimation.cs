using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    MeshRenderer m_MeshRenderer;

    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        m_MeshRenderer.material.mainTextureOffset += new Vector2(-Time.deltaTime,Time.deltaTime)*0.1f;
    }
}
