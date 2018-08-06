using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

	public List<Ingredient> ingredients;
	bool hovering = false;

	public void AddIngredient(Ingredient ingredient)
	{
		if (!ingredients.Contains(ingredient))
			ingredients.Insert(0,ingredient);
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
}
