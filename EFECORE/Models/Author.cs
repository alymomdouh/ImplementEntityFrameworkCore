using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFECORE.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

    }
}
