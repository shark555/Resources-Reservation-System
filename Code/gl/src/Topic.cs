using System;

public class Topic{
	
	private string subject, name, dateFrom, dateTo, author, category;
	private int status;	//0 - zarezerwowany, 1 - wolny, 2 - oczekujący na akceptację, 3 - usunięty
	private Student reservedBy;
	
	public Topic(){
		
	}
	
	//tworzenie tematu przez studenta z automatyczną rezerwacją
	public Topic(string a_subject, 
	             string a_name,
	             string a_dateFrom,
	             string a_dateTo,
	             string a_author,
	             string a_category,
	             Student a_reservedBy){
		status = 0;
	}
	
	//dodawanie tematu przez wykładowcę
	public Topic(string a_subject, 
	             string a_name,
	             string a_dateFrom,
	             string a_dateTo,
	             string a_author,
	             string a_category){
		status = 1;
	}
	
	//sugerowanie tematu przez studenta
	public Topic(string a_subject, 
	             string a_name,
	             string a_author,
	             string a_category){
		status = 2;
		//wykladowca po akceptacji ustawia daty
	}
	
	public void setDateFrom(string newDate){	dateFrom = newDate;}
	public void setDateTo(string newDate){	dateTo = newDate;}
}
