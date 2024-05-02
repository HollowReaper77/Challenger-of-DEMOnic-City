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

    public void userCheck()
    {
        connection();
        //username.text = usernameCheck;
        try
        {
            // SELECT COUNT(`username`) FROM `users` WHERE 1;
            //SELECT COUNT(`username`) FROM `users` WHERE `username` = 'sdfg';
            
            query = $"SELECT COUNT(*) FROM `users` WHERE `username` = '{username.text}';";

            MS_Command = new MySqlCommand(query, MS_Connection);

            MS_Reader = MS_Command.ExecuteReader();
            while (MS_Reader.Read())
            {
                //usernameCheck = MS_Reader[0].ToString();
                usernameCheck = MS_Reader[0].ToString();

                Debug.Log(usernameCheck);
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






            /*
            if (usernameCheck.ToI = 1)
            {
                registration();

            }
            else
            {
                Debug.Log("Szerepel m�r benne");
            }
            */
            /*
            Debug.Log(query);
            int count = Convert.ToInt32(query);
            if (count >= 1)
            {
                errortext = "This username is already taken.";
                Debug.Log(errortext);
            }
            */

            /*
            if (username.text != null)
            {
                errorMessage.text = "This username is already taken.";
            }
            */
            //MS_Connection = new MySqlConnection(connectionString);
            //MS_Connection.Open();
            //

        }
        catch (System.Exception)
        {
            //errorMessage.text = errortext;
            //throw;
        }
        finally
        {
            //registration();
            MS_Connection.Close();
        }
    }

    // inserting the data
    public void registration()
    {
        /*
        firstname.text = firstnameValue;
        lastname.text =lastnameValue;
        username.text = usernameValue;
        password.text = passwordValue;
        confirmpassword.text = confirmpasswordValue;
        */
        connection();
        try
        {
            // ha megfelel teljesen
            if (password.text == confirmpassword.text && (password.text.Length >= 8) && (firstname.text != null && lastname.text != null && username.text != null && email.text != null && password.text != null && password.text != null))
            {
                query = $"INSERT INTO `users`(`firstname`, `lastname`, `username`, `email`, `password`) VALUES ('{firstname.text}','{lastname.text}','{username.text}','{email.text}', '{password.text}');";
                MS_Command = new MySqlCommand(query, MS_Connection);

                MS_Command.ExecuteNonQuery();
                Debug.Log("Added");
            }
            else if (firstname.text == null && lastname.text == null && username.text == null && email.text == null && password.text == null && password.text == null)
            {
                errortext = "Nem adt�l meg minden adatot";
            }
            else if (password != confirmpassword)
            {
                errortext = "A megadott jelszavak nem egyeznek meg";
            }else if(password.text.Length !>= 8)
            {
                errortext = "Nem megfelel� a jelsz� hossza!";
            }
            else
            {
                errortext = "Hib�s adatok";
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
            //valami�rt ezzel nem �ll�tja �resre a mez�ket
            /*
            firstname.text = " ";
            lastname.text = " ";
            username.text = " ";
            password.text = " ";
            confirmpassword.text = "  ";
            */
            MS_Connection.Close();

        }
        /*

        connection();
        query = $"INSERT INTO `users`(`firstname`, `lastname`, `username`, `email`, `password`) VALUES ('{firstname.text}','{lastname.text}','{username.text}','{email.text}', '{password.text}');";
        MS_Command = new MySqlCommand(query, MS_Connection);

        MS_Command.ExecuteNonQuery();

        MS_Connection.Close();
        */
    }




    public void connection()
    {
        connectionString = "Server = localhost; Database = challengerofdemoniccity; User = root; Password = ;";

        MS_Connection = new MySqlConnection(connectionString);

        MS_Connection.Open();
    }
}