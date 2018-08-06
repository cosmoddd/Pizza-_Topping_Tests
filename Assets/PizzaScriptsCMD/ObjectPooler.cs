using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public IngredientType ingredient;
    public int poolingAmount;
    public GameObject objectPrefab;
    public float sprinkleRate = 3;
    public List<GameObject> pooledObjects;
	public List<GameObject> deployedObjects;
	float actionTime = 0;
	public bool hovering = false;
	public LayerMask layerMask;
	MeshRenderer thisMesh;

	bool placeHolderCreated = false;

    void Start()
    {
		thisMesh = GetComponent<MeshRenderer>();
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolingAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectPrefab);
			obj.name = ("Pepperoni "+ i);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    void Update()
    {
		// Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// RaycastHit hit;

		// if (Physics.Raycast(ray, out hit, 100f, layerMask))
		// {
		// 	this.transform.localPosition = hit.point;
		// 	PlaceHolderCreator(this.transform.localPosition);
		// }

        // if (Input.GetButton("Fire1")&& Time.time > actionTime)
        // {
		// 	{	
		// 		actionTime = Time.time + sprinkleRate;
		// 		DeployedObjectsManager();
		// 	}
        // }

		// if (!Physics.Raycast(ray, out hit, 100f, layerMask))
		// {
		// 	PlaceHolderClear();
		// }
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

	void DeployedObjectsManager()
	{
		if (deployedObjects.Count < poolingAmount && FetchPooledObject()!=null)
		{
			GameObject nextObject = FetchPooledObject();
			RenderPooledObject(nextObject);
			pooledObjects.Remove(nextObject);
			deployedObjects.Insert(0,nextObject);
		}
		else
		{
			GameObject lastObject = deployedObjects[deployedObjects.Count-1];		// reset the object
			RenderPooledObject(lastObject);
			deployedObjects.RemoveAt(deployedObjects.Count-1);
			lastObject.GetComponent<Ingredient>().RemoveTopping();
			deployedObjects.Insert(0,lastObject);
		}
	}

	public void RenderPooledObject(GameObject o)
	{
			o.SetActive(true);
			o.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			o.transform.localPosition = new Vector3 (this.transform.localPosition.x,
													 6f,
													 this.transform.localPosition.z);
	}

    public GameObject FetchPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

	public void ReturnPooledObject (Ingredient ingredient)
	{
		GameObject crashedObject = ingredient.gameObject;
		deployedObjects.Remove(crashedObject);
		pooledObjects.Insert(0,crashedObject);
	}

	void OnEnable()
	{
		Ingredient.ToppingCrash += ReturnPooledObject;
	}
	void OnDisable()
	{
		Ingredient.ToppingCrash -= ReturnPooledObject;
	}

}
