using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeAnalyzeMVC2015.Models
{
    public class ArticlesModel
    {
        public string ArticleID { get; set; }
        public string ArticleTitle { get; set; }
        public string InsertedDate { get; set; }
        public string ThumbsUp { get; set; }
        public string ThumbsDown { get; set; }
        public string Views { get; set; }

    }
}