using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton = null;

    private void Start()
    {
        _playButton.onClick.AddListener(LevelTransition);
    }

    private void LevelTransition()
    {
        TransitionUI.Instance.PlayTransition(1);
    }
}
