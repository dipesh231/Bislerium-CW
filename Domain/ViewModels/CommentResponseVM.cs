using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.ViewModels
{
    public class CommentResponseVM
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Comment Comment { get; set; }
        public bool V1 { get; }
        public string V2 { get; }
        public Like_Comment Likecmt { get; }

        public CommentResponseVM(bool success, string message, Comment comment = null)
        {
            Success = success;
            Message = message;
            Comment = comment;
        }

        public CommentResponseVM(bool v1, string v2, Like_Comment likecmt)
        {
            V1 = v1;
            V2 = v2;
            Likecmt = likecmt;
        }
    }
}
