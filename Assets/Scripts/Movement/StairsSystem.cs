using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSystem : MonoBehaviour
{

    private Transform mTransform;
    private LayerMask stairsLayer;

    private Vector2 Position { get { return mTransform.position; } }
    private CollisionSystem collisionSystem;
    private JumpGravitySystem gravitySystem;

    private InputController inputController;

    public bool UsingStairs { get; private set; }

    private void Awake()
    {
        mTransform = transform;
    }

    void Start()
    {
        stairsLayer = LayerMask.GetMask("Stairs");
        collisionSystem = GetComponent<CollisionSystem>();
        gravitySystem = GetComponent<JumpGravitySystem>();
        inputController = FindObjectOfType<InputController>();
    }

    public void Calculate(ref Vector2 deltaMovement)
    {
        float xOffset = 0.5f
            * (collisionSystem.BottomRightCornerOffset.x - collisionSystem.BottomLeftCornerOffset.x);

        float yOffset = 0.5f
            * (collisionSystem.TopLeftCornerOffset.y - collisionSystem.BottomLeftCornerOffset.y);

        float rayDistance = 0.3f;

        Vector2 rayOrigin = Position + new Vector2(0.0f, 0.1f);

        Vector2 direction = Vector2.down;

        Debug.DrawRay(rayOrigin, direction * rayDistance, Color.black, 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayDistance, stairsLayer);

        if (hit)
        {
            if (hit.point.y < Position.y)
            {
                // down
                if (gravitySystem.Grounded)
                {
                    if (inputController.VerticalInput < 0.0f)
                    {
                        UsingStairs = true;
                        deltaMovement.y = hit.point.y - Position.y;
                    }
                    else
                    {
                        UsingStairs = false;
                    }
                }
                else
                {
                    UsingStairs = true;
                    deltaMovement.y = hit.point.y - Position.y;
                }
            }
            else
            {
                // up
                if (gravitySystem.Grounded)
                {
                    if (inputController.VerticalInput > 0.0f)
                    {
                        UsingStairs = true;
                        deltaMovement.y = hit.point.y - Position.y;
                    }
                    else
                    {
                        UsingStairs = false;
                    }
                }
                else
                {
                    UsingStairs = true;
                    deltaMovement.y = hit.point.y - Position.y;
                }
            }
        }
        else
        {
            UsingStairs = false;
        }

        if (UsingStairs)
        {
            collisionSystem.enabled = false;
        }
        else
        {
            collisionSystem.enabled = true;
        }
    }
}
