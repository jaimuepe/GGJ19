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

    public void ResolveInteraction(string interactionId, GameObject obj)
    {
        Debug.Log("Resolving interaction: " + interactionId);

        if (interactionId == "breakable_door_00")
        {
            BreakableDoor bDoor = obj.GetComponent<BreakableDoor>();
            bDoor.HeadButt();
        }
        else if (interactionId == "exit_door_00")
        {
            LevelTransitionManager.Instance.LoadNextLevel();
        }
        else if (interactionId == "wall_fish_00" ||
            interactionId == "wall_fish_01" ||
            interactionId == "wall_fish_02")
        {
            WallRotatingFish wallFish = obj.GetComponent<WallRotatingFish>();
            wallFish.Rotate();
        }
    }
}
