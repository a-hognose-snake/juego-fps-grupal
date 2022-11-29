using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    public Collider collider;
    public GameObject tooltip;

    public AudioSource audioSource;
    public TargetManager targetManager;

    private bool canInteract = false;

    // Start is called before the first frame update
    void Awake()
    {
        this.collider = this.gameObject.GetComponent<Collider>();
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            tooltip.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            tooltip.SetActive(false);
            canInteract = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canInteract)
            {
                audioSource.Play();
                targetManager.ToggleAllTargets();
            }
        }
    }

}
