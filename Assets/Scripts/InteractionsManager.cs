using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsManager : MonoBehaviour
{
    static InteractionsManager instance;
    public static InteractionsManager Instance
    {
        get
        {
            if (instance == null)
            {
                InteractionsManager im = FindObjectOfType<InteractionsManager>();
                if (im)
                {
                    instance = im;
                }
                else
                {
                    GameObject go = new GameObject("[INTERACTIONS_MANAGER]");
                    instance = go.AddComponent<InteractionsManager>();
                }
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    GameObject player;
    GameObject Player
    {
        get
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            return player;
        }
    }

    public void ResolveInteraction(string interactionId, GameObject obj)
    {
        Debug.Log("Resolving interaction: " + interactionId);

        if (interactionId == "breakable_door_00")
        {
            Player.GetComponentInChildren<Animator>().SetBool("headbutt", true);
        }
        else if (interactionId == "exit_door_00" || interactionId == "exit_door_01")
        {
            ExitDoor exitDoor = obj.GetComponent<ExitDoor>();
            if (!exitDoor.usable)
            {
                return;
            }

            exitDoor.usable = false;

            Player.GetComponent<CharacterController2D>().WalkRightEndlessly = true;

            if (interactionId == "exit_door_00")
            {
                GameObject depthWall = GameObject.FindGameObjectWithTag("DepthWall");
                Transform depthWallTransform = depthWall.transform;
                depthWallTransform.position = new Vector3(
                    depthWallTransform.position.x,
                    depthWallTransform.position.y,
                    -5.0f);
            }

            LevelTransitionManager.Instance.LoadNextLevel();
        }
        else if (interactionId == "wall_fish_00" ||
            interactionId == "wall_fish_01" ||
            interactionId == "wall_fish_02")
        {
            RotatingWheel rotatingWheel = obj.GetComponent<RotatingWheel>();
            if (!rotatingWheel.usable)
            {
                return;
            }
            rotatingWheel.Rotate();
        }
        else if (interactionId == "wheel_puzzle_ok")
        {
            StartCoroutine(WheelPuzzleActions());
        }
    }

    IEnumerator WheelPuzzleActions()
    {
        Player.GetComponent<CharacterController2D>().MovementEnabled = false;
        Player.GetComponent<CharacterInteractions>().InteractionsEnabled = false;

        GameObject topLight = GameObject.FindGameObjectWithTag("TopLight");
        topLight.SetActive(false);

        GameObject doorLight = GameObject.FindGameObjectWithTag("DoorLight");

        doorLight.GetComponent<FMODUnity.StudioEventEmitter>().Play();

        yield return new WaitForSeconds(2.4f);
        doorLight.GetComponent<Animator>().SetBool("fadein", true);

        Player.GetComponent<CharacterController2D>().MovementEnabled = true;
        Player.GetComponent<CharacterInteractions>().InteractionsEnabled = true;

        GameObject.FindGameObjectWithTag("ExitDoor").GetComponent<BoxCollider2D>().enabled = true;

        yield return null;
    }
}
