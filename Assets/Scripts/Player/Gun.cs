using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private int MaxAmmo = 30;
    private int currentAmmo;
    public int bulletDamage = 2;
    [SerializeField] private int range = 100;
    [SerializeField] private bool isAutomatic = false;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject gunShot;
    public Camera fpsCamera;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = MaxAmmo;
    }

    public void Shoot()
    {
        if(currentAmmo > 0)
        {
            Debug.Log("Shot");
            muzzleFlash.Play();

            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                Debug.Log("Hit");
                if (hit.transform.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    enemy.getDamage(bulletDamage);
                }
                GameObject bulletFlash = Instantiate(gunShot, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(bulletFlash, 2f);
            }

            currentAmmo -= 1;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
