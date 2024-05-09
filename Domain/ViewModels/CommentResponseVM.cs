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

        public CommentResponseVM(bool success, string message, Comment comment = null)
        {
            Success = success;
            Message = message;
            Comment = comment;
        }


    }
}
