using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public int poolingAmount;
    public GameObject objectPrefab;
    public float sprinkleRate = 3;
    public List<GameObject> pooledObjects;
	public List<GameObject> deployedObjects;
	float actionTime = 0;

    void Start()
    {
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
        if (Input.GetButton("Fire1")&& Time.time > actionTime)
        {
			{	
				actionTime = Time.time + sprinkleRate;
				DeployedObjectsManager();
			}

        }


    }

	void DeployedObjectsManager()
	{
		if (deployedObjects.Count < poolingAmount && FetchPooledObject()!=null)
		{
			GameObject nextObject = FetchPooledObject();
			RenderPooledObject(nextObject);
			pooledObjects.Remove(nextObject);
			deployedObjects.Add(nextObject);
		}
		else
		{
			GameObject lastObject = deployedObjects[deployedObjects.Count-1];
			RenderPooledObject(lastObject);
			deployedObjects.RemoveAt(deployedObjects.Count-1);
			deployedObjects.Insert(0,lastObject);
		}
	}

	public void RenderPooledObject(GameObject o)
	{
			o.SetActive(true);
			o.transform.localPosition = new Vector3 (Random.Range(0,10f), this.transform.localPosition.y, Random.Range(0,10f));
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

}
