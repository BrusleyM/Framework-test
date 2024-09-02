using System;
using Framework.CheckPoints;
using UnityEngine;
using UnityEngine.Events;

namespace FrameworkTest
{
    public class Ellen : MonoBehaviour
    {
        [SerializeField]
        private PlayerData _playerData;
        [SerializeField]
        private float speed = 5f;
        [SerializeField]
        private float rotationSpeedFactor = 5f;
        [SerializeField]
        private float jumpForce = 0.2f;
        [SerializeField]
        private float moveForce = 1f;
        [SerializeField]
        private Grounded _shoeCollider;
        [SerializeField]
        private UnityEvent _onColliderEnter;

        private CharacterController controller;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void OnValidate()
        {
            if (_playerData == null)
            {
                _playerData = new PlayerData(gameObject.name, 100, 0, GetComponent<Animator>());
            }
        }

        private void Start()
        {
            //_playerData.Animator.SetBool("Grounded", _shoeCollider.OnGround);
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            if (Input.GetKeyDown(KeyCode.R))
            {
                speed = 50f;
            }
            else
                speed = 5f;
            //HandleJump();
            //UpdateAnimatorGroundedState();
        }

        private void HandleMovement()
        {
            float vertical = -Input.GetAxis("Vertical");
            Vector3 movement = transform.forward * vertical;
            movement = movement.normalized * speed * Time.deltaTime;

            controller.Move(movement);
            //_playerData.Animator.SetFloat("ForwardSpeed", vertical);
        }

        private void HandleRotation()
        {
            float horizontal = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, horizontal * rotationSpeedFactor * Time.deltaTime);
        }

        private void HandleJump()
        {
            if (Input.GetKey(KeyCode.Space) && _shoeCollider.OnGround)
            {
                Vector3 jumpMovement = Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
                controller.Move(jumpMovement * Time.deltaTime);
                _shoeCollider.OnGround = false;
                _playerData.Animator.SetFloat("VerticalSpeed", jumpForce);
            }
        }

        private void UpdateAnimatorGroundedState()
        {
            // Update the Grounded state in the Animator
            bool isGrounded = controller.isGrounded;
            _playerData.Animator.SetBool("Grounded", isGrounded);
        }
        private void OnCollisionEnter(Collision collision)
        {
            var ouliner = collision.gameObject.GetComponent<Outline>();
            var checkpoint = collision.gameObject.GetComponent<CheckPoint>();
            if (ouliner.IsOutLined)
            {
                checkpoint.MarkAsChecked();
                _onColliderEnter?.Invoke();
                
            }
        }
    }
}
