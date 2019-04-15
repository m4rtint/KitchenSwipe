using System.Collections;
using System.Collections.Generic;

public struct InstructionsMessages {

    readonly static string WRONGSWIPE = "wrong swipe !!!";
    readonly static string TUTORIAL_STEP_ONE_PART_1 = "FOOD SWIPE TUTORIAL";
    readonly static string TUTORIAL_STEP_ONE_PART_2 = "\n\n<b> SWIPE LEFT \n\n MATCH THE INGREDIENT</b>";
    public readonly static string TUTORIAL_STEP_ONE = TUTORIAL_STEP_ONE_PART_1 + TUTORIAL_STEP_ONE_PART_2;
    public readonly static string TUTORIAL_STEP_ONE_WRONG_SWIPE = WRONGSWIPE+TUTORIAL_STEP_ONE_PART_2;

    readonly static string TUTORIAL_STEP_TWO_PART_1 = "CORRECT!";
    readonly static string TUTORIAL_STEP_TWO_PART_2 = "\n\nMATCH THE INGREDIENTS";
    public readonly static string TUTORIAL_STEP_TWO = TUTORIAL_STEP_TWO_PART_1+TUTORIAL_STEP_TWO_PART_2;
    public readonly static string TUTORIAL_STEP_TWO_WRONG_SWIPE = WRONGSWIPE + TUTORIAL_STEP_TWO_PART_2;
    public readonly static string TUTORIAL_STEP_THREE = "INGREDIENT THAT DOESN'T MATCH\n\nTRASH IT";
    public readonly static string TUTORIAL_STEP_FOUR = "NICE!\n\nCOMPLETE THE REST OF THE INGREDIENTS";
    public readonly static string TUTORIAL_COMPLETE = "THATS IT! YOU DID IT\n\nNOW GO PLAY THE REAL GAME";
}
