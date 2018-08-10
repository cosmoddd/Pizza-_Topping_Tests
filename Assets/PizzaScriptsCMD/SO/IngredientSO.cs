using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pizza/Ingredient")]
public class IngredientSO : ScriptableObject {

    public string ingredientID;
    public GameObject ingredientPrefab;

}
