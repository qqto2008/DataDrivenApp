using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace DataDrivenApp
{
    public class functions
    {
        public string setUpQuestion(int qid) 
        {

            SqlConnection conn = new SqlConnection();
            SqlCommand comm = new SqlCommand();
            string connectionString = ConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;
            conn.ConnectionString = connectionString;
            conn.Open();


            string questionLabelText="";

            comm = new SqlCommand("SELECT content FROM Question WHERE qid = "+qid,conn);
            SqlDataReader reader = comm.ExecuteReader();
            while(reader.Read())
            {
                questionLabelText = reader["content"].ToString();
            }
            

            conn.Close();
            return questionLabelText;
        }

        public string getQuestionType(int qid) 
        {
            string questionType = "";
            SqlConnection conn = new SqlConnection();
            SqlCommand comm = new SqlCommand();
            string connectionString = ConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;
            conn.ConnectionString = connectionString;
            conn.Open();


            

            comm = new SqlCommand("SELECT questionType FROM Question WHERE qid = " + qid, conn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                questionType = reader["questionType"].ToString();
            }


            conn.Close();

            return questionType;

        }

    }
}