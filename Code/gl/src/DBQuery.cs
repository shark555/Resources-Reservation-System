using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;


public class DBQuery{

	static string queryText;
	static IDbConnection dbcon;
	static IDbCommand dbcmd;
	static public string name{
		get;
		set;
	}
	static public string pass{
		get;
		set;
	}
	
	public static IDataReader createQuery(string query, string lista){
		queryText += query + lista;
		return doQuery();
	}

	//problem z zamykaniem do rozwiązania (zmienne statyczne klasy)
	public static IDataReader doQuery(){
       	string connectionString =
          	"Server=localhost;" +
          	"Database=RRS;" +
          	"User ID=shark;" +
          	"Password=dupadupa;" +
          	"Pooling=false";
       	dbcon = new MySqlConnection(connectionString);
       	dbcon.Open();
       	dbcmd = dbcon.CreateCommand();
       	string sql = queryText;
       	dbcmd.CommandText = sql;
       	IDataReader reader = dbcmd.ExecuteReader();
		return reader;
    }

	public static void CloseReader(IDataReader reader) {
		reader.Close();
		reader = null;
		dbcmd.Dispose();
       	dbcmd = null;
    	dbcon.Close();
	    dbcon = null;
		queryText = null;
	}
}
