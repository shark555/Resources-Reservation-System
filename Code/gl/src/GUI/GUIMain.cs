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
		Button AddTopic;
	[Glade.Widget]
		Label HelloLabel;
	[Glade.Widget]
		Button DeleteTopic;
	[Glade.Widget]
		Button EditTopic;
	
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
		EditTopic.Clicked += OnPressEditTopicEvent;
	}
	
	public static void loadTopics(){
		if (Proxy.getInstance().canDoQuery("SELECT", UserList.getInstance().current().status)){
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
	}else{
		Proxy.getInstance().blad("SELECT");
		return;
	}
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
		if (Proxy.getInstance().canDoQuery("SELECT", UserList.getInstance().current().status) && Proxy.getInstance().canDoQuery("INSERT", UserList.getInstance().current().status))
			new GUIAddTopic();
		else{
			Proxy.getInstance().blad("SELECT, INSERT");
			return;
		}
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
				if (Proxy.getInstance().canDoQuery("DELETE", UserList.getInstance().current().status)){
					String param = " FROM Subjects WHERE ID=" + checkbuttony[i].Name;
					IDataReader reader = DBQuery.createQuery("DELETE", param);
					DBQuery.CloseReader(reader);
				}else{
					Proxy.getInstance().blad("DELETE");
					return;
				}
				j++;
			}
	
		/*if(j == 0){ 
			Gtk.MessageDialog msgDialog = new Gtk.MessageDialog(null, 
			                                                	DialogFlags.DestroyWithParent, 
                                  								MessageType.Info,
                                  								ButtonsType.Ok, 
			                                                	"Nie zaznaczono żadnego tematu");
			msgDialog.Run();
			msgDialog.Destroy();
		}*/	
		loadTopics();
	}
	
	private void OnPressEditTopicEvent(object o, EventArgs e){
		int zaznaczony = -1, indeks1 = 0, indeks2 = 0;	
		Gtk.Widget[] dzieci = TopicTable.Children;
		
		for(int i = 0; i < maxTopics; i++)
			if(checkbuttony[i].Active)
				zaznaczony = i;
		
		//nic nie jest zaznaczone
		if (zaznaczony == -1)
			return;

		zaznaczony %= 5;
		switch (zaznaczony){
			case 0:	indeks1 = 25;
					break;
			case 1:	indeks1 = 19;
					break;
			case 2:	indeks1 = 13;
					break;
			case 3:	indeks1 = 7;
					break;
			case 4:	indeks1 = 1;
					break;
		}
		
		indeks2 = indeks1 + 4;
		
		Gtk.Entry nazwaTmp = new Gtk.Entry(((Gtk.Label)dzieci[indeks2 - 1]).Text);
		Gtk.Entry katTmp = new Gtk.Entry(((Gtk.Label)dzieci[indeks1]).Text);
		Console.WriteLine(indeks1);
	
		if (EditTopic.Label == "OK"){
			if (Proxy.getInstance().canDoQuery("UPDATE", UserList.getInstance().current().status)){
				string lista = " Subjects SET Topic=\'";
				lista += ((Gtk.Entry)dzieci[1]).Text + "\', Cathegory=\'";
				lista += ((Gtk.Entry)dzieci[0]).Text + "\' WHERE id=";
				lista += ((Gtk.Label)dzieci[indeks2 + 2]).Text + ";";
				IDataReader reader = DBQuery.createQuery("UPDATE", lista);
				DBQuery.CloseReader(reader);
			}else{
				Proxy.getInstance().blad("UPDATE");
				return;
			}
			
			//tu dostaje jakieś gtk_critical, ale się program nie wywala
			TopicTable.Remove(nazwaTmp);
			TopicTable.Remove(katTmp);
			Gtk.MessageDialog msgDialog = new Gtk.MessageDialog(null, 
				                                                DialogFlags.DestroyWithParent, 
            	                      							MessageType.Info,
                	                  							ButtonsType.Ok, 
			        	                                        "Dane tematu zostały zmienione");
			msgDialog.Run();
			msgDialog.Destroy();
			EditTopic.Label = "Edytuj temat";
			loadTopics();
			return;
		}
		
		if (EditTopic.Label == "Edytuj temat"){
			EditTopic.Label = "OK";
			//dzieci są wyświetlane od tyłu
			//5 ostatnich widgetów - nazwy kolumn
			//checkboxy mają indeksy = wielokrotności 6
			//labele do edycji są na indeksach: indeks1 ... indeks2
			//Console.WriteLine(zaznaczony + " " + (dzieci[indeks1]).ToString() + " ... " + (dzieci[indeks2]).ToString());
			
			nazwaTmp.WidthChars = 10;
			katTmp.WidthChars = 10;
			
			//schowanie etykiet
			dzieci[indeks2 - 1].Hide();
			dzieci[indeks1].Hide();
			
			//pokazanie pól tekstowych
			TopicTable.Attach(nazwaTmp, 1, 2, (uint)(zaznaczony + 1), (uint)(zaznaczony + 2));
			TopicTable.Attach(katTmp, 4, 5, (uint)(zaznaczony + 1), (uint)(zaznaczony + 2));
			nazwaTmp.Show();
			katTmp.Show();
		}
	}
}