using CashFlowClient.CashRecordServiceReference;
using System;

namespace CashFlowClient
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            CashRecordServiceClient client = new CashRecordServiceClient();
            var result = client.Login(email.Value, pwd.Value);

            if (result == "Success")
            {
                Session["UserEmail"] = email.Value;
                Response.Redirect("~/CashPage.aspx");
            }
            else
            {
                errorLbl.Text = result;
            }

        }

    }
}