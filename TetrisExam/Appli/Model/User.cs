﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisExam.Appli.Model
{
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Point { get; set; }

        public string Token { get; set; }

      
    }
}
