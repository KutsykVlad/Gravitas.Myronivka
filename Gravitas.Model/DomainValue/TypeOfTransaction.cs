namespace Gravitas.Model.DomainValue
{
    public static class TypeOfTransaction
    {
        public const string WeightCanceled = "Отмена взвешивания";
        public const string WeightProcessed = "Взвешивание";
        public const string SupplyCodeChanged = "Изменение кода поставки";
        public const string GrossWeighting = "Взвешивания брутто";
        public const string TareWeighting = "Взвешивания тары";
        public const string GrossTrailerWeighting = "Взвешивания брутто прицепа";
        public const string TareTrailerWeighting = "Взвешивания тары прицепа";
        public const string GrossReset = "Обнуление брутто";
        public const string TareReset = "Обнуление тары";
        public const string GrossTrailerReset = "Обнуление брутто прицепа";
        public const string TareTrailerReset = "Обнуление тары прицепа";
        public const string GrossProcessing = "Обработка брутто";
        public const string TareProcessing = "Обработка тара";
        public const string GrossTrailerProcessing = "Обработка брутто прицепа";
        public const string TareTrailerProcessing = "Обработка тары прицепа";
        public const string GrossWithPallet = "Брутто с поддоном";
        public const string GrossWeighingControl = "Взвешивания брутто контроль";
        public const string GrossZeroingControl = "Обнуление брутто контроль";
        public const string ZeroAfterWeighing = "Обнуление после взвешивания";
        public const string PlatformChanged = "Изменение платформы";
        public const string ChangeDeliveryType = "Изменение вида поставки";
        public const string Other = "Прочее";
    }
}