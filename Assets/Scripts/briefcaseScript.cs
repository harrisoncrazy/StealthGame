﻿using UnityEngine;
using System.Collections;

public class briefcaseScript : MonoBehaviour {

	float originalY;

	public float floatStrength = 1; // You can change this in the Unity Editor to 
	// change the range of y positions that are possible.

	void Start()
	{
		this.originalY = this.transform.position.y;
	}

	void Update()
	{
		transform.position = new Vector3(transform.position.x,
			originalY + ((float)Mathf.Sin(Time.time * 5) * floatStrength),
			transform.position.z);
	}
}
