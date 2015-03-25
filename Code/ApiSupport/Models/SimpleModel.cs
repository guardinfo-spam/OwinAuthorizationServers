using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSupport.Models
{
    public sealed class SimpleModel
    {
        [Required]
        public string Value { get; set; }
    }
}
