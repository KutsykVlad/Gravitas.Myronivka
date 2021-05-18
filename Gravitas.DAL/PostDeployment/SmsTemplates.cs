using System.Data.Entity.Migrations;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Sms.DAO;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL.PostDeployment
{

    public static partial class PostDeployment
    {
        public static class SmsTemplates
        {
            public static void SmsTemplate(GravitasDbContext context)
            {
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.DestinationPointApprovalSms, Name = "Пункт призначення", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo##  очікують на ##field DestinationPoint##." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.DriverQualityMatchingSms, Name = "Погодження якості водію", Text = "ТОВ КЕ. Транспорту  ##field TransportNo## ##field TrailerNo##  необхідне узгодження якості ##field ProductName##. Чекайте інформацію про результати узгодження." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.EntranceApprovalSms, Name = "Дозвіл на в'їзд", Text = "ТОВ КЕ. Транспорту  ##field TransportNo## ##field TrailerNo##  дозволений в'їзд. Очікуємо на КПП на протязі 20хв." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.InvalidPerimeterGuardianSms, Name = "Підтвердження охорони", Text = "ТОВ КЕ. Транспорту ##field TransportNo## ##field TrailerNo## необхідно підтвердження охорони на ваговій платформі №##field NodeNumber##. ##field ScaleValidationText##" });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.QueueRegistrationSms, Name = "Реєстрація в черзі", Text = "ТОВ КЕ. Телефон диспетчера +##field DispatcherPhoneNumber##. Транспорт ##field TransportNo## ##field TrailerNo##  зареєстрований в черзі вантажу ##field ProductName##. Орієнтовний час в'їзду ##field EntranceTime##. Чекайте наступного СМС або стежте за інформацією на табло." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.RequestForQualityApprovalSms, Name = "Погодження якості", Text = "ТОВ КЕ. Транспорту ##field TransportNo## ##field TrailerNo##  необхідне узгодження якості ##field ProductName##. Додаткова інформація на пошті ##field Email##." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.RouteChangeSms, Name = "Зміна маршруту", Text = "ТОВ КЕ. Транспорту ##field TransportNo## ##field TrailerNo##  змінили маршрут. Проїдьте будь ласка на ##field DestinationPoint##." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.ReturnToCollectSamples, Name = "Повторний відбір аналізів", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo##  відправлений на повторний відбір аналізів." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.MissedEntranceTimeNeedToWait, Name = "Пропуск черги", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo##  пропустив чергу на в'їзд. Очікуйте повторного дозволу." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.RemovedFromQueue, Name = "Видалено з черги", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo##  видалено з черги." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.CentralLaboratoryCollisionSend, Name = "Відправка на погодження(Центральна лабораторія)", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## з вантажем ##field ProductName## потребує погодження. Коментар: ##field CollisionComment##." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.CentralLaboratoryCollisionProcessed, Name = "Обробка погодження(Центральна лабораторія)", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## з вантажем ##field ProductName## ##field CollisionResult## лабораторні показники." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.CentralLaboratorySuccess, Name = "Обробка успішна(Центральна лабораторія)", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## успішно оброблений на лабораторії. Рухайтеся далі згідно маршруту." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.OnRegisterEmployeeInformation, Name = "Відправка майстру інформації при реєстрації авта", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## зареєстрований в черзі вантажу ##field ProductName## ##field ReceiverName##." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.OnWeighBridgeRejected, Name = "Повернення з вагової", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## не пройшов вагові ліміти." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.OnPreRegister, Name = "Підтвердження попередньої реєстрації", Text = "ТОВ КЕ. Ви зареєстровані в черзі. Орієнтовний час вїзду: ##field EntranceTime##." });
                context.Set<SmsTemplate>().AddOrUpdate(new SmsTemplate { Id = Dom.Sms.Template.NoMixedFeedProduct, Name = "Немає відповідньої номенклатури для комбікорму", Text = "ТОВ КЕ. Транспорт ##field TransportNo## ##field TrailerNo## очікує на ##field ProductName##" });

                context.SaveChanges();
            }
        }
    }

}
