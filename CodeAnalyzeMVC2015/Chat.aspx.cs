using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Services;
//using ASPNETChat;
using System.Collections.Generic;
using CodeAnalyzeMVC2015;

public partial class Chat : System.Web.UI.Page
    {
        Users user = new Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                user = (Users)Session["User"];
                Label2.Visible = false;
            }
            else
            {
                Label2.Visible = true;
            }
            if (Session["Room"] == null)
                Label3.Text = "General";
            else
                Label3.Text = Session["Room"].ToString();

            if (user != null)
            {
                if (user.Email != null || user.FirstName != null)
                {
                    if (user.Email != null)
                        Session["emailName"] = user.Email.ToString().Split('@')[0];
                    else if (user.FirstName != null)
                        Session["emailName"] = user.FirstName;

                    Session["UserId"] = user.UserId;

                    txtMsg.Attributes.Add("onkeypress", "return clickButton(event,'btn')");
                    if (!IsPostBack)
                    {
                        if (Session["RoomId"] == null)
                            hdnRoomID.Value = "1";
                        else
                            hdnRoomID.Value = Session["RoomId"].ToString();   //"1";  //Request.QueryString["rid"];


                      CodeAnalyzeMVC2015.ChatRoom room = ChatEngine.GetRoom(hdnRoomID.Value);
                        string prevMsgs = "";
                        if (user.Email != null)
                            prevMsgs = room.JoinRoom(user.UserId.ToString(), Session["emailName"].ToString());
                        else if (user.FirstName != null)
                            prevMsgs = room.JoinRoom(user.UserId.ToString(), Session["emailName"].ToString());

                        txt.Text = prevMsgs;
                        foreach (string s in room.GetRoomUsersNames())
                        {
                            lstMembers.Items.Add(new ListItem(s, s));
                        }
                    }
                    Label2.Visible = false;
                }
                else
                {
                    Label2.Visible = true;
                }
            }
            else
            {
                Label2.Visible = true;
            }
        }


        #region Script Callback functions

        /// <summary>
        /// This function is called from the client script 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="roomID"></param>
        /// <returns></returns>
        [WebMethod]
        static public string SendMessage(string msg, string roomID)
        {

            try
            {
                CodeAnalyzeMVC2015.ChatRoom room = ChatEngine.GetRoom(roomID);
                string res = "";
                if (room != null)
                {
                    if (HttpContext.Current.Session["UserId"] != null)
                        res = room.SendMessage(msg, HttpContext.Current.Session["UserId"].ToString());
                    else
                    {

                    }
                }
                return res;
            }
            catch (Exception ex)
            {

            }
            return "";
        }


        /// <summary>
        /// This function is called peridically called from the user to update the messages
        /// </summary>
        /// <param name="otherUserID"></param>
        /// <returns></returns>
        [WebMethod]
        static public string UpdateUser(string roomID)
        {
            try
            {
            CodeAnalyzeMVC2015.ChatRoom room = ChatEngine.GetRoom(roomID);
                if (room != null)
                {
                    string res = "";
                    if (room != null)
                    {
                        if (HttpContext.Current.Session["UserId"] != null)
                            res = room.UpdateUser(HttpContext.Current.Session["UserId"].ToString());
                        else
                        {

                        }
                    }
                    return res;
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }


        /// <summary>
        /// This function is called from the client when the user is about to leave the room
        /// </summary>
        /// <param name="otherUser"></param>
        /// <returns></returns>
        [WebMethod]
        static public string LeaveRoom(string roomID)
        {
            try
            {
                CodeAnalyzeMVC2015.ChatRoom room = ChatEngine.GetRoom(roomID);
                if (room != null)
                    room.LeaveRoom(HttpContext.Current.Session["UserId"].ToString());
            }
            catch (Exception ex)
            {

            }
            return "";
        }


        /// <summary>
        /// Returns a comma separated string containing the names of the users currently online
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        [WebMethod]
        static public string UpdateRoomMembers(string roomID)
        {
            try
            {
                CodeAnalyzeMVC2015.ChatRoom room = ChatEngine.GetRoom(roomID);
                if (room != null)
                {
                    IEnumerable<string> users = room.GetRoomUsersNames();
                    string res = "";

                    foreach (string s in users)
                    {
                        res += s + ",";
                    }
                    return res;
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }
        #endregion

    }
