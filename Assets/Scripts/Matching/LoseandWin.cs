using Unity.VisualScripting;
using UnityEngine;

public class LoseandWin : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tapioka")
        {
            //Scene Transition to menu
            print("Lost");
        }
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > 90f)
        {
            //Scene transition to machine
            print("Win");
        }
    }
}
