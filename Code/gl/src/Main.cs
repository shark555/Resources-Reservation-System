using System;
using Gtk;

public class MainClass{
	
	//public static Admin a_admin;
	public static Proxy a_proxy;
	public static UserList a_userList;
	public static TopicList a_topicList;
	
	public static void Main(string[] args){
		//a_admin = new Admin();
		a_proxy = new Proxy();
		a_userList = new UserList();
		a_topicList = new TopicList();
		new GUILogin();
	}
}