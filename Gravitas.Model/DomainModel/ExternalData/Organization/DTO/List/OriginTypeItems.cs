﻿using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto {

	public static partial class ExternalData {

		public class OriginTypeItems : BaseEntity<string> {

			public IEnumerable<OriginTypeItem>	Items { get; set; }
		}
	}
}