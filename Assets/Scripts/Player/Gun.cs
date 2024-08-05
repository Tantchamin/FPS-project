using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform gunPoint;
    public int maxAmmo = 30;
    public int currentAmmo = 30;
    public int bulletDamage = 2;
    public float fireRate = 15f;
    public float nextTimeToFire = 0f;
    [SerializeField] private int range = 100;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject gunShot;
    [SerializeField] private Animator gunAnimator;
    public Camera fpsCamera;
    [SerializeField] private float reloadTime = 1.5f;
    private bool isReloading = false;
    GameManager gameManager;
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        gameManager = GameManager.GetInstance();
        soundManager = SoundManager.GetInstance();
    }

    public void Shoot()
    {
        if (isReloading) return;
        if (currentAmmo > 0)
        {
            muzzleFlash.Play();
            soundManager.PlaySound("AssaultRifleShot", false);

            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    enemy.getDamage(bulletDamage);
                }
                GameObject bulletFlash = Instantiate(gunShot, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(bulletFlash, 2f);
            }

            currentAmmo -= 1;
            gameManager.playerUi.SetAmmoText();

        }
        else
        {
            Reload();
            StartCoroutine(PlayReloadSound());
        }
    }

    public void Reload()
    {
        if (isReloading || currentAmmo == maxAmmo) return;
        gunAnimator.SetTrigger("reload");
        StartCoroutine(StartReload());
    }

    IEnumerator StartReload()
    {
        Debug.Log("Reloading");
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        gameManager.playerUi.SetAmmoText();
    }

    IEnumerator PlayReloadSound()
    {
        const float waitTime = 0.35f;
        yield return new WaitForSeconds(waitTime);
        soundManager.PlaySound("Reloading", false);
    }

}
