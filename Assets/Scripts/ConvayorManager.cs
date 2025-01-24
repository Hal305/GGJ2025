using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ConvayorManager : MonoBehaviour
{
    [SerializeField] private List<PowerSelector> _selectors = new List<PowerSelector>();
    [SerializeField] private List<Transform> _dispensers = new List<Transform>();
    [SerializeField] private Transform _bobaCup = null;
    [SerializeField] private Transform _table = null;
    [SerializeField] private AnimationCurve _movementCurve = null;
    [SerializeField] private AnimationCurve _rotationCurve = null;
    [SerializeField] private float _moveDuration = 1f;

    private int _currentIndex = -1;

    private void Start()
    {
        foreach (var selector in _selectors)
        {
            selector.onSelected += NextSelector;
        }
        StarterSelector();
    }

    private void StarterSelector()
    {
        _currentIndex = 0;
        _selectors[_currentIndex].Enable();

        StartCoroutine(MoveToNextSelector());
    }

    private void NextSelector(float power)
    {
        ++_currentIndex;

        if (_currentIndex >= _selectors.Count)
        {
            StartCoroutine(MoveToTable());
        }
        else
        {
            _selectors[_currentIndex].Enable();
            StartCoroutine(MoveToNextSelector());
        }
    }
    
    private IEnumerator MoveToNextSelector()
    {
        Vector3 startPosition = _bobaCup.position;
        Vector3 endPosition = new Vector3(_dispensers[_currentIndex].transform.position.x, _bobaCup.position.y, _bobaCup.position.z);

        Vector3 startRotation = _bobaCup.eulerAngles;
        Vector3 endRotation = new Vector3(_bobaCup.eulerAngles.x, _bobaCup.eulerAngles.y, _bobaCup.eulerAngles.z + 20f);

        float timer = 0;
        while (timer < _moveDuration)
        {
            _bobaCup.position = Vector3.Lerp(startPosition, endPosition, _movementCurve.Evaluate(timer / _moveDuration));
            _bobaCup.eulerAngles = Vector3.Lerp(startRotation, endRotation, _rotationCurve.Evaluate(timer / _moveDuration));

            timer += Time.deltaTime;
            yield return null;
        }
        _bobaCup.eulerAngles = startRotation;
    }

    private IEnumerator MoveToTable()
    {
        Vector3 startPosition = _bobaCup.position;
        Vector3 endPosition = new Vector3(_table.position.x, _bobaCup.position.y, _bobaCup.position.z);

        Vector3 startRotation = _bobaCup.eulerAngles;
        Vector3 endRotation = new Vector3(_bobaCup.eulerAngles.x, _bobaCup.eulerAngles.y, _bobaCup.eulerAngles.z + 20f);

        float timer = 0;
        while (timer < _moveDuration)
        {
            _bobaCup.position = Vector3.Lerp(startPosition, endPosition, _movementCurve.Evaluate(timer / _moveDuration));
            _bobaCup.eulerAngles = Vector3.Lerp(startRotation, endRotation, _rotationCurve.Evaluate(timer / _moveDuration));

            timer += Time.deltaTime;
            yield return null;
        }
        _bobaCup.eulerAngles = startRotation;
    }
}
