using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClickParent : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    void Start()
    {
        transform.position = new Vector3(50, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera cam = Camera.main;
            
            if (cam == null)
            {
                Debug.Log("Main camera not found.");
                return;
            }
            
            screenPosition = Input.mousePosition;
            screenPosition.z = cam.nearClipPlane+7 + Random.Range(-3,+3);

            worldPosition = cam.ScreenToWorldPoint(screenPosition);
            transform.position = worldPosition;
        }
    }
}
