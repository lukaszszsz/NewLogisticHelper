using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LogisticHelper.Models
{
    //[Table("Terc", Schema = "Terc")]
    [Keyless]

    public class Terc
    {
        public byte WOJ { get; set; }
        public byte? POW { get; set; }
        public byte? GMI { get; set; }
        public byte? RODZ { get; set; }
        public string NAZWA{ get; set; }
        public string NAZWA_DOD { get; set; }
        public DateTime STAN_NA { get; set; }

    }
}
