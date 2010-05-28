using System;

public class Memento{
	public Topic ostatniTemat;
		
	public Memento(Topic doZapamietania){
		ostatniTemat = doZapamietania;	
	}
		
	public Topic getOstatni(){	return ostatniTemat;}
}
