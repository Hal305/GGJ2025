using System.Collections;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;
    [SerializeField] private AudioSource _audio = null;

    private void Start()
    {
        ToggleAnimator(false);
    }

    public void ToggleAnimator(bool on)
    {
        if (on)
            _audio.Play();
        else
            _audio.Stop();

        _anim.enabled = on;
    }
}
