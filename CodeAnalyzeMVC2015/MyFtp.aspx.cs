using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;

public partial class MyFtp : System.Web.UI.Page
{
    private const string FTPController = "FtpController";


    private void GetFtpFilesAndFolders(FtpController ftp)
    {
        string[] directories = ftp.GetFtpDirectories();
        List<string> floderList = new List<string>();
        List<Files> files = new List<Files>();
        if (directories != null)
        {
            floderList = ftp.GetFolders(directories);
            floderList.Insert(0, "Go up a level");
            gvFolder.DataSource = floderList;
            gvFolder.DataBind();

            files = ftp.GetFiles(directories);
            gvFile.DataSource = files;
            gvFile.DataBind();
        }
        else
        {
            floderList.Insert(0, "Go up a level");
            gvFolder.DataSource = floderList;
            gvFolder.DataBind();

            gvFile.DataSource = files;
            gvFile.DataBind();
        }
    }

    private void DeleteLocalFile(string filePath)
    {
        System.IO.File.Delete(filePath);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        string serverName = txtServerName.Text.Trim();
        string userName = txtUserName.Text.Trim();
        string password = txtPassWord.Text.Trim();
        FtpController ftp = new FtpController();
        ftp.ServerName = serverName;
        ftp.UserName = userName;
        ftp.Password = password;
        if (ftp.IsConnected())
        {
            string str = "connected";
            mvFtp.ActiveViewIndex = 1;
            ftp.CurrentFtpPath = "";
            string[] directories = ftp.GetFtpDirectories();

            List<string> floderList = ftp.GetFolders(directories); //directories.ToList();
            floderList.Insert(0, "Go up a level");
            gvFolder.DataSource = floderList;
            gvFolder.DataBind();

            List<Files> files = ftp.GetFiles(directories);
            gvFile.DataSource = files;
            gvFile.DataBind();

            lblDirectory.Text = "/";
            Session[FTPController] = ftp;
        }
    }

    protected void gvFolder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            string data = (string)e.Row.DataItem;
            LinkButton lnkFolderName = (LinkButton)e.Row.FindControl("lnkFolderName");
            Image imgFolder = (Image)e.Row.FindControl("imgFolder");
            ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

            lnkFolderName.Text = data;
            lnkFolderName.Font.Underline = true;
            lnkFolderName.CommandArgument = data;
            imgDelete.CommandArgument = data;

