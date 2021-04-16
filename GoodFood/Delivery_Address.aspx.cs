using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace GoodFood
{
    public partial class Delivery_Address : System.Web.UI.Page
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
            cmd.CommandText = "SELECT delivery_address_id,delivery_point, longitude, latitude FROM DELIVERY_ADDRESS";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable("delivery_address");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewAddress.DataSource = dt;
            GridViewAddress.DataBind();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewAddress.Rows[e.RowIndex];
            int ID = Convert.ToInt32(GridViewAddress.DataKeys[e.RowIndex].Values[0]);
            string delivery_point = (row.Cells[2].Controls[0] as TextBox).Text;
            string longitude = (row.Cells[3].Controls[0] as TextBox).Text;
            string latitude = (row.Cells[4].Controls[0] as TextBox).Text;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("update delivery_address set delivery_point='" + delivery_point + "', longitude = '" + longitude + "',latitude = '" + latitude + "' where delivery_address_id = " + ID))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            GridViewAddress.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(GridViewAddress.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand(" UPDATE Orders SET delivery_address_id=NULL WHERE order_number IN (SELECT order_number FROM Orders WHERE delivery_address_id="+ID+")"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                using (OracleCommand cmd = new OracleCommand("DELETE FROM Delivery_Address WHERE delivery_address_id =" + ID))
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
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridViewAddress.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewAddress.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridViewAddress.EditIndex = -1;
            this.BindGrid();
        }

        protected void insertBtn_Click(object sender, EventArgs e)
        {
            string delivery_point = pointTxt.Text.ToString();
            string longitude = longitudeTxt.Text.ToString();
            string latitude = latitudeTxt.Text.ToString();

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("Insert into delivery_address(delivery_point,longitude, latitude)VALUES('" + delivery_point + "','" + longitude + "','" + latitude + "')"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    pointTxt.Text = "";
                    longitudeTxt.Text = "";
                    latitudeTxt.Text = "";
                }
            }
            this.BindGrid();

        }
    }
}