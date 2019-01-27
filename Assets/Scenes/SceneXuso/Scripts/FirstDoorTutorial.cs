using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoorTutorial : MonoBehaviour
{
    public InputController m_InputController;
    public TutorialManager m_TutorialManager;
    private bool _tutorialShowed;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_tutorialShowed)
        {
            m_TutorialManager.ShowActionTutorial();
            _tutorialShowed = true;
        }
    }
}
