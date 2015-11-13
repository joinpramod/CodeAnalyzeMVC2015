using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeAnalyzeMVC2015.Models
{
    public class ArticleModel
    {
        public string ArticleID { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleDetails { get; set; }
        public string InsertedDate { get; set; }
        public string ThumbsUp { get; set; }
        public string ThumbsDown { get; set; }
        public string Views { get; set; }

        //public DbSet<ArticlesModel> Articles { get; set; }
    }


    public class QuestionModel
    {
        public string QuestionID { get; set; }
        public string QuestionType { get; set; }
        public string QuestionTitle { get; set; }
        public string Question { get; set; }
        public string AskedUser { get; set; }
        public string AskedDateTime { get; set; }

    }


    public class VwSolutionsModel
    {
        public string QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string InsertedDate { get; set; }
        public string ThumbsUp { get; set; }
        public string ThumbsDown { get; set; }
        public string Views { get; set; }
        public string AskedUser { get; set; }
        public string RepliedUser { get; set; }
        public string QuestionViews { get; set; }
        public string ImageURL { get; set; }
        public string QuestionDetails { get; set; }
        public string AnswerDetails { get; set; }
    }

    public class VwArticlesModel
    {
        public long ArticleID { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleReplies { get; set; }         
        public string InsertedDate { get; set; }
        public string ThumbsUp { get; set; }
        public string ThumbsDown { get; set; }
        public string Views { get; set; }
        public string AskedUser { get; set; }
        public string ArticleViews { get; set; }
        public string ImageURL { get; set; }
        public string ArticleDetails { get; set; }
        public bool HasVideo { get; set; }
        public string AskedUserDetails { get; set; }
        public string iframeVideoURL { get; set; }
    }

    public class QuestionType
    {
        public string TypeId { get; set; }
        public string Type { get; set; }
        public List<QuestionType> Types { get; set; }
    }

    //public class UsersModel
    //{
    //    public string UserId { get; set; }
    //    public string FirstName { get; set; }
    //    public string Password { get; set; }
    //    public string LastName { get; set; }
    //    public string EMail { get; set; }
    //    public string Address { get; set; }
    //    public string ProfilePhoto { get; set; }
    //    public string Details { get; set; }
    //    public string ArticlesPosted { get; set; }
    //    public string QuestionsPosted { get; set; }
    //    public string AnswersPosted { get; set; }
    //}

    public class PagingInfo
    {
        public string SortField { get; set; }
        public string SortDirection { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
    }
}