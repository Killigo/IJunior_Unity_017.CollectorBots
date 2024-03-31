using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class RobotMover : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 300f;
    
    private float _rollSpeedMultiply = 3f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Walk(Vector3 direction)
    {
        _rigidbody.velocity = direction * _walkSpeed * Time.deltaTime;
    }

    public void Roll(Vector3 direction)
    {
        _rigidbody.velocity = direction * (_walkSpeed * _rollSpeedMultiply) * Time.deltaTime;
    }
}
