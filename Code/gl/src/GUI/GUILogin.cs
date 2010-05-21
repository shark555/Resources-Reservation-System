using System;
using Gtk;
using Glade;
using System.Data;

public class GUILogin{	
	
	[Glade.Widget]
       	Button OKButton;
	[Glade.Widget]
       	Button CancelButton;
	[Glade.Widget]
		Window LoginWindow;
	[Glade.Widget]
		Entry login;
	[Glade.Widget]
		Entry password;
	
	public GUILogin(){
		Application.Init();
		Glade.XML gxml = new Glade.XML("../../src/GUI/Glade/ekranlogowania.glade", "LoginWindow", null);
		gxml.Autoconnect(this);
	
		setEvents();
		
		password.Visibility = false;
		
		Application.Run();
	}

	private bool checkLogin(){
		//sprawdzenie, czy w bazie jest login + hasło
		string lista = " * FROM Users WHERE Name=\'";
		lista += login.Text;
		lista += "\' AND Passwd=\'";
		lista += password.Text;
		lista += "\';";
		IDataReader reader = DBQuery.createQuery("SELECT", lista);
		if (reader.Read()){
			Console.WriteLine(reader["Name"] + " - " + reader["passwd"]);
			UserList.getInstance().setCurrentPos(reader["name"].ToString());
			DBQuery.CloseReader(reader);
			return true;
		}else{
			DBQuery.CloseReader(reader);
			return false;
		}
	}
	
	private void setEvents(){
		OKButton.Clicked += OnPressButtonOKEvent;
		CancelButton.Clicked += OnPressButtonCancelEvent;
	}
	
	// Connect the Signals defined in Glade
	private void OnWindowDeleteEvent(object sender, DeleteEventArgs a){
		Application.Quit();
		a.RetVal = true;
	}

	private void OnPressButtonOKEvent(object o, EventArgs e){
		if (checkLogin()){
			LoginWindow.Destroy();
			new GUIMain();
			//new GUIAddTopic();
		}else{
			//informacja o błędnym logowaniu
			Gtk.MessageDialog msgDialog = new Gtk.MessageDialog(null, 
			                                                    DialogFlags.DestroyWithParent, 
                                  								MessageType.Error,
                                  								ButtonsType.Ok, 
			                                                    "Błędne logowanie!");
			msgDialog.Run();
			login.Text = "";
			password.Text = "";
			msgDialog.Destroy();
		}
    }
	
	private void OnPressButtonCancelEvent(object o, EventArgs e){
		OnWindowDeleteEvent(this, new DeleteEventArgs());
    }
}