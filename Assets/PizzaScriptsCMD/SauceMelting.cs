using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class SauceMelting: MonoBehaviour {

	public Material material;
	public MeshRenderer thisMesh;
	BoxCollider thisCollider;
	public Vector3 colliderSize;
	Vector3 funmk;
	float moop;
	void Start()
	{
		thisCollider = GetComponent<BoxCollider>();
//		thisMesh.materials[0] = material;
	}

	void OnCollisionEnter()
	{
		LeanTween.value(this.gameObject, new Vector3(1,1,1), new Vector3(1,.1f,1), 1).setOnUpdateVector3(ResizeCollider);
		MeltSauce();
		this.gameObject.tag = "Pizza";
	}

	void ResizeCollider(Vector3 v)
	{
		colliderSize = v;
		thisCollider.size = v;
	}

	void MeltSauce()
	{
		
		thisMesh.materials[0].SetVector("_FunkyVector3", new Vector4(1f,-1f,1f,0));
	}

}
