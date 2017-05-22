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



        protected void Page_Load(object sender, EventArgs e)
        {
            functions func = new functions();
            questionLabel.Text = func.setUpQuestion(questionId);
            string questionType = func.getQuestionType(questionId);
            

            
        }




        protected void nextQuestion(object sender, EventArgs e)
        {

        }
    }  
}