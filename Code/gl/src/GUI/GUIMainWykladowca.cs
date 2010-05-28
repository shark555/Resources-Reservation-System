using System;
using Gtk;
using Glade;

public class GUIMainWykladowca : GUIMain{
		
	private Button ShowTopics;
		
	public GUIMainWykladowca(){
		
		ShowTopics = new Button();
		ShowTopics.SetSizeRequest(296, 33);
		ShowTopics.Name = "ShowTopics";
		ShowTopics.Label = "Pokaż tematy oczekujące akceptacji";
		base.addWidget(ShowTopics, 342, 53);
		
		setEvents();
	}
	
	new private void setEvents(){
		base.setEvents();
		ShowTopics.Clicked += OnPressShowTopics;
	}
			
	protected void OnPressShowTopics(object o, EventArgs e){
		base.OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
}
