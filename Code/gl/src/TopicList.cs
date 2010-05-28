using System;
using System.Data;

public class TopicList : IteratorDefinedHere<Topic>{
	
	static private TopicList instance = null;
	
	public TopicList(){
		instance = this;
	}
	
	//jeśli mamy usera reservedBy, to w bazie odpowiada on id = 1
	public int addTopic(Topic addMe){
		lista.Add(addMe);
		string userNazwisko = addMe.author.Substring(0, addMe.author.IndexOf(' '));
		string userImie = addMe.author.Substring(addMe.author.IndexOf(' ') + 1);
		string userID = "";
		string reservedByID = "";
		//int status = 0;
		
		//pobranie id użytkownika
		string param = " * FROM Users WHERE Imie=";
		param += "\'" + userImie + "\' AND Nazwisko=";
		param += "\'" + userNazwisko + "\';";
		IDataReader reader = DBQuery.createQuery("SELECT", param);
		if (reader.Read())
			userID = reader["ID"].ToString();
		else
			Console.WriteLine("ERROR:" + param);
		DBQuery.CloseReader(reader);
		
		Console.WriteLine(userID);
		
		//pobranie id reservedBy
		if (addMe.reservedBy != null){
			param = " * FROM User WHERE Imie=";
			param += "\'" + addMe.reservedBy.imie + "\' AND Nazwisko=";
			param += "\'" + addMe.reservedBy.nazwisko + "\';";
			reader = DBQuery.createQuery("SELECT", param);
			if (reader.Read())
				reservedByID = reader["ID"].ToString();
			else
				Console.WriteLine("ERROR!" + param);
			DBQuery.CloseReader(reader);
		}else
			reservedByID = "1";
		
		Console.WriteLine(reservedByID);
		//dodanie tematu
		param = " INTO Subjects(Topic, UserID, DateFrom, DateTo, ReservedBy, Cathegory) VALUES (";
		param += "\'" + addMe.name + "\', ";
		param += "\'" + userID + "\', ";
		param += "\'" + addMe.dateFrom + "\', ";
		param += "\'" + addMe.dateTo + "\', ";
		param += "\'" + reservedByID + "\', ";
		param += "\'" + addMe.category + "\'";
		param += ");";
		Console.WriteLine(param);
		reader = DBQuery.createQuery("INSERT", param);
		DBQuery.CloseReader(reader);
		curPos++;
		return 0;
	}
	
	//do zrobienia!
	public int changeTopic(int index){
		return 0;
	}
	
	public int deleteTopic(int index){
		if (index < lista.Count && index > 0)
			lista.RemoveAt(index);
		return 0;
	}
	
	public int acceptTopic(int index){
		lista[index].status = 1;
		return 0;
	}
	
	//zwraca -1, jeśli nie znalazł danego tematu w liście
	public int searchTopic(string name){
		int i = 0;
		for (i = 0; i < lista.Count; i++)
			if (lista[i].name == name)
				return i;
		return -1;
	}
	
	public int changeTopicState(int index, int newState){
		if (index > 0 && index < lista.Count)
			lista[index].status = newState;
		return 0;
	}
	
	//zwraca każdy temat w nowej linii
	override public string ToString(){
		string ret = null;
		foreach (Topic t in lista){
			ret += t.ToString();
			ret += "\n";
		}
		return ret;
	}

	static public TopicList getInstance(){	return instance;}
	
	//zapisuje obecny temat do memento
	public Memento saveToMemento(Topic saveMe){
		return new Memento(saveMe);
	}
	
	//pobiera temat z memento i zapisuje go do bazy
	public void restoreFromMemento(Memento mem){
		addTopic(mem.getOstatni());
	}
}
