using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CaneraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothing = 5.0f;

    Vector3 offSet;

    void Awake()
    {
        Assert.IsNotNull(this.target);
    }

    // Use this for initialization
    void Start ()
    {
        this.offSet = this.transform.position - this.target.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        var targetCameraPosition = this.target.position + this.offSet;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, this.smoothing * Time.deltaTime);
	}
}
