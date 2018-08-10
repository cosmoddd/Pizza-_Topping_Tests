using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

	public delegate void IngredientDelegate(Ingredient ingredient);
	public static event IngredientDelegate ToppingAdd;
	public static event IngredientDelegate ToppingRemove;
	public static event IngredientDelegate ToppingCrash;
	
	public string ingredientID;

	MeshRenderer thisMeshRenderer;
	public Material cookedMaterial;

	public float resizeWhenCooked = 1;

	public virtual void OnCollisionEnter(Collision collision)
	{

	
		if (collision.gameObject.tag == "Pizza")
		{
			thisMeshRenderer = GetComponent<MeshRenderer>();
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

	public void CookTopping()
	{
		if (gameObject.tag == "Pizza" && gameObject != null)
		{
			if (cookedMaterial != null)
				thisMeshRenderer.material = cookedMaterial;
			this.gameObject.transform.localScale = this.gameObject.transform.localScale * resizeWhenCooked;
		}
	}

	public void RemoveTopping()
	{
		ToppingRemove(this);
	}

	public void OnEnable()
	{
		CookPizza.CookPizzaEvent += CookTopping;
	}

	public void OnDisable()
	{
		CookPizza.CookPizzaEvent -= CookTopping;
	}
}
