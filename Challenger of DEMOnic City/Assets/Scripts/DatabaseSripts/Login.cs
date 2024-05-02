using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject mainMenu;
    public GameObject loginMenu;
    [Header("InputObjects")]
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;

    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    private MySqlDataReader MS_Reader;
    string query;

    string aktUsername;
    string aktPassword;

    [Header("OutPutObject")]
    public TextMeshProUGUI aktLoggedUser;
    public GameObject aktLoggedUserObject;

    public void login()
    {
        connection();
        try
        {

            query = $"SELECT `username`, `password` FROM users WHERE username = '{username.text}' AND password = '{password.text}';";

            MS_Command = new MySqlCommand(query, MS_Connection);

            MS_Reader = MS_Command.ExecuteReader();
            while (MS_Reader.Read())
            {
                //usernameCheck = MS_Reader[0].ToString();
                //usernameCheck = MS_Reader[0].ToString();
                
                //Debug.Log(usernameCheck);
                aktUsername = MS_Reader.GetString(0);
                aktPassword = MS_Reader.GetString(1);
                Debug.Log(MS_Reader[0].ToString() +" "+ MS_Reader[1].ToString());
            }

            if (username.text == aktUsername && password.text == aktPassword)
            {

                aktLoggedUser.text = aktUsername;
                mainMenu.SetActive(true);
                loginMenu.SetActive(false);

            }


            /*
            //Debug.Log(usernameCheck);
            if (Convert.ToInt32(true) == 0)
            {
                //registration();
            }
            else
            {
                Debug.Log("Hiba a bejelentkezés közben");
            }
            */
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

    public void logout()
    {
        mainMenu.SetActive(false);
        loginMenu.SetActive(true);
    }

    public void connection()
    {
        connectionString = "Server = localhost; Database = challengerofdemoniccity; User = root; Password = ;";

        MS_Connection = new MySqlConnection(connectionString);

        MS_Connection.Open();
    }
}
