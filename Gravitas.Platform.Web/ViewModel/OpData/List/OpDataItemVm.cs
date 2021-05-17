using System;
using System.Collections.Generic;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace Gravitas.Platform.Web.ViewModel {

	public class OpDataItemVm : ICloneable {

	    public OpDataItemVm()
	    {
            Signatures = new List<OpDataItemSignature>();
	    }

		public Guid Id { get; set; }
		[DisplayName("Тип операції")]
		public object OpDataType { get; set; }

		public long StateId { get; set; }
		[DisplayName("Статус")]
		public string StateName { get; set; }
		[DisplayName("Дата\\час заїзду")]
		public DateTime? CheckInDateTime { get; set; }
		[DisplayName("Дата\\час від’їзду")]
		public DateTime? CheckOutDateTime { get; set; }

		public long? NodeId { get; set; }
		public string NodeCode { get; set; }
		[DisplayName("Контрольна точка")]
		public string NodeName { get; set; }

		[DisplayName("Підпис")]
		public int OpVisaCount { get; set; }
		[DisplayName("Стоп кадр")]
		public int OpCameraImageCount { get; set; }

        public ICollection<OpDataItemSignature> Signatures { get; set; } 

		[DisplayName("Повідомлення")]
		public string Message { get; set; }

		public bool IsNonStandard { get; set; }

		public object Clone()
	    {
	        return this.MemberwiseClone();
	    }
	}
}