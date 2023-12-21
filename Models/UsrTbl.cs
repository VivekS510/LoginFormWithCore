using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginFormWithCore.Models
{
    public partial class UsrTbl
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        [DataType (DataType.Password)]
        public string? Password { get; set; }
    }
}
