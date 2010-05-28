using System;
using System.Data;

public class UserList : IteratorDefinedHere<User>{
		
	static private UserList instance = null;
	
	public UserList(){
		instance = this;
		//dodanie do listy aktualnych użytkowników z bazy
		string args = " * FROM Users";
		IDataReader reader = DBQuery.createQuery("SELECT", args);
		while (reader.Read()){
			Console.WriteLine(reader["Name"] + " " + reader["Passwd"]);
			lista.Add(new User(int.Parse(reader["Status"].ToString()), 
			                   reader["Name"].ToString(), 
			                   reader["Passwd"].ToString(), 
			                   reader["Imie"].ToString(), 
			                   reader["Nazwisko"].ToString()));
		}
		Console.WriteLine(lista.Count);
		DBQuery.CloseReader(reader);
	}
	
	public int deleteUser(int index){
		if (index < lista.Count && index > 0)
			lista.RemoveAt(index);
		return 0;
	}
	
	public void setCurrentPos(string name){
		for (int i = 0; i < lista.Count; i++)
			if (lista[i].name.Equals(name))
				curPos = i;
	}
	
	//do zrobienia!
	public int editUser(int index){
		return 0;
	}
	
	public int searchUser(string name){
		int i = 0;
		for (i = 0; i < lista.Count; i++)
			if (lista[i].nazwisko == name || lista[i].imie == name)
				return i;
		return -1;
	}
	
	public void showProjectsList(){
		TopicList.getInstance().ToString();
	}
	
	public void changeTopic(int index){
		TopicList.getInstance().changeTopic(index);
	}
	
	public void addTopic(Topic addMe){
		TopicList.getInstance().addTopic(addMe);
	}
	
	static public UserList getInstance(){	return instance;}
	
	public int changeStatus(int index, int newState){
		lista[index].status = newState;
		return 0;
	}
}
