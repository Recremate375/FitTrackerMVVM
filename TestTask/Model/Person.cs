﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Model
{
    [Serializable]
    public class Person
    {
        public int Rank { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public int Steps { get; set; }

    }
}
