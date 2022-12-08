
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region CLASS_VARIABLES
    // Variables privadas tienen un "_" al principio de su nombre

    [Header ("References")]
    [SerializeField] private Camera _playerCamera;

    [Header ("General")]
    [SerializeField] private float _gravityScale = -6f;

    [Header ("Movement")]
    [Tooltip("Variable de velocidad")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _run = 10f;

    //[Header ("Rotation")]
    //[Tooltip("Sensibilidad de la rotación del mouse")]
    //[SerializeField] private float _rotationSensibility = 30f;

    [Header ("Jump")]
    //[Tooltip("Variable de fuerza de salto")]
    //[SerializeField] private bool _isJumping = false;
    [Tooltip("Variable de fuerza del salto del rigidbody")]
    [SerializeField] private float _jumpForce = 2f;

    [Header ("Others")]
    [Tooltip("Variable de puntaje")]
    [SerializeField] private int points = 0;

    [Header ("Audios")]
    [Tooltip("Variable de audio de salto")]
    [SerializeField] private AudioClip jumpSound;
    [Tooltip("Variable de audio de monedas")]
    [SerializeField] private AudioClip moneySound;

    // Variable que hace referencia al rigidbody del jugador
    private Rigidbody rb;
    // Variable que hace referencia al audio source del jugador
    private AudioSource audioSource;
    // Variable que hace referencia al character controller del jugador
    private CharacterController _characterController;

    //Variable que almanena el vector de los inputs del jugador
    private Vector3 _moveInput = Vector3.zero;
    private Vector3 _rotationInput = Vector3.zero;

    private float _cameraVerticalAngle = 0f;

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        // Le asignamos el rigidbody del jugador a la variable
        rb = GetComponent<Rigidbody>();
        // Le asignamos el audio source del jugador a la variable
        audioSource = GetComponent<AudioSource>();
        // Le asignamos las referencia a la variable
        _characterController = GetComponent<CharacterController>();
  
    }

    // Update is called once per frame
    void Update()
    {
        // Llamamos a los metodos
        Movement();
        Jump();
        Look();
        
    }

    public void Movement()
    {

        // Asignar el input del InputManager a la variable
        _moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _moveInput = Vector3.ClampMagnitude(_moveInput, 1f);

        // Validamos si se presiona la tecla de correr o no para asignar la velocidad de movimiento
        if(Input.GetButton("Sprint"))
        {
            _moveInput = transform.TransformDirection(_moveInput) * _run;
        }
        else
        {
            _moveInput = transform.TransformDirection(_moveInput) * _speed;
        }


        _moveInput.y = _gravityScale * Time.deltaTime;
        _characterController.Move(_moveInput * Time.deltaTime);

    }

    public void Jump()
    {
        if(Input.GetButton("Jump"))
        {
            _moveInput.y = Mathf.Sqrt(_jumpForce * -2f * _gravityScale);
            audioSource.PlayOneShot(jumpSound);
        }
    
    
    }

    public void Look()
    {
        // Asignar el input del InputManager a la variable
        _rotationInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);
        _rotationInput = Vector3.ClampMagnitude(_rotationInput, 1f);

        // Rotar el jugador en el eje y de la camara
        _cameraVerticalAngle = _cameraVerticalAngle + _rotationInput.y;
        // Esta variable hace que el jugador no pueda rotar mas de x grados
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -70, 70);

        // Hacer la rotación
        transform.Rotate(Vector3.up * _rotationInput.x);
        _playerCamera.transform.localRotation = Quaternion.Euler(-_cameraVerticalAngle, 0f, 0f);

      
    }

    // Method detects collision
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with object: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("cube"))
        {
            Destroy(collision.gameObject);
            audioSource.clip = moneySound;
            audioSource.Play();
            points++;
        }
        
    }
    
    // Method detects collision exit
    public void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit collision with " + collision.gameObject.name);
    }

    // Method detects collision stay
    public void OnCollisionStay(Collision collision)
    {
        Debug.Log("Stay collision with " + collision.gameObject.name);
    }
}
