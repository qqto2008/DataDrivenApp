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
    public partial class _default : System.Web.UI.Page
    {
        DataTable questionDataTable = new DataTable();
        DataTable optionDataTable = new DataTable();

        
        private int questionId 
        {
            get 
            {
                if (Session["questionId"] == null)
                    return 0;
                return (int)Session["questionId"];
            }
            set 
            {
                Session["questionId"] = value;
            }
        }

        

        protected void Page_Load(object sender, EventArgs e)
        {
            
            questionConnection();
            optionConnection();
            questionLabel.Text = questionDataTable.Rows[0].Field<string>(2);
            setupOptions(0);
            testGridView.DataSource = optionDataTable;
            testGridView.DataBind();
            
        }


        

        protected void nextQuestion(object sender, EventArgs e)
        {

            if (questionId < questionDataTable.Rows.Count-1)
            {
                questionId++;
            }
            else
            {
                questionId = 0;
            }



            questionLabel.Text = questionDataTable.Rows[questionId].Field<string>(2);
            Label1.Text = questionId.ToString();
            PlaceHolder1.Controls.Clear();
            setupOptions(questionId);
            
            
        }
        

        private void setupOptions(int questionId) 
        {
            string questionType = questionDataTable.Rows[questionId].Field<string>(3);
            Label1.Text = questionType;
            if (questionType == "0")
            {
                RadioButtonList options = new RadioButtonList();
                PlaceHolder1.Controls.Add(options);
                for (int i = 0; i < optionDataTable.Rows.Count; i++)
                {
                    int qid = Int32.Parse(optionDataTable.Rows[i]["qid"].ToString());
                    string option = optionDataTable.Rows[i]["option"].ToString();
                    if (qid == questionId+1)
                    {
                        options.Items.Add(new ListItem(option, optionDataTable.Rows[i]["oid"].ToString()));
                    }
                }

            }
            else if (questionType == "2")
            {
                CheckBoxList options = new CheckBoxList();
                PlaceHolder1.Controls.Add(options);
                for (int i = 0; i < optionDataTable.Rows.Count;i++ )
                {
                    int qid = Int32.Parse(optionDataTable.Rows[i]["qid"].ToString());
                    string option = optionDataTable.Rows[i]["option"].ToString();
                    if (qid == questionId + 1)
                    {
                        options.Items.Add(new ListItem(option,optionDataTable.Rows[i]["oid"].ToString()));
                    }
                }
            }
            else if (questionType == "1")
            {
                TextBox textBox = new TextBox();
                PlaceHolder1.Controls.Add(textBox);
            }
        }



        private DataTable questionConnection()
        {
            SqlConnection connection;
            SqlCommand command;

            //testConnectionString from webconfig
            string connectionString = ConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;

            connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            connection.Open();//open the sql connection using the connection string info

            //just setup a basic sql command (referencing the connection)
            command = new SqlCommand("SELECT * FROM Question", connection);

            SqlDataReader reader = command.ExecuteReader(); //execute above query

            questionDataTable.Columns.Add("id", System.Type.GetType("System.String"));
            questionDataTable.Columns.Add("qid", System.Type.GetType("System.String"));
            questionDataTable.Columns.Add("content", System.Type.GetType("System.String"));
            questionDataTable.Columns.Add("questionType", System.Type.GetType("System.String"));

            DataRow questionRow;
            while (reader.Read())
            {
                questionRow = questionDataTable.NewRow();
                questionRow["id"] = reader["id"].ToString();
                questionRow["qid"] = reader["qid"].ToString();
                questionRow["content"] = reader["content"].ToString();
                questionRow["questionType"] = reader["questionType"].ToString();

                questionDataTable.Rows.Add(questionRow);

            }
            connection.Close();
            return questionDataTable;


        }
        private DataTable optionConnection()
        {
            SqlConnection connection;
            SqlCommand command;

            //testConnectionString from webconfig
            string connectionString = ConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;

            connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            connection.Open();//open the sql connection using the connection string info

            //just setup a basic sql command (referencing the connection)
            command = new SqlCommand("SELECT * FROM [DB_9AB8B7_DDA5414].[dbo].[option]", connection);
            //command = new SqlCommand("SELECT * FROM [DB_9AB8B7_DDA5414].[dbo].[option] where qid ="+currentQuestion, connection);

            SqlDataReader reader = command.ExecuteReader(); //execute above query

            optionDataTable.Columns.Add("oid", System.Type.GetType("System.String"));
            optionDataTable.Columns.Add("qid", System.Type.GetType("System.String"));
            optionDataTable.Columns.Add("option", System.Type.GetType("System.String"));
            optionDataTable.Columns.Add("followUpQuestion", System.Type.GetType("System.String"));

            DataRow optionRow;
            while (reader.Read())
            {
                optionRow = optionDataTable.NewRow();
                optionRow["oid"] = reader["oid"].ToString();
                optionRow["qid"] = reader["qid"].ToString();
                optionRow["option"] = reader["option"].ToString();
                optionRow["followUpQuestion"] = reader["followUpQuestion"].ToString();

                optionDataTable.Rows.Add(optionRow);

            }
            connection.Close();

            return optionDataTable;
        }
       
    }
}