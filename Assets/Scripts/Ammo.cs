using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour
{
    public int ammoLeft = 6;
    public TMP_Text canvasAmmo;

    private void Start()
    {
        canvasAmmo.text = ammoLeft.ToString();
    }

    public void ReduceAmmo()
    {
        ammoLeft--;
        canvasAmmo.text = ammoLeft.ToString();
    }

}
