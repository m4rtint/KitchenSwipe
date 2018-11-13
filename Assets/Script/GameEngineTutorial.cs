using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngineTutorial : GameEngine
{
    enum TUTORIALSTEP {
        SWIPELEFT,
        SWIPERIGHT,
        PRESSTRASH,
        SWIPEUNTILCORRECT,
        TUTORIALCOMPLETE
    }

    TUTORIALSTEP currentTutorialStep;

    [SerializeField]
    TutorialPanel tutorialPanel;

    [SerializeField]
    SwipeAnimation swipeAnimation;

    [Header("TUTORIAL")]
    [SerializeField]
    Ingredient wrongIngredient;
    [SerializeField]
    GameObject screenBlock;

    #region mono
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        currentTutorialStep = 0;

        setupDelegates();

        //Setup stack of food
        foodGenerator.fillFoodForTutorial();

        setGameSpaceIngredients();

        //Swipe animation
        swipeAnimation.swipe(true);

        //Set center
        setCenterIngredientView();

        screenBlocking(false);
    }

    void setGameSpaceIngredients()
    {
        //Place ingredients onto the sides
        base.setupIngredients();

        //Choose the up next 
        base.setNextIngredient(ingredientsGenerator.chooseIngredientFrom(Direction.Left));
        //Set center
        base.setNextIngredientAsCenter();
        //Choose 2nd up next
        base.setNextIngredient(ingredientsGenerator.chooseIngredientFrom(Direction.Right));
        //Left ingredient is pulsing
        currentIngredient.pulsingAnimation();
    }


    protected override void Update() { }

    protected override void setupDelegates()
    {
        TransitionManager.instance.completedTransition += onCompleteTransition;
        Trash().GetComponent<TrashInput>().doubleTapDelegate += onPressTrash;
    }
    #endregion

    #region Orders
    protected override void incorrectlySwipeIngredient(Direction dir) {}

    protected override void CompleteOrder(Direction dir) {}
    #endregion


    #region Actions
    protected override void playerSwiped(Direction dir)
    {
        if (currentTutorialStep == 0) {
            ingredientsGenerator.userSwiped(currentIngredient, dir);
            if (dir == Direction.Left) {
                resetCurrentIngredientScale(dir);
                onStepOneCorrect();
            } else {
                tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_ONE_WRONG_SWIPE);
            }
        } else if (currentTutorialStep == TUTORIALSTEP.SWIPERIGHT) {
            ingredientsGenerator.userSwiped(currentIngredient, dir);
            if (dir == Direction.Right) {
                resetCurrentIngredientScale(dir);
                onStepTwoCorrect();
            } else {
                tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_TWO_WRONG_SWIPE);
            }
        } else if(currentTutorialStep == TUTORIALSTEP.PRESSTRASH) {
            return;
        } else if (currentTutorialStep == TUTORIALSTEP.SWIPEUNTILCORRECT) {
            ingredientsGenerator.userSwiped(currentIngredient, dir);
            base.setNextIngredientAsCenter();
            
            if (checkStepThreeCorrect()){
                stepFourCorrect();
            }
        }

        //Set current Ingredient
        base.setCenterIngredientView();
    }

    //Step 1
    void onStepOneCorrect() {
        //Ingredients setting
        base.setNextIngredientAsCenter();
        base.setNextIngredient(wrongIngredient);

        //Set tutorial message
        tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_TWO);

        //Pulsing
        currentIngredient.pulsingAnimation();

        //Tutorial Animation
        swipeAnimation.swipe(false);

        currentTutorialStep++;
    }

    //Step 2
    void onStepTwoCorrect() {
        //Ingredients setting
        base.setNextIngredientAsCenter();

        //Set tutorial message
        tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_THREE);

        //Pulsing
        trashAppears();
        //Screen block
        screenBlocking(true);

        //Tutorial Animation
        Destroy(swipeAnimation.gameObject);

        currentTutorialStep++;
    }

    void screenBlocking(bool on)
    {
        if (on)
        {
            screenBlock.transform.localScale = Vector3.one;
        } else
        {
            screenBlock.transform.localScale = Vector3.zero;
        }
    }

    void resetCurrentIngredientScale(Direction dir){
        currentIngredient.resetScale();
    }

    void trashAppears()
    {
        base.Trash().transform.localScale = Vector3.one;
        base.Trash().GetComponent<PulsingEffects>().pulsingAnimation();
    }

    //Step 3
    void onPressTrash()
    {
        if(currentTutorialStep == TUTORIALSTEP.PRESSTRASH)
        {
            base.Trash().GetComponent<PulsingEffects>().resetScale();
            tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_FOUR);
            screenBlocking(false);
            currentTutorialStep++;
        }
    }

    //Step 4
    void stepFourCorrect() {
        setInstructionPanel();
        setStateManager();
        currentTutorialStep++;
    }

    void setInstructionPanel() {
        tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_COMPLETE);
        tutorialPanel.setCompletionButtonActive(true);
    }

    void setStateManager() {
        StateManager.instance.pauseGame();
    }

    bool checkStepThreeCorrect() {
        return ingredientsGenerator.isHoldersEmpty();
    }
    #endregion

    #region delegateMethods
    void onCompleteTransition() {
        tutorialPanel.startPopoutAnimation();
        StateManager.instance.startGame();
    }
    #endregion
}
