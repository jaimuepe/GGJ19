using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFloatingFollow : MonoBehaviour
{
    public Transform boneToFollow, facingDirectionChecker;
    private float directionMultiplier;
    public CharacterController2D characterController2D;
    public Transform dialogParent;

    void Start()
    {
        characterController2D = GameObject.Find("Player").GetComponent<CharacterController2D>();
    }
    
    void Update()
    {
        
        //Debug.Log(facingDirectionChecker.eulerAngles.y);
        if (facingDirectionChecker.eulerAngles.y == 0)
        {
            directionMultiplier = 1;
            transform.eulerAngles = dialogParent.eulerAngles = new Vector3(dialogParent.eulerAngles.x, 0, dialogParent.eulerAngles.z);
        } else if (facingDirectionChecker.eulerAngles.y == 180)
        {
            directionMultiplier = -1;
            transform.eulerAngles = dialogParent.eulerAngles = new Vector3(dialogParent.eulerAngles.x, 180, dialogParent.eulerAngles.z);
        }
        transform.position = new Vector3(boneToFollow.position.x+(0.25f* directionMultiplier), boneToFollow.position.y+1f, boneToFollow.position.z);    
    }
}
