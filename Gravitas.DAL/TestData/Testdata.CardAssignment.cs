using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Dom = Gravitas.Model.DomainValue.Dom;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.DAL.TestData
{
    public static partial class TestData
    {
        public static void CardAssignment(GravitasDbContext context)
        {
            string GetEmployee(string str)
            {

                Encoding srcEncodingFormat = Encoding.UTF8;
                Encoding dstEncodingFormat = Encoding.GetEncoding("windows-1251");
                byte[] originalByteString = srcEncodingFormat.GetBytes(str.Trim());
                byte[] convertedByteString = Encoding.Convert(srcEncodingFormat,
                    dstEncodingFormat, originalByteString);
                string finalString = dstEncodingFormat.GetString(convertedByteString);

                return context.Set<ExternalData.Employee>().FirstOrDefault(e =>
                    e.ShortName.ToUpper().Contains(finalString.ToUpper()) ||
                    e.FullName.ToUpper().Contains(finalString.Trim().ToUpper()))?.Id;
            }

            #region Employees
            context.Set<Card>().AddOrUpdate(new Card { No = 1, Id = "4400A82897", IsActive = true, TypeId = Dom.Card.Type.EmployeeCard });
            int i = 30;
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Гостєва Алла Анатоліївна 				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013EE62" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Колосов Євген Олександрович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0094BD14" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Печена Наталія Григорівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013F472" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Смотрун Ольга Сергіївна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "110050E34A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Пономаренко Альона Василівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00924C7E" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Рудань Ольга Володимирівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0094A8FF" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Сушняк Наталія Іванівна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F009467ED" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Терзі Олена Володимирівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00946ACF" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Бойко Тетяна Миколаївна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0095C9AF" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Бондаренко Ольга Володимирівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3E00979D88" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Вакуленко Наталія Олегівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00927219" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Гальчинська Марина Ігорівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F009260B0" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Забудська Марія Анатоліївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B00427424" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Завальнюк Валентина Григорівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0013BB45" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Кучеренко Наталя Олександрівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4500981D7A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Лимар Валентина Володимирівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00929090" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Ліпецька Катерина Михайлівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "160067801A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Назаренко Зінаїда Іванівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013A9AF" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Нікітська Алла Володимирівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B00429B75" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Новаченко Галина Сергіївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1400505DFD" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Палієнко Тамара Іванівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013C0A9" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Попович Оксана Андріївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "160067AB1F" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Савченко Наталія Петрівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00959DE6" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Сайко Ілона Миколаївна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0094CE67" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Сковородко Ірина Геннадіївна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "110000BDA5" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Строєва Оксана Миколаївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "16006549DA" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Тетієвська Альона Миколаївна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0042B303" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Ткаченко Людмила Ярославівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00924699" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Шандор Тетяна Андріївна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "14005052D4" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Швед Тетяна Іванівна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0400CDCB9D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Шкуренко Альона Олексіївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0014FD85" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Шпурко Ірина Миколаївна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00924B40" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Якшова Тетяна Миколаївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3E003AADAA" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Яременко Валентина Сергіївна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "14004FE76B" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Головань Галина Анатоліївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3400C763D7" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Кукурудза Світлана Михайлівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1700636466" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Письмак Тетяна Євгеніївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0092690D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Сотник Юлія Віталіївна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "160067B101" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Грабенко Віталій Олегович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0095D6CC" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Гринчук-Задорожня Альона Олександрівна	"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0E00668F4A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Іванова Людмила Болеславівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3400C7714A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Масилюк Світлана Василівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013D6E0" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Мацепа Наталія Олексіївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1400504D59" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Омельченко Галина Миколаївна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3400C7F887" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Петрунь Сергій Юрійович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0013583A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Міняйло Олександр Дмитрович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013CFBC" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Найденко Борис Іванович 				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00948B98" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Рябошапка Валентин Олексійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00924DFD" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Терзі Дмитро Федорович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0E0066EAEA" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Білоус Сергій Анатолійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F009273F7" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Бузівський Олександр Анатолійович		"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00924F0E" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Власик Олександр Сергійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D009B4DD6" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Голик Руслан Анатолійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3400C81BC8" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Демченко Трохим Дмитрович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3E003AB239" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Дмітрієв Сергій Михайлович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1500270ACF" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Зінченко Сергій Борисович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013E6B0" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Карбівничий Руслан Васильович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0013955F" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Клеценко Іван Володимирович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0E0066F8A7" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Клименко Олександр Володимирович		"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B004291DD" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Книш Денис Миколайович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0400EBD483" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Куценко Вадим Миколайович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0042551F" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Малий Руслан Іванович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B004279BB" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Мовчан Петро Михайлович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "14004FECAE" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Моключенко Максим Олександрович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "45009AB8F9" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Пінязь Анатолій Олексійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013BD7E" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Погорілий Владислав Миколайович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0092332E" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Поліщук Василь Анатолійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "150027A94D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Поліщук Сергій Васильович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D0096674D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Помаз Андрій Вікторович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3E0097F707" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Руренко Дмитро Володимирович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B00137107" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Свеколкін Аркадій Валерійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4400A87B7A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Скороход Дмитро Миколайович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0094A091" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Сула Костянтин Михайлович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B00427DFD" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Хлібун Роман Ярославович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3A00490CC9" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Шилов Андрій Анатолійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0013D39D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Піщаний Борис Григорович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013BFE0" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Васильєва Людмила Анатоліївна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0014CE47" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Дубова Дарина Василівна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0042B907" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Комлик Світлана Сергіївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013A886" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Крижанівська Оксана Василівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "110050F998" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Мацепа Анастасія Олександрівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "14005069C2" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Надєєва Інна Олександрівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D005AE843" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Онищенко Тетяна Дмитрівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1600673A6B" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Сап'ян Олена Володимирівна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00960F49" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Хайнак Лариса Юріївна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "150027B3CA" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Швед Аліна Миколаївна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B00425035" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Шкуренко Любов Анатоліївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D005ADFDA" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Гаращенко Аліна Валентинівна			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D009B47CD" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Гірник Богдан Анатолійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "04007A34CA" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Ткаченко Андрій Миколайович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3E0097A1C8" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Дядюк Олександр Констянтинович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00920E0A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Нерода Віктор Миколайович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4400A82895" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Саранча Юрій Павлович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3E0098031F" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Радзівіл Роман Миколайович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1600647A35" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Скоромний Віктор Михайлович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D0096695E" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Ситник Олександр Вікторович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0095FED4" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Судико Володимир Михайлович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D005AE849" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Уманський Роман Іванович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4600433DEB" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Шевчук Микола Вікторович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0014FEA1" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Балакіров Сергій Валерійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013C21C" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Кравченко Олексій Олександрович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "14004DC296" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Коваль Ігор Степанович 					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "14005009A6" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Кушніренко Віктор Сергійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0042885A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Лавриченко Анатолій Васильович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1700635DEC" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Скоромний Станіслав Миколайович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4400A86142" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Білик Олег Володимирович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0E0066D291" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Вітряненко Олександр Михайлович 		"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4400A8780E" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Старіков Олег Сергійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1600652F80" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Воскобійник Сергій Сергійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1.70E+13  " });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Тітаренко Денис Сергійович 				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0094DE82" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Ткаченко Олег Миколайович 				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4400A86143" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Шкуренко Сергій Володимирович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F009606C2" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Гуртовенко Олександр Вікторович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013A65D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Нагорний Олександр Сергійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00946967" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Федоренко Анатолій Петрович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0094B895" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Шевченко Ярослав Русланович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00946F5D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Музиченко Микола Миколайович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0013CC37" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Швець Віталій Леонідович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B00147D3F" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Штепан Олександр Васильович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4400A82824" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Кушніренко Олександр Сергійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4400A8282E" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Лисенко Василь Михайлович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D005AE6C6" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Яременко Василь Миколайович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013F58E" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Писанко Сергій Васильович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013F9AF" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Авдєєв Сергій Михайлович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013C68D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Кучеренко Сергій Петрович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "170063A6F7" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Рудань Віталій Васильович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1700638684" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Царюк Едуард Іванович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D005ADFD8" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Янворський Сергій Віталійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3400C81BD5" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Онуфрієнко Руслан Васильович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1500274E4D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Мізецький Сергій Михайлович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4400A85D77" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Олексієнко Анатолій Віталійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3E0097F15E" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Гунченко Ярослав Васильович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0094C7EB" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Колісник Ірина Анатоліївна				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00949726" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Куза Олена Михайлівна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00924EF9" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Найденко Євгеній Борисович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "45009AADC1" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Омельченко Наталія Олександрівна		"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013AA11" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Поліщук Ольга Дмитрівна					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013C11A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Рудань Роман Васильович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00948AA8" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Сидоренко Василь Васильович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013ED2F" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Стогнієнко Сергій Олександрович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F0095B9BA" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Теліженко Олександр Анатолійович		"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "45009B24F1" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Шаповал Олег Петрович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "1400507174" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Воскобійник Сергій Володимирович		"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "45009829AC" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Гарбуз Вадим Сергійович					"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3400C8055D" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Григоров Віктор Анатолійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F009255BA" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Давидчук Олег Арсентійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013C3DB" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Дрибас Валерій Вікторович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D009B47C2" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Онопрієнко Володимир Володимирович		"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3E009772B1" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Поліщук Анатолій Андрійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013C63A" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Поштаренко Володимир Миколайович		"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F006FFF81" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Скряга Олександр Дмитрович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4400A8539C" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Ткаченко Євгеній Васильович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "100013FD7C" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Ткаченко Сергій Валентинович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0D00906C97" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Урівський Сергій Ігорович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "16006804F8" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Верета Василь Олександрович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F009599BB" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Килимистий Сергій Олексійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0900326F66" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Яременко Анатолій Петрович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0014DBDF" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Білик Роман Олександрович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B00422E68" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Корінний Олександр Анатолійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0E0066AD63" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Давидчук Віктор Олегович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0E00669047" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Музиченко Петро Андрійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "20002FECB4" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Лисенко Віктор Анатолійович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B0013DE07" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Нечитайло Володимир Петрович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "0E0066DFAB" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Пастушенко Михайло Сергійович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "3F00960410" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Погорілий Артем Миколайович				"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B004253C3" });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, EmployeeId = GetEmployee("Цехмістренко Віталій Вікторович			"), TypeId = Dom.Card.Type.EmployeeCard, IsActive = true, Id = "4B00425B41" });

            context.SaveChanges();
            #endregion

            #region LabolatoryCards

            i = 300101;

            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007CB72C", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "610080B5DF", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007CC89A", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D47F4", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007CFE71", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007CACA1", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007CAE8A", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D0780", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "6100811D17", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D91FA", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D1D7D", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D4386", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D2597", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007DAF12", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61008010AC", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "600076DB03", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007F9C9B", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "610080C6C4", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61008009E1", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "610080D42A", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007FC6E2", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "6100805F37", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D5757", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D1A00", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "6100805F07", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D99C5", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007FDB55", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007C9C8F", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007DBA74", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D5A53", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D7166", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "610080B167", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "6100812494", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007FC208", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007CCAE9", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007CD5BF", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007DB439", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "6100811FF8", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D51FF", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "6100811453", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007DA094", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D716D", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "610080B260", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007CCA23", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "6100801694", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007D5FC0", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "610081043C", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });
            context.Set<Card>().AddOrUpdate(new Card { No = i++, Id = "61007F9CD8", IsActive = true, TypeId = Dom.Card.Type.LaboratoryTray });

            context.SaveChanges();

            #endregion
        }
    }
}
