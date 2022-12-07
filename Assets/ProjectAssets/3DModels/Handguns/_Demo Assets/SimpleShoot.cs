using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] 
    [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] 
    [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] 
    [SerializeField] private float ejectPower = 150f;

    // Para el sonido
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;

    // Para la caja de balas
    [Header("Bullet Box")]
    //[SerializeField] private GameObject bulletBox;
    // Cantidad de balas
    [SerializeField] [Range(0, 100)] private int bullets = 15;
    [SerializeField] public int points = 0;


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //If you want a different input, change it here 
        // Ahora dispara con el click izquierdo del mouse
        if (Input.GetKeyDown(KeyCode.Mouse0) && bullets != 0)
        {
            //Calls animation on the gun that has the relevant animation events that will fire
            gunAnimator.SetTrigger("Fire");
            Debug.Log("Disparando");

        }
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        // Logica de las balas
        if(bullets != 0)
        {
            if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        
        // Reproduce el sonido al instanciar la bala
        audioSource.clip = shootSound;
        audioSource.Play();

        // Resta una bala
        bullets--;

        }
        else{
            Debug.Log("No quedan balas balas");
            return;
        }


    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

    // Funcion para recargar las balas (limita la carga a maximo 100 por player)
    public void SetBullets()
    {
         bullets = bullets + 15;
         if(bullets > 100)
        {
            bullets = 100;
        }
    }

    //metodo para agregar puntos
    public void AddPoints(int value)
    {
        this.points += value;
        Debug.Log("Puntos: " + points);
    }

}
