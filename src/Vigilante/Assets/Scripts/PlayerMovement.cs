using UnityEngine;

namespace Vigilante
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public float RunSpeed = 1f;

        private new Rigidbody rigidbody;

        private void Awake() => rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            float input = Input.GetAxisRaw("Horizontal");
            float speed = input * RunSpeed * Time.fixedDeltaTime;
            rigidbody.MovePosition(transform.position + (Vector3.right * speed));
        }
    }
}
