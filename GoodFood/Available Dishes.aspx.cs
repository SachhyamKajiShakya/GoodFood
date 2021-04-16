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
    public partial class Available_Dishes : System.Web.UI.Page
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
            cmd.CommandText = @"SELECT d.dish_name,
                            r.restaurant_name,r.restaurant_address,r.restaurant_contact,
                            dr.dish_rate,
                            l.loyalty_points 
                            FROM dishes d JOIN dish_restaurant dr 
                            ON d.dish_code=dr.dish_code
                            JOIN restaurants r  
                            ON dr.restaurants_id=r.restaurants_id
                            JOIN loyalty_points l
                            ON d.loyalty_id=l.loyalty_id";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewAvailableDishes.DataSource = dt;
            GridViewAvailableDishes.DataBind();
        }

        protected void searchbtn_Click(object sender, EventArgs e)
        {
            string dishcode = ddldishcode.SelectedValue.ToString();
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT d.dish_name,
                            r.restaurant_name,r.restaurant_address,r.restaurant_contact,
                            dr.dish_rate,
                            l.loyalty_points 
                            FROM dishes d JOIN dish_restaurant dr 
                            ON d.dish_code=dr.dish_code
                            JOIN restaurants r 
                            ON dr.restaurants_id=r.restaurants_id
                            JOIN loyalty_points l
                            ON d.loyalty_id=l.loyalty_id
                            WHERE d.dish_code='"+ dishcode +"'";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewAvailableDishes.DataSource = dt;
            GridViewAvailableDishes.DataBind();
        }
    }
}