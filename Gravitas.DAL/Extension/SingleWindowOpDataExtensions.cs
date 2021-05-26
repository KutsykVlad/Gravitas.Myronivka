using System;
using System.Linq;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using SingleWindowOpData = Gravitas.Model.DomainModel.OpData.DAO.SingleWindowOpData;

namespace Gravitas.DAL.Extension
{
    public static class SingleWindowOpDataExtensions
    {
        public static SingleWindowOpData FetchSingleWindowOpData(this SingleWindowOpData refOpData, SingleWindowOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            refOpData.ContactPhoneNo = srcOpData.ContactPhoneNo;
            refOpData.LoadTarget = srcOpData.LoadTarget;
            refOpData.OrganizationId = srcOpData.OrganizationId;
            refOpData.CreateOperatorId = srcOpData.OpVisaSet.FirstOrDefault()?.EmployeeId;
            refOpData.CreateDate = srcOpData.OpVisaSet.FirstOrDefault()?.DateTime;
            refOpData.EditOperatorId = srcOpData.OpVisaSet.LastOrDefault()?.EmployeeId;
            refOpData.EditDate = srcOpData.OpVisaSet.LastOrDefault()?.DateTime;
            refOpData.DocumentTypeId = srcOpData.DocumentTypeId;
            refOpData.StockId = srcOpData.StockId;
            refOpData.ReceiverTypeId = srcOpData.ReceiverTypeId;
            refOpData.ReceiverId = srcOpData.ReceiverId;
            refOpData.ReceiverAnaliticsId = srcOpData.ReceiverAnaliticsId;
            refOpData.ProductId = srcOpData.ProductId;
            refOpData.HarvestId = srcOpData.HarvestId;
            refOpData.DriverOneId = srcOpData.DriverOneId;
            refOpData.DriverTwoId = srcOpData.DriverTwoId;
            refOpData.TransportId = srcOpData.TransportId;
            refOpData.HiredDriverCode = srcOpData.HiredDriverCode;
            refOpData.HiredTransportNumber = srcOpData.HiredTransportNumber;
            refOpData.IncomeInvoiceSeries = srcOpData.IncomeInvoiceSeries;
            refOpData.IncomeInvoiceNumber = srcOpData.IncomeInvoiceNumber;
            refOpData.ReceiverDepotId = srcOpData.ReceiverDepotId;
            refOpData.IsThirdPartyCarrier = srcOpData.IsThirdPartyCarrier;
            refOpData.CarrierCode = srcOpData.CarrierCode;
            refOpData.BuyBudgetId = srcOpData.BuyBudgetId;
            refOpData.SellBudgetId = srcOpData.SellBudgetId;
            refOpData.PackingWeightValue = srcOpData.PackingWeightValue;
            refOpData.KeeperOrganizationId = srcOpData.KeeperOrganizationId;
            refOpData.OrderCode = srcOpData.OrderCode;
            refOpData.SupplyCode = srcOpData.SupplyCode;
            refOpData.SupplyTypeId = srcOpData.SupplyTypeId;
            refOpData.RegistrationDateTime = srcOpData.RegistrationDateTime;
            refOpData.CollectionPointId = srcOpData.CollectionPointId;
            refOpData.DocHumidityValue = srcOpData.DocHumidityValue;
            refOpData.DocImpurityValue = srcOpData.DocImpurityValue;
            refOpData.DocNetValue = srcOpData.DocNetValue;
            refOpData.DocNetDateTime = srcOpData.DocNetDateTime;
            refOpData.ReturnCauseId = srcOpData.ReturnCauseId;
            refOpData.TrailerId = srcOpData.TrailerId;
            refOpData.TrailerNumber = srcOpData.TrailerNumber;
            refOpData.TripTicketNumber = srcOpData.TripTicketNumber;
            refOpData.TripTicketDateTime = srcOpData.TripTicketDateTime;
            refOpData.WarrantSeries = srcOpData.WarrantSeries;
            refOpData.WarrantNumber = srcOpData.WarrantNumber;
            refOpData.WarrantDateTime = srcOpData.WarrantDateTime;
            refOpData.WarrantManagerName = srcOpData.WarrantManagerName;
            refOpData.StatusType = srcOpData.StatusType;
            refOpData.RuleNumber = srcOpData.RuleNumber;
            refOpData.IncomeDocGrossValue = srcOpData.IncomeDocGrossValue;
            refOpData.IncomeDocTareValue = srcOpData.IncomeDocTareValue;
            refOpData.IncomeDocDateTime = srcOpData.IncomeDocDateTime;
            refOpData.Comments = srcOpData.Comments;
            refOpData.SupplyTransportTypeId = srcOpData.SupplyTransportTypeId;
            refOpData.BatchNumber = srcOpData.BatchNumber;
            refOpData.CarrierRouteId = srcOpData.CarrierRouteId;
            refOpData.DeliveryBillId = srcOpData.DeliveryBillId;
            refOpData.DeliveryBillCode = srcOpData.DeliveryBillCode;
            refOpData.InformationCarrier = srcOpData.InformationCarrier;
            refOpData.ProductContents = srcOpData.ProductContents;

            return refOpData;
        }

