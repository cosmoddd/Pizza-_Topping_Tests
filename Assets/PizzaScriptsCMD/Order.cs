using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order")]
public class Order : ScriptableObject {

	public List <DesiredIngredient> desiredIngredients;
	public float differential = .1f; // how much leeway will this customer give you
}

[System.Serializable]
public class DesiredIngredient
{
	public string ingredientID;
	public int desiredAmount;
}