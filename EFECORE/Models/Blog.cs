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
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [NotMapped]
        public List<Post> Post { get; set; }
    }
}
