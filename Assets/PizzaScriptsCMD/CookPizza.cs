using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPizza : MonoBehaviour {

	public delegate void CookPizzaDelegate();
	public static event CookPizzaDelegate CookPizzaEvent;

	public void CookPizzaMethod()
	{
		if (CookPizzaEvent != null)
			CookPizzaEvent();
	}

}
