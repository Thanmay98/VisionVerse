using UnityEngine;

public class ControlsPopup : MonoBehaviour
{
    public GameObject controlsPanel;
    public float displayTime = 5f;

    void Start()
    {
        controlsPanel.SetActive(true);
        Invoke(nameof(HidePanel), displayTime);
    }

    void HidePanel()
    {
        controlsPanel.SetActive(false);
    }
}