using UnityEngine;

namespace YenTu.Characters
{
    /// <summary>
    /// Base Controller điều khiển chuyển động và tương tác của người chơi.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private float sprintMultiplier = 1.5f;
        [SerializeField] private float rotationSpeed = 10.0f;
        [SerializeField] private float gravity = -9.81f;

        [Header("Character Profile")]
        [SerializeField] private string characterName = "Minh";

        private CharacterController characterController;
        private Vector3 velocity;

        public string CharacterName => characterName;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {
            ApplyGravity();
        }

        public virtual void Move(Vector2 inputDirection, bool isSprinting)
        {
            if (characterController == null) return;

            Vector3 direction = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                float currentSpeed = moveSpeed * (isSprinting ? sprintMultiplier : 1.0f);
                characterController.Move(direction * currentSpeed * Time.deltaTime);
            }
        }

        private void ApplyGravity()
        {
            if (characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
    }
}

