using System.Collections;
using System.Collections.Generic;

public class User {
	public string username;
	public string email;

	public User(string user, string email) {
		this.username = user;
		this.email = email;
	}
}


public class Leaderboard {
	public string Name;
	public int Score;
	public int Plates;

	public Leaderboard(string name, int score, int plates) {
		this.Name = name;
		this.Score = score;
		this.Plates = plates;
	}
}
