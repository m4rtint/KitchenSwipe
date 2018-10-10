using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngineTutorial : GameEngine
{
    enum TUTORIALSTEP {
        SWIPELEFT,
        SWIPERIGHT,
        SWIPEUNTILCORRECT,
        TUTORIALCOMPLETE
    }

    TUTORIALSTEP currentTutorialStep;

    [SerializeField]
    TutorialPanel tutorialPanel;

    [SerializeField]
    SwipeAnimation swipeAnimation;

    #region mono
    protected override void Awake()
    {
        transitionMenuIfNeeded();
        base.Awake();
    }

    void transitionMenuIfNeeded() {
        if (StateManager.instance.isCompleteTutorial()) {
            TransitionManager.instance.startMainMenuScene(true);
        }
    }

    protected override void Start()
    {
        currentTutorialStep = 0;

        setupDelegates();

        //Setup stack of food
        foodGenerator.fillFoodForTutorial();

        //Place ingredients onto the sides
        base.setupIngredients();

        //Choose the ingredient in the middle
        currentIngredient = ingredientsGenerator.chooseIngredientFrom(Direction.Left);
        currentIngredient.pulsingAnimation();

        //Swipe animation
        swipeAnimation.swipe(true);

        //Set center
        setCenterIngredientView();
    }

    protected override void Update() { }

    protected override void setupDelegates()
    {
        TransitionManager.instance.completedTransition += onCompleteTransition;
    }
    #endregion

    #region Orders
    protected override void incorrectlySwipeIngredient(Direction dir) {}

    protected override void CompleteOrder(Direction dir) {}
    #endregion


    #region Actions
    protected override void playerSwiped(Direction dir)
    {
        Ingredient randomGeneratedIngredient = null;
        if (currentTutorialStep == 0) {
            ingredientsGenerator.userSwiped(currentIngredient, dir);
            if (dir == Direction.Left) {
                resetCurrentIngredientScale(dir);
                onStepOneCorrect();
            } else {
                tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_ONE_WRONG_SWIPE);
            }
        } else if (currentTutorialStep == TUTORIALSTEP.SWIPERIGHT) {
            randomGeneratedIngredient = ingredientsGenerator.userSwiped(currentIngredient, dir);
            if (dir == Direction.Right) {
                resetCurrentIngredientScale(dir);
                onStepTwoCorrect(randomGeneratedIngredient);
            } else {
                tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_TWO_WRONG_SWIPE);
            }
        } else if (currentTutorialStep == TUTORIALSTEP.SWIPEUNTILCORRECT) {
            currentIngredient = ingredientsGenerator.userSwiped(currentIngredient, dir);
            if (checkStepThreeCorrect()){
                stepThreeCorrect();
            }
        }

        //Set current Ingredient
        base.setCenterIngredientView();
    }

    //Step 1
    void onStepOneCorrect() {
        currentIngredient = ingredientsGenerator.chooseIngredientFrom(Direction.Right);
        currentIngredient.pulsingAnimation();
        tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_TWO);
        swipeAnimation.swipe(false);
        currentTutorialStep++;
    }

    //Step 2
    void onStepTwoCorrect(Ingredient ingred) {
        currentIngredient = ingred;
        tutorialPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_THREE);
        Destroy(swipeAnimation.gameObject);
        currentTutorialStep++;
    }

    void resetCurrentIngredientScale(Direction dir){
        currentIngredient.resetScale();
    }

    //Step 3
    void stepThreeCorrect() {
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
        StateManager.instance.completedTutorial();
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
