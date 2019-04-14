using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABall
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 10f;
        public float jump = 10f;
        public Rigidbody rigid;
        public bool moveWithCamera = false;
        public LayerMask ignoreLayers;
        public float rayDistance = 10f;

        public bool isGrounded = false;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
        }
        private void FixedUpdate()
        {
            Ray groundRay = new Ray(transform.position, Vector3.down);
            isGrounded = Physics.Raycast(groundRay, rayDistance, ~ignoreLayers);
        }
        private void Update()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            Move(inputH, inputV);
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        public void Jump()
        {
            if (isGrounded)
            {
                rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }
        }

        public void Move(float inputH, float inputV)
        {
            Vector3 direction = new Vector3(inputH, 0, inputV);

            if (moveWithCamera)
            {
                Vector3 euler = Camera.main.transform.eulerAngles;
                direction = Quaternion.Euler(0, euler.y, 0) * direction;
            }

            rigid.AddForce(direction * speed);
        }

        private void OnTriggerEnter(Collider col)
        {
            Collectable c = col.GetComponent<Collectable>();
            if (c)
            {
                c.Collect();
            }
        }
    }
}
