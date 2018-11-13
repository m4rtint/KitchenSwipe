using System.Collections;
using System.Collections.Generic;

public struct InstructionsMessages {

    readonly static string WRONGSWIPE = "UH OH that was the wrong swipe";
    readonly static string TUTORIAL_STEP_ONE_PART_1 = "Welcome to the Food Swipe Tutorial!";
    readonly static string TUTORIAL_STEP_ONE_PART_2 = "\n\n<b> swipe left anywhere on the screen</b>";
    public readonly static string TUTORIAL_STEP_ONE = TUTORIAL_STEP_ONE_PART_1 + TUTORIAL_STEP_ONE_PART_2;
    public readonly static string TUTORIAL_STEP_ONE_WRONG_SWIPE = WRONGSWIPE+TUTORIAL_STEP_ONE_PART_2;

    readonly static string TUTORIAL_STEP_TWO_PART_1 = "THAT'S CORRECT!";
    readonly static string TUTORIAL_STEP_TWO_PART_2 = "\n\nMatch the center ingredient to the correct stack\n\nswipe right anywhere on the screen";
    public readonly static string TUTORIAL_STEP_TWO = TUTORIAL_STEP_TWO_PART_1+TUTORIAL_STEP_TWO_PART_2;
    public readonly static string TUTORIAL_STEP_TWO_WRONG_SWIPE = WRONGSWIPE + TUTORIAL_STEP_TWO_PART_2;
    public readonly static string TUTORIAL_STEP_THREE = "SOMETIMES YOU GET INGREDIENTS THAT DON'T MATCH\n\nPRESS THE TRASH BUTTON TO REMOVE IT";
    public readonly static string TUTORIAL_STEP_FOUR = "YOU GOT IT!\n\nKeep going until you run out of food";
    public readonly static string TUTORIAL_COMPLETE = "THATS IT! YOU DID IT\n\nNOW GO PLAY THE REAL GAME";
}
