using System.Collections;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;
    public static ArmMovement Instance;

    private void Start()
    {
        Instance = this;
    }

    [ContextMenu("Trigger")]
    public void TriggerAnim()
    {
        _anim.SetTrigger("Activate");
    }
}
