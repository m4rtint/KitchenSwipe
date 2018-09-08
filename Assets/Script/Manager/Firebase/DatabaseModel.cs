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



