using System;
using System.Collections.Generic;
public class UserList : IteratorUserList{
		
	static private UserList instance = null;
	private int curPos;
	private List<User> lista;
	
	public UserList(){
		instance = this;
		//dodanie do listy aktualnych użytkowników z bazy
	}
		
	public int deleteUser(int index){
		if (index < lista.Count && index > 0)
			lista.RemoveAt(index);
		return 0;
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
	
	//z interfejsu iteratora
	public int getCurrentIndex(){
		return curPos;
	}
	
	public User next(){
		if (curPos + 1 > lista.Count - 1)
			return null;
		else{
			curPos++;
			return lista[curPos];
		}
	}
	
	public User prev(){
		if (curPos - 1 < 0)
			return null;
		else{
			curPos--;
			return lista[curPos];
		}
	}
	
	public User head(){
		curPos = 0;
		return lista[0];
	}
	
	public User current(){
		return lista[curPos];
	}
	
	public User tail(){
		curPos = lista.Count - 1;
		return lista[lista.Count - 1];
	}
	
	public User getByIndex(int index){
		return lista[index];
	}
	
	public int changeStatus(int index, int newState){
		lista[index].status = newState;
		return 0;
	}
}
