using System;

abstract public class User{
		
	private int status;	//0 - student, 1 - wykladowca, 2 - admin
	private string imie, nazwisko;
	
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
	
	public int getStatus(){	return status;}
	public void setStatus(int nowy){	status = nowy;}
	public string getImie(){	return imie;}
	public void setImie(string nowe){	imie = nowe;}
	public string getNazwisko(){	return nazwisko;}
	public void setNazwisko(string nowe){	nazwisko = nowe;}
}
