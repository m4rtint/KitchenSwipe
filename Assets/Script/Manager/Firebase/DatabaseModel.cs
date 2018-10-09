using System.Collections;
using System.Collections.Generic;

public class User
{
    public string username;
    public string email;

    public User(string user, string email)
    {
        this.username = user;
        this.email = email;
    }
}

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class Record
{
    public string key;
    public string Name;
    public int Score;
    public int Dishes;
    public int Combo;
    public int TimeLasted;

    public Record(string name, int score, int dishes, int combo, int timeLasted)
    {
        this.Name = name;
        this.Score = score;
        this.Dishes = dishes;
        this.Combo = combo;
        this.TimeLasted = timeLasted;
    }

    public Record(string key, string name, int score, int dishes, int combo, int timeLasted)
    {
        this.key = key;
        this.Name = name;
        this.Score = score;
        this.Dishes = dishes;
        this.Combo = combo;
        this.TimeLasted = timeLasted;
    }
}

public struct ErrorMessages
{
    public readonly static string LOAD_LEADERBOARD = "Error loading leaderboard, Please try again later";
    public readonly static string GENERIC_ERROR = "An error has occured";
    public readonly static string SYNC_ERROR = "Unable to Sync Profile, playing without saving to databse";
}

public struct PlayerPrefKeys
{
    //Leaderboard score
    public readonly static string DISPLAYNAME = "DISPLAYNAME";
    public readonly static string INFINITE_SCORE = "SCORE";
    public readonly static string INFINITE_DISHES = "DISHES";
    public readonly static string INFINITE_COMBO = "COMBO";
    public readonly static string INFINITE_SECONDS = "SECONDSLASTED";

    //Setting UI
    public readonly static string FONTSIZE = "FONT_SIZE";

    //Check Tutorial
    public readonly static string DONETUTORIAL = "DONE_TUTORIAL";
}

public struct SceneNames
{
    public readonly static string INFINITE_SCENE_HARD = "InfiniteHard";
    public readonly static string MAIN_MENU = "MainMenu";
    public readonly static string TUTORIAL = "Tutorial";
}


