using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    public class PlayerActions : MonoBehaviour
    {
        
        public Camera Camera;
        public float Speed = 6f;
        public float TurnSmoothTime = 0.05f;
        public bool ApplyGravity = true;
        public bool CanAct = true;
        public float AttackRate = 0.01f;

        

        #region Hidden Variables
        
        private CharacterController _controller;
        private UnityAction _playerActions;
        private float Gravity { get; } = -40f;
        private float _turnSmoothVelocity;
        private float _horizontal;
        private float _vertical;
        private Vector3 _direction;
        private Vector3 _moveDirection;
        private float _targetAngle;
        private float _angle;
        private Vector3 _gravityVector;
        private Vector3 _dashVector = Vector3.zero;
        private float _currentDashTime = 0f;
        private float _dashTime = 1f;
        private bool _isDashing;
        private bool _canAttack = true;
        [HideInInspector] public Transform Target; 
        private float _currentAttackTimer = 0f;
        private float _currentSmoothTime;
        
        #endregion

        void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _currentSmoothTime = TurnSmoothTime;
        }
        
        void Start()
        {
            _currentDashTime = _dashTime;
            
            //subscribe to actions here
            _playerActions += MoveAction;
            _playerActions += DashAction;
            _playerActions += AttackAction;
        }
        
        
        void Update()
        {
            GetInput();
            GravityApplication();
        }

        private void GetInput()
        {
            _playerActions.Invoke();
        }

        private void MoveAction()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
            _direction = new Vector3(_horizontal, 0f, _vertical).normalized;
            
            if (!(_direction.magnitude >= 0.1f)) return;
            
            MoveAndRotate();
        }

        private void DashAction()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _currentDashTime = 0f;
                _isDashing = true;
                CanAct = false;
            }

            if (_currentDashTime < _dashTime)
            {
                _dashVector = transform.rotation * Vector3.forward * Speed * (Speed/4);
                _currentDashTime += 0.1f;
            }
            else
            {
                _dashVector = Vector3.zero;
                _isDashing = false;
            }

            if (_isDashing)
            {
                _controller.Move( _dashVector * Time.deltaTime);
                CanAct = true;
            }
        }

        private void AttackAction()
        {
            if(!CanAct) return;
            
            if (_currentAttackTimer > AttackRate)
            {
                _canAttack = true;
            }
            else
            {
                _currentAttackTimer += Time.deltaTime;
                _canAttack = false;
            }
            if(!_canAttack) return;
            
            FaceTarget();
            if (Target == null) return;
            transform.LookAt(Target);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
            Target = null;
        }

        private void MoveAndRotate()
        {
            if (!CanAct) return;
            
            _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + Camera.transform.eulerAngles.y;
            _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity,
                _currentSmoothTime);
            transform.rotation = Quaternion.Euler(0f, _angle, 0f);
            _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            _controller.Move(_moveDirection.normalized * Speed * Time.deltaTime);
        }

        private void FaceTarget()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            _currentSmoothTime *= 3;
            _currentAttackTimer = 0f;
            RaycastHit hit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //if you clicked an enemy
                if (hit.transform.CompareTag("Enemy"))
                {
                    Target = hit.transform;
                    //Debug.Log("Attack facing at " + Target.name);
                }
                else
                {
                    //if not, then face towards where you clicked
                    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                    float rayLength;
                
                    if (groundPlane.Raycast(ray, out rayLength))
                    {
                        Vector3 pointToLook = ray.GetPoint(rayLength);
                        Vector3 faceTo = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
                        transform.LookAt(faceTo);
                        //Debug.Log("Attack facing at " + faceTo.ToString());
                    }  
                }
            }

            StartCoroutine("SmoothTimeDelay", 1.5f);
        }

        IEnumerator SmoothTimeDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _currentSmoothTime = TurnSmoothTime;
        }
        
        private void GravityApplication()
        {
            if (!ApplyGravity) return;
            
            _gravityVector.y += Gravity;
            _controller.Move(_gravityVector * Time.deltaTime);
            _gravityVector = Vector3.zero;
        }
    }
}
