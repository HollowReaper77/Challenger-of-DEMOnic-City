using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Unity.VisualScripting;
using System;

public class Registration : MonoBehaviour
{
    [Header("InputObjects")]
    public TextMeshProUGUI firstname;
    public TextMeshProUGUI lastname;
    public TextMeshProUGUI username;
    public TextMeshProUGUI email;
    public TextMeshProUGUI password;
    public TextMeshProUGUI confirmpassword;
    
    [Header("UI")]
    public TextMeshProUGUI errorMessage;

    // DB variables
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    private MySqlDataReader MS_Reader;
    string query;

    string errortext = "";


    string usernameCheck;


    // ellen�z�s,hogy a felhaszn�l�n�vm�r fogalt-e
    public void userCheck()
    {
        connection();

        try
        {
            
            query = $"SELECT COUNT(*) FROM `users` WHERE `username` = '{username.text}';";

            MS_Command = new MySqlCommand(query, MS_Connection);

            MS_Reader = MS_Command.ExecuteReader();
            while (MS_Reader.Read())
            {
                //usernameCheck = MS_Reader[0].ToString();
                usernameCheck = MS_Reader[0].ToString();

                //Debug.Log(usernameCheck);
            }
            Debug.Log(usernameCheck);
            if (Convert.ToInt32(usernameCheck) == 0)
            {
                registration();
            }
            else
            {
                Debug.Log("Szerepel m�r benne");
            }


        }
        catch (System.Exception)
        {
            errorMessage.text = errortext;
            throw;
        }
        finally
        {
            MS_Connection.Close();
        }
    }

    // inserting the data
    public void registration()
    {
        connection();
        try
        {
            // ha megfelel teljesen
            if (password.text == confirmpassword.text && (password.text.Length >= 8) && (firstname.text != null && lastname.text != null && username.text != null && email.text != null && password.text != null && password.text != null) && Convert.ToInt32(usernameCheck) == 0)
            {
                query = $"INSERT INTO `users`(`firstname`, `lastname`, `username`, `email`, `password`) VALUES ('{firstname.text}','{lastname.text}','{username.text}','{email.text}', '{password.text}');";
                MS_Command = new MySqlCommand(query, MS_Connection);

                MS_Command.ExecuteNonQuery();
                Debug.Log("Added");
            }
            else
            {
                if (firstname.text == null || lastname.text == null || username.text == null || email.text == null || password.text == null || password.text == null)
                {
                    errortext = "Nem adt�l meg minden adatot";
                    Debug.Log("Nem adt�l meg minden adatot");
                }
                else if (password != confirmpassword)
                {
                    errortext = "A megadott jelszavak nem egyeznek meg";
                    Debug.Log("A megadott jelszavak nem egyeznek meg");
                }
                else if (password.text.Length! >= 8)
                {
                    // password.text.Length !>= 8
                    errortext = "Nem megfelel� a jelsz� hossza!";
                    Debug.Log("Nem megfelel� a jelsz� hossza!");
                }
                else
                {
                    errortext = "Hib�s adatok";
                    Debug.Log("Hib�s adatok");
                }
            }
        }
        catch (System.Exception) // nem lgol hib�t itt
        {

            Debug.Log("hiba");
            //errorMessage.text = "Nem megfelel� regiszt�ci�s adatok";
            errorMessage.text = errortext;
            //throw;
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