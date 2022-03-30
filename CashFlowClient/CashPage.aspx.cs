using CashFlowClient.CashRecordServiceReference;
using System;
using System.Web.UI.WebControls;

namespace CashFlowClient
{
    public partial class CashPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyRecords();
        }

        protected void AddRecord_Click(object sender, EventArgs e)
        {
            string email = Session["UserEmail"].ToString();
            CashRecordServiceClient client = new CashRecordServiceClient();
            var result = client.AddRecord(desc.Value, Convert.ToInt32(amount.Value), email);

            Response.Redirect("CashPage.aspx");
        }

        protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            CashRecordServiceClient client = new CashRecordServiceClient();
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                client.DeleteRecord(id);
                Response.Redirect("CashPage.aspx");
            }
        }

        public void MyRecords()
        {
            string email = Session["UserEmail"].ToString();
            CashRecordServiceClient client = new CashRecordServiceClient();

            rpt.DataSource = client.GetAllRecords(email);
            rpt.DataBind();
        }


    }
}