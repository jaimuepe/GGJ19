using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{



#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ExitRoom();
        }
    }
#endif

    public void ExitRoom()
    {
        LevelTransitionManager.Instance.LoadNextLevel();
    }

}
