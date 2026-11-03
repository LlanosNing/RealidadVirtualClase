using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AmmoController : MonoBehaviour
{
    public bool isCharged = false;
    public bool ammoLeft = false;

    public int ammo;

    //Referencias al socket y el interactableAmmo
    public XRSocketInteractor socket;
    public XRBaseInteractable interactableAmmo;

    public InputActionProperty actionButton;

    private void Update()
    {
        if (isCharged && interactableAmmo != null)
        {
            ammo = interactableAmmo.GetComponent<Ammo>().ammoLeft;
            if (ammo > 0)
                ammoLeft = true;
            else
                ammoLeft = false;
        }

        if (actionButton.action.WasPressedThisFrame())
        {
            if (socket.hasSelection)
            {
                var interactable = socket.firstInteractableSelected;
                socket.interactionManager.SelectExit(socket, interactable);
                socket.enabled = false;
                Invoke("ActivateSocket", .5f);
            }

        }
    }

    public void ActivateSocket()
    {
        socket.enabled = true;
    }

    public void ReduceAmmo()
    {
        if (interactableAmmo != null)
        {
            interactableAmmo.GetComponent<Ammo>().ReduceAmmo();
        }
    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        isCharged = true;
        interactableAmmo = args.interactableObject as XRBaseInteractable;
    }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        isCharged = false;
        interactableAmmo = null;
    }

}
