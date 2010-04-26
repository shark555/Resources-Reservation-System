using System;

public class User{
		
	public int status{	//0 - student, 1 - wykladowca, 2 - admin
		get;
		set;
	}
	
	public string imie{
		get;
		set;
	}
	
	public string nazwisko{
		get;
		set;
	}
	
	public User(){
		status = 0;
		imie = "TestImie";
		nazwisko = "TestNazwisko";
	}
	
	public User(int a_status, string a_imie, string a_nazwisko){
		status = a_status;
		imie = a_imie;
		nazwisko = a_nazwisko;
	}
	
	public void changeUserData(int statusNew, string imieNew, string nazwiskoNew){
		if ((statusNew != status) && (statusNew < 3) && (statusNew > -1))
			status = statusNew;
		if ((!String.Equals(imie, imieNew) && !String.IsNullOrEmpty(imieNew)))
	    	imie = imieNew;
		if ((!String.Equals(nazwisko, nazwiskoNew) && !String.IsNullOrEmpty(nazwiskoNew)))
	    	nazwisko = nazwiskoNew;
	}
}
