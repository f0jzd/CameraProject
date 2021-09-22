using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float mouseSensitvity;
    [SerializeField] public Material transparentMaterial;
    //[SerializeField] public Material opaqueMaterial;
    
    


    //int layerMask = 1 << 7; //Setting the layerMask to the Player Layer, Now only casting rays to the colliders in player layer

    public LayerMask rayCastLayer;

    public float scale;
    private GameObject obstruction;
    private Material origianlMaterial;
    private Camera playerCamera;
    public Transform defaultCamerPosition;
    public float smoothingSpeed;
    private GameObject interactedGameobject;
    private Color transparencyMaker;
    private float t;

    public float exitSPeed;

    private float zoomedIn;
    private float defaultFov;
    public float transistionSpeed;
    private bool collided = false;


    private bool raycastCheck = false;
    private Vector3 hitPointPosition;


    public GameObject player;

    IEnumerator fadeRoutine;


    // Start is called before the first frame update
    void Start()
    {
        

        playerCamera = GetComponent<Camera>();
        zoomedIn = playerCamera.fieldOfView - 30;
        defaultFov = playerCamera.fieldOfView;


}

    // Update is called once per frame
    void Update()
    {

        
        

        //Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, -2));
  
        RaycastHit hit;
        //layerMask  = ~layerMask; // Inverting the layer, hitting everything except the player

        //transform.LookAt(player.transform);
        
        
               

        //Debug.DrawLine(transform.position, player.transform.position);


        Debug.Log(collided);




        if (Input.GetMouseButtonDown(1))
        {
            playerCamera.fieldOfView = Mathf.Lerp(defaultFov,zoomedIn,0.125f);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            playerCamera.fieldOfView = 60;
        }



        if (Input.GetKey(KeyCode.LeftAlt))
        {
            transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X"));
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            transform.position = defaultCamerPosition.transform.position;
            transform.rotation = defaultCamerPosition.transform.rotation;
        }

        
        //Debug.Log(Input.mouseScrollDelta.y*scale);

        if (Input.mouseScrollDelta.y > 0)
        {
            playerCamera.transform.position = Vector3.forward;
        }

        if (!Physics.Raycast(player.transform.position, transform.position - player.transform.position, out hit, 5f, rayCastLayer))
        {
            transform.position = Vector3.Lerp(transform.position, defaultCamerPosition.transform.position,exitSPeed);
        }
        
        
        // playerCamera.transform.position = new Vector3(playerCamera.transform.position.x,playerCamera.transform.position.y,playerCamera.transform.position.z+Input.mouseScrollDelta.y*scale); // X be acting strange
        // playerCamera.transform.position = Vector3.forward;

        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);

        if (Physics.Raycast(player.transform.position, transform.position - player.transform.position, out hit, 5f, rayCastLayer))
        {
           /* 
            if (interactedGameobject == null && hit.collider.tag != "Player") 
            {
                
                Debug.Log("Hit Wall");
                obstruction = hit.transform.gameObject; // Create a reference

                

                origianlMaterial = obstruction.GetComponent<Renderer>().sharedMaterial; //making a refernce to the original material of the object
                obstruction.GetComponent<Renderer>().material = transparentMaterial; //Changing the material to the transparency material
                transparentMaterial.color = origianlMaterial.color;    //new Color(origianlMaterial.color.r, origianlMaterial.color.g, origianlMaterial.color.b, 0f);//Changing the color of the material to the original color                
                //transparentMaterial.color = Color.Lerp(transparentMaterial.color, new Color(transparentMaterial.color.r, transparentMaterial.color.g, transparentMaterial.color.b,0), Mathf.Lerp(0,1,transistionSpeed));
                //StartCoroutine(Fade(transparentMaterial,0.0f,1.0f));
                fadeRoutine = Fade(transparentMaterial, 0.0f, 0.5f);
                StartCoroutine(fadeRoutine);

                //transparentMaterial.color = Color.Lerp(transparentMaterial.color, new Color(origianlMaterial.color.r, origianlMaterial.color.g, origianlMaterial.color.b, 0f), transistionSpeed);
                

                



                interactedGameobject = hit.collider.gameObject;
                return;
            }
            else if (hit.collider.tag == "Player" && fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }
            
            if (hit.collider.gameObject != interactedGameobject) 
            {
                Debug.Log("Stopped Hitting Wall");
                if (interactedGameobject != null)
                {

                    
                    obstruction.GetComponent<Renderer>().sharedMaterial = origianlMaterial;
                    obstruction.GetComponent<Renderer>().sharedMaterial.color = Color.Lerp(obstruction.GetComponent<Renderer>().sharedMaterial.color, new Color(origianlMaterial.color.r, origianlMaterial.color.g, origianlMaterial.color.b,1f),transistionSpeed);
                    //obstruction.GetComponent<Renderer>().sharedMaterial.color = new Color(origianlMaterial.color.r, origianlMaterial.color.g, origianlMaterial.color.b, 1f);
                    interactedGameobject = null;

                }
            }

            */

            if (hit.transform.tag == "GameAssets")
            {
                
                //Debug.Log("Hitting Wall");
            }
            /*else if (hit.transform.tag != "GameAssets" && obstruction != null)
            {
                
                
                

            }*/
            

            
            
            
            
            
            if (hit.transform.tag != "Main Camera")
            {

                raycastCheck = true;
                Debug.Log("Not Hitting Camera");
                hitPointPosition = hit.point;
                transform.position = Vector3.Lerp(transform.position, hit.point,smoothingSpeed) ;


                //Debug.Log("Hitting Player");
            }
            if (hit.transform.tag == "Main Camera" && collided == false)//Default camera position
            {
                Debug.Log("Hitting Camera");
                raycastCheck = true;
                transform.position = Vector3.Lerp(transform.position, defaultCamerPosition.transform.position,exitSPeed);
            }
            
            
        }

        
        

    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform)
        {
            collided = false;
            //Debug.Log("NO longer colliding");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform)
        {
            collided = true;
        }
       
    }







    IEnumerator Fade(Material material,float aValue, float aTime)
    {
        //Debug.Log("Starting Routine");

        float alpha = material.color.a;
        for (float t = 0; t < 1.0f; t+= Time.deltaTime / aTime)
        {
            Color color = new Color(material.color.r, material.color.g, material.color.b, Mathf.Lerp(alpha, aValue, t));
            material.color = color;
            yield return null;
        }


        
    }

}