            if (data == "Go up a level")
            {
                imgFolder.ImageUrl = "~/images/Up.gif";
            }
            else
            {
                imgFolder.ImageUrl = "~/images/folder.gif";
            }
        }
    }

    protected void gvFolder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string data = e.CommandArgument.ToString();
            if (data == "Go up a level")
            {
                //Goto upper level
                //data = data.Substring(data.LastIndexOf('/'));
                FtpController ftp = (FtpController)Session[FTPController];
                if (ftp.CurrentFtpPath.Length > 1)
                {
                    int len = ftp.CurrentFtpPath.LastIndexOf('/');
                    string NewPath = ftp.CurrentFtpPath.Substring(0, len);
                    ftp.CurrentFtpPath = NewPath;
                    GetFtpFilesAndFolders(ftp);
                    lblDirectory.Text = string.IsNullOrEmpty(ftp.CurrentFtpPath) ? "/" : ftp.CurrentFtpPath;
                    Session[FTPController] = ftp;
                }
            }
            else
            {
                //Go into selected folder
                FtpController ftp = (FtpController)Session[FTPController];
                ftp.CurrentFtpPath = ftp.CurrentFtpPath + "/" + data;
                GetFtpFilesAndFolders(ftp);
                lblDirectory.Text = ftp.CurrentFtpPath;
                Session[FTPController] = ftp;

            }
        }
        else if (e.CommandName == "Delete")
        {
            string data = e.CommandArgument.ToString();
            if (!string.IsNullOrEmpty(data))
            {
                FtpController ftp = (FtpController)Session[FTPController];
                bool delete = ftp.DeleteDirectoryFromFTP(data);
                if (delete)
                {
                    GetFtpFilesAndFolders(ftp);
                }
            }
        }
    }

    protected void gvFolder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Keep this event
    }
    protected void gvFolder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Keep this event
    }

    protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string data = e.CommandArgument.ToString();
            if (!string.IsNullOrEmpty(data))
            {
                FtpController ftp = (FtpController)Session[FTPController];
                ftp.FileName = data;
                byte[] strFile = ftp.DownloadFileFromFTP();
                if (strFile != null)
                {
                    Response.AppendHeader("content-disposition", "attachment; filename=" + data);
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(strFile);
                    Response.End();
                }
            }
        }
        else if (e.CommandName == "Delete")
        {
            string data = e.CommandArgument.ToString();
            if (!string.IsNullOrEmpty(data))
            {
                FtpController ftp = (FtpController)Session[FTPController];
                bool delete = ftp.DeleteFileFromFTP(data);
                if (delete)
                {
                    GetFtpFilesAndFolders(ftp);
                }
            }
        }
    }
    protected void gvFile_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Keep this event
    }

    protected void gvFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Keep this event
    }

    protected void gvFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Files data = (Files)e.Row.DataItem;
            LinkButton lnkFileName = (LinkButton)e.Row.FindControl("lnkFileName");
            Image imgFile = (Image)e.Row.FindControl("imgFile");
            ImageButton imgFileDelete = (ImageButton)e.Row.FindControl("imgFileDelete");
            lnkFileName.Text = data.FileName;
            lnkFileName.Font.Underline = true;
            lnkFileName.CommandArgument = data.FileName;
            imgFileDelete.CommandArgument = data.FileName;
            imgFile.ImageUrl = "~/images/file.gif";
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fuFtp.HasFile)
        {
            FtpController ftp = (FtpController)Session[FTPController];
            string lPath = Server.MapPath("Uploads");
            string fileName = fuFtp.PostedFile.FileName;
            fuFtp.SaveAs(lPath + "\\" + fileName);
            fuFtp.Dispose();

            ftp.FileName = fileName;
            ftp.UploadFileByFTP(lPath + "\\" + fileName);
            string[] directories = ftp.GetFtpDirectories();
            List<Files> files = ftp.GetFiles(directories);
            gvFile.DataSource = files;
            gvFile.DataBind();
            try
            {
                DeleteLocalFile(lPath + "\\" + fileName);
            }
            catch
            {
            }
        }
    }

    protected void btnCreateDirectory_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDirectory.Text))
        {
            string NewDirectory = txtDirectory.Text.Trim();
            FtpController ftp = (FtpController)Session[FTPController];
            ftp.CreateFtpDirectories(ftp.CurrentFtpPath + "/" + NewDirectory);
            string[] directories = ftp.GetFtpDirectories();
            if (directories != null)
            {
                List<string> floderList = ftp.GetFolders(directories);
                floderList.Insert(0, "Go up a level");
                gvFolder.DataSource = floderList;
                gvFolder.DataBind();
            }
        }
    }
}


public class FtpController
{
    /// <summary>
    /// Name of FTP Server
    /// </summary>
    public string ServerName { get; set; }

    /// <summary>
    /// User id of FTP server
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Authorized FTP server password
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// File path with file name.
    /// </summary>
    public string LocatFilePath { get; set; }

    /// <summary>
    /// Current FTP directory.
    /// </summary>
    public string CurrentFtpPath { get; set; }

    /// <summary>
    /// Name of the files to upload
    /// </summary>
    public string FileName { get; set; }


