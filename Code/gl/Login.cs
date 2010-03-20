using System;
using Gtk;
using Glade;

public class GUILogin{	
	
	[Glade.Widget]
       	Button OKbutton;
	
	[Glade.Widget]
       	Button CancelButton;
	
	[Glade.Widget]
		Window LoginWindow;
	
	public GUILogin(){
		Application.Init ();		
		
		Glade.XML gxml = new Glade.XML("ekranlogowania.glade", "LoginWindow", null);
		gxml.Autoconnect (this);
	
		setEvents();
		
		Application.Run();
	}

	private bool checkLogin(){
		//sprawdzenie, czy w bazie jest login + hasło
		return true;
	}
	
	private void setEvents(){
		OKbutton.Clicked += OnPressButtonOKEvent;
		CancelButton.Clicked += OnPressButtonCancelEvent;
	}
	
	// Connect the Signals defined in Glade
	private void OnWindowDeleteEvent(object sender, DeleteEventArgs a){
		Application.Quit();
		a.RetVal = true;
	}

	private void OnPressButtonOKEvent(object o, EventArgs e){
		if (checkLogin())
			new GUIMain();
		else{
			//informacja o błędnym logowaniu
		}
    }
	
	private void OnPressButtonCancelEvent(object o, EventArgs e){
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
}