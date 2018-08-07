﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

	public List<Ingredient> ingredients;
	public List<string> ingredientsIDs;

	bool hovering = false;
	int ingredientAmount;

	public void Start()
	{
	//	this.GetComponent<MeshCollider>().inflateMesh = true;
	}

	public void AddIngredient(Ingredient ingredient)
	{
		if (!ingredients.Contains(ingredient))
			ingredients.Insert(0,ingredient);
			ingredient.gameObject.transform.SetParent(this.gameObject.transform, true); // sets the pizza as the parent
	}

	public void RemoveIngredient(Ingredient ingredient)
	{
		ingredients.Remove(ingredient);
	}

	void OnEnable()
	{
		Ingredient.ToppingAdd += AddIngredient;
		Ingredient.ToppingRemove += RemoveIngredient;
	}
	void OnDisable()
	{
		Ingredient.ToppingAdd -= AddIngredient;
		Ingredient.ToppingRemove -= RemoveIngredient;
	}

	public void CaluclateAllIngredients()
	{
		foreach (string ingredient in ingredientsIDs)
		{
			CalculateIngredient(ingredient);
		}
	}

	public void CalculateIngredient(string s)
	{
		ingredientAmount = 0;
		for (int i =0; i < ingredients.Count; i++)
		{
			if (ingredients[i].ingredientID == s)
			{
				ingredientAmount++;
			}
		}
		float ingredientRatio = (Mathf.Round(ingredientAmount)/ingredients.Count) * 100;
		print("The ingredient <b>"+ s +"</b> takes up <b>"+ ingredientRatio+ "%</b> of the pizza.  Nice one.");
	}
}
