﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setParent : MonoBehaviour {

    public TextMesh textObject;

    private Rigidbody rb;
    private Vector3 initialDistance;
    private Vector3 initialInterval;

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        initialDistance = transform.position;
        initialInterval = transform.GetChild(0).transform.position - initialDistance;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{

        Vector3 currentInterval = transform.GetChild(0).transform.position - transform.position;

        float changeInInterval = Mathf.Abs(currentInterval.z - initialInterval.z);

        if (changeInInterval > 0.02)
        {
            transform.GetChild(0).transform.position = transform.position + initialInterval;
        }

        Vector3 changed = rb.velocity;
		foreach (Transform child in transform) {
            Rigidbody childRb = child.GetComponent<Rigidbody>();
            Transform childTf = child;

            Vector3 childChanged = childRb.velocity;

            if (childChanged.z != 0) {
                rb.MovePosition(transform.position + childChanged * Time.deltaTime);
            } else if (changed.z != 0) {
                childRb.MovePosition(childTf.position + changed * Time.deltaTime);
            }
        }

        float changedDistance = transform.position.z - initialDistance.z;
        textObject.text = "" + (changedDistance).ToString("0.00");

        if (changedDistance < -0.02f) {
            transform.position = Vector3.Scale(transform.position, new Vector3(1, 1, 0)) + new Vector3(0, 0, -0.02f + initialDistance.z);
        }
    }


    private void LateUpdate()
    {

    }
}
