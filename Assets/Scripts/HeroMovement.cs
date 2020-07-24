using System.CodeDom;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    float moveSpeed = 10f;

    public static bool moveUp;
    public static bool moveDown;
    public static bool moveLeft;
    public static bool moveRight;

    Vector3 curPos, lastPos;

    private void Awake()
    {
        moveUp = false;
        moveDown = false;
        moveLeft = false;
        moveRight = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.nextSpawnPoint != "")
        {
            GameObject spawnPoint = GameObject.Find(GameManager.instance.nextSpawnPoint);
            transform.position = spawnPoint.transform.position;

            GameManager.instance.nextSpawnPoint = "";
        }
        else if(GameManager.instance.lastHeroPosition != Vector3.zero)
        {
            transform.position = GameManager.instance.lastHeroPosition;
            GameManager.instance.lastHeroPosition = Vector3.zero;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Frame");
#if UNITY_EDITOR
        //float moveX = Input.GetAxis("Horizontal");
        //float moveZ = Input.GetAxis("Vertical");
#endif
        //Debug.Log("X:"+moveX);
        //Debug.Log("Z:"+moveZ);
        if(moveRight == true)
        {
            //Debug.Log("Right");
            Vector3 movement = new Vector3(0.6f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().velocity = movement * moveSpeed;//* Time.deltaTime;
        }
        else
        if (moveLeft == true)
        {
            //Debug.Log("Left");
            Vector3 movement = new Vector3(-0.6f, 0.0f, 0.0f);
            GetComponent<Rigidbody>().velocity = movement * moveSpeed;//* Time.deltaTime;
        }
        else
        if (moveUp == true)
        {
            //Debug.Log("Up");
            Vector3 movement = new Vector3(0.0f, 0.0f, 0.6f);
            GetComponent<Rigidbody>().velocity = movement * moveSpeed;//* Time.deltaTime;
        }
        else
        if (moveDown == true)
        {
            //Debug.Log("Down");
            Vector3 movement = new Vector3(0.0f, 0.0f, -0.6f);
            GetComponent<Rigidbody>().velocity = movement * moveSpeed;//* Time.deltaTime;
        }


        //if(moveX != 0 || moveZ != 0)
        //{
        //    Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
        //    GetComponent<Rigidbody>().velocity = movement * moveSpeed;//* Time.deltaTime;
        //}


        curPos = transform.position;
            if (curPos == lastPos)
            {
                GameManager.instance.isWalking = false;
            }
            else
            {
                GameManager.instance.isWalking = true;
            }
        lastPos = curPos;
    }

    void OnTriggerEnter(Collider coli)
    {
       if(coli.tag == "Teleporter")
        {
            CollisionHandler col = coli.gameObject.GetComponent<CollisionHandler>();
            GameManager.instance.nextSpawnPoint = col.spawnPointName;
            GameManager.instance.sceneToLoad = col.sceneToLoad;
            GameManager.instance.LoadNextScene();
        }
        
        if(coli.tag == "EncounterZone")
        {
            RegionData region = coli.gameObject.GetComponent<RegionData>();
            GameManager.instance.curRegion = region;
        }
    }

    void OnTriggerStay(Collider coli)
    {
        if (coli.tag == "EncounterZone")
        {
            GameManager.instance.canGetEncounter = true;
        }
    }

    void OnTriggerExit(Collider coli)
    {
        if (coli.tag == "EncounterZone")
        {
            GameManager.instance.canGetEncounter = false;
        }
    }

    public void PressedRight()
    {
        moveRight = true;
    }

    public void UnpressedRight()
    {
        moveRight = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }

    public void PressedLeft()
    {
        moveLeft = true;
    }

    public void UnpressedLeft()
    {
        moveLeft = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }

    public void PressedUp()
    {
        moveUp = true;
    }

    public void UnpressedUp()
    {
        moveUp = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }

    public void PressedDown()
    {
        moveDown = true;
    }

    public void UnpressedDown()
    {
        moveDown = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }






}
