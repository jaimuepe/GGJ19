using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class CollisionSystem : MonoBehaviour
{
    private int NumberOfVerticalRays { get { return cc.collisionParameters.numberOfVerticalRays; } }
    private int NumberOfHorizontalRays { get { return cc.collisionParameters.numberOfHorizontalRays; } }
    private float SkinWidth { get { return cc.collisionParameters.skinWidth; } }

    private Vector2 mDeltaMovement;
    [SerializeField]
    private BoxCollider2D boxCollider;

    public Vector2 TopLeftCornerOffset { get; private set; }
    public Vector2 BottomLeftCornerOffset { get; private set; }
    public Vector2 BottomRightCornerOffset { get; private set; }

    private Transform mTransform;
    private Vector2 Position { get { return mTransform.position; } }

    private LayerMask obstacleLayer;
    private readonly RaycastHit2D[] raycastHitResults = new RaycastHit2D[3];

    private CollisionData data;
    public CollisionData Data { get { return data; } }

    CharacterController2D cc;

    public delegate void OnCollisionDelegate(CollisionData data);

    private void Awake()
    {
        cc = GetComponent<CharacterController2D>();

        if (!boxCollider)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        if (!boxCollider)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
        }

        obstacleLayer = LayerMask.GetMask("Obstacle");

        mTransform = transform;
        CalculateCornerOffsets();
    }

    private void CalculateCornerOffsets()
    {
        Vector2 extents = boxCollider.bounds.extents;

        TopLeftCornerOffset = new Vector2(
           -extents.x,
           extents.y);

        BottomLeftCornerOffset = new Vector2(
           -extents.x,
           -extents.y);

        BottomRightCornerOffset = new Vector2(
           extents.x,
           -extents.y);
    }

    public Vector2 Calculate(Vector2 deltaMovement)
    {
        data.colliding = false;
        data.collisionSides = 0;

        mDeltaMovement = deltaMovement;

        HandleVerticalCollisions();
        HandleHorizontalCollisions();

        return mDeltaMovement;
    }

    private void HandleVerticalCollisions()
    {
        Vector2 origin = Position + SkinWidth * Vector2.right + (mDeltaMovement.y > 0.0f ? TopLeftCornerOffset : BottomLeftCornerOffset);

        Vector2 direction = mDeltaMovement.y > 0.0f ? Vector2.up : Vector2.down;
        float rayDistance = Mathf.Abs(mDeltaMovement.y);

        float distanceBetweenRays = (BottomRightCornerOffset.x - BottomLeftCornerOffset.x - 2 * SkinWidth) / (NumberOfVerticalRays - 1);

        bool collision = false;

        for (int i = 0; i < NumberOfVerticalRays; i++)
        {
            Vector2 rayOrigin = origin + i * distanceBetweenRays * Vector2.right;

            int hits = Physics2D.RaycastNonAlloc(rayOrigin, direction, raycastHitResults, rayDistance, obstacleLayer);
            if (hits > 0)
            {
                collision = true;
                for (int j = 0; j < hits; j++)
                {
                    RaycastHit2D hit = raycastHitResults[j];
                    float hitDistance = hit.distance;

                    CollisionObject collisionObject = hit.collider.gameObject.GetComponent<CollisionObject>();

                    if (direction.y > 0.0f && collisionObject.collideFromBottom
                        || direction.y < 0.0f && collisionObject.collideFromTop)
                    {
                        rayDistance = Mathf.Min(rayDistance, hitDistance);
                    }
                }
            }

            Debug.DrawRay(rayOrigin, rayDistance * direction, Color.red);
        }

        if (collision)
        {
            data.colliding = true;
            if (mDeltaMovement.y > 0.0f)
            {
                data.collisionSides |= CollisionData.COLLIDE_TOP;
            }
            else
            {
                data.collisionSides |= CollisionData.COLLIDE_BOTTOM;
            }
        }

        mDeltaMovement.y = Mathf.Sign(mDeltaMovement.y) * rayDistance;
    }

    private void HandleHorizontalCollisions()
    {
        Vector2 origin = Position + SkinWidth * Vector2.up + (mDeltaMovement.x > 0.0f ? BottomRightCornerOffset : BottomLeftCornerOffset);

        Vector2 direction = mDeltaMovement.x > 0.0f ? Vector2.right : Vector2.left;
        float rayDistance = Mathf.Abs(mDeltaMovement.x);

        float distanceBetweenRays = (TopLeftCornerOffset.y - BottomLeftCornerOffset.y - 2 * SkinWidth) / (NumberOfHorizontalRays - 1);

        bool collision = false;

        for (int i = 0; i < NumberOfHorizontalRays; i++)
        {
            Vector2 rayOrigin = origin + i * distanceBetweenRays * Vector2.up;

            int hits = Physics2D.RaycastNonAlloc(rayOrigin, direction, raycastHitResults, rayDistance, obstacleLayer);
            if (hits > 0)
            {
                collision = true;
                for (int j = 0; j < hits; j++)
                {
                    RaycastHit2D hit = raycastHitResults[j];
                    CollisionObject collisionObject = hit.collider.gameObject.GetComponent<CollisionObject>();

                    if (direction.x > 0.0f && collisionObject.collideFromLeft
                        || direction.x < 0.0f && collisionObject.collideFromRight)
                    {
                        float hitDistance = hit.distance;
                        rayDistance = Mathf.Min(rayDistance, hitDistance);
                    }

                }
            }

            Debug.DrawRay(rayOrigin, rayDistance * direction, Color.red);
        }

        if (collision)
        {
            data.colliding = true;

            if (mDeltaMovement.x > 0.0f)
            {
                data.collisionSides |= CollisionData.COLLIDE_RIGHT;
            }
            else
            {
                data.collisionSides |= CollisionData.COLLIDE_LEFT;
            }
        }

        mDeltaMovement.x = Mathf.Sign(mDeltaMovement.x) * rayDistance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Position + TopLeftCornerOffset, 0.1f * Vector3.one);
        Gizmos.DrawCube(Position + BottomLeftCornerOffset, 0.1f * Vector3.one);
        Gizmos.DrawCube(Position + BottomRightCornerOffset, 0.1f * Vector3.one);
    }
}
