using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using TMPro;

public class Registration : MonoBehaviour
{
    [Header("InputObjects")]
    public TextMeshProUGUI firstname;
    public TextMeshProUGUI lastname;
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;
    public TextMeshProUGUI confirmpassword;

    [Header("InputValues")]
    public string firstnameValue;
    public string lastnameValue;
    public string usernameValue;
    public string passwordValue;
    public string confirmpasswordValue;

    [Header("UI")]
    public TextMeshProUGUI errorMessage;
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    string query;


    public void registration()
    {
        firstname.text = firstnameValue;
        lastname.text =lastnameValue;
        username.text = usernameValue;
        password.text = passwordValue;
        confirmpassword.text = confirmpasswordValue;
        connection();
        try
        {

            if (password == confirmpassword && (password.text.Length >= 8))
            {
                query = $"INSERT INTO `users`(`firstname`, `lastname`, `username`, `password`) VALUES ('{firstname.text}','{lastname.text}','{username.text}','{password}';)";
                MS_Command = new MySqlCommand(query, MS_Connection);

                MS_Command.ExecuteNonQuery();
            }

        }
        catch (System.Exception)
        {

            errorMessage.text = "Nem megfelelõ regisztációs adatok";
            //throw;
        }
        finally
        {
            Debug.Log("Added");
            firstname.text = " ";
            lastname.text = " ";
            username.text = " ";
            password.text = " ";
            confirmpassword.text = "  ";
            MS_Connection.Close();

        }
    }


    public void connection()
    {
        connectionString = "Server = localhost; Database = challengerofdemoniccity; User = root; port=3306; Password = ;";

        MS_Connection = new MySqlConnection(connectionString);

        MS_Connection.Open();
    }
}