        public static SingleWindowOpData FetchSecurityCheckInOpData(this SingleWindowOpData refOpData, SecurityCheckInOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            refOpData.InTime = srcOpData.CheckOutDateTime;

            return refOpData;
        }

        public static SingleWindowOpData FetchSecurityCheckOutOpData(this SingleWindowOpData refOpData, SecurityCheckOutOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            refOpData.OutTime = srcOpData.CheckOutDateTime;
            refOpData.StampList = srcOpData.SealList;

            return refOpData;
        }

        public static SingleWindowOpData FetchFirstTareScaleOpData(this SingleWindowOpData refOpData, ScaleOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            if (srcOpData.TypeId == ScaleOpDataType.Tare) refOpData.FirstTareTime = srcOpData.TruckWeightDateTime; // TODO

            return refOpData;
        }

        public static SingleWindowOpData FetchLastTareScaleOpData(this SingleWindowOpData refOpData, ScaleOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            if (srcOpData.TypeId == ScaleOpDataType.Tare)
            {
                refOpData.TareValue = srcOpData.TruckWeightValue ?? 0;
                refOpData.LastTareTime = srcOpData.TruckWeightDateTime;
                refOpData.TrailerTareValue = srcOpData.TrailerWeightValue ?? 0;
                refOpData.TareOperatorId = srcOpData.OpVisaSet.LastOrDefault()?.EmployeeId;
            }

            return refOpData;
        }

        public static SingleWindowOpData FetchFirstGrossScaleOpData(this SingleWindowOpData refOpData, ScaleOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            if (srcOpData.TypeId == ScaleOpDataType.Gross) refOpData.FirstGrossTime = srcOpData.TruckWeightDateTime;

            return refOpData;
        }

        public static SingleWindowOpData FetchLastGrossScaleOpData(this SingleWindowOpData refOpData, ScaleOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            if (srcOpData.TypeId == ScaleOpDataType.Gross)
            {
                refOpData.GrossValue = srcOpData.TruckWeightValue ?? 0;
                refOpData.LastGrossTime = srcOpData.TruckWeightDateTime;
                refOpData.TrailerGrossValue = srcOpData.TrailerWeightValue ?? 0;
                refOpData.GrossOperatorId = srcOpData.OpVisaSet.LastOrDefault()?.EmployeeId;
            }

            return refOpData;
        }

        public static SingleWindowOpData FetchWeightResultsOpData(this SingleWindowOpData refOpData)
        {
            if (refOpData == null)
                return refOpData;

            var sumGrossValue = refOpData.GrossValue;
            if (refOpData.TrailerGrossValue != null)
                sumGrossValue += refOpData.TrailerGrossValue;

            var sumTareValue = refOpData.TareValue;
            if (refOpData.TrailerTareValue != null)
                sumTareValue += refOpData.TrailerTareValue;

            if (sumTareValue != null && sumGrossValue != null)
                refOpData.NetValue = Math.Abs(sumTareValue.Value - sumGrossValue.Value);
            refOpData.WeightDeltaValue = refOpData.NetValue - refOpData.DocNetValue;
            return refOpData;
        }

        public static SingleWindowOpData FetchLabFacelessOpData(this SingleWindowOpData refOpData, LabFacelessOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            refOpData.LabHumidityName = srcOpData.HumidityClassId;
            refOpData.LabImpurityName = srcOpData.ImpurityClassId;
            refOpData.LabIsInfectioned = srcOpData.InfectionedClassId != ExternalData.LabClassifier.Infection.Class2;
            refOpData.LabHumidityValue = srcOpData.HumidityValue;
            refOpData.LabImpurityValue = srcOpData.ImpurityValue;
            refOpData.LabolatoryOperatorId = srcOpData.OpVisaSet.LastOrDefault()?.EmployeeId;
            refOpData.LabOilContentValue = srcOpData.EffectiveValue;

            return refOpData;
        }

        public static SingleWindowOpData FetchLoadOpData(this SingleWindowOpData refOpData, LoadPointOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            refOpData.LoadingOperatorId = srcOpData.OpVisaSet.LastOrDefault()?.EmployeeId;
            refOpData.LoadOutDateTime = srcOpData.CheckOutDateTime;

            return refOpData;
        }

        public static SingleWindowOpData FetchUnloadOpData(this SingleWindowOpData refOpData, UnloadPointOpData srcOpData)
        {
            if (refOpData == null || srcOpData == null) return refOpData;

            refOpData.LoadingOperatorId = srcOpData.OpVisaSet.LastOrDefault()?.EmployeeId;
            refOpData.LoadOutDateTime = srcOpData.CheckOutDateTime;

            return refOpData;
        }
    }
}