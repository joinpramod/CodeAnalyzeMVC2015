using System;


    public partial class CaptchaCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string text = (Guid.NewGuid().ToString()).Substring(0, 5);
            Response.Cookies["Captcha"]["value"] = text;
            imgcaptcha.ImageUrl = "captcha.aspx";
            errorcaptcha.Visible = false;
        }
        protected void btncaptcha_Click(object sender, EventArgs e)
        {
            if (txtcaptcha.Text.ToString() == Request.Cookies["Captcha"]["value"])
                errorcaptcha.Text = "The text is correct!";
            else
                errorcaptcha.Text = "The text is not correct!";
            errorcaptcha.Visible = true;
        }
        protected void LBcaptcha_Click(object sender, EventArgs e)
        {
            Response.Cookies["Captcha"]["value"] = (Guid.NewGuid().ToString()).Substring(0, 5);
            imgcaptcha.ImageUrl = "captcha.aspx";
        }
    }
