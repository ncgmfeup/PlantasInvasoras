using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Vector2 startTouch, swipeForce;
    private float startTouchTime;

    private Camera mainCamera;
    private CameraMovement cameraMovement;

    public float minSwipeForce = 50;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        cameraMovement = mainCamera.GetComponent<CameraMovement>();
        Input.simulateMouseWithTouches = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region MouseInputs
        if (Input.GetMouseButtonDown(0))
        {
            cameraMovement.Stop();
            startTouch = Input.mousePosition;
            startTouchTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swipeForce = (Vector2)Input.mousePosition - startTouch;
            swipeForce /= (Time.time - startTouchTime);
            HandleSwipe();
        }
        #endregion


    }

    private void HandleSwipe()
    {
        float sfx = Mathf.Abs(swipeForce.x);
        float sfy = Mathf.Abs(swipeForce.y);

        if (sfx > sfy && sfx > minSwipeForce)
        {
            //Horizontal Swipe
            Debug.Log("Horizontal Swipe");
            cameraMovement.Swipe(swipeForce.x);

        }
        else if (sfy > minSwipeForce)
        {
            //Vertical Swipe
            Debug.Log("Vertical Swipe");
        }
        else
        {
            //Tap
            Debug.Log("Tap");
        }
    }
}

