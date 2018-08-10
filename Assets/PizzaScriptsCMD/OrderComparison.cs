using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderComparison : MonoBehaviour {


	public Order thisOrder;
	int amount; 
	int caetanoScore;

	public void Compare(Pizza pizza)
	{
		for (int i = 0; i<thisOrder.desiredIngredients.Count; i++)
		{	
			amount = 0;
			for (int j = 0; j<pizza.ingredients.Count; j++)
			{
				if (pizza.ingredients[j].ingredientID == thisOrder.desiredIngredients[i].ingredientID)
				{
					amount++;
				}
			}
			CompareIngredientCalc(amount, thisOrder.desiredIngredients[i].desiredAmount, thisOrder.desiredIngredients[i].ingredientID);	
		}

		FinalScore();
	}

	float DMinus (float d)
	{
		return (1-d);
	}

	float DPlus (float d)
	{
		return (1+d);
	}

	public void CompareIngredientCalc(int amount, int targetAmount, string ingredientString)
	{
		float ingredientCalculation = (Mathf.Round(amount)/Mathf.Round(targetAmount));
		print ("The ratio for "+ ingredientString + " is "+ ingredientCalculation);
		print ("The differential range you want is "+ (1-thisOrder.differential) + " and "+ (1+thisOrder.differential));

		if (ingredientCalculation >= (DMinus(thisOrder.differential)) && ingredientCalculation <= (DPlus(thisOrder.differential)))
		{
			print ("Your instincts are good.  Just the right amount of <b>"+ ingredientString +"</b> on the pie.");
			caetanoScore = caetanoScore +3;
			return;
		}

		if (ingredientCalculation < (DMinus(thisOrder.differential)) && ingredientCalculation >= .6)
		{
			print ("You could have used a few more <b>"+ ingredientString+"</b> but that's fine." );
			caetanoScore=caetanoScore + 2;
			return;
		}

		if (ingredientCalculation > (DPlus(thisOrder.differential)) && ingredientCalculation <= 1.4)
		{
			print ("You went a little overboard with the <b>"+ ingredientString+"</b> but whatever, man." );
			caetanoScore = caetanoScore +2;
			return;
		}
		
		if (ingredientCalculation < .6f && ingredientCalculation > 0)
		{
			print ("You trying to be cheap?  Because people actually want <b>"+ ingredientString+"</b> on their pizza.  Not cool, man.  Not good.");
			caetanoScore = caetanoScore +1;
			return;
		}

		if (ingredientCalculation > 1.4)
		{
			print ("Amateur moves, man.  Way too much <b>"+ ingredientString+"</b> for what the customer wanted.");
			caetanoScore = caetanoScore +1;
			return;
		}

		if (ingredientCalculation == 0)
		{
			print ("Mate.  You forgot the fuckin' <b>"+ ingredientString +".</b>  Get it toGETHer, man.");
			caetanoScore = caetanoScore +0;
			return;
		}
	}

	public void FinalScore()
	{
		print(caetanoScore);
		float caetanoScoreFloat = (Mathf.Round(caetanoScore) / (3 * Mathf.Round(thisOrder.desiredIngredients.Count)));
		print("Youre <b>Caetano Score</b> is <b>"+caetanoScoreFloat*100+"!</b>");
		caetanoScore = 0;
		caetanoScoreFloat = 0; // reset scores
	}

	void OnEnable()
	{
		Pizza.Serve += Compare;
	}
	void OnDisable()
	{
		Pizza.Serve -= Compare;
	}

}
