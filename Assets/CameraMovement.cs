using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool swiping = false;
    private float startSwipe;
    private float swipeForce;

    public float swipeStrength = 0.1f;
    public float smoothRatio = 0.5f;

    // Use this for initialization
    void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (swiping)
        {
            float swipeX = Mathf.Lerp(swipeForce * swipeStrength, 0, (Time.time - startSwipe) * smoothRatio);
            transform.position += Vector3.right * swipeX * Time.deltaTime;
            if (swipeX == 0)
                swiping = false;
        }

    }

    public void Swipe(float force)
    {
        swiping = true;
        startSwipe = Time.time;
        swipeForce = force;
    }

    public void Stop()
    {
        swiping = false;
    }
}
