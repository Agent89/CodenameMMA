using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
/// <summary>
/// Summary description for MMACode
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MMACode : System.Web.Services.WebService
{

    public MMACode()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetUserInfo(string username, string password)
    {
        User user = new User();
        using (MySqlConnection conn = new MySqlConnection(Tools.connectionString()))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("GetUserInfo",conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("_userid", DBNull.Value);
            cmd.Parameters.AddWithValue("_username", username);
            cmd.Parameters.AddWithValue("_password", password);

            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                user.UserID = Convert.ToInt32(reader["UserID"]);
                user.UserName = reader["UserName"].ToString();
                user.Wallet.Amount = Convert.ToDouble(reader["Amount"]);
                user.Wallet.Refills = Convert.ToInt32(reader["Refills"]);
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(user);
        return strJSON;
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

    [WebMethod]
    public string GetWalletAmount(int UserID)
    {
        using (MySqlConnection conn = new MySqlConnection(Tools.connectionString()))
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("Select Amount FROM Wallets WHERE UserID=" + UserID, conn);
            int amount = 0;
            int.TryParse(cmd.ExecuteScalar().ToString(), out amount);

            return amount.ToString("C");
        }
    }

    class User
    {
        private string _username = String.Empty;
        private int _userid = 0;
        private Wallet _wallet = new Wallet();

        public string UserName
        { get { return _username; } set { _username = value; } }

        public int UserID
        { get { return _userid; } set { _userid = value; } }

        public Wallet Wallet
        { get { return _wallet; } set { _wallet = value; } }
    }

    class Wallet
    {
        private double _amount = 0d;
        private int _refills = 0;

        public double Amount
        { get { return _amount; } set { _amount = value; } }

        public int Refills
        { get { return _refills; } set { _refills = value; } }
    }
}
