using System;

public class Topic{
	
	private string subject, dateFrom, dateTo, author, category;
	public string name{
		get;
		set;
	}
	public int status{	//0 - zarezerwowany, 1 - wolny, 2 - oczekujący na akceptację, 3 - usunięty
		get;
		set;
	}
	private User reservedBy;
	
	public Topic(){
		
	}
	
	//tworzenie tematu przez studenta z automatyczną rezerwacją
	public Topic(string a_subject, 
	             string a_name,
	             string a_dateFrom,
	             string a_dateTo,
	             string a_author,
	             string a_category,
	             User a_reservedBy){
		subject = a_subject;
		name = a_name;
		dateFrom = a_dateFrom;
		dateTo = a_dateTo;
		author = a_author;
		category = a_category;
		reservedBy = a_reservedBy;
		status = 0;
	}
	
	//dodawanie tematu przez wykładowcę
	public Topic(string a_subject, 
	             string a_name,
	             string a_dateFrom,
	             string a_dateTo,
	             string a_author,
	             string a_category){
		subject = a_subject;
		name = a_name;
		dateFrom = a_dateFrom;
		dateTo = a_dateTo;
		author = a_author;
		category = a_category;
		reservedBy = null;
		status = 1;
	}
	
	//sugerowanie tematu przez studenta
	public Topic(string a_subject, 
	             string a_name,
	             string a_author,
	             string a_category){
		subject = a_subject;
		name = a_name;
		dateFrom = null;
		dateTo = null;
		author = a_author;
		category = a_category;
		reservedBy = null;
		status = 2;
		//wykladowca po akceptacji ustawia daty
	}
	
	public void setDateFrom(string newDate){	dateFrom = newDate;}
	public void setDateTo(string newDate){	dateTo = newDate;}
	override public string ToString(){
		string ret = subject + ": " + name + ".\nAutor: " + author + "\nOd: " + dateFrom + "\nDo: " + dateTo + "\nStatus: ";
		switch (status){
			case 0:	ret += "zarezerwowany.";
					break;
			case 1: ret += "wolny.";
					break;
			case 2:	ret += "oczekujący na akceptację.";
					break;
			case 3:	ret += "usunięty.";
					break;
		}
		
		return ret;
	}
}
