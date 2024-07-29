using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private int maxAmmo = 30;
    private int currentAmmo;
    public int bulletDamage = 2;
    [SerializeField] private int range = 100;
    [SerializeField] private bool isAutomatic = false;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject gunShot;
    public Camera fpsCamera;
    [SerializeField] private float reloadTime = 1.5f;
    private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
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
                Debug.Log(hit.transform.name);
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
        else
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (isReloading || currentAmmo == maxAmmo) return;
        StartCoroutine(StartReload());
    }

    IEnumerator StartReload()
    {
        Debug.Log("Reloading");
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

}
