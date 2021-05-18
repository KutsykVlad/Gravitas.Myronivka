using System.Data.Entity.Migrations;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {
        public static class OpRoutineState
        {
            public static void SingleWindow(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.Idle, StateNo = 1, OpRoutineId = Dom.OpRoutine.SingleWindow.Id, Name = "Очікування"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    StateNo = 2,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Вибір документу"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    StateNo = 3,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Меню документу"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.ContainerCloseAddOpVisa,
                    StateNo = 3,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Підпис операції /Закриття квитка/"
                }); 

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.EditTicketForm,
                    StateNo = 5,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Правка документу"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.EditGetApiData,
                    StateNo = 6,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Забір даних з 1С"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.EditAddOpVisa,
                    StateNo = 7,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Підпис операції /Редагування/"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.EditPostApiData,
                    StateNo = 8,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Відправка даних в 1С"
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.RouteEditData,
                    StateNo = 9,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Правка маршруту"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.RouteAddOpVisa,
                    StateNo = 10,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Підпис операції /Збереження даних/"
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.CloseAddOpVisa,
                    StateNo = 11,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Підпис операції /Закриття/"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.ClosePostApiData,
                    StateNo = 12,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Відправка даних в 1С"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.SupplyChangeAddOpVisa,
                    StateNo = 13,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Підпис операції /Зміна коду поставки/"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.DivideTicketAddOpVisa,
                    StateNo = 14,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Підпис операції /Розділення ТТН/"
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SingleWindow.State.DeleteTicketAddOpVisa,
                    StateNo = 15,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = "Підпис операції /Видалення ТТН/"
                });

                context.SaveChanges();
            }

            public static void SecurityIn(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityIn.State.Idle, 
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id, 
                    Name = "Очікування", 
                    StateNo = 1
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityIn.State.CheckOwnTransport, 
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id, 
                    Name = "Перевірка власного транспорту", 
                    StateNo = 2
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityIn.State.BindLongRangeRfid,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = "Прив'язка мітки до авто",
                    StateNo = 3
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityIn.State.AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = "Підпис операції /Додавання/",
                    StateNo = 4
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityIn.State.OpenBarrier,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = "Проїзд транспорту",
                    StateNo = 5
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityIn.State.GetCamSnapshot,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = "Збереження кадрів камер спостереження",
                    StateNo = 6
                });
                context.SaveChanges();
            }

            public static void SecurityOut(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityOut.State.Idle, OpRoutineId = Dom.OpRoutine.SecurityOut.Id, Name = "Очікування", StateNo = 1
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityOut.State.CheckOwnTransport, 
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id, 
                    Name = "Перевірка власного транспорту", 
                    StateNo = 2
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityOut.State.ShowOperationsList,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = "Перевірка маршруту",
                    StateNo = 3
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityOut.State.EditStampList, OpRoutineId = Dom.OpRoutine.SecurityOut.Id, Name = "Ввід даних", StateNo = 4
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityOut.State.AddRouteControlVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = "Підпис операції /Камери спостереження/",
                    StateNo = 5
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityOut.State.AddTransportInspectionVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = "Підпис операції /Зовнішній огляд/",
                    StateNo = 6
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityOut.State.OpenBarrier,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = "Проїзд дозволено",
                    StateNo = 7
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityOut.State.GetCamSnapshot,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = "Збереження кадрів",
                    StateNo = 8
                });
                context.SaveChanges();
            }

            public static void SecurityReview(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityReview.State.Idle,
                    OpRoutineId = Dom.OpRoutine.SecurityReview.Id,
                    Name = "Очікування",
                    StateNo = 1
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.SecurityReview.State.AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityReview.Id,
                    Name = "Підпис операції /Додавання/",
                    StateNo = 2
                });
                context.SaveChanges();
            }

            public static void CentralLaboratorySample(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratorySamples.State.Idle,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    Name = "Очікування картки водія",
                    StateNo = 1
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleBindTray,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    Name = "Прив'язка лотку для відбору проб",
                    StateNo = 2
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    Name = "Відбір проб / Підпис операції",
                    StateNo = 3
                });
                context.SaveChanges();
            }

            public static void CentralLaboratoryProcess(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Реєстр проб",
                    StateNo = 1
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.AddSample,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Реєстрація проби",
                    StateNo = 2
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Підпис операції /Реєстрація проби/",
                    StateNo = 3
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintLabel,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Друк ярлика /Реєстрація проби/",
                    StateNo = 4
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Підтвердження результатів / Друк результатів",
                    StateNo = 5
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Підпис операції / Підтвердження результатів",
                    StateNo = 6
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Підтвердження результатів / Ініціалізація погодження",
                    StateNo = 7
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Підпис операції / Підтвердження результатів",
                    StateNo = 8
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Підтвердження результатів / Підпис операції",
                    StateNo = 9
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDocument,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = "Підтвердження результатів / Отримання документу",
                    StateNo = 10
                });
                context.SaveChanges();
            }

            public static void LaboratoryIn(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.Idle, OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id, Name = "Очікування", StateNo = 1
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.SampleReadTruckRfid,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Відбір проб / Сканування мітки автомобіля",
                    StateNo = 2
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.SampleBindTray,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Відбір проб / Прив'язка лотку для відбору проб",
                    StateNo = 3
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.SampleBindAnalysisTray,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Відбір проб / Прив'язка лотку для виконання аналізів",
                    StateNo = 4
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.SampleAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Відбір проб / Підпис операції",
                    StateNo = 5
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.ResultReadTrayRfid,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Ввід результатів / Сканування лотку",
                    StateNo = 6
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.ResultEditAnalysis,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Ввід результатів",
                    StateNo = 8
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.ResultAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Ввід результатів / Підпис операції",
                    StateNo = 9
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.PrintReadTrayRfid,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Підтвердження результатів / Сканування лотку",
                    StateNo = 10
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisResults,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Підтвердження результатів",
                    StateNo = 11
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Підтвердження результатів / Підпис операції",
                    StateNo = 12
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.PrintDataDisclose,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Підтвердження результатів / Друк результатів",
                    StateNo = 13
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionInit,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Підтвердження результатів / Ініціалізація погодження",
                    StateNo = 14
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionManage,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Підтвердження результатів / Погодження",
                    StateNo = 15
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Підтвердження результатів / Підпис операції",
                    StateNo = 16
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LabolatoryIn.State.PrintLaboratoryProtocol,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = "Підтвердження результатів / Друк протоколу",
                    StateNo = 17
                });

                context.SaveChanges();
            }

            public static void Weightbridge(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Аналіз магнітної петлі",
                    StateNo = 1
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.GetScaleZero, OpRoutineId = Dom.OpRoutine.Weighbridge.Id, Name = "Обнулення ваг", StateNo = 2
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.OpenBarrierIn,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Проїзд дозволено",
                    StateNo = 3
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.CheckScaleNotEmpty,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Заїдьте на ваги",
                    StateNo = 4
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.GetTicketCard,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Ідентифікація водія",
                    StateNo = 5
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.DriverTrailerEnableCheck,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Перевірка на розчіпку /Водій/",
                    StateNo = 6
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.GuardianCardPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Підпис охоронця /В'їзд/",
                    StateNo = 7
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.GuardianTruckVerification,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Перевірка охоронцем /В'їзд/",
                    StateNo = 8
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.GuardianTrailerEnableCheck,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Перевірка на розчіпку /Охоронець/",
                    StateNo = 9
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Запит на зважування /Автомобіль/",
                    StateNo = 10
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.GetGuardianTruckWeightPermission,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Дозвіл охоронця /Зважування автомобіля/",
                    StateNo = 11
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.GetTruckWeight,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Зважування /Автомобіль/",
                    StateNo = 12
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.TrailerWeightPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Запит на зважування /Причеп/",
                    StateNo = 13
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.GetGuardianTrailerWeightPermission,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Дозвіл охоронця /Зважування причепу/",
                    StateNo = 14
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.GetTrailerWeight,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Зважування /Причеп/",
                    StateNo = 15
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.WeightResultsValidation,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Перевірка результатів зважування",
                    StateNo = 16
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.OpenBarrierOut,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Виїзд дозволено",
                    StateNo = 17
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.Weighbridge.State.CheckScaleEmpty,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = "Звільніть ваги",
                    StateNo = 18
                });

                context.SaveChanges();
            }

            public static void UnloadPointGuide(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.State.Idle, OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id, Name = "Очікування", StateNo = 1
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.State.BindUnloadPoint,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = "Призначення ями вивантаження",
                    StateNo = 2
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.State.AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = "Підпис операції призначення ями",
                    StateNo = 3
                });
                
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.State.EntryAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = "Підпис операції виклику з черги",
                    StateNo = 4
                });

                context.SaveChanges();
            }
            
            public static void UnloadPointGuide2(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointGuide2.State.Idle, 
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide2.Id, 
                    Name = "Очікування", 
                    StateNo = 1
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointGuide2.State.BindUnloadPoint,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide2.Id,
                    Name = "Призначення ями вивантаження",
                    StateNo = 2
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointGuide2.State.AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide2.Id,
                    Name = "Підпис операції призначення ями",
                    StateNo = 3
                });
                
                context.SaveChanges();
            }

            public static void UnloadPointType1(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointType1.State.Workstation,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = "Робоча станція",
                    StateNo = 1
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointType1.State.Idle, OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id, Name = "Очікування", StateNo = 2
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointType1.State.AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = "Підпис операції",
                    StateNo = 3
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointType1.State.AddChangeStateVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = "Підпис операції деактивації вузла",
                    StateNo = 4
                });
                context.SaveChanges();
            }
            
            public static void UnloadPointType2(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointType2.State.Workstation,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = "Робоча станція",
                    StateNo = 1
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointType2.State.Idle, 
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id, 
                    Name = "Очікування", 
                    StateNo = 2
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointType2.State.SelectAcceptancePoint,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = "Присвоєння складу розвантаження",
                    StateNo = 3
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointType2.State.AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = "Підпис операції",
                    StateNo = 4
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadPointType2.State.AddChangeStateVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = "Підпис операції деактивації вузла",
                    StateNo = 5
                });
                context.SaveChanges();
            }

            public static void LoadCheckPoint(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadCheckPoint.State.Idle, OpRoutineId = Dom.OpRoutine.LoadCheckPoint.Id, Name = "Очікування", StateNo = 1
                });
