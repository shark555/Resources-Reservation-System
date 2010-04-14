using System;

public class UserList{
		
	static private UserList instance = null;
	
	public UserList(){
		instance = this;
	}
		
	public int deleteUser(int index){
		return 0;
	}
	
	public int editUser(int index){
		return 0;
	}
	
	public int searchUser(int index){
		return 0;
	}
	
	public void showProjectsList(){
		TopicList.getInstance().showProjectsList();
	}
	
	public void changeTopic(int index){
		TopicList.getInstance().changeTopic(index);
	}
	
	public void addTopic(Topic addMe){
		TopicList.getInstance().addTopic(addMe);
	}
	
	static public UserList getInstance(){	return instance;}
}
