using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour {

public delegate void ObjectPlacerDelegate(string s, Vector3 location);
public static event ObjectPlacerDelegate PlaceObject;
public static event ObjectPlacerDelegate GetPrefab;

public string ingredientID;
LayerMask layerMask;
float actionTime = 0;
public float sprinkleRate;
bool placeHolderCreated;
public GameObject prefabIngredient;
MeshRenderer thisMesh;
bool cooked = false;

public void Start()
{
	ingredientID = "Pepperoni";
	thisMesh  = this.gameObject.GetComponent<MeshRenderer>();
	layerMask = 512;
}

public void Update()
	{
		if (cooked)
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100f, layerMask))
		{
			this.transform.localPosition = hit.point;
			PlaceHolderCreator(this.transform.localPosition);

    	    if (Input.GetButton("Fire1")&& Time.time > actionTime)
        	{
				{	
				actionTime = Time.time + sprinkleRate;
				PlaceObject(ingredientID, this.transform.localPosition);
				}
        	}
		}

		if (!Physics.Raycast(ray, out hit, 100f, layerMask))
		{
			PlaceHolderClear();
		}
	}

	public void CookedPizza()
	{
		cooked = true;
	}

	public void PlaceHolderCreator(Vector3 v)
	{

		if (!placeHolderCreated)
		{
			GetPrefab(ingredientID, this.transform.localPosition);
			thisMesh.enabled = true;
			v = v + new Vector3(0,6f,0); // offset the Vector 3
			GameObject placeHolder = Instantiate(prefabIngredient, v, Quaternion.Euler(90,0,0));
			placeHolder.GetComponent<BoxCollider>().enabled = false;
			placeHolder.GetComponent<Rigidbody>().useGravity = false;
			placeHolder.transform.SetParent(this.transform, true);
			placeHolderCreated = true;
		}
		return;
	}

	public void SetPrefabIngredient(GameObject o)
	{
		if (o != prefabIngredient)
			{
				prefabIngredient = o;
			}
		return;
	}	

	public void PlaceHolderClear()
	{
		if (placeHolderCreated)
		{
			if (transform.GetChild(0)!=null)
			{
				Destroy(transform.GetChild(0).gameObject);
			}
			thisMesh.enabled = false;
			placeHolderCreated = false;
		}
	
		return;
	}

	public void SetIngredientID(string s)
	{
		ingredientID = s;
	}

	void OnEnable()
	{
		ObjectPooler.SendPrefabIngredient += SetPrefabIngredient;
		CookPizza.CookPizzaEvent += CookedPizza; 
	}
	void OnDisable()
	{
		ObjectPooler.SendPrefabIngredient -= SetPrefabIngredient;
		CookPizza.CookPizzaEvent -= CookedPizza;
	}
}

public enum IngredientType
{
	Pepperoni,
	Mushroom,
	Basil
}

