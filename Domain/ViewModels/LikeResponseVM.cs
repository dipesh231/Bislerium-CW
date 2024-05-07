using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.ViewModels
{
    public class LikeResponseVM
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public Like Like { get; set; }

        public LikeResponseVM(bool success, string message, Like like = null)
        {
            Success = success;
            Message = message;
            Like = like;
        }

    }
}
