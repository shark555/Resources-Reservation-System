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
	
	public GUIAddTopic(){
		Glade.XML gxml = new Glade.XML("../../src/GUI/Glade/ekrandodaniatematu.glade", "AddTopicWindow", null);
		gxml.Autoconnect(this);
	
		setEvents();
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
		
		if (zglaszajacy.status == 0){	//student
			TopicList.getInstance().addTopic(new Topic("przedmiot", 
			                                           topic.Text, 
			                                           CalendarFrom.GetDate().ToString(),	
			                                           CalendarTo.GetDate().ToString(),
			                                           zglaszajacy.nazwisko + " " + zglaszajacy.imie,
			                                           "test",
			                                           zglaszajacy));
		}
		if (zglaszajacy.status == 1){	//wykladowca
			TopicList.getInstance().addTopic(new Topic("przedmiot", 
			                                           topic.Text, 
			                                           CalendarFrom.GetDate().ToString(),	
			                                           CalendarTo.GetDate().ToString(),
			                                           zglaszajacy.nazwisko + " " + zglaszajacy.imie,
			                                           "test"));
		}
    }
	
	private void OnPressButtonCancelEvent(object o, EventArgs e){
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
}