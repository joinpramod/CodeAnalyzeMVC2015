using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace CodeAnalyzeMVC2015
{
    public class Question : ConnManager
    {
        private SqlConnection CmdLCLDBConn;
        private SqlCommand CmdExecute;
        private int IntOptID;
        private double DblQuestionId;
        private double DblQuestionTypeId;
        private string StrQuestionTitle;
        private string StrQuestion;
        private double DblAskedUser;
        private System.DateTime DtAskedDateTime;


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
        public double QuestionTypeId
        {
            get
            {
                return DblQuestionTypeId;
            }
            set
            {
                DblQuestionTypeId = value;
            }
        }

        public string QuestionTitle
        {
            get
            {
                return StrQuestionTitle;
            }
            set
            {
                StrQuestionTitle = value;
            }
        }


        public string QuestionDetails
        {
            get
            {
                return StrQuestion;
            }
            set
            {
                StrQuestion = value;
            }
        }


        public double AskedUser
        {
            get
            {
                return DblAskedUser;
            }
            set
            {
                DblAskedUser = value;
            }
        }


        public System.DateTime AskedDateTime
        {
            get
            {
                return DtAskedDateTime;
            }
            set
            {
                DtAskedDateTime = value;
            }
        }


        public bool SetCommandQuestion(ref SqlCommand CmdSent)
        {
            SqlCommand Cmd = new SqlCommand("Question_Sp", CmdLCLDBConn);
            Cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter ParamOptID = Cmd.Parameters.Add("@OptID", SqlDbType.Int);
            SqlParameter ParamQuestionId = Cmd.Parameters.Add("@QuestionId", SqlDbType.Float);
            SqlParameter ParamQuestionTypeId = Cmd.Parameters.Add("@QuestionTypeId", SqlDbType.Float);
            SqlParameter ParamQuestionTitle = Cmd.Parameters.Add("@QuestionTitle", SqlDbType.VarChar);
            SqlParameter ParamQuestion = Cmd.Parameters.Add("@Question", SqlDbType.VarChar);
            SqlParameter ParamAskedUser = Cmd.Parameters.Add("@AskedUser", SqlDbType.Float);
            SqlParameter ParamAskedDateTime = Cmd.Parameters.Add("@AskedDateTime", SqlDbType.DateTime);

            ParamOptID.Value = IntOptID;
            ParamOptID.Direction = ParameterDirection.Input;
            ParamQuestionId.Value = DblQuestionId;
            ParamQuestionId.Direction = ParameterDirection.Input;
            ParamQuestionTypeId.Value = DblQuestionTypeId;
            ParamQuestionTypeId.Direction = ParameterDirection.Input;
            ParamQuestionTitle.Value = StrQuestionTitle;
            ParamQuestionTitle.Direction = ParameterDirection.Input;
            ParamQuestion.Value = StrQuestion;
            ParamQuestion.Direction = ParameterDirection.Input;
            ParamAskedUser.Value = DblAskedUser;
            ParamAskedUser.Direction = ParameterDirection.Input;
            if (DtAskedDateTime < DateTime.Parse("1-1-2000"))
                ParamAskedDateTime.Value = DBNull.Value;
            else
                ParamAskedDateTime.Value = DtAskedDateTime;
            ParamAskedDateTime.Direction = ParameterDirection.Input;
            CmdSent = Cmd;
            return true;
        }


        public bool CreateQuestion(ref double NewMasterID, SqlTransaction TrTransaction)
        {
            if (SetCommandQuestion(ref CmdExecute))
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
