using System;
using Gtk;
using Glade;

public class GUIAbout{
	
	[Glade.Widget]
       	Label versionLabel;	
	[Glade.Widget]
       	Button OKButton;
	[Glade.Widget]
		Window AboutWindow;
	
	public GUIAbout(){
		Glade.XML gxml = new Glade.XML("../../src/GUI/Glade/ekranabout.glade", "AboutWindow", null);
		gxml.Autoconnect(this);
	
		setEvents();
	}

	private void setEvents(){
		OKButton.Clicked += OnPressButtonEvent;
		versionLabel.Text = "Wersja 0.0.0.0.1";
	}
	
	// Connect the Signals defined in Glade
	private void OnWindowDeleteEvent(object sender, DeleteEventArgs a){
		AboutWindow.Destroy();
		a.RetVal = true;
	}

	private void OnPressButtonEvent(object o, EventArgs e){
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
}