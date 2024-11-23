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
        private UnityEvent _onColliderEnter;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
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

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                speed = 50f;
            }
            else
                speed = 5f;
            //HandleJump();
            //UpdateAnimatorGroundedState();


            HandleMovement();
            HandleRotation();
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            if (Input.GetKey(KeyCode.C))
            {
                GameManager.Instance.CurrentSequentialTask.SkipCurrentTask();
            }
        }

        private void HandleMovement()
        {
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = transform.forward * vertical * speed;

            // Preserve the current vertical velocity (gravity)
            movement.y = _rb.velocity.y;

            _rb.velocity = movement;
        }

        private void HandleRotation()
        {
            float horizontal = Input.GetAxis("Horizontal");

            Vector3 rot = transform.up * horizontal * rotationSpeedFactor;

            _rb.angularVelocity = rot;
        }


        //private void HandleJump()
        //{
        //    if (Input.GetKey(KeyCode.Space) && _shoeCollider.OnGround)
        //    {
        //        Vector3 jumpMovement = Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        //        _rb.Move(jumpMovement * Time.deltaTime);
        //        _shoeCollider.OnGround = false;
        //        _playerData.Animator.SetFloat("VerticalSpeed", jumpForce);
        //    }
        //}

        //private void UpdateAnimatorGroundedState()
        //{
        //    // Update the Grounded state in the Animator
        //    bool isGrounded = _rb.isGrounded;
        //    _playerData.Animator.SetBool("Grounded", isGrounded);
        //}

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collided");
            var outliner = collision.gameObject.GetComponent<Outline>();

            if (outliner != null && outliner.IsOutLined)
            {
                outliner.DisableOutline();
                _onColliderEnter?.Invoke();
            }
        }
    }
}
