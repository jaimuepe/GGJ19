using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHelper : MonoBehaviour
{
    public bool _isGamepadConnected;
    public SpriteRenderer m_SpriteRenderer;
    public Sprite gamepadSprite, keyboardSprite;

    void Start()
    {
        _isGamepadConnected = GameObject.Find("InputController").GetComponent<InputController>()._gamepadConnected;
        UpdateSprite();
        this.gameObject.SetActive(false);
    }

    void Update()
    {

    }

    private void UpdateSprite()
    {
        if (_isGamepadConnected)
        {
            m_SpriteRenderer.sprite = gamepadSprite;
        }
        else
        {
            m_SpriteRenderer.sprite = keyboardSprite;
        }
    }
}
