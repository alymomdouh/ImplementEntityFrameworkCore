using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFECORE.Models
{
    [Table("Blogs", Schema = "hr")]
    public  class Blog
    {
        [Column("BlogId")] 
        [Key]
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [NotMapped]
        [Column(TypeName ="varchar(200)")]
        [MaxLength(100,ErrorMessage ="the length should be with 100 char ")]
        [Comment("the url of the Blog Table")]
        public DateTime ADDon { get; set; }
        [Column(TypeName ="decimal(5,2)")]
        public decimal Rating { get; set; }
        [NotMapped]
        public List<Post> Post { get; set; }
    }
}
