using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Blog
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Content { get; set; }

        public string? Image { get; set; }
        public DateTime? Created_at { get; set; } = DateTime.Now;


        [ForeignKey(nameof(userFK))]
    
        public string? User { get; set; }

        public virtual AppUser? userFK { get; set; }

        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;
        public int Comment_Count { get; set; } = 0;
        public List<Comment>? Comments { get; set; }


        public int Popularity { get; set; } = 0;
    }
}
