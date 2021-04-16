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
    public partial class Restaurant : System.Web.UI.Page
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
            cmd.CommandText = "SELECT restaurants_id, restaurant_name, restaurant_address, restaurant_contact FROM RESTAURANTS";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable("restaurants");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewRestaurant.DataSource = dt;
            GridViewRestaurant.DataBind();
        }

        protected void insertBtn_Click(object sender, EventArgs e)
        {
            string restaurant_name = nameTxt.Text.ToString();
            string restaurant_address = addressTxt.Text.ToString();
            string restaurant_contact = contactTxt.Text.ToString();

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("Insert into restaurants(restaurant_name,restaurant_address, restaurant_contact)VALUES('" + restaurant_name + "','" + restaurant_address + "','" + restaurant_contact + "')"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    nameTxt.Text = "";
                    addressTxt.Text = "";
                    contactTxt.Text = "";
                }
            }
            this.BindGrid();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewRestaurant.Rows[e.RowIndex];
            int restaurants_id = Convert.ToInt32(GridViewRestaurant.DataKeys[e.RowIndex].Values[0]);
            string restaurant_name = (row.Cells[2].Controls[0] as TextBox).Text;
            string restaurant_address = (row.Cells[3].Controls[0] as TextBox).Text;
            string restaurant_contact = (row.Cells[4].Controls[0] as TextBox).Text;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("update restaurants set restaurant_name = '" + restaurant_name + "',restaurant_address = '" + restaurant_address + "',restaurant_contact = '" + restaurant_contact +"' where restaurants_id = " + restaurants_id))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            GridViewRestaurant.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridViewRestaurant.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int restaurants_id = Convert.ToInt32(GridViewRestaurant.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("UPDATE Dish_Order_Restaurant SET restaurants_id=NULL WHERE dish_code IN (SELECT dish_code FROM dish_order_restaurant WHERE restaurants_id = '" + restaurants_id + "')"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                using (OracleCommand cmd = new OracleCommand("UPDATE Dish_Restaurant SET restaurants_id=NULL WHERE dish_code IN (SELECT dish_code FROM dish_restaurant WHERE restaurants_id ='" + restaurants_id + "')"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                using (OracleCommand cmd = new OracleCommand("DELETE FROM restaurants WHERE restaurants_id =" + restaurants_id))
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
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridViewRestaurant.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRestaurant.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
    }
}