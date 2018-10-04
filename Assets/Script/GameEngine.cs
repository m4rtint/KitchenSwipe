using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
public class GameEngine : MonoBehaviour {

	//========================Local
    Ingredient currentIngredient;

	[SerializeField]
	int m_NumberOfFood;

    //========================Dependencies
	//Food Generator
	protected FoodGenerator foodGenerator;

	//Ingredients Generator
	[SerializeField]
	GameObject m_FoodHolder;
	IngredientGenerator ingredientsGenerator;

    //Center
    [SerializeField]
    GameObject center;

    //Announcement Manager
    [SerializeField]
    AnnouncementManager announcementManager;
    

    #region getter/setter
    protected int NumberOfFood() {
        return m_NumberOfFood;
    }

    protected void NumberOfFood(int size){
        m_NumberOfFood = size;
    }
    #endregion

    #region Mono
    // Use this for initialization
    void Awake () {
		foodGenerator = GetComponent<FoodGenerator>();
		ingredientsGenerator = m_FoodHolder.GetComponent<IngredientGenerator> ();

        SetupHolders ();
    }

	protected virtual void Start()
	{
        setupDelegates();

        //Get Stack of Food
        foodGenerator.fillStackWithRandomFood(m_NumberOfFood);

        //Place First 4 food onto each side
        setupIngredients();

        //Choose a random current ingredient
        ChooseNewCurrentIngredient ();
	}

    //Start Setup Only
    void ChooseNewCurrentIngredient()
    {
        currentIngredient = ingredientsGenerator.randomlyChooseIngredient();
        setCenterIngredientView();
    }

    void setupIngredients() {
        for (int i = 0; i < ingredientsGenerator.foodHolderLength(); i++)
        {
            SetNewFood((Direction)i);
        }
	}

    void setupDelegates()
    {
        TimeManager.instance.isGameOverDelegate += onGameOver;
        TransitionManager.instance.completedTransition += onCompleteTransition; 
        announcementManager.onTimesUpComplete += GetComponent<UIManager>().startGameOverScreen;
    }

	void SetupHolders(){
		center.GetComponent<UserInput> ().swipeDelegate += PlayerSwiped;
        FoodHolder[] holders = ingredientsGenerator.FoodHolders();
        for (int i = 0; i < holders.Length; i++)
        {
            holders[i].Direction(i);
            holders[i].orderDelegate += CompleteOrder;
            holders[i].orderTimerDelegate += IncorrectlySwipeIngredient;
        }
	}

	void Update() {
		if (StateManager.instance.isInGame ()) {
			RunDownOrderTimer (Time.deltaTime * TimeManager.instance.orderTimeVaryingSpeed);
		}
	}

	void RunDownOrderTimer(float seconds) {
        //Decrement all timer of food
        ingredientsGenerator.updateFoodTimer(seconds);
    }
	#endregion

	#region Ingredients
	void setCenterIngredientView() {
        center.GetComponent<CenterIngredient>().SetCenter (currentIngredient);
	}

    void SetNewFood(Direction dir)
    {
        ingredientsGenerator.insertFoodIntoHolder(foodGenerator.firstFoodOnStack(),dir);
    }
    #endregion

    #region Orders
    protected virtual void CompleteOrder(Direction dir)
    {
        SetNewFood(dir);
    }

	void IncorrectlySwipeIngredient(Direction dir)
	{
        ingredientsGenerator.decrementOrderTimer (dir, TimeManager.instance.FoodPenaltyTime());
	}
    #endregion

    #region Actions
    void PlayerSwiped(Direction dir)
    {
        currentIngredient = ingredientsGenerator.userSwiped(currentIngredient, dir);
        if (currentIngredient == null)
        {
            currentIngredient = foodGenerator.peekFoodOnStack().GetNeededIngredient();
        }
        setCenterIngredientView();
    }

    #endregion

    #region Delegate
    void onGameOver()
    {
        StateManager.instance.gameOver();
        ScoreManager.instance.saveScore();
        announcementManager.gameObject.SetActive(true);
        announcementManager.startTimesUpAnimate();
    }

    void onCompleteTransition()
    {
        announcementManager.startCountDownAnimate();
    }
    #endregion
}
