﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerPasswordCredentialsGrant
{
    public class DbSetting
    {
        public static string App
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["App"].ConnectionString; }
        }
    }
}
