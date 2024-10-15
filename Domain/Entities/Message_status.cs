﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message_status
    {
        [Key]
        public int id {  get; set; }
        [Required]
        public string name { get; set; }
    }
}
