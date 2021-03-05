using UnityEngine;

namespace Vigilante
{
    public class Bow : MonoBehaviour
    {
        [Range(100f, 5000f)] public float ArrowShootForce = 1000f;
        [Min(0)] public float ArrowDrawTime = 1f;

        [SerializeField] private Transform arrowPrefab;
        [SerializeField] private Transform referenceArrow;

        private Camera mainCamera;
        private CameraFollow cameraFollow;

        private Transform currentArrow;
        private bool canShoot;
        private float shootDeltaTime;

        private void Awake()
        {
            mainCamera = Camera.main;
            cameraFollow = mainCamera.GetComponent<CameraFollow>();
        }

        private void Update()
        {
            shootDeltaTime += Time.deltaTime;

            if (Input.GetButton("Fire2"))
            {
                Aim();

                if (Input.GetButtonUp("Fire1") && shootDeltaTime > ArrowDrawTime) CreateArrow();
            }
            else
                referenceArrow.gameObject.SetActive(false);
        }

        private void FixedUpdate() => TryShoot();

        private void Aim()
        {
            Vector3 mouseInput = Input.mousePosition;
            Vector3 position = transform.position;

            float zOffset = position.z - cameraFollow.FollowOffset.z;
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(mouseInput.x, mouseInput.y, zOffset));

            // Debug.DrawLine(position, mousePos);

            Vector3 relativePosition = transform.InverseTransformPoint(mousePos);
            float arrowAngle = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;
            var arrowRotation = Quaternion.AngleAxis(arrowAngle, Vector3.forward);

            referenceArrow.gameObject.SetActive(true);
            referenceArrow.rotation = arrowRotation;
        }

        private void CreateArrow()
        {
            referenceArrow.gameObject.SetActive(false);

            // TODO: get from pool
            currentArrow = Instantiate(arrowPrefab, referenceArrow.position, referenceArrow.rotation);
            currentArrow.rotation = referenceArrow.rotation;

            canShoot = true;
        }

        private void TryShoot()
        {
            if (canShoot)
            {
                shootDeltaTime = 0f;
                canShoot = false;

                Vector3 force = currentArrow.right * ArrowShootForce;
                Rigidbody arrowRigidbody = currentArrow.GetComponent<Rigidbody>();
                arrowRigidbody.AddForce(force * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        }
    }
}
