using UnityEngine;

[ExecuteInEditMode]
public class ColorByImageFill : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _image = null;
    [SerializeField] private Gradient _gradient = null;

    private void Update()
    {
        if (_image == null || _gradient == null)
            return;

        _image.color = _gradient.Evaluate(_image.fillAmount);
    }
}
