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


public class Record
{
    public string Name;
    public int Score;
    public int Plates;
    public int Combo;
    public int TimeLasted;

    public Record(string name, int score, int plates, int combo, int timeLasted)
    {
        this.Name = name;
        this.Score = score;
        this.Plates = plates;
        this.Combo = combo;
        this.TimeLasted = timeLasted;
    }
}

public struct ErrorMessages
{
    public readonly static string LOAD_LEADERBOARD = "Error loading leaderboard, Please try again later";
    public readonly static string GENERIC_ERROR = "An error has occured";
    public readonly static string SYNC_ERROR = "Unable to Sync Profile, playing in Offline mode";
}

public struct PlayerPrefKeys
{
    public readonly static string INFINITE_SCORE = "SCORE";
    public readonly static string INFINITE_DISHES = "DISHES";
    public readonly static string INFINITE_COMBO = "COMBO";
    public readonly static string INFINITE_SECONDS = "SECONDSLASTED";
}


