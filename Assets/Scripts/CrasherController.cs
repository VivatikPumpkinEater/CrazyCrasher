using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CrasherController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] private Rig _crasherRig = null;
    
    [SerializeField] private Transform _target = null;
    [SerializeField] private Transform _targetHome = null;
    
    private Joystick _joystick = null;

    public Joystick JoystickInfo
    {
        get => _joystick;
        set => _joystick = value;
    }

    public Rig CrasherRig => _crasherRig;

    public Transform SawPosition => _targetHome;

    private Rigidbody _rigidbody = null;

    private Rigidbody _rb => _rigidbody = _rigidbody ? _rigidbody : _target.GetComponent<Rigidbody>();

    private void Start()
    {
        _joystick.FingerUp += ResetTarget;
    }

    private void FixedUpdate()
    {
        if (_joystick != null && _joystick.Vertical != 0 || _joystick.Horizontal != 0)
        {
            _rb.isKinematic = false;
            
            Vector3 movement = new Vector2(_joystick.Horizontal * _speed, _joystick.Vertical * _speed);
            
            //transform.Translate(movement * Time.deltaTime);
            
            _rb.MovePosition(_target.position + movement * Time.deltaTime);
        }

        if (Vector2.Distance(_target.position, _targetHome.position) > 0.5f)
        {
            ResetTarget();
        }
    }

    private void ResetTarget()
    {
        _rb.isKinematic = true;
        
        _target.position = _targetHome.position;
    }

    private void OnDestroy()
    {
        _joystick.FingerUp -= ResetTarget;
    }
}
