using System;

public class UserList : IteratorUserList{
		
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
	
	//z interfejsu iteratora
	public int getCurrentIndex(){
		return 0;
	}
	
	public User next(){
		return new User();
	}
	
	public User prev(){
		return new User();
	}
	
	public User head(){
		return new User();
	}
	
	public User current(){
		return new User();
	}
	
	public User tail(){
		return new User();
	}
	
	public User getByIndex(int index){
		return new User();
	}
	
	public int changeStatus(int index){
		return 0;
	}
}
