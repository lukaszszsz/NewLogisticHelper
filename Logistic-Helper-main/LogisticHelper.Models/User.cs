
using System.ComponentModel.DataAnnotations;

namespace LogisticHelper.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string LAST_NAME { get; set; }
        [Required]
        public string NAME { get; set; }
        [Required]
        public DateTime DATE_OF_BIRTH { get; set; }
        [Required]
        public string MAIL { get; set; }

        public string PHONE { get; set; }

    }
}
