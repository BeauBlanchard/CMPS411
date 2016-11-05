using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Data;
using Mono.Data.Sqlite;

public class Register : MonoBehaviour {

    public GameObject username;
    public GameObject password;
    public GameObject confPassword;
    private string Username;
    private string Password;
    private string ConfPassword;
    private string form;
    private string connectionString;
    private int check = 0;

    // Use this for initialization
    void Start () {

        connectionString = "URI=file:" + Application.dataPath + "/Login DB.sqlite";

    }

    public void RegisterButton() {
        bool PW = false; 
        bool CPW = false;
        bool UN = false;
        
        //Confirm username parameters
        if (Username != "") {
            if (Username.Length >= 6) {
                UN = true;
            }
            else {
                print("Username must be at least 6 characters long");
            }
        }
        else {
            print("The username field is empty");
        }
        
        //Confirm password parameters
        if (Password != "") {
            if (Password.Length >= 6) {
                PW = true;
            }
            else {
                print("Password must be at least 6 characters long.");
            }
        }
        else {
            print("The password field is empty");
        }
        
        //Confirm matching password
        if(ConfPassword == Password) {
            CPW = true;
        }
        else {
            print("Passwords do not match");
        }

        if(UN == true && PW == true && CPW == true) {
            bool taken = Checker(Username);
            if (taken == false)
            {
                print("Registration Successful!");
                InsertProfile(Username, Password);
            }
            else
            {
                Debug.Log("Username taken!");
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (username.GetComponent<InputField>().isFocused) {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused) {
                confPassword.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            if(Username != "" && Password != "" && ConfPassword != "") {
                RegisterButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confPassword.GetComponent<InputField>().text;
        
    }


    private bool Checker(string username)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = String.Format("SELECT * FROM Holden WHERE Username = \"{0}\"", username);
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

        if (check == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void InsertProfile(string name, string pass)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {

            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = String.Format("INSERT INTO Holden(Username,Password) VALUES(\"{0}\",\"{1}\")", name, pass);
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }


}
