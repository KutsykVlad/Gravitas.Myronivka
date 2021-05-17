﻿using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Product.DTO
{
    public class ProductDetail : BaseEntity<string>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
    }
}