using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public Movementparameters movementParameters;
    public CollisionParameters collisionParameters;

    public float smoothSpeed;

    public int MovementDirection { get; private set; }

    private Transform mTransform;

    [Header("Debug")]

    private Vector2 deltaMovement;

    [NonSerialized]
    public MovementSystem movementSystem;
    [NonSerialized]
    public JumpGravitySystem jumpSystem;
    [NonSerialized]
    public CollisionSystem collisionSystem;
    [NonSerialized]
    public StairsSystem stairsSystem;

    [SerializeField]
    float turnSpeed;

    private Animator mAnimator;

    public bool MovementEnabled { get; set; } = true;

    private void Awake()
    {
        mTransform = transform;

        collisionSystem = GetComponent<CollisionSystem>();
        if (!collisionSystem)
        {
            collisionSystem = gameObject.AddComponent<CollisionSystem>();
        }

        movementSystem = GetComponent<MovementSystem>();
        if (!movementSystem)
        {
            movementSystem = gameObject.AddComponent<MovementSystem>();
        }

        jumpSystem = GetComponent<JumpGravitySystem>();
        if (!jumpSystem)
        {
            jumpSystem = gameObject.AddComponent<JumpGravitySystem>();
        }

        stairsSystem = GetComponent<StairsSystem>();
        if (!stairsSystem)
        {
            stairsSystem = gameObject.AddComponent<StairsSystem>();
        }
    }

    private void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
    }

    public void RequestHorizontal(float moveDirection)
    {
        // either -1, 0 or 1
        if (moveDirection != 0.0f)
        {
            moveDirection = Mathf.Sign(moveDirection);
        }

        movementSystem.Direction = moveDirection;
    }

    private void Update()
    {
        if (!MovementEnabled)
        {
            deltaMovement = Vector2.zero;
            return;
        }

        movementSystem.Calculate();
        jumpSystem.Calculate();

        Vector2 mTotalVelocity = new Vector2(
            movementSystem.AccumulatedVelocity,
            jumpSystem.AccumulatedVelocity.y);

        deltaMovement = mTotalVelocity * Time.deltaTime;

        stairsSystem.Calculate(ref deltaMovement);
        if (collisionSystem.enabled)
        {
            deltaMovement = collisionSystem.Calculate(deltaMovement);
        }

        movementSystem.UpdateCollisionData();
        jumpSystem.UpdateCollisionData();

        if (deltaMovement.x != 0.0f)
        {
            MovementDirection = deltaMovement.x > 0.0f ? 1 : 0;
            int direction = (int)Mathf.Sign(deltaMovement.x);

            if (previousDirection != direction)
            {
                Rotate(direction);
            }
        }

        mAnimator.SetBool("walk", deltaMovement.x != 0.0f);
    }

    int previousDirection = -100;

    private void Rotate(int direction)
    {
        if (rotateLeftCoroutine != null)
        {
            StopCoroutine(rotateLeftCoroutine);
        }

        if (rotateRightCoroutine != null)
        {
            StopCoroutine(rotateRightCoroutine);
        }

        if (direction > 0)
        {
            rotateRightCoroutine = StartCoroutine(RotateRight());
        }
        else
        {
            rotateLeftCoroutine = StartCoroutine(RotateLeft());
        }

        previousDirection = direction;
    }

    Coroutine rotateLeftCoroutine;
    Coroutine rotateRightCoroutine;

    IEnumerator RotateLeft()
    {
        float rotation = mTransform.eulerAngles.y;
        while (rotation > -180.0f)
        {
            rotation -= Time.deltaTime * turnSpeed;
            mTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            yield return null;
        }
        mTransform.rotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
    }

    IEnumerator RotateRight()
    {
        float rotation = - Mathf.Abs(mTransform.eulerAngles.y);
        while (rotation < 0.0f)
        {
            rotation += Time.deltaTime * turnSpeed;
            mTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            yield return null;
        }
        mTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    private void LateUpdate()
    {
        ResolvePosition();
        movementSystem.ClearVariablesEndFrame();
    }

    private void ResolvePosition()
    {
        float y = mTransform.position.y + deltaMovement.y;

        mTransform.position = new Vector3(
                Mathf.Lerp(
                    mTransform.position.x,
                    mTransform.position.x + deltaMovement.x,
                    smoothSpeed * Time.deltaTime),
                y,
                /*
                Mathf.Lerp(
                     mTransform.position.y,
                     y,
                    smoothSpeed * Time.deltaTime),
                 */
                mTransform.position.z);
    }
}
