﻿using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Subdivision.DTO
{
    public class SubdivisionDetail : BaseEntity<string>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}