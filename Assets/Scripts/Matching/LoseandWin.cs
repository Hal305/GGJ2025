using Unity.VisualScripting;
using UnityEngine;

public class LoseandWin : MonoBehaviour
{
    private bool end = false;
    private float winTime = 60f;
    
    
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tapioka" && !end)
        {
            //Scene Transition to menu
            TransitionUI.Instance.PlayTransition(0);
            print("Lost");
            end = true;
        }
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > winTime && !end)
        {
            //Scene transition to machine
            TransitionUI.Instance.PlayTransition(1);
            print("Win");
            end = true;
        }
    }
}
