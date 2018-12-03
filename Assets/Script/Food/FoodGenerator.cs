using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [SerializeField]
    Food[] m_Foods;

    Stack<Food> chosenFoodStack = new Stack<Food>();

    public Stack<Food> ChosenFoodStack()
    {
        return chosenFoodStack;
    }

    public Food firstFoodOnStack()
    {
        return ChosenFoodStack().Pop();
    }

    public Food peekFoodOnStack()
    {
        return ChosenFoodStack().Peek();
    }

    public Food[] Foods()
    {
        return m_Foods;
    }


    #region Generation
    public void fillStackWithRandomFood(int num)
    {
        for (int i = 0; i < num; i++)
        {
            chooseRandomFood();
        }
    }

    public void chooseRandomFood()
    {
        //Choose random
        int randomFoodIndex = Random.Range(0, m_Foods.Length);
        //Store
        chosenFoodStack.Push(m_Foods[randomFoodIndex]);
    }

    #endregion

    #region tutorial
    public void fillFoodForTutorial() {
        if (m_Foods.Length > 1) {
            chosenFoodStack.Push(m_Foods[0]);
            chosenFoodStack.Push(m_Foods[1]);
        } else {
            //TODO
            //THrow Exception
        }
    }
    #endregion

}
