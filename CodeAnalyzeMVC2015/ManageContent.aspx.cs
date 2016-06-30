using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace CodeAnalyzeMVC2015
{
    public partial class ManageContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShowFiles_Click(object sender, EventArgs e)
        {
            //FTP Server URL.
            string ftp = txtServer.Text;

            //FTP Folder name. Leave blank if you want to list files from root folder.
            string ftpFolder = txtPath.Text;

            try
            {
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                //Enter FTP Server credentials.
                request.Credentials = new NetworkCredential(txtUsername.Text, txtPassword.Text);
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = false;

                //Fetch the Response and read it using StreamReader.
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                List<string> entries = new List<string>();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    //Read the Response as String and split using New Line character.
                    entries = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                response.Close();

                //Create a DataTable.
                DataTable dtFiles = new DataTable();
                dtFiles.Columns.AddRange(new DataColumn[3] { new DataColumn("Name", typeof(string)),
                                                    new DataColumn("Size", typeof(decimal)),
                                                    new DataColumn("Date", typeof(string))});

                //Loop and add details of each File to the DataTable.
                foreach (string entry in entries)
                {
                    string[] splits = entry.Split(new string[] { " ", }, StringSplitOptions.RemoveEmptyEntries);

                    //Determine whether entry is for File or Directory.
                    bool isFile = splits[0].Substring(0, 1) != "d";
                    bool isDirectory = splits[0].Substring(0, 1) == "d";

                    //If entry is for File, add details to DataTable.
                    if (isFile)
                    {
                        dtFiles.Rows.Add();
                        dtFiles.Rows[dtFiles.Rows.Count - 1]["Size"] = decimal.Parse(splits[4]) / 1024;
                        dtFiles.Rows[dtFiles.Rows.Count - 1]["Date"] = string.Join(" ", splits[5], splits[6], splits[7]);
                        string name = string.Empty;
                        for (int i = 8; i < splits.Length; i++)
                        {
                            name = string.Join(" ", name, splits[i]);
                        }
                        dtFiles.Rows[dtFiles.Rows.Count - 1]["Name"] = name.Trim();
                    }
                }

                //Bind the GridView.
                gvFiles.DataSource = dtFiles;
                gvFiles.DataBind();
            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string fileName = (sender as LinkButton).CommandArgument;

            //FTP Server URL.
            string ftp = txtServer.Text;

            //FTP Folder name. Leave blank if you want to Download file from root folder.
            string ftpFolder = txtPath.Text;

            try
            {
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                //Enter FTP Server credentials.
                request.Credentials = new NetworkCredential(txtUsername.Text, txtPassword.Text);
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = false;

                //Fetch the Response and read it into a MemoryStream object.
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                using (MemoryStream stream = new MemoryStream())
                {
                    //Download the File.
                    response.GetResponseStream().CopyTo(stream);
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            }
        }



        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //FTP Server URL.
            string ftp = txtServer.Text;

            //FTP Folder name. Leave blank if you want to upload to root folder.
            string ftpFolder = txtPath.Text;

            byte[] fileBytes = null;

            //Read the FileName and convert it to Byte array.
            string fileName = Path.GetFileName(FileUpload1.FileName);
            using (StreamReader fileStream = new StreamReader(FileUpload1.PostedFile.InputStream))
            {
                fileBytes = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
                fileStream.Close();
            }

            try
            {
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                //Enter FTP Server credentials.
                request.Credentials = new NetworkCredential(txtUsername.Text, txtPassword.Text);
                request.ContentLength = fileBytes.Length;
                request.UsePassive = true;
                request.UseBinary = true;
                request.ServicePoint.ConnectionLimit = fileBytes.Length;
                request.EnableSsl = false;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileBytes, 0, fileBytes.Length);
                    requestStream.Close();
                }

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                lblMessage.Text += fileName + " uploaded.<br />";
                response.Close();
            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            }
        }

  

    
    }
}