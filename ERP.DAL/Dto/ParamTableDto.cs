﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.DAL.Dto
{
    public class ParamTableDto
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string PrefixKey { get; set; }
        public int MinDigit { get; set; }
        public int CurrentId { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
    }
}
