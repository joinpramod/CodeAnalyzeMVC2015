using System;
using System.Data;
using System.Data.SqlClient;


namespace CodeAnalyzeMVC2015
{
    public class ArticleComments : ConnManager
    {
        private SqlConnection CmdLCLDBConn;
        private SqlCommand CmdExecute;
        private int IntOptID;
        private double DblReplyId;
        private double DblReplyUserId;
        private double DblArticleId;
        private string StrReplyText;
        private System.DateTime DtInsertedDate;


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

        public double ReplyId
        {
            get
            {
                return DblReplyId;
            }
            set
            {
                DblReplyId = value;
            }
        }

        public double ReplyUserId
        {
            get
            {
                return DblReplyUserId;
            }
            set
            {
                DblReplyUserId = value;
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

        public string ReplyText
        {
            get
            {
                return StrReplyText;
            }
            set
            {
                StrReplyText = value;
            }
        }

        public System.DateTime InsertedDate
        {
            get
            {
                return DtInsertedDate;
            }
            set
            {
                DtInsertedDate = value;
            }
        }


        public bool SetCommandReplies(ref SqlCommand CmdSent)
        {
            SqlCommand Cmd = new SqlCommand("ArticleReply_Sp", CmdLCLDBConn);
            Cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter ParamOptID = Cmd.Parameters.Add("@OptID", SqlDbType.Int);
            SqlParameter ParamReplyId = Cmd.Parameters.Add("@ReplyId", SqlDbType.Float);
            SqlParameter ParamReplyUserId = Cmd.Parameters.Add("@ReplyUserId", SqlDbType.Float);
            SqlParameter ParamArticleId = Cmd.Parameters.Add("@ArticleId", SqlDbType.Float);
            SqlParameter ParamReplyText = Cmd.Parameters.Add("@ReplyText", SqlDbType.VarChar);
            SqlParameter ParamInsertedDate = Cmd.Parameters.Add("@InsertedDate", SqlDbType.DateTime);

            ParamOptID.Value = IntOptID;
            ParamOptID.Direction = ParameterDirection.Input;
            ParamReplyId.Value = DblReplyId;
            ParamReplyId.Direction = ParameterDirection.Input;
            ParamReplyUserId.Value = DblReplyUserId;
            ParamReplyUserId.Direction = ParameterDirection.Input;
            ParamArticleId.Value = DblArticleId;
            ParamArticleId.Direction = ParameterDirection.Input;
            ParamReplyText.Value = StrReplyText;
            ParamReplyText.Direction = ParameterDirection.Input;

            if (DtInsertedDate < DateTime.Parse("1-1-2000"))
            {
                ParamInsertedDate.Value = DBNull.Value;
            }
            else
            {
                ParamInsertedDate.Value = DtInsertedDate;
            }
            ParamInsertedDate.Direction = ParameterDirection.Input;

            CmdSent = Cmd;
            return true;
        }


        public bool CreateArticleComments(ref double NewMasterID, SqlTransaction TrTransaction)
        {
            if (SetCommandReplies(ref CmdExecute))
            {
                try
                {
                    if (TrTransaction != null)
                    {
                        CmdExecute.Transaction = TrTransaction;
                    }
                    SqlDataReader DATReader = CmdExecute.ExecuteReader();
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
