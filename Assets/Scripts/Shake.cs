using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private Vector2 _shakeAmount = Vector2.one;
    [SerializeField] private float _strongShakeMultiplier = 1.5f;

    private bool _shake = true;
    private bool _strongShake = false;

    private Vector3 _originPosition = Vector3.zero;

    public void TriggerShake(float duration)
    {
        _strongShake = true;
        Invoke("StopStrongShake", duration);
    }

    private void StopStrongShake()
    {
        _strongShake = false;
    }

    public void StopShake()
    {
        _shake = false;
    }

    private void Start()
    {
        _originPosition = transform.position;
        StartCoroutine(Shakeit());
    }

    private IEnumerator Shakeit()
    {
        while (_shake)
        {
            float x = Random.Range(-_shakeAmount.x, _shakeAmount.x);
            float y = Random.Range(-_shakeAmount.y, _shakeAmount.y);

            if (_strongShake)
            {
                x *= _strongShakeMultiplier;
                y *= _strongShakeMultiplier;
            }

            transform.position = _originPosition + new Vector3(x, y, _originPosition.z);
            yield return null;
        }
    }
}
