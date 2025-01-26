using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "Scriptable Objects/Difficulty")]
public class Difficulty : ScriptableObject
{
    public int tapiCount;
    public float riseSpeed = 0.05f;
    public float shootSpeed = 0.3f;
}
