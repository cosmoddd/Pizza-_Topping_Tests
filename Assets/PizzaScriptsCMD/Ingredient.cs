using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

	public delegate void IngredientDelegate(Ingredient ingredient);
	public static event IngredientDelegate ToppingAdd;
	public static event IngredientDelegate ToppingRemove;
	public static event IngredientDelegate ToppingCrash;
	
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Pizza")
		{
			this.gameObject.tag = "Pizza";
			if (ToppingAdd != null)
				ToppingAdd(this);
			
			return;
		}

		if (collision.gameObject.tag == "DeSpawn")
			{

				this.transform.localPosition = new Vector3 (0,0,0);
				this.gameObject.SetActive(false);
				this.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				ToppingRemove(this);
				ToppingCrash(this); // if it lands in the bin, put it back in the pool, too
				return;
			}

	}


	public void RemoveTopping()
	{
		ToppingRemove(this);
	}
}
