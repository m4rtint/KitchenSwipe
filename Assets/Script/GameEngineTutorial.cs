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
    InstructionsPanel instructionsPanel;

    #region mono
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
                stepOneCorrect();
            } else {
                instructionsPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_ONE_WRONG_SWIPE);
            }
        } else if (currentTutorialStep == TUTORIALSTEP.SWIPERIGHT) {
            randomGeneratedIngredient = ingredientsGenerator.userSwiped(currentIngredient, dir);
            if (dir == Direction.Right) {
                stepTwoCorrect(randomGeneratedIngredient);
            } else {
                instructionsPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_TWO_WRONG_SWIPE);
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

    void stepOneCorrect() {
        currentIngredient = ingredientsGenerator.chooseIngredientFrom(Direction.Right);
        instructionsPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_TWO);
        currentTutorialStep++;
    }

    void stepTwoCorrect(Ingredient ingred) {
        currentIngredient = ingred;
        instructionsPanel.setInstructions(InstructionsMessages.TUTORIAL_STEP_THREE);
        currentTutorialStep++;
    }

    void stepThreeCorrect() {
        instructionsPanel.setInstructions(InstructionsMessages.TUTORIAL_COMPLETE);
        instructionsPanel.setCompletionButtonActive(true);
        StateManager.instance.pauseGame();
        currentTutorialStep++;
    }

    bool checkStepThreeCorrect() {
        return ingredientsGenerator.isHoldersEmpty();
    }
    #endregion

    #region delegateMethods
    void onCompleteTransition() {
        instructionsPanel.startPopoutAnimation();
        StateManager.instance.startGame();
    }
    #endregion
}
