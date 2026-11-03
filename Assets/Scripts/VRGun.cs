using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class VRGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;

    public AmmoController ammoController;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Shoot()
    {
        if(ammoController.isCharged && ammoController.ammoLeft)
        {
            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().linearVelocity = spawnPoint.forward * fireSpeed;
            ammoController.ReduceAmmo();
            Destroy(spawnedBullet, 5f);
        }
    }
}
