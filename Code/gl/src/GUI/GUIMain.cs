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
	[Glade.Widget]
		Button AddTopic;

	public GUIMain(){
		Glade.XML gxml = new Glade.XML("../../src/GUI/Glade/ekranglowny.glade", "MainWindow", null);
		gxml.Autoconnect(this);
	
		setEvents();
	}

	private void setEvents(){
		m_quit.ButtonPressEvent += OnPressMenuQuitEvent;
		m_about.ButtonPressEvent += OnPressMenuAboutEvent;
		AddTopic.Clicked += OnPressAddTopicEvent;
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
		new GUIAddTopic();
    }
}