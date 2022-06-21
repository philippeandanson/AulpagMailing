using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulpagMailing.Models
{
    [Table("theme")]
    class Themes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idtheme { get; set; }
        public string lbltheme { get; set; }
    }
}
