using UnityEngine;

namespace Vigilante
{
    public class Arrow : MonoBehaviour
    {
        public static readonly float DamageValue = 10f;
        public bool IsPlayerOwned;

        [SerializeField] [Min(0f)] private float ArrowheadMass = 1f;

        [SerializeField] private Transform arrowhead;
        private new Rigidbody rigidbody;

        private void Awake() => rigidbody = GetComponent<Rigidbody>();

        private void Start()
            => rigidbody.AddForceAtPosition(Vector3.down * ArrowheadMass, arrowhead.localPosition, ForceMode.Force);
    }
}
