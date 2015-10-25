using System;



    public partial class ChatRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (lstRooms.SelectedValue != null)
                Session["RoomId"] = lstRooms.SelectedValue;
            else
                Session["RoomId"] = 1;

            if (lstRooms.SelectedItem != null)
                Session["Room"] = lstRooms.SelectedItem.Text;
            else
                Session["Room"] = "General";
        }

        protected void btnJoinChat_Click(object sender, EventArgs e)
        {
            Response.Redirect("Chat.aspx");

        }
    }
