using System.Collections;
using UnityEngine;

public class SetStraw : MonoBehaviour
{
    [SerializeField] private GameObject _strawPrefab = null;
    [SerializeField] private AnimationCurve _spawnCurve = null;
    [SerializeField] private Transform _spawnStart = null;
    [SerializeField] private Transform _spawnEnd = null;
    [SerializeField] private float _spawnDuration = 0.5f;

    public void Enable(Transform parent)
    {
        StartCoroutine(StrawAnimation(parent));
    }

    private IEnumerator StrawAnimation(Transform parent)
    {
        GameObject straw = Instantiate(_strawPrefab, _spawnStart.position, _spawnStart.rotation);

        float timer = 0;
        while (timer < _spawnDuration)
        {
            straw.transform.position = Vector3.Lerp(_spawnStart.position, _spawnEnd.position, _spawnCurve.Evaluate(timer / _spawnDuration));

            timer += Time.deltaTime;
            yield return null;
        }
        straw.transform.parent = parent;
    }
}
