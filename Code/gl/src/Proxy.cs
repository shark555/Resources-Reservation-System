using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Gtk;

public class Proxy{
		
	Dictionary<string, int> mapaDostepu;
	
	static private Proxy instance = null;
	
	public Proxy(){
		mapaDostepu = new Dictionary<string, int>();
		mapaDostepu.Add("SELECT", 0);
		mapaDostepu.Add("INSERT", 0);
		mapaDostepu.Add("UPDATE", 0);
		mapaDostepu.Add("DELETE", 0);
		mapaDostepu.Add("CREATE", 2);
		mapaDostepu.Add("DROP", 2);
		mapaDostepu.Add("GRANT", 2);
		mapaDostepu.Add("REFERENCES", 2);
		mapaDostepu.Add("INDEX", 2);
		mapaDostepu.Add("ALTER", 2);
		mapaDostepu.Add("CREATE_TMP_TABLE", 2);
		mapaDostepu.Add("LOCK_TABLES", 2);
		mapaDostepu.Add("CREATE_VIEW", 2);
		mapaDostepu.Add("SHOW_VIEW", 2);
		mapaDostepu.Add("CREATE_ROUTINE", 2);
		mapaDostepu.Add("ALTER_ROUTINE", 2);
		mapaDostepu.Add("EXECUTE", 2);
		mapaDostepu.Add("EVENT", 2);
		mapaDostepu.Add("TRIGGER", 2);
		instance = this;	
	}
	
	public bool canDoQuery(string query, int poziomUprawnien){
		
		if (mapaDostepu[query] <= poziomUprawnien)
			return true;
		else 
			return false;
	}
	
	public static Proxy getInstance(){	return instance;}
	
	public void blad(string query){
		Gtk.MessageDialog msgDialog = new Gtk.MessageDialog(null, 
				                                                DialogFlags.DestroyWithParent, 
            	                      							MessageType.Warning,
                	                  							ButtonsType.Ok, 
			        	                                        "Brak uprawnień do wykonania operacji: " + query + "!");
		msgDialog.Run();
		msgDialog.Destroy();
	}
}
