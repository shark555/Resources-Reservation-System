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
		Button DeleteTopic;
	
	public static Label pageLabel;
	public static Table TopicTable;
	private Button nextPage;
	private Button prevPage;
	public static int lastTableIndex = 0;
	public static int page = 1;
	public static int maxPage = 1;
	public static int maxTopics = 0;
	public static CheckButton[] checkbuttony = new CheckButton[100];
	
	public static int iletematow = 0;

	public GUIMain(){
		Glade.XML gxml = new Glade.XML("../../src/GUI/Glade/ekranglowny.glade", "MainWindow", null);
		gxml.Autoconnect(this);
	
		TopicTable = (Gtk.Table)gxml.GetWidget("TopicTable");
		prevPage = (Gtk.Button)gxml.GetWidget("prevPage");
		nextPage = (Gtk.Button)gxml.GetWidget("nextPage");
		pageLabel = (Gtk.Label)gxml.GetWidget("pageLabel");
		setEvents();
		loadTopics();
		
		HelloLabel.Text = "Witaj, " + UserList.getInstance().current().imie;
	}

	private void setEvents(){
		m_quit.ButtonPressEvent += OnPressMenuQuitEvent;
		m_about.ButtonPressEvent += OnPressMenuAboutEvent;
		AddTopic.Clicked += OnPressAddTopicEvent;
		prevPage.Clicked += OnPressPrevPageEvent;
		nextPage.Clicked += OnPressNextPageEvent;
		DeleteTopic.Clicked += OnPressDeleteTopicEvent;
	}
	
	public static void loadTopics(){
		string userID = "1";
		uint curTop = 1, curBot = 2;
		string param = " * FROM Users WHERE Imie=";
		bool isEmpty = true;
		Label nazwaLabel = new Label("Nazwa");
		Label dataOdLabel = new Label("Data rozpoczęcia");
		Label dataDoLabel = new Label("Data zakończenia");
		Label kategoriaLabel = new Label("Kategoria");
		
		maxTopics = 0;
		lastTableIndex = 0;
		
		//czyszczenie tablicy - zeby nie nadpisywać tego, co było
		Gtk.Widget[] dzieci = TopicTable.Children;
		for (int i = 0; i < dzieci.Length; i++)
			TopicTable.Remove(dzieci[i]);
		
		//wypełnianie tabeli nagłówkami
		TopicTable.Attach(new Label("ID"), 0, 1, 0, 1);
		TopicTable.Attach(nazwaLabel, 1, 2, 0, 1);
		TopicTable.Attach(dataOdLabel, 2, 3, 0, 1);
		TopicTable.Attach(dataDoLabel, 3, 4, 0, 1);
		TopicTable.Attach(kategoriaLabel, 4, 5, 0, 1);

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
		int ile = 0;
		while (reader.Read()){
			Label idLabelTmp = new Label(reader["id"].ToString());
			checkbuttony[lastTableIndex] = new CheckButton();
			checkbuttony[lastTableIndex].Name = idLabelTmp.Text;
			if (lastTableIndex < page * 5 && lastTableIndex >= (page - 1) * 5){
				isEmpty = false;
				nazwaLabel = new Label(reader["Topic"].ToString());
				dataOdLabel = new Label(reader["DateFrom"].ToString());
				dataDoLabel = new Label(reader["DateTo"].ToString());
				kategoriaLabel = new Label(reader["Cathegory"].ToString());
		
				TopicTable.Attach(idLabelTmp, 0, 1, curTop, curBot);
				TopicTable.Attach(nazwaLabel, 1, 2, curTop, curBot);
				TopicTable.Attach(dataOdLabel, 2, 3, curTop, curBot);
				TopicTable.Attach(dataDoLabel, 3, 4, curTop, curBot);
				TopicTable.Attach(kategoriaLabel, 4, 5, curTop, curBot);
				TopicTable.Attach((checkbuttony[lastTableIndex]), 5, 6, curTop++, curBot++);
				ile++;
			}
			lastTableIndex++;
			maxTopics++;
		}
		
		DBQuery.CloseReader(reader);
		
		//jeśli nie znalazł żadnych tematów
		if (isEmpty){
			Label empty = new Label("Brak tematów do wyświetlenia.");
			TopicTable.Attach(empty, 0, 4, 2, 4);
		}
		
		TopicTable.ShowAll();
		
		//wyświetlanie stron
		if ((int)(maxTopics / 5) == (maxTopics / 5) + (maxTopics % 5))
			maxPage = (int)(maxTopics / 5);
		else
			maxPage = (int)(maxTopics / 5) + 1;
		pageLabel.Text = page + "/" + maxPage;
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
	
	private void OnPressNextPageEvent(object o, EventArgs e){
		if (page < maxPage){
			page++;
		}
		loadTopics();
	}
	
	private void OnPressPrevPageEvent(object o, EventArgs e){
		if (page > 1){
			page--;
		}
		loadTopics();
	}
	
	private void OnPressDeleteTopicEvent(object o, EventArgs e){
		int j = 0;	
	
		for(int i = 0; i < maxTopics; i++)
			if(checkbuttony[i].Active){
				Console.WriteLine(checkbuttony[i].Name);
				String param = " FROM Subjects WHERE ID=" + checkbuttony[i].Name;
				IDataReader reader = DBQuery.createQuery("DELETE", param);
				DBQuery.CloseReader(reader);	
				j++;
			}
	
		if(j == 0){ 
			Gtk.MessageDialog msgDialog = new Gtk.MessageDialog(null, 
			                                                	DialogFlags.DestroyWithParent, 
                                  								MessageType.Info,
                                  								ButtonsType.Ok, 
			                                                	"Nie zaznaczono żadnego tematu");
			int res = msgDialog.Run();
			msgDialog.Destroy();
		}	
		loadTopics();
	}
}