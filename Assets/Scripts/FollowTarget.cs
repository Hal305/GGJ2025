using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private float _smooth = 1f;
    [SerializeField] private Vector3 _offset = Vector3.zero;

    private Transform _target = null;

    private bool _follow = true;
    public bool Follow { set { _follow = value; } }

    private Vector3 _newPosition = Vector3.zero;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (!_follow || _target == null)
            return;

        _newPosition.x = _target.position.x;
        _newPosition.y = _target.position.y;
        _newPosition.z = transform.position.z;

        _newPosition += _offset;

        transform.position = Vector3.MoveTowards(transform.position, _newPosition, Time.deltaTime * _smooth * Vector3.Distance(transform.position, _newPosition));
    }
}
