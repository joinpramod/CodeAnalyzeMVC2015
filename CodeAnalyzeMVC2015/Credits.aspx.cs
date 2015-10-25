using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;
using CodeAnalyzeMVC2015;

public partial class Credits : System.Web.UI.Page
    {

       Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {

            user = (Users)Session["User"];
            if (user != null && user.Email != null && user.Email == "admin@codeanalyze.com")
            {
                BindHonours("Select * from Users order by userid desc");
            }
            LinkButton btnCredits = (LinkButton)this.Master.FindControl("btnCredits");
            //  Panel pnlWebDetails = (Panel)this.Master.FindControl("pnlWebDetails");
            //  pnlWebDetails.Visible = false;


            //btnCredits.Font.Size = 24;
            btnCredits.ForeColor = Color.Yellow;
        }


        private void BindHonours(string sqlQuery)
        {

            DataTable DTHonoursTbl = new DataTable();
            DataRow DRHonoursData = null;
            DataSet DSHonoursDetails = new DataSet();
            int i = 0;



            ConnManager connManager = new ConnManager();
            connManager.OpenConnection();
            DSHonoursDetails = connManager.GetData(sqlQuery);
            connManager.DisposeConn();
            if (DSHonoursDetails.Tables[0].Rows.Count > 0)
            {
                DTHonoursTbl.Columns.Add(new DataColumn("Status", typeof(string)));
                DTHonoursTbl.Columns.Add(new DataColumn("FullName", typeof(string)));
                DTHonoursTbl.Columns.Add(new DataColumn("Company", typeof(string)));
                DTHonoursTbl.Columns.Add(new DataColumn("ImageURL", typeof(string)));

                // DTHonoursTbl = LoadData(DTHonoursTbl);

                int tempFor1 = DSHonoursDetails.Tables[0].Rows.Count;
                for (i = 0; i < tempFor1; i++)
                {
                    if (!DSHonoursDetails.Tables[0].Rows[i]["EMail"].ToString().Contains("codeanalyze.com"))
                    {
                        DRHonoursData = DTHonoursTbl.NewRow();
                        //DRHonoursData[0] = (DSHonoursDetails.Tables[0].Rows[i]["FirstName"].ToString()) == null ? "" : System.Convert.ToString(DSHonoursDetails.Tables[0].Rows[i]["QuestionID"]);

                        DRHonoursData[1] = DSHonoursDetails.Tables[0].Rows[i]["FirstName"].ToString() + " " + DSHonoursDetails.Tables[0].Rows[i]["LastName"].ToString();
                        if (string.IsNullOrEmpty(DSHonoursDetails.Tables[0].Rows[i]["FirstName"].ToString()))
                            DRHonoursData[1] = DSHonoursDetails.Tables[0].Rows[i]["EMail"].ToString().Split('@')[0];

                        DRHonoursData[0] = (DSHonoursDetails.Tables[0].Rows[i]["Status"].ToString()) == null ? "" : System.Convert.ToString(DSHonoursDetails.Tables[0].Rows[i]["Status"]);
                        DRHonoursData[2] = (DSHonoursDetails.Tables[0].Rows[i]["Company"].ToString()) == null ? "" : Convert.ToString(DSHonoursDetails.Tables[0].Rows[i]["Company"]);
                        DRHonoursData[3] = (string.IsNullOrEmpty(DSHonoursDetails.Tables[0].Rows[i]["ImageURL"].ToString())) ? "~/Images/Person.JPG" : System.Convert.ToString(DSHonoursDetails.Tables[0].Rows[i]["ImageURL"]);
                        DTHonoursTbl.Rows.Add(DRHonoursData);
                    }
                }

                GVHonours.DataSource = DTHonoursTbl;




                GVHonours.DataBind();
                GVHonours.Visible = true;
            }
            else
            {
                GVHonours.Visible = false;
            }

        }

        protected void GVHonours_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVHonours.PageIndex = e.NewPageIndex;
            if (user != null && user.Email != null && user.Email == "admin@codeanalyze.com")
            {
                BindHonours("Select * from Users");
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //  BindHonours("Select * from Users where firstname like '%" + txtFullName.Text + "%' or LastName like '%" + txtFullName.Text + "%' or EMail like '%" + txtFullName.Text + "%' ");
        }


        private DataTable LoadData(DataTable DTHonoursTbl)
        {


            DataRow row = DTHonoursTbl.NewRow();
            row = DTHonoursTbl.NewRow(); row[1] = "John Smith"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "William Miller"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "David Brown"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Joseph Harris"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Richard Jones"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Carles Young"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Daniel Anderson"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Paul Clark"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Steven Martin"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Mark Anderson"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Jason Allen"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Dennis Lewis"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            row = DTHonoursTbl.NewRow(); row[1] = "Andrew Wilson"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Gerald Wright"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Brandon Carter"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Wayne Adams"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Fred Scott"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Eugene Turner"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Jesse Murphy"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Dale Parker"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Kyle Rogers"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Marcus Cooper"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Jerome Hayes"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Dustin Simmons"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Dean Flores"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);
            //row = DTHonoursTbl.NewRow(); row[1] = "Raul Ford"; row[3] = "~/Images/Person.JPG"; DTHonoursTbl.Rows.Add(row);


            return DTHonoursTbl;
        }

    }


//SELECT     UserId
//FROM         Users


//for each userid in users


//   SELECT     COUNT(*) AS Expr1
//   FROM         VwSolutions
//   WHERE     (RepliedUser = 1) AND (AskedUser <> 1)



//userid    no of replies
