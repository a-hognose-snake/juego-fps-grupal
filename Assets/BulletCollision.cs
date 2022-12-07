using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField] private int value;
    // Referencia al script del arma
    [Header("Weapon")]
    [SerializeField] private SimpleShoot weapon;


    // Start is called before the first frame update
    void Start()
    {
        // busco en la escena el objeto que tiene el script SimpleShoot
		weapon = FindObjectOfType<SimpleShoot>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Target hit!");
            weapon.AddPoints(value);
            Destroy(collision.gameObject);  
        }
    }

}
