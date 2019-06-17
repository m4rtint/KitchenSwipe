using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoodGenerator : MonoBehaviour
{
    [SerializeField]
    Food[] m_Foods;

    [SerializeField]
    Food[] burgers;
    [SerializeField]
    Food[] subs;
    [SerializeField]
    Food[] drinks;
    [SerializeField]
    Food[] deserts;
    [SerializeField]
    Food[] hotdogs;


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
        chooseRandomFoodFromStack(burgers);
        chooseRandomFoodFromStack(drinks);
        chooseRandomFoodFromStack(deserts);
        chooseRandomFoodFromStack(subs);
        chooseRandomFoodFromStack(hotdogs);
    }

    private void chooseRandomFoodFromStack(Food[] foods)
    {
        int chooseTimes = foods.Length;
        for (int i = 0; i < chooseTimes/2; i++)
        {
            int randomFoodIndex = Random.Range(0, chooseTimes);
            chosenFoodStack.Push(foods[randomFoodIndex]);
        }
    }

    #endregion

    #region tutorial
    public void fillFoodForTutorial() {
        if (m_Foods.Length > 1) {
            chosenFoodStack.Push(m_Foods[0]);
            chosenFoodStack.Push(m_Foods[1]);
        } else {
            Assert.AreEqual(m_Foods.Length, 2);
        }
    }
    #endregion

}
