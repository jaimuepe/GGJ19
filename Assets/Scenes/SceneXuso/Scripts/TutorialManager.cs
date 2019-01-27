using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public InputController m_InputController;
    public GameObject MovementTutorial, ActionTutorial;

    void Start()
    {
        m_InputController._onTutorial = true;
        StartCoroutine(ShowMovementTutorial_Begin());
    }

    private IEnumerator ShowMovementTutorial_Begin()
    {
        yield return new WaitForSeconds(3f);
        ShowMovementTutorial();
        m_InputController._blockMovement = false;
    }

    public void ShowMovementTutorial(bool show=true)
    {
        MovementTutorial.SetActive(show);
    }

    public void ShowActionTutorial(bool show=true)
    {
        if (show)
        {
            m_InputController._onTutorial = true;
            m_InputController._blockMovement = true;
        }
        ActionTutorial.SetActive(show);
    }
}
