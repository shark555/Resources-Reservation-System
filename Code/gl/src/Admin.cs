using System;
using System.Collections.Generic;
using System.Data;

public class Admin : User{
		
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
	
	public int connectSubjectToTeacher(string ID_Teacher, string ID_subject){
		if (ID_Teacher != null && ID_subject != null){
			string lista = " * FROM Diplomas;";
			IDataReader reader = DBQuery.createQuery("SELECT", lista);
			while(reader.Read()) {
            	Console.WriteLine("Name: " + reader["ID"]);
       		}
			//DBQuery.CloseReader(reader);
			return 0;
		}else
			return 1;
	}
	
	private int deleteUser(int index){
		return UserList.getInstance().deleteUser(index);
	}
	
	private int editUser(int index){
		return UserList.getInstance().editUser(index);
	}
	
	static public Admin getInstance(){	return instance;}
	
}
