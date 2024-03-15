using System;
using System.Web;

namespace XSSProtectedApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userInput = Request.QueryString["input"];
                if (!string.IsNullOrEmpty(userInput))
                {
                    // Защита от XSS с помощью HttpUtility.HtmlEncode
                    userInput = HttpUtility.HtmlEncode(userInput);
                    outputLabel.Text = userInput;
                }
            }
        }
    }
}