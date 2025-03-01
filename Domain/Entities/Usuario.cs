﻿using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario : Entidad<int>
    {
        [StringLength(100)]
        public string? NameUser { get; set; }
        [StringLength(100)]
        public string? Password { get; set; }
    }
}
