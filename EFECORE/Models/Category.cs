using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFECORE.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // to make types not int a primary key
        public byte Id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
    }
}