    /// <summary>
    /// Uploads file to FTP Server
    /// </summary>
    /// <param name="FilePath">Local path with file name</param>
    /// <param name="FtpPath">FTP Path with file name</param>
    /// <returns>Boolean</returns>
    public bool UploadFileByFTP(string LocalFilePath)
    {
        bool success = true;
        try
        {
            //Create FTP request
            if (!ServerName.Contains("ftp://")) ServerName = "ftp://" + ServerName;
            string fileFtpPath = string.Empty;
            if (string.IsNullOrEmpty(CurrentFtpPath))
            {
                fileFtpPath = ServerName;
            }
            else
            {
                fileFtpPath = ServerName + "" + CurrentFtpPath + "/" + FileName;
            }
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(fileFtpPath);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(UserName, Password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //Load the file
            FileStream stream = File.OpenRead(LocalFilePath);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            stream.Dispose();

            //Upload file
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();
        }
        catch { success = false; }
        return success;
    }

    /// <summary>
    /// Download from FTP server
    /// </summary>
    /// <returns></returns>
    public byte[] DownloadFileFromFTP()
    {
        byte[] retBytes = null;
        try
        {
            //Create FTP request
            if (!ServerName.Contains("ftp://")) ServerName = "ftp://" + ServerName;
            string ftpFilePath = string.Empty;
            if (string.IsNullOrEmpty(CurrentFtpPath))
            {
                ftpFilePath = ServerName + "/" + FileName;
            }
            else
            {
                ftpFilePath = ServerName + CurrentFtpPath + "/" + FileName;
            }
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ftpFilePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(UserName, Password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //Stream responseStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(responseStream);
            //retString= (reader..ReadToEnd());

            //Streams
            FtpWebResponse response = request.GetResponse() as FtpWebResponse;
            Stream reader = response.GetResponseStream();

            //Download to memory
            //Note: adjust the streams here to download directly to the hard drive
            MemoryStream memStream = new MemoryStream();
            byte[] buffer = new byte[1024]; //downloads in chuncks

            while (true)
            {
                //Try to read the data
                int bytesRead = reader.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    break;
                }
                else
                {
                    //Write the downloaded data
                    memStream.Write(buffer, 0, bytesRead);

                }
            }

            //Convert the downloaded stream to a byte array
            retBytes = memStream.ToArray();

            //Clean up
            reader.Close();
            memStream.Close();
            response.Close();
        }

        catch { }
        return retBytes;
    }

    /// <summary>
    /// Delete directory from FTP server
    /// </summary>
    /// <param name="DirecotryName"></param>
    /// <returns></returns>
    public bool DeleteDirectoryFromFTP(string DirecotryName)
    {
        bool success = true;
        FtpWebRequest request = null;
        FtpWebResponse response = null;
        try
        {
            //Create FTP request
            if (!ServerName.Contains("ftp://")) ServerName = "ftp://" + ServerName;
            string fileFtpPath = ServerName + "" + CurrentFtpPath + "/" + DirecotryName;

            request = (FtpWebRequest)FtpWebRequest.Create(fileFtpPath);
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            request.Credentials = new NetworkCredential(UserName, Password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }
        catch { success = false; }
        return success;
    }

    /// <summary>
    /// Delete file from FTP Server
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public bool DeleteFileFromFTP(string fileName)
    {
        bool success = true;
        FtpWebRequest request = null;
        FtpWebResponse response = null;
        try
        {
            //Create FTP request
            if (!ServerName.Contains("ftp://")) ServerName = "ftp://" + ServerName;
            string fileFtpPath = ServerName + "" + CurrentFtpPath + "/" + fileName;

            request = (FtpWebRequest)FtpWebRequest.Create(fileFtpPath);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(UserName, Password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }
        catch { success = false; }
        return success;
    }

    /// <summary>
    /// Get Directories and Files under parent filePath
    /// </summary>
    /// <param name="filePath">Parent Directory</param>
    /// <returns>Array of directories</returns>
    public string[] GetFtpDirectories()
    {
        string[] downloadFiles;
        StringBuilder result = new StringBuilder();

        FtpWebRequest request = null;
        FtpWebResponse response = null;
        StreamReader reader = null;
        try
        {
            // Get the object used to communicate with the server.
            if (!ServerName.Contains("ftp://")) ServerName = "ftp://" + ServerName;
            string filePath = string.Empty;
            if (string.IsNullOrEmpty(CurrentFtpPath))
            {
                filePath = ServerName;
            }
            else
            {
                filePath = ServerName + "/" + CurrentFtpPath;
            }
            request = (FtpWebRequest)WebRequest.Create(filePath);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            request.Credentials = new NetworkCredential(UserName, Password);
            response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            reader = new StreamReader(responseStream);
            string line = reader.ReadLine();
            while (line != null)
            {
                result.Append(line);
                result.Append("\n");
                line = reader.ReadLine();
            }
            // to remove the trailing '\n'
            result.Remove(result.ToString().LastIndexOf('\n'), 1);
            reader.Close();
            response.Close();
            return result.ToString().Split('\n');
        }
        catch
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (response != null)
            {
                response.Close();
            }
            downloadFiles = null;
            return downloadFiles;
        }
    }

    /// <summary>
    /// Create a directory in FTP Server
    /// </summary>
    /// <param name="filePath">Full path of new directory</param>
    /// <returns>Boolean</returns>
    public bool CreateFtpDirectories(string filePath)
    {
        bool success = false;
        FtpWebRequest request = null;
        FtpWebResponse response = null;
        try
        {
            if (!ServerName.Contains("ftp://")) ServerName = "ftp://" + ServerName;
            string fileFtpPath = string.Empty;
            if (string.IsNullOrEmpty(CurrentFtpPath))
            {
                fileFtpPath = ServerName;
            }
            else
            {
                fileFtpPath = ServerName + filePath;
            }

            // Get the object used to communicate with the server.
            request = (FtpWebRequest)WebRequest.Create(fileFtpPath);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            request.Credentials = new NetworkCredential(UserName, Password);
            response = (FtpWebResponse)request.GetResponse();
            response.Close();
            success = true;
        }
        catch
        {
            if (response != null)
            {
                response.Close();
            }
        }
        return success;
    }

    /// <summary>
    /// Finds a string in an array of strings
    /// </summary>
    /// <param name="strArray">Array of string</param>
    /// <param name="strToFind">String to search</param>
    /// <returns>Boolean</returns>
    public bool IsExistsIn(string[] strArray, string strToFind)
    {
        bool exist = false;
        int strIndex = Array.IndexOf(strArray, strToFind);
        if (strIndex >= 0)
        {
            exist = true;
        }
        return exist;
    }

    /// <summary>
    /// Checks whether connected to FTP server
    /// </summary>
    /// <returns>Boolean</returns>
    public bool IsConnected()
    {
        bool success = false;
        FtpWebRequest request = null;
        FtpWebResponse response = null;
        StreamReader reader = null;
        try
        {
            // Get the object used to communicate with the server.
            if (!ServerName.Contains("ftp://")) ServerName = "ftp://" + ServerName;
            request = (FtpWebRequest)WebRequest.Create(ServerName);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            request.Credentials = new NetworkCredential(UserName, Password);
            response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            reader = new StreamReader(responseStream);
            success = true;
            reader.Close();
            response.Close();
        }
        catch
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (response != null)
            {
                response.Close();
            }
            success = false;
        }
        return success;
    }

    public List<Files> GetFiles(string[] directoryDetails)
    {
        string[] files = directoryDetails.Where(s => s.StartsWith("-")).ToArray();
        return (from file in files let fileName = file.Substring(52) let fileSize = Convert.ToInt64(file.Substring(23, 15)) let midifyDate = file.Substring(39, 13) select new Files { FileName = fileName, LastModifiedDate = midifyDate, ImageUrl = string.Empty, ModifiedDate = DateTime.Now, Size = fileSize }).ToList();
    }

    public List<string> GetFolders(string[] directoryDetails)
    {
        return directoryDetails.Where(s => s.StartsWith("d")).ToArray().Select(f => f.Substring(52)).ToList();
    }
}

public class Files
{
    public string FileName { get; set; }
    public Int64 Size { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string LastModifiedDate { get; set; }
    public string ImageUrl { get; set; }
}