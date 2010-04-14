using System;

public class Admin{
		
	static private Admin instance = null;
	
	public Admin(){
		instance = this;
	}
	
	private Wykladowca createWykladowca(){
		Wykladowca ret = new Wykladowca();
		return ret;
	}
	
	private Student createStudent(){
		Student ret = new Student();
		return ret;
	}
	
	private int connectSubjectToTeacher(Wykladowca teacher, string subject){
		return 0;
	}
	
	private int deleteUser(int index){
		return UserList.getInstance().deleteUser(index);
	}
	
	private int editUser(int index){
		return UserList.getInstance().editUser(index);
	}
	
	static public Admin getInstance(){	return instance;}
	
}
