using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;

namespace GoodFood
{
    public partial class Dish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }
        private void BindGrid() {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT dish_code, dish_name, local_name,loyalty_id FROM DISHES";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable("dishes");

            using (OracleDataReader sdr = cmd.ExecuteReader()) {
                dt.Load(sdr);
            }

            con.Close();

            GridViewDish.DataSource = dt;
            GridViewDish.DataBind();
        }

        protected void insertBtn_Click(object sender, EventArgs e)
        {
            string dish_code = dishcodeTxt.Text.ToString();
            string dish_name = dishnameTxt.Text.ToString();
            string local_name = localnameTxt.Text.ToString();
            string loyalty_score = ddlloyaltyscore.SelectedValue.ToString();

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr)) {
                using (OracleCommand cmd = new OracleCommand("Insert into dishes(dish_code,dish_name, local_name,loyalty_id)VALUES('" + dish_code + "','" + dish_name + "','" + local_name + "','" + loyalty_score + "')")) {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    dishcodeTxt.Text = "";
                    dishnameTxt.Text = "";
                    localnameTxt.Text = "";
                }
            }
            this.BindGrid();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewDish.Rows[e.RowIndex];
            string dish_code = Convert.ToString(GridViewDish.DataKeys[e.RowIndex].Values[0]);
            string dish_name = (row.Cells[2].Controls[0] as TextBox).Text;
            string local_name = (row.Cells[3].Controls[0] as TextBox).Text;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("update dishes set dish_name = '" + dish_name + "',local_name = '" + local_name + "' where dish_code = '" + dish_code + "'"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            GridViewDish.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string dish_code = Convert.ToString(GridViewDish.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("UPDATE Order_Dishes SET dish_code=NULL WHERE order_number IN (SELECT order_number FROM Order_Dishes WHERE dish_code='"+dish_code+"')"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                using (OracleCommand cmd = new OracleCommand("UPDATE Dish_Order_Restaurant SET dish_code=NULL WHERE order_number IN (SELECT order_number from Dish_Order_Restaurant WHERE dish_code='" + dish_code + "')"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                using (OracleCommand cmd = new OracleCommand("UPDATE Dish_Restaurant SET dish_code=NULL WHERE restaurants_id IN (SELECT restaurants_id FROM Dish_Restaurant WHERE dish_code='" + dish_code + "')"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                using (OracleCommand cmd = new OracleCommand("DELETE FROM Dishes WHERE dish_code ='" + dish_code + "'"))
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
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridViewDish.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewDish.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridViewDish.EditIndex = -1;
            this.BindGrid();
        }
    }
}