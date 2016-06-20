using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace CodeAnalyzeMVC2015
{
    public class CodeArticles : ConnManager
    {
        private SqlConnection CmdLCLDBConn;
        private SqlCommand CmdExecute;
        private int IntOptID;
        private double DblArticleId;
        private string StrArticleTitle;
        private string StrArticleDetails;
        private string StrWordFile;
        private string StrSourceFile;
        private double DblUserId;
        private double DblArticleType;
        private int IntThumbsUp;
        private int IntThumbsDown;
        private System.DateTime DtCreatedDateTime;
        private System.DateTime DtModifiedDateTime;
        private string StrYouTubeURL;

        public int Views { get; set; }

        public SqlConnection SetConnection
        {
            get
            {
                return CmdLCLDBConn;
            }
            set
            {
                CmdLCLDBConn = value;
            }
        }


        public int OptionID
        {
            get
            {
                return IntOptID;
            }
            set
            {
                IntOptID = value;
            }
        }
        public double ArticleId
        {
            get
            {
                return DblArticleId;
            }
            set
            {
                DblArticleId = value;
            }
        }


        public string ArticleTitle
        {
            get
            {
                return StrArticleTitle;
            }
            set
            {
                StrArticleTitle = value;
            }
        }


        public string ArticleDetails
        {
            get
            {
                return StrArticleDetails;
            }
            set
            {
                StrArticleDetails = value;
            }
        }

        public string WordFile
        {
            get
            {
                return StrWordFile;
            }
            set
            {
                StrWordFile = value;
            }
        }


        public string SourceFile
        {
            get
            {
                return StrSourceFile;
            }
            set
            {
                StrSourceFile = value;
            }
        }

        public string YouTubeURL
        {
            get
            {
                return StrYouTubeURL;
            }
            set
            {
                StrYouTubeURL = value;
            }
        }

        public Double UserId
        {
            get
            {
                return DblUserId;
            }
            set
            {
                DblUserId = value;
            }
        }


        public Double ArticleType
        {
            get
            {
                return DblArticleType;
            }
            set
            {
                DblArticleType = value;
            }
        }


        public int ThumbsUp
        {
            get
            {
                return IntThumbsUp;
            }
            set
            {
                IntThumbsUp = value;
            }
        }


        public int ThumbsDown
        {
            get
            {
                return IntThumbsDown;
            }
            set
            {
                IntThumbsDown = value;
            }
        }


        public System.DateTime CreatedDateTime
        {
            get
            {
                return DtCreatedDateTime;
            }
            set
            {
                DtCreatedDateTime = value;
            }
        }


        public System.DateTime ModifiedDateTime
        {
            get
            {
                return DtModifiedDateTime;
            }
            set
            {
                DtModifiedDateTime = value;
            }
        }


        public int IsDisplay { get; set; }



        public bool SetCommandArtcles(ref SqlCommand CmdSent)
        {
            SqlCommand Cmd = new SqlCommand("Articles_Sp", CmdLCLDBConn);
            Cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter ParamOptID = Cmd.Parameters.Add("@OptID", SqlDbType.Int);
            SqlParameter ParamArticlesId = Cmd.Parameters.Add("@ArticleId", SqlDbType.Float);
            SqlParameter ParamArticleTitle = Cmd.Parameters.Add("@ArticleTitle", SqlDbType.VarChar);
            SqlParameter ParamArticleDetails = Cmd.Parameters.Add("@ArticleDetails", SqlDbType.VarChar);
            SqlParameter ParamWordFile = Cmd.Parameters.Add("@WordFile", SqlDbType.VarChar);
            SqlParameter ParamSourceFile = Cmd.Parameters.Add("@SourceFile", SqlDbType.VarChar);
            SqlParameter ParamYouTubeURL = Cmd.Parameters.Add("@YouTubURL", SqlDbType.VarChar);
            SqlParameter ParamUserId = Cmd.Parameters.Add("@UserId", SqlDbType.Float);
            SqlParameter ParamArticleType = Cmd.Parameters.Add("@ArticleType", SqlDbType.Float);
            SqlParameter ParamThumbsUp = Cmd.Parameters.Add("@ThumbsUp", SqlDbType.Int);
            SqlParameter ParamThumbsDown = Cmd.Parameters.Add("@ThumbsDown", SqlDbType.Int);
            SqlParameter ParamCreatedDateTime = Cmd.Parameters.Add("@InsertedDate", SqlDbType.DateTime);
            SqlParameter ParamModifiedDateTime = Cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime);
            SqlParameter ParamViews = Cmd.Parameters.Add("@Views", SqlDbType.Int);
            SqlParameter ParamIsDisplay = Cmd.Parameters.Add("@IsDisplay", SqlDbType.Int);

            ParamOptID.Value = IntOptID;
            ParamOptID.Direction = ParameterDirection.Input;
            ParamArticlesId.Value = DblArticleId;
            ParamArticlesId.Direction = ParameterDirection.Input;
            ParamArticleTitle.Value = StrArticleTitle;
            ParamArticleTitle.Direction = ParameterDirection.Input;
            ParamArticleDetails.Value = StrArticleDetails;
            ParamArticleDetails.Direction = ParameterDirection.Input;
            ParamWordFile.Value = StrWordFile;
            ParamWordFile.Direction = ParameterDirection.Input;
            ParamSourceFile.Value = StrSourceFile;
            ParamSourceFile.Direction = ParameterDirection.Input;
            ParamYouTubeURL.Value = StrYouTubeURL;
            ParamYouTubeURL.Direction = ParameterDirection.Input;
            ParamUserId.Value = DblUserId;
            ParamUserId.Direction = ParameterDirection.Input;
            ParamArticleType.Value = DblArticleType;
            ParamArticleType.Direction = ParameterDirection.Input;
            ParamThumbsUp.Value = IntThumbsUp;
            ParamThumbsUp.Direction = ParameterDirection.Input;
            ParamThumbsDown.Value = IntThumbsDown;
            ParamThumbsDown.Direction = ParameterDirection.Input;
            ParamViews.Value = Views;
            ParamViews.Direction = ParameterDirection.Input;

            ParamIsDisplay.Value = IsDisplay;
            ParamIsDisplay.Direction = ParameterDirection.Input;


            if (DtCreatedDateTime < DateTime.Parse("1-1-2000"))
            {
                ParamCreatedDateTime.Value = DBNull.Value;
            }
            else
            {
                ParamCreatedDateTime.Value = DtCreatedDateTime;
            }
            ParamCreatedDateTime.Direction = ParameterDirection.Input;


            if (DtModifiedDateTime < DateTime.Parse("1-1-2000"))
            {
                ParamModifiedDateTime.Value = DBNull.Value;
            }
            else
            {
                ParamModifiedDateTime.Value = DtModifiedDateTime;
            }
            ParamModifiedDateTime.Direction = ParameterDirection.Input;

            CmdSent = Cmd;
            return true;
        }


        public bool CreateArticles(ref double NewMasterID, SqlTransaction TrTransaction)
        {
            if (SetCommandArtcles(ref CmdExecute))
            {
                try
                {
                    if (TrTransaction != null)
                    {
                        CmdExecute.Transaction = TrTransaction;
                    }
                    SqlDataReader DATReader = CmdExecute.ExecuteReader();
                    while (DATReader.Read())
                    {
                        NewMasterID = double.Parse(DATReader[0].ToString());
                    }
                    DATReader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }




    }

}
