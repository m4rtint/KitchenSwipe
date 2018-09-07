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

public class SortByPoints : IComparer
{
    public int Compare(object x, object y)
    {
        Record r1 = (Record)x;
        Record r2 = (Record)y;
        return Comparer.Default.Compare(r2.Score, r1.Score);
    }
}

public class SortByPlates : IComparer
{
    public int Compare(object x, object y)
    {
        Record r1 = (Record)x;
        Record r2 = (Record)y;
        return Comparer.Default.Compare(r2.Plates, r1.Plates);
    }
}

public class SortByCombo : IComparer
{
    public int Compare(object x, object y)
    {
        Record r1 = (Record)x;
        Record r2 = (Record)y;
        return Comparer.Default.Compare(r2.Combo, r1.Combo);
    }
}

public class SortByTime : IComparer
{
    public int Compare(object x, object y)
    {
        Record r1 = (Record)x;
        Record r2 = (Record)y;
        return Comparer.Default.Compare(r2.TimeLasted, r1.TimeLasted);
    }
}


