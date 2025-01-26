using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class ConvayorManager : MonoBehaviour
{
    [SerializeField] private List<Dispenser> _dispensers = new List<Dispenser>();
    [SerializeField] private SetStraw _strawSetter = null;
    [SerializeField] private Transform _table = null;
    [SerializeField] private AnimationCurve _movementCurve = null;
    [SerializeField] private AnimationCurve _rotationCurve = null;
    [SerializeField] private float _moveDuration = 1f;
    [SerializeField] private ArrowMovement _arrow = null;

    [Header("Boba Cup")]
    [SerializeField] private BobaCup _bobaCupPrefab = null;
    [SerializeField] private Transform _spawnStartPoint = null;
    [SerializeField] private Transform _spawnEndPoint = null;
    [SerializeField] private AnimationCurve _spawnCurve = null;
    [SerializeField] private AnimationCurve _spawnRotationCurve = null;
    [SerializeField] private float _spawnDuration = 1.0f;

    [Header("Camera")]
    [SerializeField] private FollowTarget _camera = null;

    private BobaCup _bobaCup = null;

    private int _currentIndex = -1;

    private void Start()
    {
        foreach (var selector in _dispensers)
        {
            selector.OnDifficultySelected += DifficultySelected;
        }

        _camera.SetTarget(_spawnEndPoint);

        SpawnCup();
    }

    private void SpawnCup()
    {
        _bobaCup = Instantiate(_bobaCupPrefab, _spawnStartPoint.position, _spawnStartPoint.rotation);
        StartCoroutine(SpawnAnimation());
    }

    private IEnumerator SpawnAnimation()
    {
        yield return new WaitForSeconds(1f);

        Vector3 startRotation = new Vector3(_bobaCup.transform.eulerAngles.x, _bobaCup.transform.eulerAngles.y, _bobaCup.transform.eulerAngles.z - 10f);
        Vector3 endRotation = new Vector3(_bobaCup.transform.eulerAngles.x, _bobaCup.transform.eulerAngles.y, _bobaCup.transform.eulerAngles.z + 10f);

        float timer = 0;
        while (timer < _spawnDuration)
        {
            _bobaCup.transform.position = Vector3.Lerp(_spawnStartPoint.position, _spawnEndPoint.position, _spawnCurve.Evaluate(timer / _spawnDuration));
            _bobaCup.transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, _spawnRotationCurve.Evaluate(timer / _spawnDuration));

            timer += Time.deltaTime;
            yield return null;
        }
        _bobaCup.transform.position = _spawnEndPoint.position;

        _camera.SetTarget(_bobaCup.transform);

        yield return new WaitForSeconds(1f);
        StarterSelector();
    }

    private void StarterSelector()
    {
        _currentIndex = 0;
        _dispensers[_currentIndex].ActivateSelector();
        _arrow.ToggleAnimator(true);

        StartCoroutine(MoveToNextSelector());
    }

    private void DifficultySelected(Color color, float difficulty)
    {
        Shakies();
        ArmMovement.Instance.TriggerAnim();
        _arrow.ToggleAnimator(false);
        StartCoroutine(FillCup(color, difficulty));
    }

    private void StrawSet()
    {

    }
    
    private IEnumerator FillCup(Color color, float difficulty)
    {
        if (_dispensers[_currentIndex].DispenserType == Dispenser.Type.Water)
        {
            _dispensers[_currentIndex].SetParticleColor(color);
            _bobaCup.FillCup(color);
        }
        else if (_dispensers[_currentIndex].DispenserType == Dispenser.Type.Boba)
        {
            _bobaCup.AddBoba();
        }

        _dispensers[_currentIndex].PlayParticles();

        yield return new WaitForSeconds(1f);

        // Go to next selector or table
        _dispensers[_currentIndex].DeactivateSelector();
        ++_currentIndex;

        if (_currentIndex >= _dispensers.Count)
        {
            StartCoroutine(MoveToStraw());
            yield return new WaitForSeconds(_moveDuration);
            _strawSetter.Enable(_bobaCup.transform);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(MoveToTable());
        }
        else
        {
            _arrow.ToggleAnimator(true);
            _dispensers[_currentIndex].ActivateSelector(difficulty);
            StartCoroutine(MoveToNextSelector());
        }
    }

    private void Shakies()
    {
        Shake[] shakes = (Shake[])FindObjectsByType(typeof(Shake), FindObjectsSortMode.None);
        foreach (var shake in shakes)
        {
            shake.TriggerShake(1.0f);
        }
    }

    private IEnumerator MoveToStraw()
    {
        Vector3 startPosition = _bobaCup.transform.position;
        Vector3 endPosition = new Vector3(_strawSetter.transform.position.x, _bobaCup.transform.position.y, _bobaCup.transform.position.z);

        Vector3 startRotation = _bobaCup.transform.eulerAngles;
        Vector3 endRotation = new Vector3(_bobaCup.transform.eulerAngles.x, _bobaCup.transform.eulerAngles.y, _bobaCup.transform.eulerAngles.z + 20f);

        float timer = 0;
        while (timer < _moveDuration)
        {
            _bobaCup.transform.position = Vector3.Lerp(startPosition, endPosition, _movementCurve.Evaluate(timer / _moveDuration));
            _bobaCup.transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, _rotationCurve.Evaluate(timer / _moveDuration));

            timer += Time.deltaTime;
            yield return null;
        }
        _bobaCup.transform.eulerAngles = startRotation;
    }

    private IEnumerator MoveToNextSelector()
    {
        Vector3 startPosition = _bobaCup.transform.position;
        Vector3 endPosition = new Vector3(_dispensers[_currentIndex].transform.position.x, _bobaCup.transform.position.y, _bobaCup.transform.position.z);

        Vector3 startRotation = _bobaCup.transform.eulerAngles;
        Vector3 endRotation = new Vector3(_bobaCup.transform.eulerAngles.x, _bobaCup.transform.eulerAngles.y, _bobaCup.transform.eulerAngles.z + 20f);

        float timer = 0;
        while (timer < _moveDuration)
        {
            _bobaCup.transform.position = Vector3.Lerp(startPosition, endPosition, _movementCurve.Evaluate(timer / _moveDuration));
            _bobaCup.transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, _rotationCurve.Evaluate(timer / _moveDuration));

            timer += Time.deltaTime;
            yield return null;
        }
        _bobaCup.transform.eulerAngles = startRotation;
    }

    private IEnumerator MoveToTable()
    {
        Vector3 startPosition = _bobaCup.transform.position;
        Vector3 endPosition = new Vector3(_table.position.x, _bobaCup.transform.position.y, _bobaCup.transform.position.z);

        Vector3 startRotation = _bobaCup.transform.eulerAngles;
        Vector3 endRotation = new Vector3(_bobaCup.transform.eulerAngles.x, _bobaCup.transform.eulerAngles.y, _bobaCup.transform.eulerAngles.z + 20f);

        float timer = 0;
        while (timer < _moveDuration)
        {
            _bobaCup.transform.position = Vector3.Lerp(startPosition, endPosition, _movementCurve.Evaluate(timer / _moveDuration));
            _bobaCup.transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, _rotationCurve.Evaluate(timer / _moveDuration));

            timer += Time.deltaTime;
            yield return null;
        }
        _bobaCup.transform.eulerAngles = startRotation;

        yield return new WaitForSeconds(0.5f);
        TransitionUI.Instance.PlayTransition(2, 1);
    }
}
