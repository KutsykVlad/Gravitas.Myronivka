using System.Collections.Generic;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.Sms
{
    public class SmsTemplatesRepository : ISmsTemplatesRepository
    {
        private readonly Dictionary<SmsTemplate, string> Templates = new Dictionary<SmsTemplate, string>()
        {
            {
                SmsTemplate.DestinationPointApprovalSms, "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo##  очікують на ##field DestinationPoint##."
            },
            {
                SmsTemplate.DriverQualityMatchingSms,
                "ТОВ КЕ. Транспорту  ##field TransportNo## ##field TrailerNo##  необхідне узгодження якості ##field ProductName##. Чекайте інформацію про результати узгодження."
            },
            {
                SmsTemplate.EntranceApprovalSms, "ТОВ КЕ. Транспорту  ##field TransportNo## ##field TrailerNo##  дозволений в'їзд. Очікуємо на КПП на протязі 20хв."
            },
            {
                SmsTemplate.InvalidPerimeterGuardianSms,
                "ТОВ КЕ. Транспорту ##field TransportNo## ##field TrailerNo## необхідно підтвердження охорони на ваговій платформі №##field NodeNumber##. ##field ScaleValidationText##"
            },
            {
                SmsTemplate.QueueRegistrationSms,
                "ТОВ КЕ. Телефон диспетчера +##field DispatcherPhoneNumber##. Транспорт ##field TransportNo## ##field TrailerNo##  зареєстрований в черзі вантажу ##field ProductName##. Автомобілів в черзі ##field TrucksInQueue##. Орієнтовний час проходження маршруту ##field AverageProcessingTime##. Чекайте наступного СМС або стежте за інформацією на табло."
            },
            {
                SmsTemplate.RequestForQualityApprovalSms,
                "ТОВ КЕ. Транспорту ##field TransportNo## ##field TrailerNo##  необхідне узгодження якості ##field ProductName##. Додаткова інформація на пошті ##field Email##."
            },
            {
                SmsTemplate.RouteChangeSms, "ТОВ КЕ. Транспорту ##field TransportNo## ##field TrailerNo##  змінили маршрут. Проїдьте будь ласка на ##field DestinationPoint##."
            },
            {
                SmsTemplate.ReturnToCollectSamples, "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo##  відправлений на повторний відбір аналізів."
            },
            {
                SmsTemplate.MissedEntranceTimeNeedToWait, "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo##  пропустив чергу на в'їзд. Очікуйте повторного дозволу."
            },
            {
                SmsTemplate.RemovedFromQueue, "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo##  видалено з черги."
            },
            {
                SmsTemplate.CentralLaboratoryCollisionSend,
                "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## з вантажем ##field ProductName## потребує погодження. Коментар: ##field CollisionComment##."
            },
            {
                SmsTemplate.CentralLaboratoryCollisionProcessed,
                "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## з вантажем ##field ProductName## ##field CollisionResult## лабораторні показники."
            },
            {
                SmsTemplate.CentralLaboratorySuccess, "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## успішно оброблений на лабораторії. Рухайтеся далі згідно маршруту."
            },
            {
                SmsTemplate.OnRegisterEmployeeInformation, "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## зареєстрований в черзі вантажу ##field ProductName## ##field ReceiverName##."
            },
            {
                SmsTemplate.OnWeighBridgeRejected, "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## не пройшов вагові ліміти."
            },
            {
                SmsTemplate.NoMixedFeedProduct, "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## очікує на ##field ProductName##"
            },
        };

        public string GetSmsTemplate(SmsTemplate templateId)
        {
            return Templates[templateId];
        }
    }
}
