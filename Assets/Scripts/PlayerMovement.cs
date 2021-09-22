using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float mouseSensitvity;


    private Rigidbody playerBody;
    
    private Camera playerCamera;
    private float horizontalMovement;
    private float forwardMovement;
    private Vector3 mousePosition;
    private float cameraDirection;





    // Start is called before the first frame update
    void Start()
    {

        playerBody = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();


    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        forwardMovement = Input.GetAxis("Vertical");
       

       



        if (Input.GetButton("Horizontal"))
        {
            transform.position += transform.right * horizontalMovement * _movementSpeed * Time.deltaTime;
        }
        if (Input.GetButton("Vertical"))
        {

            transform.position += transform.forward * forwardMovement* _movementSpeed *Time.deltaTime;

            
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.rotation *= Quaternion.Euler(0, -5, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.rotation *= Quaternion.Euler(0,  5, 0);
        }


        if (Input.GetMouseButton(1))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        }


    }
}



