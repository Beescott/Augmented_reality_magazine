using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour {

    public float speed;
    public float zoomSpeed;

    private bool isClicked;
    private bool isZooming;
    private Vector3 initialMousePosition;
    private Vector3 initialScale;
    private float fingerDistance;

    // Use this for initialization
    void Start () {
        isClicked = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isClicked = true;
                initialMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isClicked = false;
            }

            if (isClicked)
            {
                transform.Rotate((-(initialMousePosition.y - Input.mousePosition.y) / 50 * speed),
                                  ((initialMousePosition.x - Input.mousePosition.x) / 50 * speed),
                                    0,
                                    Space.World);
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (!isZooming)
            {
                isZooming = true;
                fingerDistance = Vector3.Distance(touch1.position, touch2.position);
                initialScale = transform.localScale;
            }
            else
            {
                transform.localScale = initialScale * (Vector3.Distance(touch1.position, touch2.position) / fingerDistance);
                transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 0.1f, 5.0f),
                                                   Mathf.Clamp(transform.localScale.y, 0.1f, 5.0f),
                                                   Mathf.Clamp(transform.localScale.z, 0.1f, 5.0f));
            }
        }
        else
        {
            isZooming = false;
        }
    }
}
