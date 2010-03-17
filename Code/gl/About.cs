using System;
using Gtk;
using Glade;

public class GUIAbout{
	
	[Glade.Widget]
       Label versionLabel;	
	
	[Glade.Widget]
       Button OKbutton;
	
	//[Glade.Widget]
	//	Window window;
	
	public GUIAbout (){
		Application.Init ();		
		
		Glade.XML gxml = new Glade.XML ("ekranabout.glade", "AboutWindow", null);
		gxml.Autoconnect (this);
	
		setEvents();
		
		Application.Run ();
	}

	private void setEvents(){
		OKbutton.Clicked += OnPressButtonEvent;
		versionLabel.Text = "Wersja 0.0.0.0.1";
	}
	
	// Connect the Signals defined in Glade
	private void OnWindowDeleteEvent (object sender, DeleteEventArgs a){
		Application.Quit ();
		a.RetVal = true;
	}

	private void OnPressButtonEvent( object o, EventArgs e){
		//window.Destroy(); -> rzuca wyjątek
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
}