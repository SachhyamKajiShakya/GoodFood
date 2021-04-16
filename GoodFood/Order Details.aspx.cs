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
    public partial class Order_Details : System.Web.UI.Page
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
            cmd.CommandText = @"SELECT c.customer_name,c.customer_address,c.customer_phone,
                              o.order_number,TO_CHAR(o.date_time,'yyyy/mm/dd') AS ORDER_DATE,o.order_amount,o.status,
                              a.delivery_point,
                              od.line_total,od.order_unit,
                              d.dish_name
                              FROM Customers c JOIN Orders o
                              ON c.customer_id=o.customer_id
                              JOIN delivery_address a
                              ON o.delivery_address_id = a.delivery_address_id
                              JOIN order_dishes od
                              ON od.order_number = o.order_number
                              JOIN dishes d
                              ON d.dish_code=od.dish_code";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewOrderDetails.DataSource = dt;
            GridViewOrderDetails.DataBind();
        }

        protected void searchbtn_Click(object sender, EventArgs e)
        {
            string customer = ddlcustomer.SelectedValue.ToString();
            string address = ddladdress.SelectedValue.ToString();
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT c.customer_name,
                            o.order_number,o.date_time,o.order_amount,o.status,
                            a.delivery_point,
                            od.line_total,od.order_unit,
                            d.dish_name
                            FROM Customers c JOIN Orders o
                            ON c.customer_id=o.customer_id
                            JOIN delivery_address a
                            ON o.delivery_address_id = a.delivery_address_id
                            JOIN order_dishes od
                            ON od.order_number = o.order_number
                            JOIN dishes d
                            ON d.dish_code=od.dish_code
                            WHERE o.customer_id='"+customer+"' AND o.delivery_address_id='"+address+"'";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewOrderDetails.DataSource = dt;
            GridViewOrderDetails.DataBind();
        }
    }
}