using System.Collections;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;

    private void Start()
    {
        ToggleAnimator(false);
    }

    public void ToggleAnimator(bool on)
    {
        _anim.enabled = on;
    }
}
