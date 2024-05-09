using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.ViewModels
{
    public class LikeCommentResponseVM
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Like_Comment Likecmt { get; set; }

        public LikeCommentResponseVM(bool success, string message, Like_Comment like = null)
        {
            Success = success;
            Message = message;
            Likecmt = like;
        }

    }
}