//                context.Set<Model.OpRoutineState>().AddOrUpdate(new Model.OpRoutineState
//                {
//                    Id = Dom.OpRoutine.LoadCheckPoint.State.GetTareValue,
//                    OpRoutineId = Dom.OpRoutine.LoadCheckPoint.Id,
//                    Name = "Введення тари",
//                    StateNo = 2
//                });
//                context.Set<Model.OpRoutineState>().AddOrUpdate(new Model.OpRoutineState
//                {
//                    Id = Dom.OpRoutine.LoadCheckPoint.State.AddOperationVisa,
//                    OpRoutineId = Dom.OpRoutine.LoadCheckPoint.Id,
//                    Name = "Підпис операції",
//                    StateNo = 3
//                });
                context.SaveChanges();
            }

            public static void UnloadCheckPoint(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.UnloadCheckPoint.State.Idle, OpRoutineId = Dom.OpRoutine.UnloadCheckPoint.Id, Name = "Очікування", StateNo = 1
                });
//                context.Set<Model.OpRoutineState>().AddOrUpdate(new Model.OpRoutineState
//                {
//                    Id = Dom.OpRoutine.UnloadCheckPoint.State.GetTareValue,
//                    OpRoutineId = Dom.OpRoutine.UnloadCheckPoint.Id,
//                    Name = "Введення тари",
//                    StateNo = 2
//                });
//                context.Set<Model.OpRoutineState>().AddOrUpdate(new Model.OpRoutineState
//                {
//                    Id = Dom.OpRoutine.UnloadCheckPoint.State.AddOperationVisa,
//                    OpRoutineId = Dom.OpRoutine.UnloadCheckPoint.Id,
//                    Name = "Підпис операції",
//                    StateNo = 3
//                });
                context.SaveChanges();
            }

            public static void LoadPointType1(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointType1.State.Workstation,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = "Робоча станція",
                    StateNo = 1
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointType1.State.Idle, OpRoutineId = Dom.OpRoutine.LoadPointType1.Id, Name = "Очікування", StateNo = 2
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointType1.State.AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = "Підпис операції",
                    StateNo = 3
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointType1.State.AddChangeStateVisa,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = "Підпис операції деактивації вузла",
                    StateNo = 3
                });
                context.SaveChanges();
            }

            public static void LoadPointGuide(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointGuide.State.Idle, OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id, Name = "Очікування", StateNo = 1
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    Name = "Призначення точки завантаження",
                    StateNo = 2
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointGuide.State.AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    Name = "Підпис операції призначення точки завантаження",
                    StateNo = 3
                });
                context.SaveChanges();
            }

            public static void LoadPointGuide2(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointGuide2.State.Idle, 
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide2.Id,
                    Name = "Очікування", 
                    StateNo = 1
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointGuide2.State.BindLoadPoint,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide2.Id,
                    Name = "Призначення точки завантаження",
                    StateNo = 2
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.LoadPointGuide2.State.AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide2.Id,
                    Name = "Підпис операції призначення точки завантаження",
                    StateNo = 3
                });
                context.SaveChanges();
            }

            public static void MixedFeedManage(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedManage.State.Workstation,
                    OpRoutineId = Dom.OpRoutine.MixedFeedManage.Id,
                    Name = "Показники силосів",
                    StateNo = 1
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedManage.State.Edit,
                    OpRoutineId = Dom.OpRoutine.MixedFeedManage.Id,
                    Name = "Редагування показників",
                    StateNo = 2
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedManage.State.AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedManage.Id,
                    Name = "Підпис операції редагування показників",
                    StateNo = 3
                });
                context.SaveChanges();
            }

            public static void MixedFeedLoad(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = "Робоча станція",
                    StateNo = 1
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.State.Idle, OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id, Name = "Очікування", StateNo = 2
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.State.AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = "Підпис операції",
                    StateNo = 3
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.State.Cleanup, OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id, Name = "Очистка", StateNo = 4
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.State.AddCleanupVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = "Підпис очистки",
                    StateNo = 5
                });
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = "Підпис операції деактивації вузла",
                    StateNo = 6
                });
                context.SaveChanges();
            }

            public static void MixedFeedGuide(GravitasDbContext context)
            {
                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedGuide.State.Idle, OpRoutineId = Dom.OpRoutine.MixedFeedGuide.Id, Name = "Очікування", StateNo = 1
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedGuide.State.BindLoadPoint,
                    OpRoutineId = Dom.OpRoutine.MixedFeedGuide.Id,
                    Name = "Призначення проїзду завантаження",
                    StateNo = 2
                });

                context.Set<Model.DomainModel.OpRoutine.DAO.OpRoutineState>().AddOrUpdate(new Model.DomainModel.OpRoutine.DAO.OpRoutineState
                {
                    Id = Dom.OpRoutine.MixedFeedGuide.State.AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedGuide.Id,
                    Name = "Підпис операції призначення проїзду завантаження",
                    StateNo = 3
                });
                context.SaveChanges();
            }
        }
    }
}