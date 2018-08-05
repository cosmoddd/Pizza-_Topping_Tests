using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	public int poolingAmount;
	public GameObject objectPrefab;
	public float sprinkleRate;
	// Use this for initialization
	void Start () {
		
		// loop / load prefabs into a large list, all disabled
		sprinkleRate = 3;
	}
	

	void Update() 
	{
		if (Input.GetButton("Fire1") && Time.time > sprinkleRate) 
		{
		    
			sprinkleRate = Time.time + sprinkleRate;
			GameObject clone = Instantiate(objectPrefab, transform.position, transform.rotation) as GameObject;
		}

	}

	// 

}
