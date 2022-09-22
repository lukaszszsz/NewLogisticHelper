using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace LogisticHelper.Models
{
    //[Table("Terc", Schema = "Terc")]


    public class Terc
    {

      [Key]
        public int ID_TERC { get; set; }
        public string WOJ { get; set; }
        public string? POW { get; set; }
        public string? GMI { get; set; }
        public string? RODZ { get; set; }
        public string NAZWA{ get; set; }
        public string NAZWA_DOD { get; set; }
        public string STAN_NA { get; set; }

    }
}
