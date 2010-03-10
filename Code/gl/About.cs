using System;
using Gtk;
using Glade;

public class GUIAbout
{
	public static void Main (string[] args)
	{
		new GUIAbout (args);
	}

	public	GUIAbout (string[] args)
	{
		Application.Init ();
		
		
		
		Glade.XML gxml = new Glade.XML ("ekranabout.glade", "window1", null);
		gxml.Autoconnect (this);
		
		OKbutton.Clicked += OnPressButtonEvent;
		versionLabel.Text = "Wersja 0.0.0.0.1";	
	
		Application.Run ();
	}

	// Connect the Signals defined in Glade
	private void OnWindowDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
//	 [Glade.Widget]
//       Button button1;
//	
//	[Glade.Widget]
//       Image image1;
	
	[Glade.Widget]
       Label versionLabel;	
	
	[Glade.Widget]
       Button OKbutton;	
	
	

	public void OnPressButtonEvent( object o, EventArgs e)
        {
		
		
		Application.Quit();
		
		
        }
	
	
	

	
}

