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

}