using System.Data.Entity.Migrations;
using Gravitas.Model;

namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {
        public static class OpRoutineTransition
        {
            public static void SingleWindow(GravitasDbContext context)
            {
                // Common
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.Idle__GetTicket_Core,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.Idle__GetTicket_Core),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.Idle__GetTicket_Web,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.Idle__GetTicket_Web),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });


                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.GetTicket__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.GetTicket__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.GetTicket__ContainerCloseAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.GetTicket__ContainerCloseAddOpVisa),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ContainerCloseAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.GetTicket__Idle,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.GetTicket__Idle),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__GetTicket,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__GetTicket),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__Idle,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__Idle),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ContainerCloseAddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ContainerCloseAddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ContainerCloseAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ContainerCloseAddOpVisa__GetTicket,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ContainerCloseAddOpVisa__GetTicket),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ContainerCloseAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ContainerCloseAddOpVisa__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ContainerCloseAddOpVisa__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ContainerCloseAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                }); 
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__SupplyChangeAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__SupplyChangeAddOpVisa),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.SupplyChangeAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.SupplyChangeAddOpVisa__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.SupplyChangeAddOpVisa__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.SupplyChangeAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.DivideTicketAddOpVisa__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.DivideTicketAddOpVisa__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.DivideTicketAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__DivideTicketAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__DivideTicketAddOpVisa),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.DivideTicketAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });    
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.GetTicket__DeleteTicketAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.GetTicket__DeleteTicketAddOpVisa),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.DeleteTicketAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.DeleteTicketAddOpVisa__GetTicket,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.DeleteTicketAddOpVisa__GetTicket),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.DeleteTicketAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                }); 
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.DeleteTicketAddOpVisa__GetTicketCore,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.DeleteTicketAddOpVisa__GetTicketCore),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.DeleteTicketAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                }); 

                //Edit branch
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__EditTicketForm,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__EditTicketForm),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.EditTicketForm,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.EditTicketForm__EditAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.EditTicketForm__EditAddOpVisa),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.EditTicketForm,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.EditAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.EditAddOpVisa__EditPostApiData,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.EditAddOpVisa__EditPostApiData),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.EditAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.EditPostApiData,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.EditPostApiData__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.EditPostApiData__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.EditPostApiData,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.EditTicketForm__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.EditTicketForm__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.EditTicketForm,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.GetTicket__EditGetApiData,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.GetTicket__EditGetApiData),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.GetTicket,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.EditGetApiData,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__EditGetApiData,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__EditGetApiData),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.EditGetApiData,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.EditGetApiData__EditTicketForm,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.EditGetApiData__EditTicketForm),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.EditGetApiData,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.EditTicketForm,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.EditGetApiData__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.EditGetApiData__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.EditGetApiData,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.EditPostApiData__EditTicketForm,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.EditPostApiData__EditTicketForm),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.EditPostApiData,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.EditTicketForm,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.EditAddOpVisa__EditTicketForm,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.EditAddOpVisa__EditTicketForm),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.EditAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.EditTicketForm,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                //Route branch
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__RouteEditData,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__RouteEditData),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.RouteEditData,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.RouteEditData__RouteAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.RouteEditData__RouteAddOpVisa),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.RouteEditData,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.RouteAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.RouteAddOpVisa__RouteEditData,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.RouteAddOpVisa__RouteEditData),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.RouteAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.RouteEditData,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.RouteAddOpVisa__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.RouteAddOpVisa__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.RouteAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.RouteEditData__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.RouteEditData__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.RouteEditData,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                //Close branch
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__CloseAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ShowTicketMenu__CloseAddOpVisa),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.CloseAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.CloseAddOpVisa__ClosePostApiData,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.CloseAddOpVisa__ClosePostApiData),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.CloseAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ClosePostApiData,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.ClosePostApiData__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.ClosePostApiData__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.ClosePostApiData,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SingleWindow.Transition.CloseAddOpVisa__ShowTicketMenu,
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    Name = nameof(Dom.OpRoutine.SingleWindow.Transition.CloseAddOpVisa__ShowTicketMenu),
                    StartStateId = Dom.OpRoutine.SingleWindow.State.CloseAddOpVisa,
                    StopStateId = Dom.OpRoutine.SingleWindow.State.ShowTicketMenu,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.SaveChanges();
            }

            public static void SecurityIn(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.Idle__BindLongRangeRfid,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.Idle__BindLongRangeRfid),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.Idle,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.BindLongRangeRfid,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.BindLongRangeRfid__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.BindLongRangeRfid__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.BindLongRangeRfid,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.AddOperationVisa__OpenBarrier,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.AddOperationVisa__OpenBarrier),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.OpenBarrier,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.OpenBarrier__GetCamSnapshot,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.OpenBarrier__GetCamSnapshot),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.OpenBarrier,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.GetCamSnapshot,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.GetCamSnapshot__Idle,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.GetCamSnapshot__Idle),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.GetCamSnapshot,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.Idle__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.Idle__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.Idle,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });   
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.Idle__CheckOwnTransport,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.Idle__CheckOwnTransport),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.Idle,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.CheckOwnTransport,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.CheckOwnTransport__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.CheckOwnTransport__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.CheckOwnTransport,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.CheckOwnTransport__Idle,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.CheckOwnTransport__Idle),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.CheckOwnTransport,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });        
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.BindLongRangeRfid__Idle,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.BindLongRangeRfid__Idle),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.BindLongRangeRfid,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });     
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityIn.Transition.AddOperationVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    Name = nameof(Dom.OpRoutine.SecurityIn.Transition.AddOperationVisa__Idle),
                    StartStateId = Dom.OpRoutine.SecurityIn.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.SecurityIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                
                context.SaveChanges();
            }

            public static void SecurityOut(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.Idle__ShowOperationsList,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.Idle__ShowOperationsList),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.Idle,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.ShowOperationsList,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.ShowOperationsList__EditStampList,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.ShowOperationsList__EditStampList),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.ShowOperationsList,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.EditStampList,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.EditStampList__AddRouteControlVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.EditStampList__AddRouteControlVisa),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.EditStampList,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.AddRouteControlVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.AddRouteControlVisa__AddTransportInspectionVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.AddRouteControlVisa__AddTransportInspectionVisa),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.AddRouteControlVisa,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.AddTransportInspectionVisa,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.AddTransportInspectionVisa__OpenBarrier,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.AddTransportInspectionVisa__OpenBarrier),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.AddTransportInspectionVisa,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.OpenBarrier,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.OpenBarrier__GetCamSnapshot,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.OpenBarrier__GetCamSnapshot),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.OpenBarrier,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.GetCamSnapshot,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.GetCamSnapshot__Idle,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.GetCamSnapshot__Idle),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.GetCamSnapshot,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.EditStampList__ShowOperationsList,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.EditStampList__ShowOperationsList),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.EditStampList,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.ShowOperationsList,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });     
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.Idle__CheckOwnTransport,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.Idle__CheckOwnTransport),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.Idle,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.CheckOwnTransport,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });           
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.CheckOwnTransport__AddRouteControlVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.CheckOwnTransport__AddRouteControlVisa),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.CheckOwnTransport,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.AddRouteControlVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });         
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.CheckOwnTransport__Idle,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.CheckOwnTransport__Idle),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.CheckOwnTransport,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });  
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityOut.Transition.Idle__AddTransportInspectionVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    Name = nameof(Dom.OpRoutine.SecurityOut.Transition.Idle__AddTransportInspectionVisa),
                    StartStateId = Dom.OpRoutine.SecurityOut.State.Idle,
                    StopStateId = Dom.OpRoutine.SecurityOut.State.AddTransportInspectionVisa,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.SaveChanges();
            }

            public static void SecurityReview(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityReview.Transition.Idle__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.SecurityReview.Id,
                    Name = nameof(Dom.OpRoutine.SecurityReview.Transition.Idle__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.SecurityReview.State.Idle,
                    StopStateId = Dom.OpRoutine.SecurityReview.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityReview.Transition.AddOperationVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.SecurityReview.Id,
                    Name = nameof(Dom.OpRoutine.SecurityReview.Transition.AddOperationVisa__Idle),
                    StartStateId = Dom.OpRoutine.SecurityReview.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.SecurityReview.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.SecurityReview.Transition.AddOperationVisa__Idle__Core,
                    OpRoutineId = Dom.OpRoutine.SecurityReview.Id,
                    Name = nameof(Dom.OpRoutine.SecurityReview.Transition.AddOperationVisa__Idle__Core),
                    StartStateId = Dom.OpRoutine.SecurityReview.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.SecurityReview.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.SaveChanges();
            }

            public static void CentralLabolatorySamples(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratorySamples.Transition.Idle__CentralLabolatorySampleBindTray,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratorySamples.Transition.Idle__CentralLabolatorySampleBindTray),
                    StartStateId = Dom.OpRoutine.CentralLaboratorySamples.State.Idle,
                    StopStateId = Dom.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleBindTray,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratorySamples.Transition.CentralLabSampleBindTray__CentralLabSampleAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratorySamples.Transition.CentralLabSampleBindTray__CentralLabSampleAddOpVisa),
                    StartStateId = Dom.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleBindTray,
                    StopStateId = Dom.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratorySamples.Transition.CentralLabSampleAddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratorySamples.Transition.CentralLabSampleAddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.CentralLaboratorySamples.State.CentralLabSampleAddOpVisa,
                    StopStateId = Dom.OpRoutine.CentralLaboratorySamples.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.SaveChanges();
            }

            public static void CentralLabolatoryProcess(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.Idle__PrintDataDisclose,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.Idle__PrintDataDisclose),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDataDisclose__Idle,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDataDisclose__Idle),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDataDisclose__PrintAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDataDisclose__PrintAddOpVisa),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDataDisclose__PrintCollisionInitVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDataDisclose__PrintCollisionInitVisa),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.Idle_AddSample,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.Idle_AddSample),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.AddSample,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.AddSample_Idle,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.AddSample_Idle),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.AddSample,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionInit_PrintDataDisclose,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionInit_PrintDataDisclose),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDocument_Idle,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDocument_Idle),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDocument,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.AddSample_AddSampleVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.AddSample_AddSampleVisa),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.AddSample,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintLabel_Idle,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintLabel_Idle),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintLabel,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.AddSampleVisa__PrintLabel,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.AddSampleVisa__PrintLabel),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintLabel,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintAddOpVisa_PrintDocument,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintAddOpVisa_PrintDocument),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDocument,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDataDisclose__PrintCollisionStartVisa,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintDataDisclose__PrintCollisionStartVisa),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionStartVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionStartVisa__Idle),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionInitVisa__PrintCollisionInit,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionInitVisa__PrintCollisionInit),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionInit__Idle,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionInit__Idle),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionInitVisa__PrintDataDisclose,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionInitVisa__PrintDataDisclose),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                }); 
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintAddOpVisa__PrintDataDisclose,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintAddOpVisa__PrintDataDisclose),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionStartVisa__PrintDataDisclose,
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    Name = nameof(Dom.OpRoutine.CentralLaboratoryProcess.Transition.PrintCollisionStartVisa__PrintDataDisclose),
                    StartStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa,
                    StopStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.SaveChanges();
            }

            public static void LaboratoryIn(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.Idle__SampleReadTruckRfid,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.Idle__SampleReadTruckRfid),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.SampleReadTruckRfid,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.SampleReadTruckRfid__SampleBindTray,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.SampleReadTruckRfid__SampleBindTray),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.SampleReadTruckRfid,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.SampleBindTray,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.SampleBindTray__SampleBindAnalysisTray,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.SampleBindTray__SampleBindAnalysisTray),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.SampleBindTray,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.SampleBindAnalysisTray,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.SampleBindAnalysisTray__SampleAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.SampleBindAnalysisTray__SampleAddOpVisa),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.SampleBindAnalysisTray,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.SampleAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.SampleAddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.SampleAddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.SampleAddOpVisa,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.Idle__PrintCollisionManage,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.Idle__PrintCollisionManage),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionManage,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionInit__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionInit__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionInit,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionManage__PrintAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionManage__PrintAddOpVisa),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionManage,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionInit__PrintDataDisclose,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionInit__PrintDataDisclose),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionInit,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintDataDisclose,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionManage__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionManage__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionManage,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionManage__SampleReadTruckRfid,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintCollisionManage__SampleReadTruckRfid),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionManage,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.SampleReadTruckRfid,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintAddOpVisa__PrintLaboratoryProtocol,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintAddOpVisa__PrintLaboratoryProtocol),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintLaboratoryProtocol,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintDataDisclose__PrintLaboratoryProtocol,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintDataDisclose__PrintLaboratoryProtocol),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintDataDisclose,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintLaboratoryProtocol,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintLaboratoryProtocol__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintLaboratoryProtocol__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintLaboratoryProtocol,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.Idle__ResultReadTrayRfid,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.Idle__ResultReadTrayRfid),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.ResultReadTrayRfid,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.ResultReadTrayRfid__ResultEditAnalysis,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.ResultReadTrayRfid__ResultEditAnalysis),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.ResultReadTrayRfid,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.ResultEditAnalysis,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.ResultEditAnalysis__ResultAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.ResultEditAnalysis__ResultAddOpVisa),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.ResultEditAnalysis,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.ResultAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.ResultAddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.ResultAddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.ResultAddOpVisa,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.Idle__PrintReadTrayRfid,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.Idle__PrintReadTrayRfid),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintReadTrayRfid,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintReadTrayRfid__PrintAnalysisResults,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintReadTrayRfid__PrintAnalysisResults),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintReadTrayRfid,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisResults,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintAnalysisResults__PrintAnalysisAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintAnalysisResults__PrintAnalysisAddOpVisa),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisResults,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintAnalysisAddOpVisa__PrintDataDisclose,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintAnalysisAddOpVisa__PrintDataDisclose),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisAddOpVisa,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintDataDisclose,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintDataDisclose__PrintCollisionInit,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintDataDisclose__PrintCollisionInit),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintDataDisclose,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionInit,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintDataDisclose__PrintAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintDataDisclose__PrintAddOpVisa),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintDataDisclose,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintAddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintAddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });


                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.SampleReadTruckRfid__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.SampleReadTruckRfid__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.SampleReadTruckRfid,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.SampleBindTray__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.SampleBindTray__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.SampleBindTray,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.ResultReadTrayRfid__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.ResultReadTrayRfid__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.ResultReadTrayRfid,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintReadTrayRfid__Idle,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintReadTrayRfid__Idle),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintReadTrayRfid,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.PrintAnalysisAddOpVisa__PrintAnalysisResults,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.PrintAnalysisAddOpVisa__PrintAnalysisResults),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisResults,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LabolatoryIn.Transition.Idle_PrintCollisionInit,
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    Name = nameof(Dom.OpRoutine.LabolatoryIn.Transition.Idle_PrintCollisionInit),
                    StartStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                    StopStateId = Dom.OpRoutine.LabolatoryIn.State.PrintCollisionInit,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.SaveChanges();
            }

            public static void LabolatoryOut(GravitasDbContext context)
            {
                //context.SaveChanges();
            }

            public static void WeightbridgeTare(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.Idle__GetScaleZero,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.Idle__GetScaleZero),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GetScaleZero,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetScaleZero__OpenBarrierIn,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetScaleZero__OpenBarrierIn),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetScaleZero,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.OpenBarrierIn,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.OpenBarrierIn__CheckScaleNotEmpty,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.OpenBarrierIn__CheckScaleNotEmpty),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.OpenBarrierIn,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.CheckScaleNotEmpty,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.CheckScaleNotEmpty__GetTicketCard,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.CheckScaleNotEmpty__GetTicketCard),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.CheckScaleNotEmpty,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GetTicketCard,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTicketCard__DriverTrailerEnableCheck,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTicketCard__DriverTrailerEnableCheck),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTicketCard,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.DriverTrailerEnableCheck,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTicketCard__GuardianCardPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTicketCard__GuardianCardPrompt),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTicketCard,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GuardianCardPrompt,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTicketCard__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTicketCard__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTicketCard,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.DriverTrailerEnableCheck__GuardianCardPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.DriverTrailerEnableCheck__GuardianCardPrompt),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.DriverTrailerEnableCheck,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GuardianCardPrompt,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.DriverTrailerEnableCheck__TruckWeightPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.DriverTrailerEnableCheck__TruckWeightPrompt),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.DriverTrailerEnableCheck,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GuardianCardPrompt__GuardianTruckVerification,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GuardianCardPrompt__GuardianTruckVerification),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GuardianCardPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GuardianTruckVerification,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GuardianTruckVerification__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GuardianTruckVerification__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GuardianTruckVerification,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GuardianTruckVerification__Idle_Core,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GuardianTruckVerification__Idle_Core),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GuardianTruckVerification,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GuardianTrailerEnableCheck__TruckWeightPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GuardianTrailerEnableCheck__TruckWeightPrompt),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GuardianTrailerEnableCheck,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TruckWeightPrompt__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.TruckWeightPrompt__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTruckWeight__TrailerWeightPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTruckWeight__TrailerWeightPrompt),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTruckWeight,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.TrailerWeightPrompt,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTruckWeight__WeightResultsValidation,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTruckWeight__WeightResultsValidation),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTruckWeight,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.WeightResultsValidation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TrailerWeightPrompt__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.TrailerWeightPrompt__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TrailerWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TruckWeightPrompt__GetTruckWeight,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.TruckWeightPrompt__GetTruckWeight),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GetTruckWeight,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TrailerWeightPrompt__GetTrailerWeight,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.TrailerWeightPrompt__GetTrailerWeight),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TrailerWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GetTrailerWeight,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetGuardianTruckWeightPermission__GetTruckWeight,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .GetGuardianTruckWeightPermission__GetTruckWeight),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetGuardianTruckWeightPermission,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GetTruckWeight,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTrailerWeight__WeightResultsValidation,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTrailerWeight__WeightResultsValidation),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTrailerWeight,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.WeightResultsValidation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.WeightResultsValidation__OpenBarrierOut,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.WeightResultsValidation__OpenBarrierOut),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.WeightResultsValidation,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.OpenBarrierOut,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.WeightResultsValidation__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.WeightResultsValidation__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.WeightResultsValidation,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.OpenBarrierOut__CheckScaleEmpty,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.OpenBarrierOut__CheckScaleEmpty),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.OpenBarrierOut,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.CheckScaleEmpty,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.CheckScaleEmpty__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition.CheckScaleEmpty__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.CheckScaleEmpty,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TrailerWeightPrompt__GetGuardianTrailerWeightPermission,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .TrailerWeightPrompt__GetGuardianTrailerWeightPermission),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TrailerWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GetGuardianTrailerWeightPermission,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TruckWeightPrompt__GetGuardianTruckWeightPermission,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .TruckWeightPrompt__GetGuardianTruckWeightPermission),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GetGuardianTruckWeightPermission,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GuardianTruckVerification__GuardianTrailerEnableCheck,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .GuardianTruckVerification__GuardianTrailerEnableCheck),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GuardianTruckVerification,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GuardianTrailerEnableCheck,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TruckWeightPrompt__OpenBarrierOut,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .TruckWeightPrompt__OpenBarrierOut),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.OpenBarrierOut,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TrailerWeightPrompt__OpenBarrierOut,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .TrailerWeightPrompt__OpenBarrierOut),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TrailerWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.OpenBarrierOut,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GuardianTruckVerification__TruckWeightPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .GuardianTruckVerification__TruckWeightPrompt),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GuardianTruckVerification,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetGuardianTrailerWeightPermission__GetTrailerWeight,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .GetGuardianTrailerWeightPermission__GetTrailerWeight),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetGuardianTrailerWeightPermission,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GetTrailerWeight,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GuardianTruckVerification__GetTruckWeight,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .GuardianTruckVerification__GetTruckWeight),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GuardianTruckVerification,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.GetTruckWeight,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTicketCard__TruckWeightPrompt,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                                     .GetTicketCard__TruckWeightPrompt),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTicketCard,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTicketCard__Idle_WebUi,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                        .GetTicketCard__Idle_WebUi),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTicketCard,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.CheckScaleNotEmpty__Idle_WebUi,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                        .CheckScaleNotEmpty__Idle_WebUi),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.CheckScaleNotEmpty,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.CheckScaleNotEmpty__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                        .CheckScaleNotEmpty__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.CheckScaleNotEmpty,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.DriverTrailerEnableCheck__Idle_Core,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                        .DriverTrailerEnableCheck__Idle_Core),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.DriverTrailerEnableCheck,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TruckWeightPrompt__Idle_Core,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                        .TruckWeightPrompt__Idle_Core),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TruckWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.TrailerWeightPrompt__Idle_Core,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    Name = nameof(Dom.OpRoutine.Weighbridge.Transition
                        .TrailerWeightPrompt__Idle_Core),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.TrailerWeightPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTruckWeight__OpenBarrierOut,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id, Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTruckWeight__OpenBarrierOut),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTruckWeight,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.OpenBarrierOut,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTrailerWeight__OpenBarrierOut,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id, Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTrailerWeight__OpenBarrierOut),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTrailerWeight,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.OpenBarrierOut,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });  
                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.DriverTrailerEnableCheck__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id, Name = nameof(Dom.OpRoutine.Weighbridge.Transition.DriverTrailerEnableCheck__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.DriverTrailerEnableCheck,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GuardianCardPrompt__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id, Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GuardianCardPrompt__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GuardianCardPrompt,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetGuardianTruckWeightPermission__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id, Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetGuardianTruckWeightPermission__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetGuardianTruckWeightPermission,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTruckWeight__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id, Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTruckWeight__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTruckWeight,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetGuardianTrailerWeightPermission__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id, Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetGuardianTrailerWeightPermission__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetGuardianTrailerWeightPermission,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.Weighbridge.Transition.GetTrailerWeight__Idle,
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id, Name = nameof(Dom.OpRoutine.Weighbridge.Transition.GetTrailerWeight__Idle),
                    StartStateId = Dom.OpRoutine.Weighbridge.State.GetTrailerWeight,
                    StopStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.SaveChanges();
            }

            public static void UnloadPointGuide(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.Transition.Idle__BindUnloadPoint,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide.Transition.Idle__BindUnloadPoint),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide.State.BindUnloadPoint,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.Transition.BindUnloadPoint__AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide.Transition.BindUnloadPoint__AddOpVisa),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide.State.BindUnloadPoint,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide.State.AddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.Transition.AddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide.Transition.AddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide.State.AddOpVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.Transition.BindUnloadPoint__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide.Transition.BindUnloadPoint__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide.State.BindUnloadPoint,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.Transition.EntryAddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide.Transition.EntryAddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide.State.EntryAddOpVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.Transition.EntryAddOpVisa__IdleCore,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide.Transition.EntryAddOpVisa__IdleCore),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide.State.EntryAddOpVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide.Transition.Idle__EntryAddOpVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide.Transition.Idle__EntryAddOpVisa),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide.State.EntryAddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.SaveChanges();
                
            }public static void UnloadPointGuide2(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide2.Transition.Idle__BindUnloadPoint,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide2.Transition.Idle__BindUnloadPoint),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide2.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide2.State.BindUnloadPoint,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide2.Transition.BindUnloadPoint__AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide2.Transition.BindUnloadPoint__AddOpVisa),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide2.State.BindUnloadPoint,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide2.State.AddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide2.Transition.AddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide2.Transition.AddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide2.State.AddOpVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointGuide2.Transition.BindUnloadPoint__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointGuide2.Transition.BindUnloadPoint__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointGuide2.State.BindUnloadPoint,
                    StopStateId = Dom.OpRoutine.UnloadPointGuide2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.SaveChanges();
            }

            public static void UnloadPointType1(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.Workstation__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.Workstation__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.Workstation,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.Workstation__Idle__Core,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.Workstation__Idle__Core),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.Workstation,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.Idle__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.Idle__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.AddOperationVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.AddOperationVisa__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.Idle__Workstation,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.Idle__Workstation),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.AddOperationVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.AddOperationVisa__Workstation),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });        
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.Idle__AddChangeStateVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.Idle__AddChangeStateVisa),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.AddChangeStateVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.AddChangeStateVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.AddChangeStateVisa__Workstation),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.AddChangeStateVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                }); 
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.AddChangeStateVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.AddChangeStateVisa__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.AddChangeStateVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });      
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.GetTareValue__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.GetTareValue__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.GetTareValue,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });       
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType1.Transition.Idle__GetTareValue,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType1.Transition.Idle__GetTareValue),
                    StartStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointType1.State.GetTareValue,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.SaveChanges();
            }
            
            public static void UnloadPointType2(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.Workstation__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.Workstation__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.Workstation,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.Workstation__Idle__Core,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.Workstation__Idle__Core),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.Workstation,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.Idle__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.Idle__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.AddOperationVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.AddOperationVisa__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.AddOperationVisa__Idle__Core,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.AddOperationVisa__Idle__Core),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.AddOperationVisa__SelectAcceptancePoint,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.AddOperationVisa__SelectAcceptancePoint),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.SelectAcceptancePoint,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.Idle__AddChangeStateVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.Idle__AddChangeStateVisa),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.AddChangeStateVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.AddChangeStateVisa__Idle__Core,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.AddChangeStateVisa__Idle__Core),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.AddChangeStateVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.AddChangeStateVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.AddChangeStateVisa__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.AddChangeStateVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.SelectAcceptancePoint__Idle,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.SelectAcceptancePoint__Idle),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.SelectAcceptancePoint,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.SelectAcceptancePoint__Idle__Core,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.SelectAcceptancePoint__Idle__Core),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.SelectAcceptancePoint,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.Idle__SelectAcceptancePoint,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.Idle__SelectAcceptancePoint),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.SelectAcceptancePoint,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.SelectAcceptancePoint__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.SelectAcceptancePoint__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.SelectAcceptancePoint,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.Idle__Workstation,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.Idle__Workstation),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.Idle__Workstation__Core,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.Idle__Workstation__Core),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.Idle,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.AddOperationVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.AddOperationVisa__Workstation),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.UnloadPointType2.Transition.AddChangeStateVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    Name = nameof(Dom.OpRoutine.UnloadPointType2.Transition.AddChangeStateVisa__Workstation),
                    StartStateId = Dom.OpRoutine.UnloadPointType2.State.AddChangeStateVisa,
                    StopStateId = Dom.OpRoutine.UnloadPointType2.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });

                context.SaveChanges();
            }

            public static void LoadCheckPoint(GravitasDbContext context)
            {
//                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
//                {
//                    Id = Dom.OpRoutine.LoadCheckPoint.Transition.Idle__GetTareValue,
//                    OpRoutineId = Dom.OpRoutine.LoadCheckPoint.Id,
//                    Name = nameof(Dom.OpRoutine.LoadCheckPoint.Transition.Idle__GetTareValue),
//                    StartStateId = Dom.OpRoutine.LoadCheckPoint.State.Idle,
//                    StopStateId = Dom.OpRoutine.LoadCheckPoint.State.GetTareValue,
//                    ProcessorId = Dom.OpRoutine.Processor.CoreService
//                });
//                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
//                {
//                    Id = Dom.OpRoutine.LoadCheckPoint.Transition.GetTareValue__AddOperationVisa,
//                    OpRoutineId = Dom.OpRoutine.LoadCheckPoint.Id,
//                    Name = nameof(Dom.OpRoutine.LoadCheckPoint.Transition.GetTareValue__AddOperationVisa),
//                    StartStateId = Dom.OpRoutine.LoadCheckPoint.State.GetTareValue,
//                    StopStateId = Dom.OpRoutine.LoadCheckPoint.State.AddOperationVisa,
//                    ProcessorId = Dom.OpRoutine.Processor.WebUI
//                });
//                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
//                {
//                    Id = Dom.OpRoutine.LoadCheckPoint.Transition.AddOperationVisa__GetTareValue,
//                    OpRoutineId = Dom.OpRoutine.LoadCheckPoint.Id,
//                    Name = nameof(Dom.OpRoutine.LoadCheckPoint.Transition.AddOperationVisa__GetTareValue),
//                    StartStateId = Dom.OpRoutine.LoadCheckPoint.State.AddOperationVisa,
//                    StopStateId = Dom.OpRoutine.LoadCheckPoint.State.GetTareValue,
//                    ProcessorId = Dom.OpRoutine.Processor.WebUI
//                });
//                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
//                {
//                    Id = Dom.OpRoutine.LoadCheckPoint.Transition.AddOperationVisa__Idle,
//                    OpRoutineId = Dom.OpRoutine.LoadCheckPoint.Id,
//                    Name = nameof(Dom.OpRoutine.LoadCheckPoint.Transition.AddOperationVisa__Idle),
//                    StartStateId = Dom.OpRoutine.LoadCheckPoint.State.AddOperationVisa,
//                    StopStateId = Dom.OpRoutine.LoadCheckPoint.State.Idle,
//                    ProcessorId = Dom.OpRoutine.Processor.CoreService
//                });

                context.SaveChanges();
            }
            
            public static void UnloadCheckPoint(GravitasDbContext context)
            {
//                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
//                {
//                    Id = Dom.OpRoutine.UnloadCheckPoint.Transition.Idle__GetTareValue,
//                    OpRoutineId = Dom.OpRoutine.UnloadCheckPoint.Id,
//                    Name = nameof(Dom.OpRoutine.UnloadCheckPoint.Transition.Idle__GetTareValue),
//                    StartStateId = Dom.OpRoutine.UnloadCheckPoint.State.Idle,
//                    StopStateId = Dom.OpRoutine.UnloadCheckPoint.State.GetTareValue,
//                    ProcessorId = Dom.OpRoutine.Processor.CoreService
//                });
//                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
//                {
//                    Id = Dom.OpRoutine.UnloadCheckPoint.Transition.GetTareValue__AddOperationVisa,
//                    OpRoutineId = Dom.OpRoutine.UnloadCheckPoint.Id,
//                    Name = nameof(Dom.OpRoutine.UnloadCheckPoint.Transition.GetTareValue__AddOperationVisa),
//                    StartStateId = Dom.OpRoutine.UnloadCheckPoint.State.GetTareValue,
//                    StopStateId = Dom.OpRoutine.UnloadCheckPoint.State.AddOperationVisa,
//                    ProcessorId = Dom.OpRoutine.Processor.WebUI
//                });
//                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
//                {
//                    Id = Dom.OpRoutine.UnloadCheckPoint.Transition.AddOperationVisa__GetTareValue,
//                    OpRoutineId = Dom.OpRoutine.UnloadCheckPoint.Id,
//                    Name = nameof(Dom.OpRoutine.UnloadCheckPoint.Transition.AddOperationVisa__GetTareValue),
//                    StartStateId = Dom.OpRoutine.UnloadCheckPoint.State.AddOperationVisa,
//                    StopStateId = Dom.OpRoutine.UnloadCheckPoint.State.GetTareValue,
//                    ProcessorId = Dom.OpRoutine.Processor.WebUI
//                });
//                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
//                {
//                    Id = Dom.OpRoutine.UnloadCheckPoint.Transition.AddOperationVisa__Idle,
//                    OpRoutineId = Dom.OpRoutine.UnloadCheckPoint.Id,
//                    Name = nameof(Dom.OpRoutine.UnloadCheckPoint.Transition.AddOperationVisa__Idle),
//                    StartStateId = Dom.OpRoutine.UnloadCheckPoint.State.AddOperationVisa,
//                    StopStateId = Dom.OpRoutine.UnloadCheckPoint.State.Idle,
//                    ProcessorId = Dom.OpRoutine.Processor.CoreService
//                });

                context.SaveChanges();
            }
            
            public static void LoadPointType1(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.Workstation__Idle,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.Workstation__Idle),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.Workstation,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.Workstation__Idle__Core,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.Workstation__Idle__Core),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.Workstation,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.Idle__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.Idle__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.AddOperationVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.AddOperationVisa__Idle),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.Idle__Workstation,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.Idle__Workstation),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.AddOperationVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.AddOperationVisa__Workstation),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });     
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.Idle__AddChangeStateVisa,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.Idle__AddChangeStateVisa),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.AddChangeStateVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });          
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.AddChangeStateVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.AddChangeStateVisa__Workstation),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.AddChangeStateVisa,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.AddChangeStateVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.AddChangeStateVisa__Idle),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.AddChangeStateVisa,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });  
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.Idle__GetTareValue,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.Idle__GetTareValue),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.GetTareValue,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });    
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointType1.Transition.GetTareValue__Idle,
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointType1.Transition.GetTareValue__Idle),
                    StartStateId = Dom.OpRoutine.LoadPointType1.State.GetTareValue,
                    StopStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.SaveChanges();
            }

            public static void LoadPointGuide(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide.Transition.Idle__BindUnloadPoint,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide.Transition.Idle__BindUnloadPoint),
                    StartStateId = Dom.OpRoutine.LoadPointGuide.State.Idle,
                    StopStateId = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide.Transition.BindUnloadPoint__AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide.Transition.BindUnloadPoint__AddOpVisa),
                    StartStateId = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint,
                    StopStateId = Dom.OpRoutine.LoadPointGuide.State.AddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide.Transition.AddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide.Transition.AddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.LoadPointGuide.State.AddOpVisa,
                    StopStateId = Dom.OpRoutine.LoadPointGuide.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide.Transition.BindLoadPoint__Idle,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide.Transition.BindLoadPoint__Idle),
                    StartStateId = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint,
                    StopStateId = Dom.OpRoutine.LoadPointGuide.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                }); 
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide.Transition.AddOpVisa__BindLoadPoint,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide.Transition.AddOpVisa__BindLoadPoint),
                    StartStateId = Dom.OpRoutine.LoadPointGuide.State.AddOpVisa,
                    StopStateId = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.SaveChanges();
            }
            
            public static void LoadPointGuide2(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide2.Transition.Idle__BindUnloadPoint,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide2.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide2.Transition.Idle__BindUnloadPoint),
                    StartStateId = Dom.OpRoutine.LoadPointGuide2.State.Idle,
                    StopStateId = Dom.OpRoutine.LoadPointGuide2.State.BindLoadPoint,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide2.Transition.BindUnloadPoint__AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide2.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide2.Transition.BindUnloadPoint__AddOpVisa),
                    StartStateId = Dom.OpRoutine.LoadPointGuide2.State.BindLoadPoint,
                    StopStateId = Dom.OpRoutine.LoadPointGuide2.State.AddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide2.Transition.AddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide2.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide2.Transition.AddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.LoadPointGuide2.State.AddOpVisa,
                    StopStateId = Dom.OpRoutine.LoadPointGuide2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide2.Transition.BindLoadPoint__Idle,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide2.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide2.Transition.BindLoadPoint__Idle),
                    StartStateId = Dom.OpRoutine.LoadPointGuide2.State.BindLoadPoint,
                    StopStateId = Dom.OpRoutine.LoadPointGuide2.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                }); 
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.LoadPointGuide2.Transition.AddOpVisa__BindLoadPoint,
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide2.Id,
                    Name = nameof(Dom.OpRoutine.LoadPointGuide2.Transition.AddOpVisa__BindLoadPoint),
                    StartStateId = Dom.OpRoutine.LoadPointGuide2.State.AddOpVisa,
                    StopStateId = Dom.OpRoutine.LoadPointGuide2.State.BindLoadPoint,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.SaveChanges();
            }
            
            public static void MixedFeedManage(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedManage.Transition.Edit__Workstation,
                    OpRoutineId = Dom.OpRoutine.MixedFeedManage.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedManage.Transition.Edit__Workstation),
                    StartStateId = Dom.OpRoutine.MixedFeedManage.State.Edit,
                    StopStateId = Dom.OpRoutine.MixedFeedManage.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedManage.Transition.Workstation__Edit,
                    OpRoutineId = Dom.OpRoutine.MixedFeedManage.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedManage.Transition.Workstation__Edit),
                    StartStateId = Dom.OpRoutine.MixedFeedManage.State.Workstation,
                    StopStateId = Dom.OpRoutine.MixedFeedManage.State.Edit,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedManage.Transition.AddOperationVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.MixedFeedManage.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedManage.Transition.AddOperationVisa__Workstation),
                    StartStateId = Dom.OpRoutine.MixedFeedManage.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.MixedFeedManage.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedManage.Transition.Edit__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedManage.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedManage.Transition.Edit__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.MixedFeedManage.State.Edit,
                    StopStateId = Dom.OpRoutine.MixedFeedManage.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.SaveChanges();
            }
            
            public static void MixedFeedLoad(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Workstation__Idle,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Workstation__Idle),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Workstation__Idle__Core,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Workstation__Idle__Core),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Idle__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Idle__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Idle__AddOperationVisaCore,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Idle__AddOperationVisaCore),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.AddOperationVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.AddOperationVisa__Idle),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Idle__Workstation,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Idle__Workstation),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.AddOperationVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.AddOperationVisa__Workstation),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.AddOperationVisa,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Workstation__Cleanup,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Workstation__Cleanup),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Cleanup,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Cleanup__AddCleanupVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Cleanup__AddCleanupVisa),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Cleanup,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.AddCleanupVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Cleanup__Workstation,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Cleanup__Workstation),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Cleanup,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.AddCleanupVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.AddCleanupVisa__Workstation),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.AddCleanupVisa,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Workstation__AddOperationVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Workstation__AddOperationVisa),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.AddOperationVisa,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                }); 
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.Idle__AddChangeStateVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.Idle__AddChangeStateVisa),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                }); 
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.AddChangeStateVisa__Workstation,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.AddChangeStateVisa__Workstation),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });                
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedLoad.Transition.AddChangeStateVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedLoad.Transition.AddChangeStateVisa__Idle),
                    StartStateId = Dom.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa,
                    StopStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });

                context.SaveChanges();
            }

            public static void MixedFeedGuide(GravitasDbContext context)
            {
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedGuide.Transition.Idle__BindUnloadPoint,
                    OpRoutineId = Dom.OpRoutine.MixedFeedGuide.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedGuide.Transition.Idle__BindUnloadPoint),
                    StartStateId = Dom.OpRoutine.MixedFeedGuide.State.Idle,
                    StopStateId = Dom.OpRoutine.MixedFeedGuide.State.BindLoadPoint,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedGuide.Transition.BindUnloadPoint__AddOpVisa,
                    OpRoutineId = Dom.OpRoutine.MixedFeedGuide.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedGuide.Transition.BindUnloadPoint__AddOpVisa),
                    StartStateId = Dom.OpRoutine.MixedFeedGuide.State.BindLoadPoint,
                    StopStateId = Dom.OpRoutine.MixedFeedGuide.State.AddOpVisa,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedGuide.Transition.AddOpVisa__Idle,
                    OpRoutineId = Dom.OpRoutine.MixedFeedGuide.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedGuide.Transition.AddOpVisa__Idle),
                    StartStateId = Dom.OpRoutine.MixedFeedGuide.State.AddOpVisa,
                    StopStateId = Dom.OpRoutine.MixedFeedGuide.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.CoreService
                });
                context.Set<Model.OpRoutineTransition>().AddOrUpdate(new Model.OpRoutineTransition
                {
                    Id = Dom.OpRoutine.MixedFeedGuide.Transition.BindLoadPoint__Idle,
                    OpRoutineId = Dom.OpRoutine.MixedFeedGuide.Id,
                    Name = nameof(Dom.OpRoutine.MixedFeedGuide.Transition.BindLoadPoint__Idle),
                    StartStateId = Dom.OpRoutine.MixedFeedGuide.State.BindLoadPoint,
                    StopStateId = Dom.OpRoutine.MixedFeedGuide.State.Idle,
                    ProcessorId = Dom.OpRoutine.Processor.WebUI
                });
                context.SaveChanges();
            }
        }
    }
}