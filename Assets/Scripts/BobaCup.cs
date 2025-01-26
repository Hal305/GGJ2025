using System.Collections;
using UnityEngine;

public class BobaCup : MonoBehaviour
{
    [Header("Filling")]
    [SerializeField] private UnityEngine.UI.Image _fillImage = null;
    [SerializeField] private AnimationCurve _fillCurve = null;
    [SerializeField] private float _fillDuration = 1.0f;
    [SerializeField] private ParticleSystem _bobaParticles = null;

    private void Start()
    {
        _fillImage.fillAmount = 0;
    }

    public void FillCup(Color color)
    {
        _fillImage.color = color;
        StartCoroutine(FillingAnimation());
    }

    private IEnumerator FillingAnimation()
    {
        float timer = 0;
        while (timer < _fillDuration)
        {
            _fillImage.fillAmount = _fillCurve.Evaluate(timer / _fillDuration);

            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void AddBoba()
    {
        _bobaParticles.Play();
    }
}
