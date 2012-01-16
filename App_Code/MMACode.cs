using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for MMACode
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MMACode : System.Web.Services.WebService {

    public MMACode () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    
    [WebMethod]   
    public string GetUserName(int UserID)
    {
        //using(SQLiteConnection conn = new SQLiteConnection(Tools.connectionString()))
        //{
        //    conn.Open();
        //    SQLiteCommand cmd = new SQLiteCommand("Select UserName FROM CodenameMMA.Logins WHERE UserID=" + UserID, conn);
        //    return cmd.ExecuteScalar().ToString();
        //}   
        using (MySqlConnection conn = new MySqlConnection(Tools.connectionString()))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select Username FROM Logins WHERE UserID=" + UserID, conn);
            return cmd.ExecuteScalar().ToString();
        }
    }
}
