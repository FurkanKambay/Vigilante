using UnityEngine;

namespace Vigilante
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform FollowObject;
        public Vector3 FollowOffset = new Vector3(default, -5, -10);

        private void LateUpdate() => transform.position = FollowObject.position + FollowOffset;
    }
}
