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
	[Glade.Widget]
		Fixed fixed1;
	//MEMENTO
	//dodać guzik cofający zmiany (usuwanie, cofanie)
	//w jego akcji uruchomić metodę TopicList.restoreFromMemento(Memento);
	//obiekty typu Memento można trzymać w liście -> cofanie do dowolnego stanu poprzedniego
	
	public static Label pageLabel;
	public static Table TopicTable;
	protected Button nextPage;
	protected Button prevPage;
	public static int lastTableIndex = 0;
	public static int page = 0;
	public static int maxPage = 0;
	public static int maxTopics = 0;
	public static CheckButton[] checkbuttony = new CheckButton[100];
	public static int iletematow = 0;
	protected bool maszDwaNoweKomponentyDoPoliczenia = false;

	public GUIMain(){
		Glade.XML gxml = new Glade.XML("../../src/GUI/Glade/ekranglowny.glade", "MainWindow", null);
		gxml.Autoconnect(this);
	
		TopicTable = (Gtk.Table)gxml.GetWidget("TopicTable");
		prevPage = (Gtk.Button)gxml.GetWidget("prevPage");
		nextPage = (Gtk.Button)gxml.GetWidget("nextPage");
		pageLabel = (Gtk.Label)gxml.GetWidget("pageLabel");
		
		setEvents();
		loadTopics();
	}
	
	protected void addWidget(Gtk.Widget addMe, int posX, int posY){
		fixed1.Put(addMe, posX, posY);
		addMe.Show();
	}

	protected void setEvents(){
		HelloLabel.Text = "Witaj, " + UserList.getInstance().current().imie;
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
			
			//MEMENTO
			//MainClass.a_topicList = new TopicList();
			
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
					
					//MEMENTO
					//tutaj trzeba dodać tematy do TopicList
					//przy każdym ładowaniu, instancja TopicList musi być null na początku
			
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
	protected void OnWindowDeleteEvent(object sender, DeleteEventArgs a){
		Application.Quit();
		a.RetVal = true;
	}
	
	protected void OnPressMenuQuitEvent(object o, EventArgs e){
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
	
	protected void OnPressMenuAboutEvent(object o, EventArgs e){
		new GUIAbout();
    }
	
	protected void OnPressAddTopicEvent(object o, EventArgs e){
		if (Proxy.getInstance().canDoQuery("SELECT", UserList.getInstance().current().status) && Proxy.getInstance().canDoQuery("INSERT", UserList.getInstance().current().status))
			new GUIAddTopic();
		else{
			Proxy.getInstance().blad("SELECT, INSERT");
			return;
		}
    }
	
	protected void OnPressNextPageEvent(object o, EventArgs e){
		if (page < maxPage){
			page++;
		}
		loadTopics();
	}
	
	protected void OnPressPrevPageEvent(object o, EventArgs e){
		if (page > 1){
			page--;
		}
		loadTopics();
	}
	
	protected void OnPressDeleteTopicEvent(object o, EventArgs e){
		int j = 0;	
	
		for(int i = 0; i < maxTopics; i++)
			if(checkbuttony[i].Active){
				if (Proxy.getInstance().canDoQuery("DELETE", UserList.getInstance().current().status)){
					String param = " FROM Subjects WHERE ID=" + checkbuttony[i].Name;
					IDataReader reader = DBQuery.createQuery("DELETE", param);
					DBQuery.CloseReader(reader);
				
					//MEMENTO
					/*param = " * FROM Subjects WHERE ID=" + checkbuttony[i].Name;
					reader = DBQuery.createQuery("SELECT", param);
					if (reader.Read())
						TopicList.getInstance().saveToMemento(TopicList.getInstance().getByIndex(TopicList.getInstance().searchTopic(reader["Topic"].ToString())));
					DBQuery.CloseReader(reader);*/
				}else{
					Proxy.getInstance().blad("DELETE");
					return;
				}
				j++;
			}
		loadTopics();
	}
	
	protected void OnPressEditTopicEvent(object o, EventArgs e){
		int zaznaczony = -1, indeks1 = 0, indeks2 = 0, offset = 0;	
		Gtk.Widget[] dzieci = TopicTable.Children;
		Gtk.Entry nazwaTmp = null, katTmp = null;
		
		if (maszDwaNoweKomponentyDoPoliczenia)
			offset = 12;
		else
			offset = 10;
		
		for(int i = 0; i < maxTopics; i++)
			if(checkbuttony[i].Active)
				zaznaczony = i;
		
		//nic nie jest zaznaczone
		if (zaznaczony == -1)
			return;

		zaznaczony %= 5;
		switch (zaznaczony){
			case 0:	indeks1 = dzieci.Length - offset;
					break;
			case 1:	indeks1 = dzieci.Length - offset - 6;
					break;
			case 2:	indeks1 = dzieci.Length - offset - 12;
					break;
			case 3:	indeks1 = dzieci.Length - offset - 18;
					break;
			case 4:	indeks1 = dzieci.Length - offset - 24;
					break;
		}
		
		indeks2 = indeks1 + 4;
	
		if (EditTopic.Label == "OK"){
			
			maszDwaNoweKomponentyDoPoliczenia = false;
			
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

			Gtk.MessageDialog msgDialog = new Gtk.MessageDialog(null, 
				                                                DialogFlags.DestroyWithParent, 
            	                      							MessageType.Info,
                	                  							ButtonsType.Ok, 
			        	                                        "Dane tematu zostały zmienione");
			msgDialog.Run();
			msgDialog.Destroy();
			EditTopic.Label = "Edytuj temat";
			loadTopics();
			
			prevPage.Visible  = true;
			nextPage.Visible  = true;
			DeleteTopic.Visible = true;
			AddTopic.Visible = true;
			
			return;
		}
		
		if (EditTopic.Label == "Edytuj temat"){
			
			nazwaTmp = new Gtk.Entry(((Gtk.Label)dzieci[indeks2 - 1]).Text);
			katTmp = new Gtk.Entry(((Gtk.Label)dzieci[indeks1]).Text);
			
			prevPage.Visible  = false;
			nextPage.Visible  = false;
			DeleteTopic.Visible = false;
			AddTopic.Visible = false;
			
			//po dodaniu dwóch Gtk.Entry się sypie, to jest zabezpieczenie
			maszDwaNoweKomponentyDoPoliczenia = true;
			
			EditTopic.Label = "OK";
			
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