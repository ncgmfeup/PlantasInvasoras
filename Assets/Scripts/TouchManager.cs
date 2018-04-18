using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateNamespace;
public class TouchManager : MonoBehaviour
{
    private Vector2 startTouch, swipeForce;
    private float startTouchTime;

    private Camera mainCamera;
    private CameraMovement cameraMovement;


    public float minSwipeForce = 50;
    public float tapTimeLimit = 1000;

    private StageManager manager; // To tell what to do with input.

    public TouchManager(StageManager manager) { // TODO Maybe change this to be a persistent member, and update manager
        this.manager = manager;

        mainCamera = Camera.main;
        cameraMovement = mainCamera.GetComponent<CameraMovement>();
        Input.simulateMouseWithTouches = true;
    }

    // Update is called once per frame
    public void updateTouch()   {
        #region MouseInputs
        if (Input.GetMouseButtonDown(0)) //On Touch/Mouse down
        {
            cameraMovement.Stop();
            startTouch = Input.mousePosition;
            startTouchTime = Time.time;
        }
        else if (Input.GetMouseButton(0)) //While Touch/Mouse down
        {
            swipeForce = (Vector2)Input.mousePosition - startTouch;
            swipeForce /= Time.deltaTime;
            startTouch = Input.mousePosition;
            HandleSwipe(false);
        }
        else if (Input.GetMouseButtonUp(0)) //On Touch/Mouse up
        {
            swipeForce = (Vector2)Input.mousePosition - startTouch;
            swipeForce /= Time.deltaTime;
            HandleSwipe(true);
        }
        #endregion
    }

    private void HandleSwipe(bool ended)
    {
        float sfx = Mathf.Abs(swipeForce.x);
        float sfy = Mathf.Abs(swipeForce.y);

        if (sfx > sfy && sfx > minSwipeForce)
        {
            //Horizontal Swipe
            Debug.Log("Horizontal Swipe");
            cameraMovement.Swipe(swipeForce.x);
            manager.swiped(swipeForce);

        }
        else if (sfy > minSwipeForce)
        {
            //Vertical Swipe
            Debug.Log("Vertical Swipe");
            manager.swiped(swipeForce);
        }
        else if(ended && Time.time - startTouchTime < tapTimeLimit)
        {
            //Tap
            Debug.Log("Tap");
            manager.swiped(swipeForce);

            Vector2 touch = mainCamera.ScreenToWorldPoint(startTouch);
            RaycastHit2D hit = Physics2D.Raycast(touch, Vector2.zero);

            if (hit)
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
            }
        }
    }

}

