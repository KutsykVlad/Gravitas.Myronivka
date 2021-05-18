﻿using System.Collections.Generic;
using Gravitas.Model.DomainModel.Sms.DAO;

namespace Gravitas.Model.DomainModel.Sms.DTO
{
    public class SmsTemplates
    {
        public SmsTemplates()
        {
            Items = new List<SmsTemplate>();
        }

        public ICollection<SmsTemplate> Items { get; set; }
        public int Count { get; set; }
    }
}
