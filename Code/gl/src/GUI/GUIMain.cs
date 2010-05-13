using System;
using Gtk;
using Glade;
using System.Data;

public class GUIMain{
	
	[Glade.Widget]
		ImageMenuItem m_about;
	[Glade.Widget]
		ImageMenuItem m_quit;
	[Glade.Widget]
		Window MainWindow;
	[Glade.Widget]
		Button AddTopic;
	[Glade.Widget]
		Label HelloLabel;
	[Glade.Widget]
		public static Table TopicTable;

	public GUIMain(){
		Glade.XML gxml = new Glade.XML("../../src/GUI/Glade/ekranglowny.glade", "MainWindow", null);
		gxml.Autoconnect(this);
	
		TopicTable = (Gtk.Table)gxml.GetWidget("TopicTable");
		setEvents();
		loadTopics();
		
		HelloLabel.Text = "Witaj, " + UserList.getInstance().current().imie;
	}

	private void setEvents(){
		m_quit.ButtonPressEvent += OnPressMenuQuitEvent;
		m_about.ButtonPressEvent += OnPressMenuAboutEvent;
		AddTopic.Clicked += OnPressAddTopicEvent;
	}
	
	//do zrobienia - scroll listy tematów!
	public static void loadTopics(){
		string userID = "1";
		uint curTop = 1, curBot = 2;
		string param = " * FROM Users WHERE Imie=";
		bool isEmpty = true;
		Label nazwaLabel = new Label("Nazwa");
		Label dataOdLabel = new Label("Data rozpoczęcia");
		Label dataDoLabel = new Label("Data zakończenia");
		Label kategoriaLabel = new Label("Kategoria");
		
		//czyszczenie tablicy - zeby nie nadpisywać tego, co było
		Gtk.Widget[] dzieci = TopicTable.Children;
		for (int i = 0; i < dzieci.Length; i++)
			TopicTable.Remove(dzieci[i]);
		
		//wypełnianie tabeli nagłówkami
		TopicTable.Attach(nazwaLabel, 0, 1, 0, 1);
		TopicTable.Attach(dataOdLabel, 1, 2, 0, 1);
		TopicTable.Attach(dataDoLabel, 2, 3, 0, 1);
		TopicTable.Attach(kategoriaLabel, 3, 4, 0, 1);
		
		//pobranie userid z bazy
		param += "\'" + UserList.getInstance().current().imie + "\' ";
		param += "AND Nazwisko=\'" + UserList.getInstance().current().nazwisko + "\';";
		IDataReader reader = DBQuery.createQuery("SELECT", param);
		if (reader.Read())
			userID = reader["ID"].ToString();
		else
			Console.WriteLine("ERROR: " + param);
		
		DBQuery.CloseReader(reader);
		
		//pobranie tematów użytkownika i wpisanie ich do tabeli		
		param = " * FROM Subjects WHERE UserID=\'" + userID + "\';";
		reader = DBQuery.createQuery("SELECT", param);
		while (reader.Read()){
			isEmpty = false;
			Label nazwaLabelTmp = new Label(reader["Topic"].ToString());
			Label dataOdLabelTmp = new Label(reader["DateFrom"].ToString());
			Label dataDoLabelTmp = new Label(reader["DateTo"].ToString());
			kategoriaLabel = new Label(reader["Cathegory"].ToString());
		
			TopicTable.Attach(nazwaLabelTmp, 0, 1, curTop, curBot);
			TopicTable.Attach(dataOdLabelTmp, 1, 2, curTop, curBot);
			TopicTable.Attach(dataDoLabelTmp, 2, 3, curTop, curBot);
			TopicTable.Attach(kategoriaLabel, 3, 4, curTop++, curBot++);
		}
		
		DBQuery.CloseReader(reader);
		
		//jeśli nie znalazł żadnych tematów
		if (isEmpty){
			Label empty = new Label("Brak tematów do wyświetlenia.");
			TopicTable.Attach(empty, 0, 4, 2, 4);
		}
		
		TopicTable.ShowAll();
	}
	
	// Connect the Signals defined in Glade
	private void OnWindowDeleteEvent(object sender, DeleteEventArgs a){
		Application.Quit();
		a.RetVal = true;
	}
	
	private void OnPressMenuQuitEvent(object o, EventArgs e){
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
	
	private void OnPressMenuAboutEvent(object o, EventArgs e){
		new GUIAbout();
    }
	
	private void OnPressAddTopicEvent(object o, EventArgs e){
		new GUIAddTopic();
    }
}