using System;
using Gtk;
using Glade;

public class GUIMain{
	
	[Glade.Widget]
		ImageMenuItem m_about;
	
	[Glade.Widget]
		ImageMenuItem m_quit;
	
	[Glade.Widget]
		Window MainWindow;
			
	public static void Main (string[] args){
		new GUIMain (args);
	}

	public GUIMain (string[] args){
		Application.Init ();		
		
		Glade.XML gxml = new Glade.XML ("ekranglowny.glade", "MainWindow", null);
		gxml.Autoconnect (this);
	
		setEvents();
		
		Application.Run ();
	}

	private void setEvents(){
		m_quit.ButtonPressEvent += OnPressMenuQuitEvent;
		m_about.ButtonPressEvent += OnPressMenuAboutEvent;
	}
	
	// Connect the Signals defined in Glade
	private void OnWindowDeleteEvent (object sender, DeleteEventArgs a){
		MainWindow.Destroy();
		Application.Quit ();
		a.RetVal = true;
	}
	
	private void OnPressMenuQuitEvent( object o, EventArgs e){
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
	
	private void OnPressMenuAboutEvent( object o, EventArgs e){
		new GUIAbout();
    }
}