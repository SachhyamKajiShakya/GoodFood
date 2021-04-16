using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace GoodFood
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)   
            {
                this.BindGrid();
            }
        }
        private void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT customer_id, customer_name, customer_address, customer_phone, total_loyalty_points FROM CUSTOMERS";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable("customers");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewCustomer.DataSource = dt;
            GridViewCustomer.DataBind();
        }
        protected void insertBtn_Click(object sender, EventArgs e)
        {
            string customer_name = nameTxt.Text.ToString();
            string customer_address = addressTxt.Text.ToString();
            string customer_phone = phoneTxt.Text.ToString();
            

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("Insert into customers(customer_name,customer_address,customer_phone)VALUES('" + customer_name + "','" + customer_address + "','" + customer_phone + "' )"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    nameTxt.Text = "";
                    addressTxt.Text = "";
                    phoneTxt.Text = "";
                    
                }
            }
            this.BindGrid();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewCustomer.Rows[e.RowIndex];
            int customer_id = Convert.ToInt32(GridViewCustomer.DataKeys[e.RowIndex].Values[0]);
            string customer_name = (row.Cells[2].Controls[0] as TextBox).Text;
            string customer_address = (row.Cells[3].Controls[0] as TextBox).Text;
            string customer_phone = (row.Cells[4].Controls[0] as TextBox).Text;
            int loyalty_points = Convert.ToInt32((row.Cells[5].Controls[0] as TextBox).Text);
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("update customers set customer_name = '" + customer_name + "',customer_address = '" + customer_address + "',customer_phone = '" + customer_phone + "', total_loyalty_points = '" + loyalty_points + "' where customer_id = " + customer_id))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            GridViewCustomer.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridViewCustomer.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int customer_id = Convert.ToInt32(GridViewCustomer.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {   
                using (OracleCommand cmd = new OracleCommand("UPDATE Orders SET customer_id=NULL WHERE order_number in (SELECT order_number FROM Orders WHERE customer_id="+customer_id+")"))
                {
                    System.Diagnostics.Debug.WriteLine(customer_id);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                using (OracleCommand cmd = new OracleCommand("DELETE FROM customers WHERE customer_id =" + customer_id))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            this.BindGrid();
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridViewCustomer.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewCustomer.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
    }
}