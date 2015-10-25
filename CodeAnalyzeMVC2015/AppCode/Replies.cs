using System;
using System.Data;
using System.Data.SqlClient;


namespace CodeAnalyzeMVC2015
{
    public class Replies : ConnManager
    {
        private SqlConnection CmdLCLDBConn;
        private SqlCommand CmdExecute;
        private int IntOptID;
        private double DblReplyId;
        private string StrReply;
        private double DblQuestionId;
        private double DblRepliedUser;
        private System.DateTime DtRepliedDate;


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


        public string Reply
        {
            get
            {
                return StrReply;
            }
            set
            {
                StrReply = value;
            }
        }


        public double QuestionId
        {
            get
            {
                return DblQuestionId;
            }
            set
            {
                DblQuestionId = value;
            }
        }


        public double RepliedUser
        {
            get
            {
                return DblRepliedUser;
            }
            set
            {
                DblRepliedUser = value;
            }
        }


        public System.DateTime RepliedDate
        {
            get
            {
                return DtRepliedDate;
            }
            set
            {
                DtRepliedDate = value;
            }
        }


        public bool SetCommandReplies(ref SqlCommand CmdSent)
        {
            SqlCommand Cmd = new SqlCommand("Replies_Sp", CmdLCLDBConn);
            Cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter ParamOptID = Cmd.Parameters.Add("@OptID", SqlDbType.Int);
            SqlParameter ParamReplyId = Cmd.Parameters.Add("@ReplyId", SqlDbType.Float);
            SqlParameter ParamReply = Cmd.Parameters.Add("@Reply", SqlDbType.VarChar);
            SqlParameter ParamQuestionId = Cmd.Parameters.Add("@QuestionId", SqlDbType.Float);
            SqlParameter ParamRepliedUser = Cmd.Parameters.Add("@RepliedUser", SqlDbType.Float);
            SqlParameter ParamRepliedDate = Cmd.Parameters.Add("@RepliedDate", SqlDbType.DateTime);

            ParamOptID.Value = IntOptID;
            ParamOptID.Direction = ParameterDirection.Input;
            ParamReplyId.Value = DblReplyId;
            ParamReplyId.Direction = ParameterDirection.Input;
            ParamReply.Value = StrReply;
            ParamReply.Direction = ParameterDirection.Input;
            ParamQuestionId.Value = DblQuestionId;
            ParamQuestionId.Direction = ParameterDirection.Input;
            ParamRepliedUser.Value = DblRepliedUser;
            ParamRepliedUser.Direction = ParameterDirection.Input;
            ParamRepliedDate.Value = DtRepliedDate;
            ParamRepliedDate.Direction = ParameterDirection.Input;
            //if (DtCreatedDate < DateTime.Parse("1-1-2000"))
            //{
            //    ParamCreatedDate.Value = DBNull.Value;
            //}
            //else
            //{
            //    ParamCreatedDate.Value = DtCreatedDate;
            //}
            //ParamCreatedDate.Direction = ParameterDirection.Input;

            //if (DtModifiedDate < DateTime.Parse("1-1-2000"))
            //{
            //    ParamModifiedDate.Value = DBNull.Value;
            //}
            //else
            //{
            //    ParamModifiedDate.Value = DtModifiedDate;
            //}
            //ParamModifiedDate.Direction = ParameterDirection.Input;

            CmdSent = Cmd;
            return true;
        }


        public bool CreateReplies(ref double NewMasterID, SqlTransaction TrTransaction)
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
