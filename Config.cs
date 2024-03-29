﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicLearn
{
    internal class Config
    {
        public int configVersion { get; set; }
        public string databaseAddress { get; set; } = "";
        public string databaseName { get; set; } = "";
        public string databaseFile { get; set; } = "";
        public bool useLiteDb { get; set; }
    }
}
