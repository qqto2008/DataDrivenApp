using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataDrivenApp
{
    public partial class QuestionControl : System.Web.UI.UserControl
    {
        public Label Question
        {
            get
            {
                return questionLabel;
            }
            set
            {
                questionLabel = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}