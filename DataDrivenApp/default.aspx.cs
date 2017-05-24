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
        private int questionId
        {
            get
            {
                if (Session["questionId"] == null)
                    return 1;
                return (int)Session["questionId"];
            }
            set
            {
                Session["questionId"] = value;
            }
        }
        private string questionType;
        RadioButtonList rbl = new RadioButtonList();
        TextBox tb = new TextBox();
        CheckBoxList cbl = new CheckBoxList();


        protected void Page_Load(object sender, EventArgs e)
        {

            // database connection setting 
            SqlConnection conn = new SqlConnection();
            //sql command for questions
            SqlCommand Command;
            //sql command for options
            

            string connectionString = ConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;
            conn.ConnectionString = connectionString;
            string getCommand = "SELECT content, questionType, qid FROM Question WHERE qid = " + questionId.ToString();
            string optionCommand = "SELECT option, followUpQuestion, oid FROM option WHERE qid = " + questionId.ToString();


            Command = new SqlCommand(getCommand, conn);

            //open connection
            conn.Open();

            SqlDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                string questionContent = reader["content"].ToString();
                questionType = reader["questionType"].ToString();
                string qid = reader["qid"].ToString();

                questionNumLabel.Text = qid;
                questionLabel.Text = questionContent;

            }
            //close connection


            SqlCommand oCommand = new SqlCommand("select * from [DB_9AB8B7_DDA5414].[dbo].[option] where qid = " + questionId, conn);
            SqlDataReader optionReader = oCommand.ExecuteReader();
            string[] ddlDatasource = new string[]{};
            while (optionReader.Read())
            {
                string oid = optionReader["oid"].ToString();
                string option = optionReader["userOption"].ToString();
                string followUpQuestion = optionReader["followUpQuestion"].ToString();
                
                if (questionType.Equals("0"))
                {
                    ListItem rblItem = new ListItem(option, oid);
                    rbl.Items.Add(rblItem);   
                }
                else if (questionType.Equals("2"))
                {
                    
                    ListItem cblItem = new ListItem(option, oid);
                    cbl.Items.Add(cblItem);   
                }
            }
            if (questionType.Equals("0"))
            {
                PlaceHolder1.Controls.Add(rbl);
            }
            else if(questionType.Equals("1")){
                PlaceHolder1.Controls.Add(tb);
            }
            else if(questionType.Equals("2"))
            {
                PlaceHolder1.Controls.Add(cbl);
            }
            conn.Close();


            
        }




        protected void nextQuestion(object sender, EventArgs e)
        {
            PlaceHolder1.Controls.Clear();
            
            if(questionType == "0")
            {
                string selectedItem = rbl.SelectedItem.ToString();
                rbl.Items.Clear();
                SqlConnection conn = new SqlConnection();
                //sql command for questions
                SqlCommand Command;
                //sql command for options


                string connectionString = ConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;
                conn.ConnectionString = connectionString;
                conn.Open();
                Command = new SqlCommand("select * from [DB_9AB8B7_DDA5414].[dbo].[option] where userOption='"+selectedItem+"'", conn);
                SqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    questionId = Int32.Parse(reader["followUpQuestion"].ToString());
                }
                string questionCommand = "SELECT content, questionType, qid FROM Question WHERE qid = " + questionId.ToString();
                SqlCommand getQuestionCommand = new SqlCommand(questionCommand, conn);
                SqlDataReader questionReader = getQuestionCommand.ExecuteReader();
                while(questionReader.Read())
                {
                    questionLabel.Text = questionReader["content"].ToString();
                    questionType = questionReader["questionType"].ToString();
                }
                SqlCommand optionCommand = new SqlCommand("select * from [DB_9AB8B7_DDA5414].[dbo].[option] where qid = "+questionId, conn);
                SqlDataReader optionReader = optionCommand.ExecuteReader();
                while(optionReader.Read())
                {
                    string oid = optionReader["oid"].ToString();
                    string option = optionReader["userOption"].ToString();
                    string followUpQuestion = optionReader["followUpQuestion"].ToString();
                    if (questionType.Equals("0"))
                    {
                        ListItem optionItem = new ListItem(option, oid);
                        rbl.Items.Add(optionItem);
                    }
                }

                PlaceHolder1.Controls.Add(rbl);
                conn.Close();
                
            }
            else if(questionType == "1")
            {
                questionId = questionId + 1;
            }
            else if (questionType == "2") 
            {

            }
            
        }
    }  
}