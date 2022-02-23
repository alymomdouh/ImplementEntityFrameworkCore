using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFECORE.Models
{
    [Table("Posts")]
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public List<PostTag> postTags { get; set; }
    }
    public class Tag
    {
        public string TagId { get; set; }
        public ICollection<Post> Posts { get; set; }
        public List<PostTag> postTags { get; set; }
    }
    public class PostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
        public Post Post { get; set; }
        public Tag Tag { get; set; } 
        public DateTime AddedOn { get; set; }
    }
}
