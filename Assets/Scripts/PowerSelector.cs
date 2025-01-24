using System.Collections;
using UnityEngine;

public class PowerSelector : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve = null;
    [SerializeField] private UnityEngine.UI.Image _fillImage = null;
    [SerializeField] private float _animationSpeed = 1f;

    private bool _selected = false;

    public System.Action<float> onSelected;

    private void Awake()
    {
        _fillImage.fillAmount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnSelected();
    }

    public void Enable()
    {
        _selected = false;
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        float timer = 0;
        while (!_selected)
        {
            _fillImage.fillAmount = _curve.Evaluate(timer / 1.0f);

            timer += Time.deltaTime;
            if (timer >= 1.0f)
                timer = 0;
            yield return null;
        }

        onSelected?.Invoke(_fillImage.fillAmount);
    }

    public void OnSelected()
    {
        _selected = true;
    }
}
