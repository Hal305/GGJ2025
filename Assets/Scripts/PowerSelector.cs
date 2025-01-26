using System.Collections;
using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    public enum Type { Water, Boba, Straw }
    [SerializeField] private Type _type = default;
    public Type SelectorType { get { return _type; } }

    [SerializeField] private AnimationCurve _curve = null;
    [SerializeField] private UnityEngine.UI.Image _fillImage = null;
    [SerializeField] private float _animationSpeed = 1f;

    private bool _selected = false;

    public System.Action<Color, float> onSelected;

    private void Awake()
    {
        _fillImage.fillAmount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnSelected();
    }

    public void Enable(float startPoint = 0)
    {
        _selected = false;
        StartCoroutine(PlayAnimation(startPoint));
    }

    private IEnumerator PlayAnimation(float startPoint)
    {
        float timer = startPoint;
        while (!_selected)
        {
            _fillImage.fillAmount = _curve.Evaluate(timer / 1.0f);

            timer += Time.deltaTime;
            if (timer >= 1.0f)
                timer = 0;
            yield return null;
        }

        onSelected?.Invoke(_fillImage.color, _fillImage.fillAmount);
    }

    public void OnSelected()
    {
        _selected = true;
    }
}
