using System;
using Gtk;
using Glade;

public class GUIAddTopic{	
	
	[Glade.Widget]
       	Button OKButton;
	[Glade.Widget]
	   	Button CancelButton;
	[Glade.Widget]
		Window AddTopicWindow;
	[Glade.Widget]
		Entry topic;
	[Glade.Widget]
		Calendar CalendarFrom;
	[Glade.Widget]
		Calendar CalendarTo;
	[Glade.Widget]
		Entry catEntry;
	
	public GUIAddTopic(){
		Glade.XML gxml = new Glade.XML("../../src/GUI/Glade/ekrandodaniatematu.glade", "AddTopicWindow", null);
		gxml.Autoconnect(this);
	
		setEvents();
		
		CalendarFrom.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
		CalendarTo.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
		
		AddTopicWindow.Show();
	}
	
	private void setEvents(){
		OKButton.Clicked += OnPressButtonOKEvent;
		CancelButton.Clicked += OnPressButtonCancelEvent;
	}
	
	// Connect the Signals defined in Glade
	private void OnWindowDeleteEvent(object sender, DeleteEventArgs a){
		AddTopicWindow.Destroy();
		a.RetVal = true;
	}

	private void OnPressButtonOKEvent(object o, EventArgs e){
		User zglaszajacy = UserList.getInstance().current();
		bool zapisz = true;
		
		//brak tematu
		if (topic.Text.Length == 0){
			Gtk.MessageDialog msgDialog1 = new Gtk.MessageDialog(null, 
			            	                                    DialogFlags.DestroyWithParent, 
                    	              							MessageType.Warning,
                	                  							ButtonsType.Ok,
				                                                "Pole \"Temat\" nie może być puste!");
			msgDialog1.Run();
			msgDialog1.Destroy();
			zapisz = false;
		}
		
		//brak kategorii
		if (catEntry.Text.Length == 0){
			Gtk.MessageDialog msgDialog2 = new Gtk.MessageDialog(null, 
			                                                	DialogFlags.DestroyWithParent, 
	                                  							MessageType.Warning,
    	                              							ButtonsType.Ok,
				                                                "Pole \"Kategoria\" nie może być puste!");
			msgDialog2.Run();
			msgDialog2.Destroy();
			zapisz = false;
		}
		
		//zła data
		if (CalendarFrom.GetDate() > CalendarTo.GetDate()){
			Gtk.MessageDialog msgDialog3 = new Gtk.MessageDialog(null, 
			                                                	DialogFlags.DestroyWithParent, 
	                                  							MessageType.Warning,
    	                              							ButtonsType.Ok,
				                                                "Data początku nie może być po dacie końca!");
			msgDialog3.Run();
			msgDialog3.Destroy();
			zapisz = false;
		}
		
		if (zapisz){
			string dataOd = CalendarFrom.GetDate().ToString().Substring(0, CalendarFrom.GetDate().ToString().IndexOf(" "));
			string dataDo = CalendarTo.GetDate().ToString().Substring(0, CalendarTo.GetDate().ToString().IndexOf(" "));
			if (zglaszajacy.status == 0){	//student
				TopicList.getInstance().addTopic(new Topic("przedmiot", 
				                                           topic.Text, 
				                                           dataOd,	
			    	                                       dataDo,
			        	                                   zglaszajacy.nazwisko + " " + zglaszajacy.imie,
			            	                               catEntry.Text,
			                	                           zglaszajacy));
			}
			if (zglaszajacy.status >= 1){	//wykladowca lub admin
				TopicList.getInstance().addTopic(new Topic("przedmiot", 
				                                           topic.Text, 
			    	                                       dataOd,	
			    	                                       dataDo,
			            	                               zglaszajacy.nazwisko + " " + zglaszajacy.imie,
			                	                           catEntry.Text));
			}
		
			Gtk.MessageDialog msgDialog = new Gtk.MessageDialog(null, 
				                                                DialogFlags.DestroyWithParent, 
        	                          							MessageType.Info,
            	                      							ButtonsType.Ok, 
			    	                                            "Dodano temat.");
			msgDialog.Run();
			msgDialog.Destroy();
			GUIMain.loadTopics();
			OnWindowDeleteEvent(this, new DeleteEventArgs());
		}
    }
	
	private void OnPressButtonCancelEvent(object o, EventArgs e){
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
}