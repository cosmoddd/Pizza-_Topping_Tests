using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class SauceMelting: MonoBehaviour {

	public Material material;
	public MeshRenderer thisMesh;
	BoxCollider thisCollider;
	Vector3 colliderSize;
	public Vector3 sauceColliderTarget = new Vector3(1,.1f,1);
	public Vector3 sauceScaleMeltingTarget;
	public Vector3 sauceMeltingTarget = new Vector3(1,-1,1);
	bool collided;
	Vector4 sauceMelting;
	Vector3 localScale;

	void Start()
	{
		thisCollider = GetComponent<BoxCollider>();
		localScale = this.gameObject.transform.localScale;
//		thisMesh.materials[0] = material;
	}

	void OnCollisionEnter(Collision col)
	{
		
		if (!collided)
		{
			collided= true;
			localScale = (this.transform.localScale)/col.gameObject.transform.localScale.x;
			LeanTween.value(this.gameObject, new Vector3(1,1,1), sauceColliderTarget, 1).setOnUpdateVector3(ResizeCollider);
			LeanTween.value(this.gameObject, new Vector3(0,0,0), sauceMeltingTarget, 1).setOnUpdateVector3(MeltSauceShader);
			LeanTween.value(this.gameObject, localScale, sauceScaleMeltingTarget, 1).setOnUpdateVector3(MeltSauceScale);
		}
	}

	void ResizeCollider(Vector3 v)
	{
		colliderSize = v;
		thisCollider.size = v;
	}

	void MeltSauceScale(Vector3 sauceScale)
	{
		this.gameObject.transform.localScale = sauceScale;
	}

	void MeltSauceShader(Vector3 sauceVector)
	{
		sauceMelting = new Vector4 (sauceVector.x, sauceVector.y, sauceVector.z, 0);
		thisMesh.materials[0].SetVector("_FunkyVector3", sauceMelting);
	}

}
