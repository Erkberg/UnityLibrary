using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ErksUnityLibrary
{
    public class GroundChecker : MonoBehaviour
    {
        public enum PhysicsCastDimensionType
        {
            TwoD,
            ThreeD
        }

        public enum PhysicsCastColliderType
        {
            Box,
            Sphere,
            Capsule
        }

        public PhysicsCastDimensionType dimensionType;
        public PhysicsCastColliderType colliderType;

        [Space]
        public Transform castOrigin;
        public Vector3 colliderSize = new Vector3(1f, 1f, 1f);
        public float castDistance = 0.02f;
        public Vector3 castDirection = Vector3.down;

        [Space]
        public int groundCheckFrameInterval = 1;
        public LayerMask layersToCheckAgainst;

        [Header("State")]
        public bool isGrounded;
        public bool wasGroundedLastFrame;
        public Vector3 lastGroundedPosition;

        [Space]
        public bool drawGizmos = false;

        private int framesPassedSinceLastCheck = 0;
        private Vector3 hitPosition;

        private void Awake()
        {
            if (!castOrigin)
                castOrigin = transform;
        }

        private void Update()
        {
            wasGroundedLastFrame = isGrounded;

            framesPassedSinceLastCheck++;

            if (framesPassedSinceLastCheck >= groundCheckFrameInterval)
            {
                framesPassedSinceLastCheck = 0;
                isGrounded = CheckGround();
            }
        }

        private bool CheckGround()
        {
            bool hitGround = false;
            hitPosition = Vector3.zero;
            Vector3 origin = castOrigin.position;

            switch(dimensionType)
            {
                case PhysicsCastDimensionType.TwoD:
                    RaycastHit2D[] hits2D = GetColliderCastHits2D(origin);
                    if (hits2D != null)
                    {
                        foreach (RaycastHit2D hit in hits2D)
                        {
                            hitGround = true;
                            GameObject hitObject = hit.collider.gameObject;
                            float distance = Mathf.Abs(origin.y - hit.point.y);
                            //Debug.Log(hitObject.name + " " + distance + hit.point);
                            lastGroundedPosition = transform.position;

                            if (hit.point != Vector2.zero)
                                hitPosition = hit.point;
                            else
                                hitPosition = transform.position;

                            break;
                        }
                    }
                    break;

                case PhysicsCastDimensionType.ThreeD:
                    RaycastHit[] hits3D = GetColliderCastHits3D(origin);
                    if (hits3D != null)
                    {
                        foreach (RaycastHit hit in hits3D)
                        {
                            hitGround = true;
                            GameObject hitObject = hit.collider.gameObject;
                            float distance = Mathf.Abs(origin.y - hit.point.y);
                            //Debug.Log(hitObject.name + " " + distance + hit.point);
                            lastGroundedPosition = transform.position;                            

                            if (hit.point != Vector3.zero)                                
                                hitPosition = transform.position + castDirection * hit.distance;
                            else
                                hitPosition = transform.position;

                            break;
                        }
                    }
                    break;
            }

            return hitGround;
        }

        private RaycastHit2D[] GetColliderCastHits2D(Vector3 origin)
        {
            RaycastHit2D[] hits = null;
            int layerMask = layersToCheckAgainst.value;
            // Switch between volumes to cast
            switch (colliderType)
            {
                case PhysicsCastColliderType.Box:
                    hits = Physics2D.BoxCastAll(origin, colliderSize, 0f, castDirection, castDistance, layerMask);
                    break;
                case PhysicsCastColliderType.Sphere:
                    hits = Physics2D.CircleCastAll(origin, colliderSize.x * 2, castDirection, castDistance, layerMask);
                    break;
                case PhysicsCastColliderType.Capsule:
                    hits = Physics2D.CapsuleCastAll(origin, colliderSize, CapsuleDirection2D.Vertical, 0f, castDirection, castDistance, layerMask);
                    break;
            }

            return hits;
        }

        private RaycastHit[] GetColliderCastHits3D(Vector3 origin)
        {
            RaycastHit[] hits = null;
            int layerMask = layersToCheckAgainst.value;
            // Switch between volumes to cast
            switch (colliderType)
            {
                case PhysicsCastColliderType.Box:
                    hits = Physics.BoxCastAll(origin, colliderSize / 2, castDirection, Quaternion.identity, castDistance, layerMask);
                    break;
                case PhysicsCastColliderType.Sphere:
                    hits = Physics.SphereCastAll(origin, colliderSize.x * 2, castDirection, castDistance, layerMask);
                    break;
                case PhysicsCastColliderType.Capsule:
                    Vector3 pointsOffset = new Vector3(0f, colliderSize.y / 2, 0f);
                    hits = Physics.CapsuleCastAll(origin + pointsOffset, origin - pointsOffset, colliderSize.x, castDirection, castDistance, layerMask);
                    break;
            }

            return hits;
        }

        private void OnDrawGizmos()
        {
            if (drawGizmos && Application.isPlaying)
            {
                // Distance
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + castDistance * castDirection);

                // Origin      
                Gizmos.color = Color.white;
                switch (colliderType)
                {
                    case PhysicsCastColliderType.Box:
                        Gizmos.DrawWireCube(transform.position, colliderSize);
                        break;
                    case PhysicsCastColliderType.Sphere:
                        Gizmos.DrawWireSphere(transform.position, colliderSize.x * 2f);
                        break;
                    case PhysicsCastColliderType.Capsule:
                        /*Vector3 pointsOffset = new Vector3(0f, colliderSize.y / 2 - colliderSize.x, 0f);
                        Gizmos.DrawWireSphere(transform.position + pointsOffset, colliderSize.x);
                        Gizmos.DrawWireSphere(transform.position - pointsOffset, colliderSize.x);*/
                        break;
                }

                // Hit
                if(hitPosition != Vector3.zero)
                {
                    Gizmos.color = Color.cyan;

                    switch (colliderType)
                    {
                        case PhysicsCastColliderType.Box:
                            Gizmos.DrawWireCube(hitPosition, colliderSize);
                            break;
                        case PhysicsCastColliderType.Sphere:
                            Gizmos.DrawWireSphere(hitPosition, colliderSize.x * 2f);
                            break;
                        case PhysicsCastColliderType.Capsule:
                            //Gizmos.DrawWireSphere(hitPosition, colliderSize.x * 2f);
                            break;
                    }
                }
            }
        }
    }
}