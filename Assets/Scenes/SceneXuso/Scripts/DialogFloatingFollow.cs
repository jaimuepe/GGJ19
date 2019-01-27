using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFloatingFollow : MonoBehaviour
{
    public Transform boneToFollow, facingDirectionChecker;
    private float directionMultiplier;
    public CharacterController2D characterController2D;
    public List<Transform> dialogParent;
    public List<Transform> dialogChildren;

    void Start()
    {
        characterController2D = GameObject.Find("Player").GetComponent<CharacterController2D>();
    }
    
    void Update()
    {
        
        //Debug.Log(facingDirectionChecker.eulerAngles.y);
        if (facingDirectionChecker.eulerAngles.y == 0)
        {
            foreach (Transform t in dialogParent)
            {
                transform.eulerAngles = t.eulerAngles = new Vector3(t.eulerAngles.x, 0, t.eulerAngles.z);
            }

            foreach (Transform t in dialogChildren)
            {
                transform.eulerAngles = t.eulerAngles = new Vector3(t.eulerAngles.x, 0, t.eulerAngles.z);
            }
            directionMultiplier = 1;
            
        } else if (facingDirectionChecker.eulerAngles.y == 180)
        {
            foreach(Transform t in dialogParent)
            {
                transform.eulerAngles = t.eulerAngles = new Vector3(t.eulerAngles.x, 180, t.eulerAngles.z);
            }
            foreach (Transform t in dialogChildren)
            {
                transform.eulerAngles = t.eulerAngles = new Vector3(t.eulerAngles.x, 180, t.eulerAngles.z);
            }
            directionMultiplier = -1;
        }
        transform.position = new Vector3(boneToFollow.position.x+(0.5f* directionMultiplier), boneToFollow.position.y+1f, boneToFollow.position.z);    
    }
}
