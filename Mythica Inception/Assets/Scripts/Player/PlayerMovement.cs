using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController Controller;
        public Transform Camera;
        public float Speed = 6f;
        public float TurnSmoothTime = 0.1f;
        public bool ApplyGravity = true;
        public bool Movable = true;
        
        
        #region PrivateVariables
        [field: SerializeField] private float Gravity { get; } = -55f;
        private float _turnSmoothVelocity;
        private float _horizontal;
        private float _vertical;
        private Vector3 _direction;
        private Vector3 _moveDirection;
        private float _targetAngle;
        private float _angle;
        private Vector3 _moveVector;
        
        #endregion

        
        
        void Update()
        {
            GetInput();
            
            _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

            if (_direction.magnitude >= 0.1f)
            {
                MoveAndRotate();
            }
            
            GravityApplication();
        }

        private void GravityApplication()
        {
            if (!ApplyGravity) return;
            
            _moveVector.y += Gravity;
            Controller.Move(_moveVector * Time.deltaTime);
            _moveVector = Vector3.zero;
        }

        private void MoveAndRotate()
        {
            if (!Movable) return;
            
            _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity,
                TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, _angle, 0f);
            _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            Controller.Move(_moveDirection.normalized * Speed * Time.deltaTime);
        }

        private void GetInput()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
        }
    }
}
