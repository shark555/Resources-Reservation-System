using System;

public class Zegar{
		
	static private Zegar instance = null;
	
	public Zegar(){
		
	}
	
	static public bool canDeleteTopic(){
		return true;	
	}
	
	static public bool canShowTopic(){
		return true;
	}
	
	static public Zegar getInstance(){	return instance;}
}
