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

  public void Start()
  {
    mainCamera = Camera.main;
    cameraMovement = mainCamera.GetComponent<CameraMovement>();
    Input.simulateMouseWithTouches = true;
  }


  // Update is called once per frame
  public void updateTouch()
  {
    /*#region MouseInputs
    if (Input.GetMouseButtonDown(0)) //On Touch/Mouse down
    {
      cameraMovement.Stop();
      startTouch = Input.mousePosition;
      startTouchTime = Time.time;

      Vector2 altered = (mainCamera.ScreenToWorldPoint(startTouch));
      StageManager.sharedInstance.touched(altered);
    }
    else if (Input.GetMouseButton(0)) //While Touch/Mouse down
    {
      swipeForce = (Vector2)Input.mousePosition - startTouch;
      StageManager.sharedInstance.touched((Vector2)startTouch);

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
    #endregion*/

		#region MouseInputs
    if (Input.GetMouseButtonDown(0)) //On Touch/Mouse down
    {
      Vector2 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      StageManager.sharedInstance.TouchStart(position);
    }
    else if (Input.GetMouseButton(0)) //While Touch/Mouse down
    {
			Vector2 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      StageManager.sharedInstance.TouchContinue(position);
    }
    else if (Input.GetMouseButtonUp(0)) //On Touch/Mouse up
    {
      Vector2 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
      StageManager.sharedInstance.TouchEnd(position);
    }
    #endregion
  }
}

