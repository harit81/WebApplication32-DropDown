using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication32_DropDown.Models
{
    public class TblCountry
    {
        [Key]
        public int Cid { get; set; }
        public string Cname { get; set; }
    }
}
