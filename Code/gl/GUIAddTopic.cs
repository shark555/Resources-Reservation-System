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
	
	public GUIAddTopic(){
		Application.Init ();		
		
		Glade.XML gxml = new Glade.XML("ekrandodaniatematu.glade", "AddTopicWindow", null);
		gxml.Autoconnect(this);
	
		setEvents();
		
		Application.Run();
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
		//wyświetlenie informacji o temacie i przycisku OK
    }
	
	private void OnPressButtonCancelEvent(object o, EventArgs e){
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
}