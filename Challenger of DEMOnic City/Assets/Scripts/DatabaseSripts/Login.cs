using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class Login : MonoBehaviour
{
    [Header("InputObjects")]
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;

    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    private MySqlDataReader MS_Reader;
    string query;

    public void login()
    {
        connection();
        try
        {

            query = $"SELECT * FROM users WHERE username = '{username.text}' AND password = '{password.text}';";

            MS_Command = new MySqlCommand(query, MS_Connection);

            MS_Reader = MS_Command.ExecuteReader();
            while (MS_Reader.Read())
            {
                //usernameCheck = MS_Reader[0].ToString();
                //usernameCheck = MS_Reader[0].ToString();

                //Debug.Log(usernameCheck);
                Debug.Log(MS_Reader[0].ToString()+ MS_Reader[1].ToString() + MS_Reader[2].ToString() + MS_Reader[3].ToString()+ MS_Reader[4].ToString());
            }
            //Debug.Log(usernameCheck);
            if (Convert.ToInt32(true) == 0)
            {
                //registration();
            }
            else
            {
                Debug.Log("Hiba a bejelentkezés közben");
            }
        }
        catch (System.Exception)
        {
            //errorMessage.text = errortext;
            throw;
        }
        finally
        {
            MS_Connection.Close();
        }
    }

    public void connection()
    {
        connectionString = "Server = localhost; Database = challengerofdemoniccity; User = root; Password = ;";

        MS_Connection = new MySqlConnection(connectionString);

        MS_Connection.Open();
    }
}
