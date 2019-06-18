using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoodGenerator : MonoBehaviour
{
    private List<Food> listOfChosenFood = new List<Food>();
    [SerializeField]
    private Food[] m_Foods;

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
        Food[] arrayOfFood = null;

        if (m_Foods != null && m_Foods.Length > 0)
        {
            arrayOfFood = m_Foods;
        } else
        {
            arrayOfFood = listOfChosenFood.ToArray();
        }

        return arrayOfFood;
    }


    #region Generation
    private void Awake()
    {
        chooseRandomFoodFromStack(burgers);
        chooseRandomFoodFromStack(drinks);
        chooseRandomFoodFromStack(deserts);
        chooseRandomFoodFromStack(subs);
        chooseRandomFoodFromStack(hotdogs);
    }
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
        int randomFoodIndex = Random.Range(0, listOfChosenFood.Count);
        //Store
        chosenFoodStack.Push(listOfChosenFood[randomFoodIndex]);
    }

    private void chooseRandomFoodFromStack(Food[] foods)
    {
        if (foods != null && foods.Length > 0)
        {
            int chooseTimes = foods.Length;
            for (int i = 0; i < chooseTimes / 2; i++)
            {
                int randomFoodIndex = Random.Range(0, chooseTimes);
                listOfChosenFood.Add(foods[randomFoodIndex]);
            }
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
