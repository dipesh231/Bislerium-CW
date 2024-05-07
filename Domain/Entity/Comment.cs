using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

   
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public virtual AppUser? User { get; set; }

      
        public string? Content { get; set; }

       
        public DateTime Posted_At { get; set; } = DateTime.UtcNow;


        [ForeignKey(nameof(Blog))]
        public Guid Blog_Id { get; set; }


        public virtual Blog? Blog { get; set; }

        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;


    }
}
