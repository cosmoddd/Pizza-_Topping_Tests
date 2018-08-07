using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public delegate void ObjectPoolerDelegate(GameObject o);
	public static event ObjectPoolerDelegate SendPrefabIngredient;

	public string ingredientID;
    public int poolingAmount;
    public GameObject objectPrefab;
    public List<GameObject> pooledObjects;
	public List<GameObject> deployedObjects;

	bool placeHolderCreated = false;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolingAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectPrefab);
			obj.name = (ingredientID + " " + i);
            obj.SetActive(false);
			obj.GetComponent<Ingredient>().ingredientID = ingredientID;
            pooledObjects.Add(obj);
        }
    }

	void DeployedObjectsManager(string s, Vector3 v)
	{
		if (s == ingredientID)
		{
			if (deployedObjects.Count < poolingAmount && FetchPooledObject()!=null)
			{
				GameObject nextObject = FetchPooledObject();
				RenderPooledObject(nextObject, v);
				pooledObjects.Remove(nextObject);
				deployedObjects.Insert(0,nextObject);
			}
			else
			{
				GameObject lastObject = deployedObjects[deployedObjects.Count-1];		// reset the object
				RenderPooledObject(lastObject, v);
				deployedObjects.RemoveAt(deployedObjects.Count-1);
				lastObject.GetComponent<Ingredient>().RemoveTopping();
				deployedObjects.Insert(0,lastObject);
			}
		}
	}

	public void RenderPooledObject(GameObject o, Vector3 v)
	{
			o.SetActive(true);
			o.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			o.transform.localPosition = new Vector3 (v.x, 6f, v.z);
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

	public void GetPrefabIngredient (string s, Vector3 v)
	{
		if (s == ingredientID)
		{
			SendPrefabIngredient(objectPrefab);
		}
		else
		{
			return;
		}
	}

	void OnEnable()
	{
		Ingredient.ToppingCrash += ReturnPooledObject;
		ObjectPlacer.PlaceObject += DeployedObjectsManager;
		ObjectPlacer.GetPrefab += GetPrefabIngredient;
	}
	void OnDisable()
	{
		Ingredient.ToppingCrash -= ReturnPooledObject;
		ObjectPlacer.PlaceObject -= DeployedObjectsManager;
		ObjectPlacer.GetPrefab -= GetPrefabIngredient;
	}

}
