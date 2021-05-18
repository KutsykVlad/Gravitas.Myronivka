﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Model.DomainModel.EmployeeRoles.DTO;
using Gravitas.Model.Dto;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.Platform.Web.Controllers.Api
{
    public class EmployeeSeedApiController : ApiController
    {
        private readonly IEmployeeRolesRepository _employeeRolesRepository;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly GravitasDbContext _context;

        private EmployeeSeedApiController(IExternalDataRepository externalDataRepository,
            IEmployeeRolesRepository employeeRolesRepository, 
            GravitasDbContext context)
        {
            _externalDataRepository = externalDataRepository;
            _employeeRolesRepository = employeeRolesRepository;
            _context = context;
        }
        // GET: EmployeeSeed

        #region Seed Roles

        [HttpGet]
        public async Task<IHttpActionResult> SeedGuards()
        {
            try
            {
                var data = await Task.Run(() =>
                {
                    AddEmployee("Горобець Владислав  ", "Охоронець1");
                    AddEmployee("Крутофал Іван       ", "Охоронець2");
                    AddEmployee("Поліщук Сергій      ", "Охоронець3");
                    AddEmployee("Брусліновський Олекс", "Охоронець4");
                    AddEmployee("Попівнюк Віктор     ", "Охоронець5");
                    AddEmployee("Береговий Олександр ", "Охоронець6");
                    return true;
                });
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> SeedRoles()
        {
            try
            {
                var data = await Task.Run(() =>
                {
                    AssignRoles(GetRoleId("Лаборант"), new[]
                    {
                        "Гостєва Алла Анатоліївна				", "Колосов Євген Олександрович            ", "Печена Наталія Григорівна              ",
                        "Александрова Ольга Павлівна            ", "Смотрун Ольга Сергіївна                ",
                        "Пономаренко Альона Василівна           ", "Рудань Ольга Володимирівна             ",
                        "Сушняк Наталія Іванівна                ", "Терзі Олена Володимирівна              ",
                        "Бойко Тетяна Миколаївна                ", "Бондаренко Ольга Володимирівна         ",
                        "Вакуленко Наталія Олегівна             ", "Гальчинська Марина Ігорівна            ",
                        "Жак Вікторія Вікторівна                ", "Забудська Марія Анатоліївна            ",
                        "Завальнюк Валентина Григорівна         ", "Каленюк Олена Володимирівна            ",
                        "Кучеренко Наталя Олександрівна         ", "Лимар Валентина Володимирівна          ",
                        "Ліпецька Катерина Михайлівна           ", "Мирошниченко Анастасія Володимирівна   ",
                        "Назаренко Зінаїда Іванівна             ", "Нікітська Алла Володимирівна           ",
                        "Новаченко Галина Сергіївна             ", "Палієнко Тамара Іванівна               ",
                        "Попович Оксана Андріївна               ", "Приємська Ольга Андріївна              ",
                        "Савенко Олена Юріївна                  ", "Савченко Наталія Петрівна              ",
                        "Сайко Ілона Миколаївна                 ", "Сковородко Ірина Геннадіївна           ",
                        "Строєва Оксана Миколаївна              ", "Тетієвська Альона Миколаївна           ",
                        "Ткаченко Людмила Ярославівна           ", "Шандор Тетяна Андріївна                ",
                        "Швед Тетяна Іванівна                   ", "Шкуренко Альона Олексіївна             ",
                        "Шпурко Ірина Миколаївна                ", "Щербатюк Альона Володимирівна          ",
                        "Якшова Тетяна Миколаївна               ", "Яременко Валентина Сергіївна           ",
                        "Гевліченко Наталія Петрівна            ", "Головань Галина Анатоліївна            ",
                        "Горовенко Людмила Віталіївна           ", "Кукурудза Світлана Михайлівна          ",
                        "Письмак Тетяна Євгеніївна              ", "Сотник Юлія Віталіївна                 ",
                        "Строєва Олена Володимирівна            ", "Верета Олександр Віталійович           ",
                        "Глоба Наталія Петрівна                 ", "Грабенко Віталій Олегович              ",
                        "Гринчук-Задорожня Альона Олександрівна", "Іванова Людмила Болеславівна          ", "Масилюк Світлана Василівна            ",
                        "Мацепа Наталія Олексіївна             ", "Омельченко Галина Миколаївна          ", "Петрунь Сергій Юрійович               ",
                        "Стогнієнко Людмила Василівна          "
                    });

                    AssignRoles(GetRoleId("Майстер зміни"), new[]
                    {
                        "Міняйло Олександр Дмитрович        ", "Найденко Борис Іванович            ", "Рябошапка Валентин Олексійович     ",
                        "Терзі Дмитро Федорович             ", "Гірник Богдан Анатолійович         ", "Ткаченко Андрій Миколайович        ",
                        "Кушніренко Олександр Сергійович    ", "Лисенко Василь Михайлович          ", "Церно Володимир Леонідович         ",
                        "Яременко Василь Миколайович        ", "Мізецький Сергій Михайлович        ", "Олексієнко Анатолій Віталійович    ",
                        "Верета Василь Олександрович        ", "Килимистий Сергій Олексійович      ", "Пендерова Алла Юріївна             ",
                        "Яременко Анатолій Петрович         "
                    });
                    AssignRoles(GetRoleId("Машиніст"), new[]
                    {
                        "Білоус Сергій Анатолійович			", "Бузівський Олександр Анатолійович  ", "Власик Олександр Сергійович        ",
                        "Галанський Віталій Миколайович     ", "Голик Руслан Анатолійович          ", "Грачьов Сергій Геннадійович        ",
                        "Демченко Трохим Дмитрович          ", "Дмітрієв Сергій Михайлович         ", "Зінченко Сергій Борисович          ",
                        "Карбівничий Руслан Васильович      ", "Клеценко Іван Володимирович        ", "Клименко Олександр Володимирович   ",
                        "Книш Денис Миколайович             ", "Куценко Вадим Миколайович          ", "Кучеренко Віталій Петрович         ",
                        "Малий Руслан Іванович              ", "Мовчан Петро Михайлович            ", "Моключенко Максим Олександрович    ",
                        "Пінязь Анатолій Олексійович        ", "Погорілий Владислав Миколайович    ", "Поліщук Василь Анатолійович        ",
                        "Поліщук Владислав Анатолійович     ", "Поліщук Сергій Васильович          ", "Помаз Андрій Вікторович            ",
                        "Похиленко Сергій Анатолійович      ", "Руренко Дмитро Володимирович       ", "Свеколкін Аркадій Валерійович      ",
                        "Скороход Дмитро Миколайович        ", "Сула Костянтин Михайлович          ", "Хлібун Роман Ярославович           ",
                        "Шилов Андрій Анатолійович          ", "Піщаний Борис Григорович", "Дерев'янко Віктор Юрійович			",
                        "Дядюк Олександр Констянтинович     ", "Нерода Віктор Миколайович          ", "Олійник Юрій Єрофейович            ",
                        "Павлик Євгеній Іванович            ", "Саранча Юрій Павлович              ", "Ситник Олександр Вікторович        ",
                        "Судико Володимир Михайлович        ", "Уманський Роман Іванович           ", "Шевчук Микола Вікторович           ",
                        "Войченко Даніло Сергійович         ", "Гунченко Ярослав Васильович        ", "Каракай Юрій Анатолійович          ",
                        "Колісник Ірина Анатоліївна         ", "Коміренко Ігор Сергійович          ", "Куза Олена Михайлівна              ",
                        "Мудрегель Дмитро Олександрович     ", "Найденко Євгеній Борисович         ", "Омельченко Наталія Олександрівна   ",
                        "Онуфрієнко Наталія Миколаївна      ", "Палубінський Владислав Миколайович ", "Полікаренко Сергій Миколайович     ",
                        "Поліщук Ольга Дмитрівна            ", "Рудань Роман Васильович            ", "Сидоренко Василь Васильович        ",
                        "Стогнієнко Сергій Олександрович    ", "Теліженко Олександр Анатолійович   ", "Удуденко Олександр Володимирович   ",
                        "Шаповал Олег Петрович              ", "Акулов Назар Юрійович              ", "Білик Роман Олександрович          ",
                        "Будьздоровенко Артем Ігорович      ", "Дащенко Володимир Олександрович    ", "Денисенко Олександр Вікторович     ",
                        "Дорошенко Михайло Юрійович         ", "Забудський Віктор Григорович       ", "Заремба Валерій Олександрович      ",
                        "Корінний Олександр Анатолійович    ", "Корольова Надія Дмитрівна          ", "Мельниченко Тарас Віталійович      ",
                        "Мороз Віталій Павлович             ", "Наливайко Володимир Анатолійович   ", "Писарець Павло Федорович           ",
                        "Поленякін Іван Олександрович       ", "Рибачук Галина Миколаївна          ", "Слизький Іван Сергійович           ",
                        "Сліпченко Максим Сергійович        ", "Юр'єв Андрій Юрійович              "
                    });
                    AssignRoles(GetRoleId("Вагар"), new[]
                    {
                        "Буряк Оксана Іванівна			", "Васильєва Людмила Анатоліївна  ", "Галіцька Світлана Іванівна     ",
                        "Дубова Дарина Василівна        ", "Коваленко Тетяна Іванівна      ", "Комлик Світлана Сергіївна      ",
                        "Крижанівська Оксана Василівна  ", "Мацепа Анастасія Олександрівна ", "Надєєва Інна Олександрівна     ",
                        "Нахайчук Анна Вікторівна       ", "Обмокла Вікторія Вікторівна    ", "Онищенко Тетяна Дмитрівна      ",
                        "Палубінська  Аліна  Миколаївна ", "Поліщук Євгенія Іванівна       ", "Полташевська Юлія Анатоліївна  ",
                        "Савченко Альона Станіславівна  ", "Сап'ян Олена Володимирівна     ", "Старікова Наталія Олександрівна",
                        "Хайнак Лариса Юріївна          ", "Хівренко Анжела Іонівна        ", "Швед Аліна Миколаївна          ",
                        "Шияненко Наталія Геннадіївна   ", "Шкуренко Любов Анатоліївна     ", "Гаращенко Аліна Валентинівна   ",
                        "Свеколкіна Світлана Іванівна   "
                    });
                    AssignRoles(GetRoleId("Оператор"), new[]
                    {
                        "Радзівіл Роман Миколайович			", "Скоромний Віктор Михайлович        ", "Балакіров Сергій Валерійович       ",
                        "Кравченко Олексій Олександрович    ", "Воскобійник Сергій Володимирович   ", "Гарбуз Вадим Сергійович            ",
                        "Григоров Віктор Анатолійович       ", "Давидчук Олег Арсентійович         ", "Дрибас Валерій Вікторович          ",
                        "Онопрієнко Володимир Володимирович ", "Поліщук Анатолій Андрійович        ", "Поштаренко Володимир Миколайович   ",
                        "Скряга Олександр Дмитрович         ", "Ткаченко Євгеній Васильович        ", "Ткаченко Сергій Валентинович       ",
                        "Урівський Сергій Ігорович          ", "Власенко Олег Миколайович          ", "Гавриш Роман Олександрович         ",
                        "Давидчук Віктор Олегович           ", "Малінський Сергій Борисович        ", "Музиченко Петро Андрійович         ",
                        "Пендеров Вадим Георгійович         ", "Цимбал Олександр Васильович        "
                    });

                    return true;
                });
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private long GetRoleId(string roleName)
        {
            var trimmed = roleName.Trim();
            return _context.Roles.First(e => e.Name.Contains(trimmed)).Id;
        }

        private void AssignRoles(long roleId, string[] employees)
        {
            foreach (var employee in employees)
            {
                var employeeId = _externalDataRepository.GetFirstOrDefault<ExternalData.Employee, string>(e => e.ShortName.Contains(employee.Trim()))?.Id;
                if (employeeId != null)
                    _employeeRolesRepository.ApplyEmployeeRoles(new RolesDto
                    {
                        Items = new List<RoleDetail>
                        {
                            new RoleDetail
                            {
                                RoleId = roleId
                            }
                        }
                    }, employeeId);
            }
        }
        
        private string CreateEncoded(string input)
        {
            var srcEncodingFormat = Encoding.UTF8;
            var dstEncodingFormat = Encoding.GetEncoding("windows-1251");
            var originalByteString = srcEncodingFormat.GetBytes(input.Trim());
            var convertedByteString = Encoding.Convert(srcEncodingFormat,
                dstEncodingFormat, originalByteString);
            return dstEncodingFormat.GetString(convertedByteString);
        }

        private void AddEmployee(string name, string id)
        {
            _externalDataRepository.Add<ExternalData.Employee, string>(new ExternalData.Employee
            {
                Id = CreateEncoded(id),
                ShortName = CreateEncoded(name),
                FullName = CreateEncoded(name),
                Position = CreateEncoded("Охоронець"),
                IsFolder = false
            });
        }

        #endregion
    }
}