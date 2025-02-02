using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionUI : MonoBehaviour
{
    [SerializeField] private Transform[] _visual;
    [SerializeField] private float _duration = 1.0f;
    [SerializeField] private float _startScale = 0;
    [SerializeField] private float _endScale = 100;
    [SerializeField] private float _rotationSpeed = 10;
    
    private bool _continue = false;

    public static TransitionUI Instance;

    [ContextMenu("Continue")]
    public void Continue()
    {
        _continue = true;
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            foreach (var visual in _visual)
                visual.transform.localScale = Vector3.one * _startScale;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Update()
    {
        foreach (var visual in _visual)
            visual.transform.Rotate(Vector3.forward, Time.deltaTime * _rotationSpeed);
    }

    [ContextMenu("Play Transition")]
    public void PlayTransition(int nextScene, int sprite = 0)
    {
        StartCoroutine(TransitionAnimation(nextScene, sprite));
    }

    private IEnumerator TransitionAnimation(int nextScene, int sprite)
    {
        _visual[sprite].transform.localScale = Vector3.one * _startScale;

        float timer = 0;
        while (timer < _duration)
        {
            _visual[sprite].transform.localScale = Vector3.Lerp(Vector3.one * _startScale, Vector3.one * _endScale, timer / _duration);

            timer += Time.deltaTime;
            yield return null;
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        yield return op.isDone;

        timer = 0;
        while (timer < _duration)
        {
            _visual[sprite].transform.localScale = Vector3.Lerp(Vector3.one * _endScale, Vector3.one * _startScale, timer / _duration);

            timer += Time.deltaTime;
            yield return null;
        }

        _visual[sprite].transform.localScale = Vector3.one * _startScale;
    }
}
