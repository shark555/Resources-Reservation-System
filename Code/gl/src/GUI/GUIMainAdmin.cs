using System;
using Gtk;
using Glade;

public class GUIMainAdmin : GUIMain{
		
	private Button UserAdministration;
		
	public GUIMainAdmin(){
		
		UserAdministration = new Button();
		UserAdministration.SetSizeRequest(296, 33);
		UserAdministration.Name = "UserAdministration";
		UserAdministration.Label = "Otwórz okno administracji użytkownikami";
		base.addWidget(UserAdministration, 342, 53);
		
		setEvents();
	}
	
	new private void setEvents(){
		base.setEvents();
		UserAdministration.Clicked += OnPressUserAdmin;
	}
			
	protected void OnPressUserAdmin(object o, EventArgs e){
		base.OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
}
