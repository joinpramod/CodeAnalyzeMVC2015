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

    //public class ExampleClass
    //{
    //    [AllowHtml]
    //    public string HtmlContent { get; set; }
    //}


}