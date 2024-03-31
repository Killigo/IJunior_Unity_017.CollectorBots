using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RobotAnimation))]
[RequireComponent(typeof(RobotMover))]

public class Robot : MonoBehaviour
{
    [SerializeField] private float _takeDistance = 4;
    [SerializeField] private float _putDistance = 4;

    private Base _base;
    private GameObject _resourse;
    private GameObject _target;
    private RobotAnimation _robotAnimation;
    private RobotMover _robotMover;

    public bool IsIdle { get; private set; }

    private void Start()
    {
        _robotAnimation = GetComponent<RobotAnimation>();
        _robotMover = GetComponent<RobotMover>();
        _base = GetComponentInParent<Base>();

        IsIdle = true;
    }

    private void Update()
    {
        if (_resourse != null)
        {
            if (_target == _resourse && Vector3.Distance(transform.position, _resourse.transform.position) <= _takeDistance)
                TakeResource();

            if (_target == _base.gameObject && Vector3.Distance(transform.position, _base.transform.position) <= _putDistance)
                PutResource();
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            transform.LookAt(_target.transform);

            if (_target == _resourse)
                _robotMover.Roll(transform.forward);
            else if (_target == _base.gameObject)
                _robotMover.Walk(transform.forward);
        }
    }

    public void SetResource(GameObject resource)
    {
        _resourse = resource;
        IsIdle = false;
        StartCoroutine(RollActivate());
    }

    private IEnumerator RollActivate()
    {
        _robotAnimation.SetRoll();
        yield return new WaitForSeconds(2f);
        _target = _resourse;
    }

    private void TakeResource()
    {
        _target = null;
        StartCoroutine(WalkActivate());
    }

    private IEnumerator WalkActivate()
    {
        _robotAnimation.SetWalk();
        yield return new WaitForSeconds(2f);
        _resourse.transform.parent = transform;
        _target = _base.gameObject;
    }

    private void PutResource()
    {
        _resourse.GetComponent<Resource>().Destroy();
        _resourse = null;
        _target = null;
        IsIdle = true;
        _robotAnimation.SetIdle();
    }
}