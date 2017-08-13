using System.Collections;
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

        Vector3 currentDistance = transform.position;
        float changedDistance = currentDistance.z - initialDistance.z;
        textObject.text = "" + (changedDistance).ToString("0.00");
    }
}
