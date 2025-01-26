using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Rigidbody _body = null;
    [SerializeField] private float _lifeTime = 20f;
    [SerializeField] private float _speed = 1f;

    private void OnEnable()
    {
        _body.linearVelocity = new Vector3(0, _speed, 0);
        Invoke("Destroy", _lifeTime);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
