using System;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public enum Type { Water, Boba, Straw }
    [SerializeField] private Type _type = default;
    public Type DispenserType { get { return _type; } }

    [SerializeField] private DifficultySelector _selector = null;
    [SerializeField] private ParticleSystem _particles = null;
    [SerializeField] private AudioSource _audioSource = null;

    public System.Action<Color, float> OnDifficultySelected;

    private void Start()
    {
        _selector.onSelected += OnSelected;
        _selector.gameObject.SetActive(false);
    }

    private void OnSelected(Color color, float difficulty)
    {
        _audioSource.Play();
        OnDifficultySelected?.Invoke(color, difficulty);
    }

    public void ActivateSelector(float startPoint = 0)
    {
        _selector.gameObject.SetActive(true);
        _selector.Enable(startPoint);
    }

    public void DeactivateSelector()
    {
        _selector.gameObject.SetActive(false);
    }

    public void SetParticleColor(Color color)
    {
        if (_particles == null)
            return;

        ParticleSystem.MainModule main = _particles.main;
        main.startColor = color;
    }

    public void PlayParticles()
    {
        if (_particles == null)
            return;

        _particles.Play();
    }
}
