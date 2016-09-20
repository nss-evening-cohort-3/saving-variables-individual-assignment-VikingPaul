using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.Models
{
    public class Variables
    {
        [Key]
        public int VariableId { get; set; }
        [Required]
        public string Var { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
