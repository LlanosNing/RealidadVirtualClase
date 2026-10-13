using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;
    public InputActionProperty menuButton;

    void Start()
    {
        
    }

    void Update()
    {
        if (menuButton.action.WasPressedThisFrame())
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
