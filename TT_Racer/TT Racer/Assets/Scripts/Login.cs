using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Data;
using Mono.Data.Sqlite;

public class Login : MonoBehaviour {

    public GameObject username;
    public GameObject password;
    public static string Username;
    private string Password;
    private string connectionString;
    private int check = 0;
    
    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/Login DB.sqlite";
    }


    public void LoginButton() {
        bool user = false;
        bool pass = false;

        if(Username != "") {
            user = true;
        }
        else {
            print("Please enter a username");
        }

        if(Password != "") {
            pass = true;
        }
        else {
            print("Please enter a password");
        }

        if(user == true && pass == true) {
            bool valid = Checker(Username, Password);
            if (valid == true)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("Incorrect username or password!");
            }
        }
    }
	
    private bool Checker(string username, string password)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("SELECT * FROM Holden WHERE Username = \"{0}\" AND Password = \"{1}\"", username, password);
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        check++;

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        
        if(check > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    void Update () {
	    if (Input.GetKeyDown(KeyCode.Tab)) {
            if (username.GetComponent<InputField>().isFocused) {
                password.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            if(Username != "" && Password != "") {
                LoginButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
	}
}
