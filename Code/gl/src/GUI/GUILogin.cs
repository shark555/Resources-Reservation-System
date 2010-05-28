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
		LoginWindow.KeyPressEvent += HandleKeyPressEvent;
	}

	//nie wiem, gdzie jest enter
	private void HandleKeyPressEvent(object o, KeyPressEventArgs args){
		switch(args.Event.Key){
			case Gdk.Key.Escape:	CancelButton.Click();
									break;
			default:	break;
		}
	}
	
	// Connect the Signals defined in Glade
	private void OnWindowDeleteEvent(object sender, DeleteEventArgs a){
		Application.Quit();
		a.RetVal = true;
	}

	private void OnPressButtonOKEvent(object o, EventArgs e){
		if (checkLogin()){
			LoginWindow.Destroy();
			switch (UserList.getInstance().current().status) {
				//student
				case 0:	new GUIMain();
						break;
				//wykladowca
				case 1:	new GUIMainWykladowca();
						break;
				//admin
				case 2:	new GUIMainAdmin();
						break;
			}
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