using UnityEngine;

namespace Vigilante
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform FollowObject;
        public Vector3 FollowOffset = new Vector3(default, -5, -10);

        private void Update()
        {
            Vector2 scrollDelta = Input.mouseScrollDelta;
            FollowOffset.z += scrollDelta.y;
        }

        private void LateUpdate() => transform.position = FollowObject.position + FollowOffset;
    }
}
