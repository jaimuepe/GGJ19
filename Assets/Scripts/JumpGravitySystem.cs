using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGravitySystem : MonoBehaviour
{
    public bool Jumping { get { return jumping; } set { jumping = value; } }

    public bool Grounded { get { return grounded; } }

    public bool Airborne { get { return !Grounded; } }

    public Vector2 AccumulatedVelocity { get { return mAccumulatedVelocity; } }

    private float JumpMagnitude { get { return cc.movementParameters.jumpMagnitude; } }

    private float MaxJumpTimeSeconds { get { return cc.movementParameters.jumpTimeSeconds; } }

    private Vector2 GravityVector { get { return cc.movementParameters.gravityVector; } }

    [Header("Debug")]

    [SerializeField]
    private bool grounded;

    [SerializeField]
    private bool falling;

    [SerializeField]
    private bool jumping;

    [SerializeField]
    private Vector2 mAccumulatedVelocity;

    [SerializeField]
    private float airborneTime;

    private CharacterController2D cc;

    private bool wasJumping;

    private void Awake()
    {
        cc = GetComponent<CharacterController2D>();
        airborneTime = MaxJumpTimeSeconds;
    }

    public void Calculate()
    {
        falling = false;

        if (Grounded && Jumping)
        {
            mAccumulatedVelocity = -JumpMagnitude * GravityVector.normalized;
            airborneTime = 0.0f;
        }
        else if (Airborne)
        {
            if (airborneTime < MaxJumpTimeSeconds)
            {
                // going up
                mAccumulatedVelocity = -JumpMagnitude * GravityVector.normalized
                    * Mathf.Lerp(0.0f, 1.0f, MaxJumpTimeSeconds - airborneTime);
            }
            else
            {
                // falling
                mAccumulatedVelocity = GravityVector
                    * Mathf.Lerp(0.0f, 1.0f, airborneTime - MaxJumpTimeSeconds);
            }
            airborneTime += Time.deltaTime;
        }
        else
        {
            airborneTime = 0.0f;
        }
    }

    public void TryJump()
    {
        if (Grounded)
        {
            jumping = true;
        }
    }

    public void UpdateCollisionData()
    {
        CollisionData cData = cc.collisionSystem.Data;
        int collisionSides = cData.collisionSides;

        if ((collisionSides & CollisionData.COLLIDE_BOTTOM) > 0)
        {
            grounded = true;
            Jumping = false;

            airborneTime = 0.0f;
            mAccumulatedVelocity = Vector2.zero;
        }
        else
        {
            grounded = false;
        }

        if ((collisionSides & CollisionData.COLLIDE_TOP) > 0)
        {
            airborneTime = MaxJumpTimeSeconds;
        }
    }

    public void ClearVariablesEndFrame()
    {
        wasJumping = Jumping;
    }
}
