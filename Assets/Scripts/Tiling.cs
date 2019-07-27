using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour
{
    public int offsetX = 2;

    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    public bool reverseScale = false;

    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            //calculating the camera extentent and half its witdh. World coordinates.
            float camHorizontalExtental = cam.orthographicSize * Screen.width / Screen.height;

            //calculate the x position where the camera can see the edge of the sprite
            float edgePositionVisibleRigth = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtental;
            float edgePositionVisibleLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtental;

            //check if we can see the edge of the element
            if (cam.transform.position.x >= edgePositionVisibleRigth - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if(cam.transform.position.x <= edgePositionVisibleLeft + offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    void MakeNewBuddy(int rigthOrLeft) {
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rigthOrLeft, myTransform.position.y, myTransform.position.z);
        Transform newBuddy =  Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        if (reverseScale)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * (-1), newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;

        if (rigthOrLeft>0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;

        }
    }
}
