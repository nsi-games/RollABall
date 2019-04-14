using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABall
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;                        // Target object to orbit around
        public bool hideCursor = true;                  // Is the cursor hidden?
        [Header("Orbit")]
        public Vector3 offset = new Vector3(0, 5f, 0);  // Vector offset from original position
        public float xSpeed = 120.0f;                   // X orbit speed
        public float ySpeed = 120.0f;                   // Y orbit speed
        public float yMinLimit = -20f;                  // minimum Y limit
        public float yMaxLimit = 80f;                   // maximum Y limit
        public float distanceMin = .5f;                 // Minimum distance to target
        public float distanceMax = 15f;                 // Maximum distance to target

        [Header("Collision")]
        public bool cameraCollision = true;             // Is camera collision enabled?
        public float camRadius = .3f;                   // Radius of the camera collision cast
        public LayerMask ignoreLayers;                  // Layers ignored by collision

        private Vector3 originalOffset;                 // Original offset from start of game
        private float distance;                         // Current distance to camera
        private float rayDistance = 1000f;              // Distance ray travels for collision

        private float x = 0.0f;                         // X degrees of rotation
        private float y = 0.0f;                         // Y degrees of rotation

        // Use this for initialization
        void Start()
        {
            // Is the cursor supposed to be hidden?
            if (hideCursor)
            {
                // Lock...
                Cursor.lockState = CursorLockMode.Locked;
                // ... and hide the cursor
                Cursor.visible = false;
            }

            // Calculate original offset from target position
            originalOffset = transform.position - target.position;
            // Set ray distance to current distance magnitude of Camera
            rayDistance = originalOffset.magnitude;

            // Get current camera rotation
            Vector3 angles = transform.eulerAngles;
            // Set X and Y degrees to current camera rotation
            x = angles.y;
            y = angles.x;
        }

        void FixedUpdate()
        {
            // If a target has been set
            if (target)
            {
                // Is camera collision enabled?
                if (cameraCollision)
                {
                    // Create a ray starting from target's position and pointing backwards from camera
                    Ray camRay = new Ray(target.position, -transform.forward);
                    RaycastHit hit;
                    // Shoot a sphere in defined ray direction
                    if (Physics.SphereCast(camRay, camRadius, out hit, rayDistance, ~ignoreLayers, QueryTriggerInteraction.Ignore))
                    {
                        // Set current camera distance to hit object's distance
                        distance = hit.distance;
                        // Exit the function
                        return;
                    }
                }
                // Set distance to original distance
                distance = originalOffset.magnitude;
            }
        }

        void Update()
        {
            // If a target has been set
            if (target)
            {
                // Rotate the camera based on Mouse X and Mouse Y inputs
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

                // Clamp the angle using a custom 'ClampAngle' function defined in this script
                y = ClampAngle(y, yMinLimit, yMaxLimit);

                // Rotate the transform using euler angles (y for X rotation and x for Y rotation)
                transform.rotation = Quaternion.Euler(y, x, 0);
            }
        }

        void LateUpdate()
        {
            // If a target has been set
            if (target)
            {
                // Calculate a localOffset from offset
                Vector3 localOffset = transform.TransformDirection(offset);
                // Reposition camera to new position, taking into account Distance & localOffset
                transform.position = (target.position + localOffset) + -transform.forward * distance;
            }
        }

        // Clamps the angle in between +360 to -360 degrees and using min and max angle
        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}