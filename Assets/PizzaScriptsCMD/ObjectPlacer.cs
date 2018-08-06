using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour {

public LayerMask layerMask;
public ObjectPooler SelectedIngredient;

float actionTime = 0;
public float sprinkleRate;
bool placeHolderCreated;
MeshRenderer thisMesh;


public void Start()
{
	thisMesh  = this.gameObject.GetComponent<MeshRenderer>();
}

public void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100f, layerMask))
		{
			this.transform.localPosition = hit.point;
			PlaceHolderCreator(this.transform.localPosition);
		}

        if (Input.GetButton("Fire1")&& Time.time > actionTime)
        {
			{	
				actionTime = Time.time + sprinkleRate;
				DeployedObjectsManager();
			}
        }

		if (!Physics.Raycast(ray, out hit, 100f, layerMask))
		{
			PlaceHolderClear();
		}
	}

	public void PlaceHolderCreator(Vector3 v)
	{
		if (!placeHolderCreated)
		{
			print("World placeholder created");
			thisMesh.enabled = true;
			v = v + new Vector3(0,6f,0); // offset the Vector 3
			GameObject placeHolder = Instantiate(objectPrefab, 
												v, 
												Quaternion.Euler(90,0,0));
			placeHolder.GetComponent<BoxCollider>().enabled = false;
			placeHolder.GetComponent<Rigidbody>().useGravity = false;
			placeHolder.transform.SetParent(this.transform, true);
			placeHolderCreated = true;
		}
		return;
	}

	public void PlaceHolderClear()
	{
		if (placeHolderCreated)
		{
			print("Placeholder clear");
			if (transform.GetChild(0)!=null)
			{
				Destroy(transform.GetChild(0).gameObject);
			}
			thisMesh.enabled = false;
			placeHolderCreated = false;
		}
	
		return;
	}
}

public enum IngredientType
{
	Pepperoni,
	Mushroom,
	Basil
}

