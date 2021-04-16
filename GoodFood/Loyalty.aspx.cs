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
    public partial class Loyalty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                startDateCalendar.Visible = false;
                endDateCalendar.Visible = false;
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
            cmd.CommandText = "SELECT loyalty_id, loyalty_points, TO_CHAR(start_date,'yyyy/mm/dd') AS Start_Date, TO_CHAR(end_date,'yyyy/mm/dd') AS End_Date FROM loyalty_points";
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable("loyalty_points");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            GridViewLoyalty.DataSource = dt;
            GridViewLoyalty.DataBind();
        }

        protected void insertBtn_Click(object sender, EventArgs e)
        {
            int loyalty_points = Convert.ToInt32(pointTxt.Text);
            string start_date = startdateTxt.Text.ToString();
            string end_date = enddateTxt.Text.ToString();
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("Insert into loyalty_points(loyalty_points,start_date, end_date)VALUES("+loyalty_points+", TO_DATE ('" + start_date + "','yyyy/mm/dd'), TO_DATE('" + end_date + "','yyyy/mm/dd'))"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    pointTxt.Text = "";
                    startdateTxt.Text = "";
                    enddateTxt.Text = "";
                }
            }
            this.BindGrid();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewLoyalty.Rows[e.RowIndex];
            int ID = Convert.ToInt32(GridViewLoyalty.DataKeys[e.RowIndex].Values[0]);
            string loyalty_points = (row.Cells[2].Controls[0] as TextBox).Text;
            string start_date = (row.Cells[3].Controls[0] as TextBox).Text;
            string end_date = (row.Cells[4].Controls[0] as TextBox).Text;
            
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string formatted_start_date = String.Format("TO_DATE('{0}','yyyy/mm/dd')", start_date);
            string formatted_end_date = String.Format("TO_DATE('{0}','yyyy/mm/dd')", end_date);
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("UPDATE loyalty_points SET loyalty_points = '" + loyalty_points + "', start_date = " + formatted_start_date + ", end_date=" + formatted_end_date +" WHERE loyalty_id = " + ID ))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            GridViewLoyalty.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(GridViewLoyalty.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("UPDATE Dishes SET loyalty_id=NULL WHERE dish_code IN (SELECT dish_code FROM Dishes WHERE loyalty_id=" + ID+")"))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                using (OracleCommand cmd = new OracleCommand("DELETE FROM loyalty_points WHERE loyalty_id =" + ID ))
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
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridViewLoyalty.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewLoyalty.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridViewLoyalty.EditIndex = -1;
            this.BindGrid();
        }

        protected void startDateBtn_Click(object sender, ImageClickEventArgs e)
        {
            if (startDateCalendar.Visible)
            {
                startDateCalendar.Visible = false;
            }
            else {
                startDateCalendar.Visible = true;
            }
        }

        protected void endDateBtn_Click(object sender, ImageClickEventArgs e)
        {
            if (endDateCalendar.Visible)
            {
                endDateCalendar.Visible = false;
            }
            else
            {
                endDateCalendar.Visible = true;
            }
        }

        protected void startDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            startdateTxt.Text = startDateCalendar.SelectedDate.ToString("yyyy/MM/dd");
            startDateCalendar.Visible = false;
        }

        protected void endDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            enddateTxt.Text = endDateCalendar.SelectedDate.ToString("yyyy/MM/dd");
            endDateCalendar.Visible = false;
        }
    }
}