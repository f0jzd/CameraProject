using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class OrbitCamera : MonoBehaviour
{

    private float smoothingSpeed = 1;
    private bool _collided = false;
    public LayerMask rayCastLayer;
    public float mouseSensitivity = 10f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    
    public GameObject player;
    public float offsetDistance;
    public Vector3 offset;
    private cameraHandling cameraState = cameraHandling.followBehindPlayer;

    enum cameraHandling
    {
        rotateAroundPlayer= 0,
        followBehindPlayer = 1,

    }
    

    
    
    
    
    
    
    void Start()
    {
        
        
        transform.position = offset;


    }

    // Update is called once per frame
    void LateUpdate()
    {

        Debug.Log(cameraState);
        
        
        if (Input.mouseScrollDelta.y > 0 && offsetDistance > 0)
        {
            offsetDistance -= 1f;
            

        }
        if (Input.mouseScrollDelta.y < 0 && offset.z < 10)
        {
            offsetDistance += 1f;
            
        }
        if (Input.GetMouseButton(0))
        {
            velocityX += Input.GetAxis("Mouse X") * mouseSensitivity;
            velocityY += Input.GetAxis("Mouse Y") * mouseSensitivity;
            cameraState = cameraHandling.rotateAroundPlayer;
            
        }
        if (Input.GetMouseButton(1))
        {
            cameraState = cameraHandling.followBehindPlayer;
            
        }

        if (cameraState == cameraHandling.rotateAroundPlayer)
        {

            
           
            rotationYAxis = velocityX;
            rotationXAxis = velocityY;
            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion toRotation = Quaternion.Euler(-rotationXAxis, rotationYAxis, 0);
            Debug.Log(toRotation); 

            Quaternion rotation = toRotation;

            Vector3 negDistance = new Vector3(0.0f, offset.y, -offsetDistance);
            Vector3 position = rotation * negDistance + player.transform.position;

            
            transform.rotation = rotation;
            transform.position = position;
        }
        
        if (cameraState == cameraHandling.followBehindPlayer)
        {
            velocityX = transform.rotation.eulerAngles.y;
            velocityY = transform.rotation.eulerAngles.x;

            
            Quaternion rotationD = Quaternion.LookRotation(player.transform.forward, Vector3.up);
            Vector3 negDistanceD = new Vector3(0.0f, offset.y, -offsetDistance);
            Vector3 positionD = rotationD * negDistanceD + player.transform.position; 
            transform.rotation = rotationD;
            transform.position = positionD;
        }
        
        
        
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
        
        RaycastHit hit;

         if (Physics.Raycast(player.transform.position, transform.position - player.transform.position, out hit, Vector3.Distance(transform.position,player.transform.position), rayCastLayer))
        {
            if (hit.transform.tag != "CameraRig")
            {
                

                //Mathf.Lerp(offsetDistance, hit.point.z, smoothingSpeed);
                //offsetDistance = hit.point.z;
                Debug.Log("Not Hitting Camera");
                transform.position = Vector3.Lerp(transform.position, hit.point,smoothingSpeed);
                

                //Debug.Log("Hitting Player");
            }
            if (hit.transform.tag == "CameraRig" && _collided == false)//Default camera position
            {
                
                Debug.Log("Hitting Camera");
     
            }
        }
         
        
        
        
         


         
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform)
        {
            _collided = false;
            //Debug.Log("NO longer colliding");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform)
        {
            _collided = true;
        }
       
    }
}
