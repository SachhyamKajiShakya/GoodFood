using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;

namespace GoodFood
{
    public partial class Order_History : System.Web.UI.Page
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
            cmd.CommandText = @"SELECT r.restaurant_name,r.restaurant_address,r.restaurant_contact, a.count ""Number of Dishes""
                            FROM restaurants r
                            JOIN(
                                SELECT dor.restaurants_id, COUNT(*) count
                                FROM dish_order_restaurant dor
                                JOIN(
                                    SELECT o.order_number FROM orders o ) p ON dor.order_number = p.order_number GROUP BY dor.restaurants_id) a ON r.restaurants_id = a.restaurants_id ORDER BY a.count DESC";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewOrderHistory.DataSource = dt;
            GridViewOrderHistory.DataBind();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            string month = monthTxt.Text.ToString();
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT r.restaurant_name,r.restaurant_address,r.restaurant_contact, a.count ""Number of Dishes""
                            FROM restaurants r
                            JOIN(
                                SELECT dor.restaurants_id, COUNT(*) count
                                FROM dish_order_restaurant dor
                                JOIN(
                                    SELECT o.order_number FROM orders o WHERE to_char(o.date_time, 'yyyy/mm') = '" + month + "') p " +
                                    "ON dor.order_number = p.order_number GROUP BY dor.restaurants_id) a ON r.restaurants_id = a.restaurants_id WHERE rownum <= 5 ORDER BY a.count DESC";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewOrderHistory.DataSource = dt;
            GridViewOrderHistory.DataBind();
        }
    }
}