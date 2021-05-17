using System.Collections.Generic;
using Gravitas.Model;
using System.Data.Entity.Migrations;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {
        public static class Nodes
        {
            #region Basic nodes

            public static void SingleWindow(GravitasDbContext context)
            {
                //// Єдине вікно (перше робоче місце)
                //context.Set<Node>().AddOrUpdate(new Node
                //{
                //    Id = (int) NodeIdValue.SingleWindowFirst,
                //    NodeGroup = NodeGroup.SingleWindow,
                //    Code = "Перше робоче місце",
                //    Name = "Єдине вікно",
                //    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                //    OrganisationUnitId = null,
                //    Quota = 2000,
                //    IsActive = true,
                //    IsStart = true,
                //    IsFinish = false,
                //    MaximumProcessingTime = 120,
                //    Context = new Model.Dto.NodeContext
                //    {
                //        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                //        TicketContainerId = null,
                //        TicketId = null
                //    }.ToJson(),
                //    Config = new Model.Dto.NodeConfig
                //    {
                //        DI = new Dictionary<string, Model.Dto.NodeConfig.DiConfig> { },
                //        DO = new Dictionary<string, Model.Dto.NodeConfig.DoConfig> { },
                //        Rfid = new Dictionary<string, Model.Dto.NodeConfig.RfidConfig>
                //        {
                //            [Dom.Node.Config.Rfid.TableReader] = new Model.Dto.NodeConfig.RfidConfig {DeviceId = 10000300, Timeout = 4}
                //        },
                //        Scale = new Dictionary<string, Model.Dto.NodeConfig.ScaleConfig> { },
                //        Camera = new Dictionary<string, Model.Dto.NodeConfig.CameraConfig> { }
                //    }.ToJson(),
                //    ProcessingMessage = new Model.Dto.NodeProcessingMsg().ToJson()
                //});

                //// Єдине вікно (друге робоче місце)
                //context.Set<Node>().AddOrUpdate(new Node
                //{
                //    Id = (int) NodeIdValue.SingleWindowSecond,
                //    NodeGroup = NodeGroup.SingleWindow,
                //    Code = "Друге робоче місце",
                //    Name = "Єдине вікно",
                //    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                //    OrganisationUnitId = null,
                //    Quota = 2000,
                //    IsActive = true,
                //    IsStart = true,
                //    IsFinish = false,
                //    MaximumProcessingTime = 120,
                //    Context = new Model.Dto.NodeContext
                //    {
                //        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                //        TicketContainerId = null,
                //        TicketId = null
                //    }.ToJson(),
                //    Config = new Model.Dto.NodeConfig
                //    {
                //        DI = new Dictionary<string, Model.Dto.NodeConfig.DiConfig> { },
                //        DO = new Dictionary<string, Model.Dto.NodeConfig.DoConfig> { },
                //        Rfid = new Dictionary<string, Model.Dto.NodeConfig.RfidConfig>
                //        {
                //            [Dom.Node.Config.Rfid.TableReader] = new Model.Dto.NodeConfig.RfidConfig {DeviceId = 10000400, Timeout = 4}
                //        },
                //        Scale = new Dictionary<string, Model.Dto.NodeConfig.ScaleConfig> { },
                //        Camera = new Dictionary<string, Model.Dto.NodeConfig.CameraConfig> { }
                //    }.ToJson(),
                //    ProcessingMessage = new Model.Dto.NodeProcessingMsg().ToJson()
                //});
                
                //// Єдине вікно (третє робоче місце)
                //context.Set<Node>().AddOrUpdate(new Node
                //{
                //    Id = (int) NodeIdValue.SingleWindowThird,
                //    NodeGroup = NodeGroup.SingleWindow,
                //    Code = "Третє робоче місце",
                //    Name = "Єдине вікно",
                //    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                //    OrganisationUnitId = null,
                //    Quota = 2000,
                //    IsActive = true,
                //    IsStart = true,
                //    IsFinish = false,
                //    MaximumProcessingTime = 120,
                //    Context = new Model.Dto.NodeContext
                //    {
                //        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                //        TicketContainerId = null,
                //        TicketId = null
                //    }.ToJson(),
                //    Config = new Model.Dto.NodeConfig
                //    {
                //        DI = new Dictionary<string, Model.Dto.NodeConfig.DiConfig> { },
                //        DO = new Dictionary<string, Model.Dto.NodeConfig.DoConfig> { },
                //        Rfid = new Dictionary<string, Model.Dto.NodeConfig.RfidConfig>
                //        {
                //            [Dom.Node.Config.Rfid.TableReader] = new Model.Dto.NodeConfig.RfidConfig {DeviceId = 10000500, Timeout = 4}
                //        },
                //        Scale = new Dictionary<string, Model.Dto.NodeConfig.ScaleConfig> { },
                //        Camera = new Dictionary<string, Model.Dto.NodeConfig.CameraConfig> { }
                //    }.ToJson(),
                //    ProcessingMessage = new Model.Dto.NodeProcessingMsg().ToJson()
                //});
                
                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.SingleWindowReadonly1,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SingleWindowReadonly2,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SingleWindowReadonly3,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SingleWindowReadonly4,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SingleWindowReadonly5,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SingleWindowReadonly6,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SingleWindowReadonly7,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SingleWindowReadonly8,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SingleWindowReadonly9,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Єдине вікно (тільки для читання)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SingleWindowReadonly10,
                    NodeGroup = NodeGroup.SingleWindow,
                    Code = "Огляд автомобілів",
                    Name = "Єдине вікно",
                    OpRoutineId = Dom.OpRoutine.SingleWindow.Id,
                    OrganisationUnitId = null,
                    Quota = 2000,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.SingleWindow.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig> { },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void Security(GravitasDbContext context)
            {
                //// 02.01.01/1 - КПП №1 Пост №8 в'їзд
                //context.Set<Node>().AddOrUpdate(new Node
                //{
                //    Id = (int) NodeIdValue.SecurityIn1,
                //    NodeGroup = NodeGroup.Security,
                //    Code = @"02.01.01/1",
                //    Name = "КПП №1 Пост №8 в'їзд",
                //    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                //    OrganisationUnitId = null,
                //    Quota = 20,
                //    IsActive = true,
                //    IsStart = false,
                //    IsFinish = false,
                //    MaximumProcessingTime = 120,
                //    Context = new Model.Dto.NodeContext()
                //    {
                //        OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.Idle,
                //        TicketContainerId = null,
                //        TicketId = null
                //    }.ToJson(),
                //    Config = new Model.Dto.NodeConfig()
                //    {
                //        DI = new Dictionary<string, Model.Dto.NodeConfig.DiConfig>
                //        {
                //            [Dom.Node.Config.DI.Barrier] = new Model.Dto.NodeConfig.DiConfig() {DeviceId = 1000701}
                //        },
                //        DO = new Dictionary<string, Model.Dto.NodeConfig.DoConfig>
                //        {
                //            [Dom.Node.Config.DO.Barrier] = new Model.Dto.NodeConfig.DoConfig {DeviceId = 1000703},
                //            [Dom.Node.Config.DO.RfidCheckFirst] = new Model.Dto.NodeConfig.DoConfig {DeviceId = 1000903},
                //            [Dom.Node.Config.DO.RfidCheckSecond] = new Model.Dto.NodeConfig.DoConfig {DeviceId = 1000904}
                //        },
                //        Rfid = new Dictionary<string, Model.Dto.NodeConfig.RfidConfig>
                //        {
                //            [Dom.Node.Config.Rfid.OnGateReader] = new Model.Dto.NodeConfig.RfidConfig {DeviceId = 1000300, Timeout = 4},
                //            [Dom.Node.Config.Rfid.LongRangeReader] = new Model.Dto.NodeConfig.RfidConfig {DeviceId = 1000501, Timeout = 4}
                //        },
                //        Scale = new Dictionary<string, Model.Dto.NodeConfig.ScaleConfig> { },
                //        Camera = new Dictionary<string, Model.Dto.NodeConfig.CameraConfig>
                //        {
                //            [Dom.Node.Config.Camera.Camera1] = new Model.Dto.NodeConfig.CameraConfig() {DeviceId = 1000100},
                //            [Dom.Node.Config.Camera.Camera2] = new Model.Dto.NodeConfig.CameraConfig() {DeviceId = 1000200}
                //        }
                //    }.ToJson(),
                //    ProcessingMessage = new Model.Dto.NodeProcessingMsg().ToJson()
                //});

                //// 02.01.01/2 - КПП №1 Пост №8 виїзд
                //context.Set<Node>().AddOrUpdate(new Node
                //{
                //    Id = (int) NodeIdValue.SecurityOut1,
                //    NodeGroup = NodeGroup.Security,
                //    Code = @"02.01.01/2",
                //    Name = "КПП №1 Пост №8 виїзд",
                //    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                //    OrganisationUnitId = null,
                //    Quota = 20,
                //    IsActive = true,
                //    IsStart = false,
                //    IsFinish = false,
                //    MaximumProcessingTime = 120,
                //    Context = new Model.Dto.NodeContext()
                //    {
                //        OpRoutineStateId = Dom.OpRoutine.SecurityOut.State.Idle,
                //        TicketContainerId = null,
                //        TicketId = null
                //    }.ToJson(),
                //    Config = new Model.Dto.NodeConfig()
                //    {
                //        DI = new Dictionary<string, Model.Dto.NodeConfig.DiConfig>
                //        {
                //            [Dom.Node.Config.DI.Barrier] = new Model.Dto.NodeConfig.DiConfig() {DeviceId = 1000801}
                //        },
                //        DO = new Dictionary<string, Model.Dto.NodeConfig.DoConfig>
                //        {
                //            [Dom.Node.Config.DO.Barrier] = new Model.Dto.NodeConfig.DoConfig {DeviceId = 1000803},
                //            [Dom.Node.Config.DO.RfidCheckFirst] = new Model.Dto.NodeConfig.DoConfig {DeviceId = 1001003},
                //            [Dom.Node.Config.DO.RfidCheckSecond] = new Model.Dto.NodeConfig.DoConfig {DeviceId = 1001004}
                //        },
                //        Rfid = new Dictionary<string, Model.Dto.NodeConfig.RfidConfig>
                //        {
                //            [Dom.Node.Config.Rfid.OnGateReader] = new Model.Dto.NodeConfig.RfidConfig {DeviceId = 1000400, Timeout = 4},
                //            [Dom.Node.Config.Rfid.TableReader] = new Model.Dto.NodeConfig.RfidConfig {DeviceId = 1000600, Timeout = 4}
                //        },
                //        Scale = new Dictionary<string, Model.Dto.NodeConfig.ScaleConfig> { },
                //        Camera = new Dictionary<string, Model.Dto.NodeConfig.CameraConfig>
                //        {
                //            [Dom.Node.Config.Camera.Camera1] = new Model.Dto.NodeConfig.CameraConfig() {DeviceId = 1000100},
                //            [Dom.Node.Config.Camera.Camera2] = new Model.Dto.NodeConfig.CameraConfig() {DeviceId = 1000200}
                //        }
                //    }.ToJson(),
                //    ProcessingMessage = new Model.Dto.NodeProcessingMsg().ToJson()
                //});

                // КПП №2 Пост №6 в'їзд
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SecurityIn2,
                    NodeGroup = NodeGroup.Security,
                    Code = @"",
                    Name = "КПП №2 Пост №6 в'їзд",
                    OpRoutineId = Dom.OpRoutine.SecurityIn.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.SecurityIn.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 26000201, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                        {
                            [Dom.Node.Config.Camera.Camera1] = new NodeConfig.CameraConfig { DeviceId = 26000100 },
                            [Dom.Node.Config.Camera.Camera2] = new NodeConfig.CameraConfig { DeviceId = 26000200 }
                        }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // КПП №2 Пост №6 виїзд
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SecurityOut2,
                    NodeGroup = NodeGroup.Security,
                    Code = @"",
                    Name = "КПП №2 Пост №6 виїзд",
                    OpRoutineId = Dom.OpRoutine.SecurityOut.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.SecurityOut.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 7000700, Timeout = 4 },
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 26000202, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                        {
                            [Dom.Node.Config.Camera.Camera1] = new NodeConfig.CameraConfig { DeviceId = 26000100 },
                            [Dom.Node.Config.Camera.Camera2] = new NodeConfig.CameraConfig { DeviceId = 26000200 }
                        }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // КПП №2 Пост №6 Точка оглядова
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.SecurityReview1,
                    NodeGroup = NodeGroup.Security,
                    Code = @"",
                    Name = "КПП №2 Пост №6 Точка оглядова",
                    OpRoutineId = Dom.OpRoutine.SecurityReview.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.SecurityReview.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 25000100, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void UnloadLaboratory(GravitasDbContext context)
            {
                // 03.01.01  Дільниця №2 Візіровка Проїзд №3 
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.Laboratory3,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"03.01.01",
                    Name = "Дільниця №2 Візіровка Проїзд №3",
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 800300, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 800501, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        LabAnalyser = new Dictionary<string, NodeConfig.LabAnalyserConfig>
                        {
                            [Dom.Node.Config.LabAnalyser.Foss2] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000101, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Foss] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000100, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Bruker] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000200, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Infrascan] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000300, Timeout = 180}
                        },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Дільниця №2 Візіровка Лабораторія 
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.Laboratory0,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"",
                    Name = "Дільниця №2 Візіровка Лабораторія",
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 7000300, Timeout = 4},
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        LabAnalyser = new Dictionary<string, NodeConfig.LabAnalyserConfig>
                        {
                            [Dom.Node.Config.LabAnalyser.Foss2] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000101, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Foss] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000100, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Bruker] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000200, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Infrascan] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000300, Timeout = 180}
                        },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });


                // 03.01.02  Дільниця №2 Візіровка Проїзд №4
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.Laboratory4,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"03.01.02",
                    Name = "Дільниця №2 Візіровка Проїзд №4",
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 9000400, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 9000501, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        LabAnalyser = new Dictionary<string, NodeConfig.LabAnalyserConfig>
                        {
                            [Dom.Node.Config.LabAnalyser.Foss2] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000101, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Foss] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000100, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Bruker] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000200, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Infrascan] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000300, Timeout = 180}
                        },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Дільниця №1 Візіровка 
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.Laboratory5,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"",
                    Name = "Дільниця №1 Візіровка",
                    OpRoutineId = Dom.OpRoutine.LabolatoryIn.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 25000200, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 25000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        LabAnalyser = new Dictionary<string, NodeConfig.LabAnalyserConfig>
                        {
                            [Dom.Node.Config.LabAnalyser.Foss2] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000101, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Foss] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000100, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Bruker] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000200, Timeout = 180},
                            [Dom.Node.Config.LabAnalyser.Infrascan] = new NodeConfig.LabAnalyserConfig() {DeviceId = 9000300, Timeout = 180}
                        },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void CentralLaboratory(GravitasDbContext context)
            {
                // Центральна лабораторія (Точка відбору проб шроту)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.CentralLaboratoryGetShrot,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"13.01.01/1",
                    Name = "Точка відбору проб шроту",
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.CentralLaboratorySamples.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8001103},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8001104}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 15000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 15000200, Timeout = 4},
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Центральна лабораторія (Відбір проб олії соєвої)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.CentralLaboratoryGetOil1,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"13.01.01/1",
                    Name = "Відбір проб олії соєвої",
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.CentralLaboratorySamples.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                            { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                            { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 21000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 21000201, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                            { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Центральна лабораторія (Відбір проб олії соняшникової)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.CentralLaboratoryGetOil2,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"13.01.01/1",
                    Name = "Відбір проб олії соняшникової",
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.CentralLaboratorySamples.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                            { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                            { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 21000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 21000200, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                            { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Центральна лабораторія (Точка відбору проб митного складу)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.CentralLaboratoryGetCustomStore,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"",
                    Name = "Точка відбору проб митного складу",
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.CentralLaboratorySamples.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>(),
                        DO = new Dictionary<string, NodeConfig.DoConfig>(),

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 22000200, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 22000200, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>()
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Центральна лабораторія (Точка відбору проб автомобільної рампи)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.CentralLaboratoryGetTruckRamp,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"",
                    Name = "Точка відбору проб автомобільної рампи",
                    OpRoutineId = Dom.OpRoutine.CentralLaboratorySamples.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.CentralLaboratorySamples.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>(),
                        DO = new Dictionary<string, NodeConfig.DoConfig>(),

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 22000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 22000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig>(),
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>()
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Лабораторія відвантаження 1
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.CentralLaboratoryProcess1,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"14.01.01/1",
                    Name = "Лабораторія відвантаження 1",
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                            { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                            { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 16000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                            { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Лабораторія відвантаження 2
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.CentralLaboratoryProcess2,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"14.01.01/1",
                    Name = "Лабораторія відвантаження 2",
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                            { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                            { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 16000200, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                            { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Центральна лабораторія 3
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.CentralLaboratoryProcess3,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"14.01.01/1",
                    Name = "Лабораторія відвантаження 3",
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                            { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                            { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 16000300, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                            { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Центральна лабораторія 4
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.CentralLaboratoryProcess4,
                    NodeGroup = NodeGroup.Laboratory,
                    Code = @"14.01.01/1",
                    Name = "Лабораторія відвантаження 4",
                    OpRoutineId = Dom.OpRoutine.CentralLaboratoryProcess.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                            { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                            { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 36000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                            { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void Weighbridges(GravitasDbContext context)
            {
                // 04.01.01 Авто Вагова №1
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.Weighbridge1,
                    NodeGroup = NodeGroup.WeighBridge,
                    Code = "04.01.01",
                    Name = "Авто Вагова №1",
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.LoopIncoming] = new NodeConfig.DiConfig() {DeviceId = 3000501},
                            [Dom.Node.Config.DI.LoopOutgoing] = new NodeConfig.DiConfig() {DeviceId = 3000502},

                            [Dom.Node.Config.DI.PerimeterLeft] = new NodeConfig.DiConfig() {DeviceId = 3000403},
                            [Dom.Node.Config.DI.PerimeterRight] = new NodeConfig.DiConfig() {DeviceId = 3000401},
                            [Dom.Node.Config.DI.PerimeterTop] = new NodeConfig.DiConfig() {DeviceId = 3000404},
                            [Dom.Node.Config.DI.PerimeterBottom] = new NodeConfig.DiConfig() {DeviceId = 3000402},
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.TrafficLightIncoming] = new NodeConfig.DoConfig {DeviceId = 3000503},
                            [Dom.Node.Config.DO.TrafficLightOutgoing] = new NodeConfig.DoConfig {DeviceId = 3000504}
                        },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 3000300, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig>
                        {
                            [Dom.Node.Config.Scale.Scale1] = new NodeConfig.ScaleConfig {DeviceId = 3000100, Timeout = 20}
                        },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                        {
                            [Dom.Node.Config.Camera.Camera1] = new NodeConfig.CameraConfig {DeviceId = 3000600},
                            [Dom.Node.Config.Camera.Camera2] = new NodeConfig.CameraConfig {DeviceId = 3000700},
                            [Dom.Node.Config.Camera.Camera3] = new NodeConfig.CameraConfig {DeviceId = 3000800}
                        }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // 04.01.02 Авто Вагова №2
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.Weighbridge2,
                    NodeGroup = NodeGroup.WeighBridge,
                    Code = "04.01.02",
                    Name = "Авто Вагова №2",
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.LoopIncoming] = new NodeConfig.DiConfig() {DeviceId = 4000501},
                            [Dom.Node.Config.DI.LoopOutgoing] = new NodeConfig.DiConfig() {DeviceId = 4000502},

                            [Dom.Node.Config.DI.PerimeterLeft] = new NodeConfig.DiConfig() {DeviceId = 4000403},
                            [Dom.Node.Config.DI.PerimeterRight] = new NodeConfig.DiConfig() {DeviceId = 4000401},
                            [Dom.Node.Config.DI.PerimeterTop] = new NodeConfig.DiConfig() {DeviceId = 4000404},
                            [Dom.Node.Config.DI.PerimeterBottom] = new NodeConfig.DiConfig() {DeviceId = 4000402},
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.TrafficLightIncoming] = new NodeConfig.DoConfig {DeviceId = 4000503},
                            [Dom.Node.Config.DO.TrafficLightOutgoing] = new NodeConfig.DoConfig {DeviceId = 4000504}
                        },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 4000300, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig>
                        {
                            [Dom.Node.Config.Scale.Scale1] = new NodeConfig.ScaleConfig {DeviceId = 4000100, Timeout = 20}
                        },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                        {
                            [Dom.Node.Config.Camera.Camera1] = new NodeConfig.CameraConfig {DeviceId = 4000600},
                            [Dom.Node.Config.Camera.Camera2] = new NodeConfig.CameraConfig {DeviceId = 4000700},
                            [Dom.Node.Config.Camera.Camera3] = new NodeConfig.CameraConfig {DeviceId = 4000800}
                        }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // 04.01.03 Авто Вагова №3
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.Weighbridge3,
                    NodeGroup = NodeGroup.WeighBridge,
                    Code = "04.01.03",
                    Name = "Авто Вагова №3",
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.LoopIncoming] = new NodeConfig.DiConfig() {DeviceId = 5000501},
                            [Dom.Node.Config.DI.LoopOutgoing] = new NodeConfig.DiConfig() {DeviceId = 5000502},

                            [Dom.Node.Config.DI.PerimeterLeft] = new NodeConfig.DiConfig() {DeviceId = 5000403},
                            [Dom.Node.Config.DI.PerimeterRight] = new NodeConfig.DiConfig() {DeviceId = 5000401},
                            [Dom.Node.Config.DI.PerimeterTop] = new NodeConfig.DiConfig() {DeviceId = 5000404},
                            [Dom.Node.Config.DI.PerimeterBottom] = new NodeConfig.DiConfig() {DeviceId = 5000402},
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.TrafficLightIncoming] = new NodeConfig.DoConfig {DeviceId = 5000503},
                            [Dom.Node.Config.DO.TrafficLightOutgoing] = new NodeConfig.DoConfig {DeviceId = 5000504}
                        },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 5000300, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig>
                        {
                            [Dom.Node.Config.Scale.Scale1] = new NodeConfig.ScaleConfig {DeviceId = 5000100, Timeout = 20}
                        },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                        {
                            [Dom.Node.Config.Camera.Camera1] = new NodeConfig.CameraConfig {DeviceId = 5000600},
                            [Dom.Node.Config.Camera.Camera2] = new NodeConfig.CameraConfig {DeviceId = 5000700},
                            [Dom.Node.Config.Camera.Camera3] = new NodeConfig.CameraConfig {DeviceId = 5000800}
                        }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // 04.01.04 Авто Вагова №4
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.Weighbridge4,
                    NodeGroup = NodeGroup.WeighBridge,
                    Code = "04.01.04",
                    Name = "Авто Вагова №4",
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.LoopIncoming] = new NodeConfig.DiConfig() {DeviceId = 6000501},
                            [Dom.Node.Config.DI.LoopOutgoing] = new NodeConfig.DiConfig() {DeviceId = 6000502},

                            [Dom.Node.Config.DI.PerimeterLeft] = new NodeConfig.DiConfig() {DeviceId = 6000403},
                            [Dom.Node.Config.DI.PerimeterRight] = new NodeConfig.DiConfig() {DeviceId = 6000401},
                            [Dom.Node.Config.DI.PerimeterTop] = new NodeConfig.DiConfig() {DeviceId = 6000402},
                            [Dom.Node.Config.DI.PerimeterBottom] = new NodeConfig.DiConfig() {DeviceId = 6000404},
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.TrafficLightIncoming] = new NodeConfig.DoConfig {DeviceId = 6000503},
                            [Dom.Node.Config.DO.TrafficLightOutgoing] = new NodeConfig.DoConfig {DeviceId = 6000504}
                        },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 6000300, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig>
                        {
                            [Dom.Node.Config.Scale.Scale1] = new NodeConfig.ScaleConfig {DeviceId = 6000100, Timeout = 20}
                        },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                        {
                            [Dom.Node.Config.Camera.Camera1] = new NodeConfig.CameraConfig {DeviceId = 6000600},
                            [Dom.Node.Config.Camera.Camera2] = new NodeConfig.CameraConfig {DeviceId = 6000700},
                            [Dom.Node.Config.Camera.Camera3] = new NodeConfig.CameraConfig {DeviceId = 6000800}
                        }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                 // Авто Вагова №5
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.Weighbridge5,
                    NodeGroup = NodeGroup.WeighBridge,
                    Code = "",
                    Name = "Авто Вагова №5",
                    OpRoutineId = Dom.OpRoutine.Weighbridge.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.Weighbridge.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.LoopIncoming] = new NodeConfig.DiConfig {DeviceId = 7000501},
                            [Dom.Node.Config.DI.LoopOutgoing] = new NodeConfig.DiConfig {DeviceId = 7000502},

                            [Dom.Node.Config.DI.PerimeterLeft] = new NodeConfig.DiConfig {DeviceId = 7000402},
                            [Dom.Node.Config.DI.PerimeterRight] = new NodeConfig.DiConfig {DeviceId = 7000404},
                            [Dom.Node.Config.DI.PerimeterTop] = new NodeConfig.DiConfig {DeviceId = 7000403},
                            [Dom.Node.Config.DI.PerimeterBottom] = new NodeConfig.DiConfig {DeviceId = 7000401},
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.TrafficLightOutgoing] = new NodeConfig.DoConfig {DeviceId = 7000503},
                            [Dom.Node.Config.DO.TrafficLightIncoming] = new NodeConfig.DoConfig {DeviceId = 7000504}
                        },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 7000700, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig>
                        {
                            [Dom.Node.Config.Scale.Scale1] = new NodeConfig.ScaleConfig {DeviceId = 7000100, Timeout = 20}
                        },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig>
                        {
                            [Dom.Node.Config.Camera.Camera1] = new NodeConfig.CameraConfig {DeviceId = 7000600},
                            [Dom.Node.Config.Camera.Camera2] = new NodeConfig.CameraConfig {DeviceId = 7000601},
                            [Dom.Node.Config.Camera.Camera3] = new NodeConfig.CameraConfig {DeviceId = 7000602}
                        }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });


                context.SaveChanges();
            }

            #endregion

            #region Unload points

            public static void UnloadEl23(GravitasDbContext context)
            {
                // Призначення ями вигрузки
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPointGuideEl23,
                    NodeGroup = NodeGroup.UnloadGuide,
                    Code = "",
                    Name = "Призначення вигрузки на Елеватор 2, 3",
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 12000400, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Призначення точки розвантаження нижня територія
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPointGuideLowerArea,
                    NodeGroup = NodeGroup.UnloadGuide,
                    Code = "",
                    Name = "Призначення вигрузки на нижній території",
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide2.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide2.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 27000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Елеватор №2 Авторозвантажувач  №30
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint20,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"ЦБ10000004",
                    Name = "Авторозвантажувач  №30",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator2,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.Barrier] = new NodeConfig.DiConfig() {DeviceId = 13000402}
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.Barrier] = new NodeConfig.DoConfig() {DeviceId = 13000403}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 13000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 13000201, Timeout = 4},

                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Елеватор №2 Авторозвантажувач  №40
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint21,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"ЦБ10000006",
                    Name = "Авторозвантажувач  №40",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator2,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.Barrier] = new NodeConfig.DiConfig() {DeviceId = 13000401}
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.Barrier] = new NodeConfig.DoConfig() {DeviceId = 13000404}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 13000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 13000301, Timeout = 4},

                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Елеватор №3 Авторозвантажувач  №50
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint22,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"ЦБ10000008",
                    Name = "Авторозвантажувач  №50",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator3,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.Barrier] = new NodeConfig.DiConfig() {DeviceId = 14000501}
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.Barrier] = new NodeConfig.DoConfig() {DeviceId = 14000503}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 14000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 14000201, Timeout = 4},

                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void UnloadEl45(GravitasDbContext context)
            {
                // Призначення ями вигрузки
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPointGuideEl45,
                    NodeGroup = NodeGroup.UnloadGuide,
                    Code = "",
                    Name = "Призначення вигрузки на Елеватор 4, 5",
                    OpRoutineId = Dom.OpRoutine.UnloadPointGuide.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 12000300, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // 03.05.04/1 Елеватор №4 та №5 Авторозвантажувач №100
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint10,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"ЦБ10000044",
                    Name = "Авторозвантажувач  №100",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator4_5,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.Barrier] = new NodeConfig.DiConfig() {DeviceId = 11000601}
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.Barrier] = new NodeConfig.DoConfig() {DeviceId = 11000603}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 11000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 11000301, Timeout = 4},

                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // 03.05.04  Елеватор №4 та №5 Авторозвантажувач  №200
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint11,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"ЦБ10000046",
                    Name = "Авторозвантажувач  №200",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator4_5,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.Barrier] = new NodeConfig.DiConfig() {DeviceId = 11000701}
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.Barrier] = new NodeConfig.DoConfig() {DeviceId = 11000703}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 11000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 11000401, Timeout = 4},

                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // 03.05.04  Елеватор №4 та №5 Авторозвантажувач  №300
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint12,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"ЦБ10000042",
                    Name = "Авторозвантажувач  №300",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator4_5,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.Barrier] = new NodeConfig.DiConfig() {DeviceId = 11000801}
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.Barrier] = new NodeConfig.DoConfig() {DeviceId = 11000803}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 11000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 11000501, Timeout = 4},
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Елеватор №4 та №5 Авторозвантажувач  Схема 5
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint13,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"",
                    Name = "Авторозвантажувач  Схема 5",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator1,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.Barrier] = new NodeConfig.DiConfig() {DeviceId = 19000302}
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.Barrier] = new NodeConfig.DoConfig() {DeviceId = 19000303}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 19000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 19000201, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Елеватор №4 та №5 Авторозвантажувач ККЗ
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint14,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"",
                    Name = "Авторозвантажувач ККЗ",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator1,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.Barrier] = new NodeConfig.DiConfig() {DeviceId = 19000301}
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.Barrier] = new NodeConfig.DoConfig() {DeviceId = 19000304}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 19000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 19000211, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void UnloadShrot(GravitasDbContext context)
            {
                // Прийом рідких компонентів
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint32,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"",
                    Name = "Прийом рідких компонентів",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Stores,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8000903},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8000904}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 22000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 20000300, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Вузол ТМЦ
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint61,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"",
                    Name = "Вузол ТМЦ",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.TMC,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 29000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 29000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void UnloadTareWarehouse(GravitasDbContext context)
            {
                // Розвантаження на складі тарировки
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint40,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"",
                    Name = "Розвантаження на складі тарировки",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.TareWarehouse,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8001203},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8001204}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 21000200, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 23000200, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void UnloadLowerTerritory(GravitasDbContext context)
            {
                // Точка вивантаження - Нижня територія
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint50,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"",
                    Name = "Точка вивантаження - Нижня територія",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType2.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Husk,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.Workstation,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 27000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void CheckPoints(GravitasDbContext context)
            {
                // Вивантаження поле
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint60,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"",
                    Name = "Вивантаження поле",
                    OpRoutineId = Dom.OpRoutine.UnloadCheckPoint.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadCheckPoint.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.Barrier] = new NodeConfig.DiConfig {DeviceId = 14000501}
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.Barrier] = new NodeConfig.DoConfig {DeviceId = 14000503}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.LongRangeReader] = new NodeConfig.RfidConfig {DeviceId = 14000301, Timeout = 4},
                            [Dom.Node.Config.Rfid.LongRangeReader2] = new NodeConfig.RfidConfig {DeviceId = 14000401, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            public static void UnloadStores(GravitasDbContext context)
            {
                // Автомобільна рампа 
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint30,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"",
                    Name = "Автомобільна рампа прихід",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Stores,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 22000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 22000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Митний склад
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.UnloadPoint31,
                    NodeGroup = NodeGroup.Unload,
                    Code = @"",
                    Name = "Митний склад прихід",
                    OpRoutineId = Dom.OpRoutine.UnloadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Stores,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.UnloadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 22000200, Timeout = 4},
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 22000200, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                context.SaveChanges();
            }
            #endregion
            
            #region Load points
            public static void LoadEl23(GravitasDbContext context)
            {
                // Призначення точки завантаження (Елеватор 2, 3)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPointGuideEl23,
                    NodeGroup = NodeGroup.LoadGuide,
                    Code = "",
                    Name = "Призначення завантаження на Елеватор 2, 3",
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 12000400, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // 5.02.04 Елеватор №2 Завантаження зернових №1
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint10,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Елеватор №2. Завантаження зернових №1",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator2,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 13000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 13000500, Timeout = 4 },

                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // 5,02,05 Елеватор №2 Завантаження зернових №2
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint11,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Елеватор №2. Завантаження зернових №2",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator2,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 13000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 13000600, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // 5.03.03 Елеватор №3 Завантаження зернових №1
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint12,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Елеватор №3 Завантаження зернових №1",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator3,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 14000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 13000700, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                context.SaveChanges();
            }
            
             public static void LoadMPZ(GravitasDbContext context)
            {
                // Призначення точки завантаження МПЗ
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPointGuideMPZ,
                    NodeGroup = NodeGroup.LoadGuide,
                    Code = "",
                    Name = "Призначення завантаження на МПЗ",
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 24000100, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Завантаження соєвої оболонки
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint60,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Завантаження соєвої оболонки",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.MPZ,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8000503},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8000504}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 24000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 24000200, Timeout = 4 },

                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
            
                // Завантаження подрібненої лушпиння соняшника
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint61,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Завантаження подрібненої лушпиння соняшника",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.MPZ,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8000403},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8000404}
                        },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 24000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 24000300, Timeout = 4 }

                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Завантаження Підлоговий склад схема №5 та схема №6
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint62,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Завантаження Підлоговий склад схема №5 та схема №6",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.MPZ,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8000203},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8000204}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 24000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 24000400, Timeout = 4 },

                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                context.SaveChanges();
            }
            
            public static void LoadEl45(GravitasDbContext context)
            {
                // Призначення точки завантаження (Елеватор 4, 5)
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPointGuideEl45,
                    NodeGroup = NodeGroup.LoadGuide,
                    Code = "",
                    Name = "Призначення завантаження на Елеватор 4, 5",
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 12000300, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Елеватор №4 Завантаження олійних №1
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint20,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Елеватор №4 Завантаження олійних №1",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator4_5,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.EmergencyOff] = new NodeConfig.DoConfig { DeviceId = 21000303 },
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8000103},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8000104}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 11000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 11000200, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Елеватор №4 Завантаження олійних №2
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint21,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Елеватор №4 Завантаження олійних №2",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator4_5,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.EmergencyOff] = new NodeConfig.DoConfig { DeviceId = 21000304 },
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8000203},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8000204}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 11000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 11000201, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Елеватор №5 Завантаження олійних №1
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint22,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Елеватор №4 Завантаження олійних №3",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Elevator4_5,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 11000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 11000202, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                context.SaveChanges();
            }

            public static void LoadShrotHuskOil(GravitasDbContext context)
            {
                // Призначення точки завантаження шроту, лузги, олії
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPointGuideShrotHuskOil,
                    NodeGroup = NodeGroup.LoadGuide,
                    Code = "",
                    Name = "Призначення завантаження шроту, лузги, олії",
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [$"{Dom.Node.Config.Rfid.TableReader}1"] = new NodeConfig.RfidConfig { DeviceId = 20000100, Timeout = 4 },
                            [$"{Dom.Node.Config.Rfid.TableReader}2"] = new NodeConfig.RfidConfig {DeviceId = 19000100, Timeout = 4},
                            [$"{Dom.Node.Config.Rfid.TableReader}3"] = new NodeConfig.RfidConfig {DeviceId = 19000100, Timeout = 4},
                            [$"{Dom.Node.Config.Rfid.TableReader}4"] = new NodeConfig.RfidConfig { DeviceId = 21000100, Timeout = 4 },
                            [$"{Dom.Node.Config.Rfid.TableReader}5"] = new NodeConfig.RfidConfig { DeviceId = 24000100, Timeout = 4 },
                            [$"{Dom.Node.Config.Rfid.TableReader}6"] = new NodeConfig.RfidConfig {DeviceId = 16000300, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Елеватор №1 Завантаження шроту 
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint30,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Елеватор №1 Завантаження шроту ",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.ShrotLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8000603},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8000604}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 20000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 20000200, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Завантаження шроту  Схеми №5
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint31,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Завантаження шроту  Схеми №5",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.ShrotLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8000703},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8000704}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 20000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 20000201, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // 10С12 та завантаження Шроту соняшникового
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint32,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "10С12 та завантаження Шроту соняшникового",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.ShrotLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8001003},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8001004}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 20000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 20000202, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Елеватор №1 Завантаження зернових 
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint33,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Елеватор №1 Завантаження зернових ",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.ShrotLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 8000803},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 8000804}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 20000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 20000203, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                context.SaveChanges();
            }
            
            public static void LoadTareWarehouse(GravitasDbContext context)
            {
                // Цех затаровки
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint50,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Цех затаровки",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.TareWarehouse,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Workstation,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 21000200, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 21000200, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                context.SaveChanges();
            }
            
            public static void LoadOil(GravitasDbContext context)
            {
                // Завантаження олії соєвої
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint40,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Завантаження олії соєвої",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.OilLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 21000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 21000201, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Завантаження олії Соняшникової та Фузів
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.LoadPoint41,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Завантаження олії Соняшникової та Фузів",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.OilLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 21000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 21000202, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }
            
            public static void LoadStores(GravitasDbContext context)
            {
                // Автомобільна рампа 
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.LoadPoint70,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Автомобільна рампа розхід",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Stores,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 22000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 22000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Підлоговий склад №28 та №27
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.LoadPoint73,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Підлоговий склад №28 та №27",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.TMC,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 29000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 29000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                // Митний склад розхід та Завантаження Гідрофузу
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.LoadPoint71,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Митний склад розхід та Завантаження Гідрофузу",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Stores,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 22000200, Timeout = 4},
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 22000200, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                context.SaveChanges();
            }
            
            public static void LoadLowerTerritory(GravitasDbContext context)
            {
                // Призначення точки завантаження
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.LoadPointGuideLowerArea,
                    NodeGroup = NodeGroup.LoadGuide,
                    Code = "",
                    Name = "Призначення завантаження на нижній території",
                    OpRoutineId = Dom.OpRoutine.LoadPointGuide2.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointGuide2.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 27000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Точка завантаження - Нижня територія
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int) NodeIdValue.LoadPoint72,
                    NodeGroup = NodeGroup.Load,
                    Code = @"",
                    Name = "Точка завантаження - Нижня територія",
                    OpRoutineId = Dom.OpRoutine.LoadPointType1.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.Husk,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig {DeviceId = 27000100, Timeout = 4},
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig {DeviceId = 27000100, Timeout = 4}
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });

                context.SaveChanges();
            }

            #endregion

            #region MixedFeed
            public static void MixedFeed(GravitasDbContext context)
            {
                // Менеджер силосів комбікорму
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.MixedFeedManager,
                    NodeGroup = NodeGroup.MixedFeed,
                    Code = "Комбікормовий завод",
                    Name = "Редагування показників силосів",
                    OpRoutineId = Dom.OpRoutine.MixedFeedManage.Id,
                    OrganisationUnitId = null,
                    Quota = 1,
                    IsActive = true,
                    IsStart = true,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.MixedFeedManage.State.Workstation,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 17100900, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // СГП Проїзд №1
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.MixedFeedLoad1,
                    NodeGroup = NodeGroup.MixedFeed,
                    Code = @"Комбікормовий завод",
                    Name = "СГП Проїзд №1",
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.MixedFeedLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.LoopIncoming] = new NodeConfig.DiConfig { DeviceId = 17100211 }
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.EmergencyOff] = new NodeConfig.DoConfig { DeviceId = 17100213 },
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 17200103},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 17200104}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 17000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 17100100, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // СГП Проїзд №2
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.MixedFeedLoad2,
                    NodeGroup = NodeGroup.MixedFeed,
                    Code = @"Комбікормовий завод",
                    Name = "СГП Проїзд №2",
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.MixedFeedLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.LoopIncoming] = new NodeConfig.DiConfig { DeviceId = 17100212 }
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.EmergencyOff] = new NodeConfig.DoConfig { DeviceId = 17100214 },
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 17200203},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 17200204}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 17000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 17100200, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // СГП Проїзд №3
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.MixedFeedLoad3,
                    NodeGroup = NodeGroup.MixedFeed,
                    Code = @"Комбікормовий завод",
                    Name = "СГП Проїзд №3",
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.MixedFeedLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.LoopIncoming] = new NodeConfig.DiConfig { DeviceId = 17100221 }
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.EmergencyOff] = new NodeConfig.DoConfig { DeviceId = 17100223 },
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 17200303},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 17200304}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 17000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 17100300, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // СГП Проїзд №4
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.MixedFeedLoad4,
                    NodeGroup = NodeGroup.MixedFeed,
                    Code = @"Комбікормовий завод",
                    Name = "СГП Проїзд №4",
                    OpRoutineId = Dom.OpRoutine.MixedFeedLoad.Id,
                    OrganisationUnitId = Dom.OrganizationUnit.Type.Workstantion.MixedFeedLoad,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext()
                    {
                        OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig()
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig>
                        {
                            [Dom.Node.Config.DI.LoopIncoming] = new NodeConfig.DiConfig { DeviceId = 17100222 }
                        },
                        DO = new Dictionary<string, NodeConfig.DoConfig>
                        {
                            [Dom.Node.Config.DO.EmergencyOff] = new NodeConfig.DoConfig { DeviceId = 17100224 },
                            [Dom.Node.Config.DO.RfidCheckFirst] = new NodeConfig.DoConfig {DeviceId = 17200403},
                            [Dom.Node.Config.DO.RfidCheckSecond] = new NodeConfig.DoConfig {DeviceId = 17200404}
                        },

                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 17000100, Timeout = 4 },
                            [Dom.Node.Config.Rfid.OnGateReader] = new NodeConfig.RfidConfig { DeviceId = 17100400, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                // Комбікормовий завод - призначення точки завантаження
                context.Set<Node>().AddOrUpdate(new Node
                {
                    Id = (int)NodeIdValue.MixedFeedGuide,
                    NodeGroup = NodeGroup.MixedFeed,
                    Code = "Комбікормовий завод",
                    Name = "Призначення точки завантаження",
                    OpRoutineId = Dom.OpRoutine.MixedFeedGuide.Id,
                    OrganisationUnitId = null,
                    Quota = 20,
                    IsActive = true,
                    IsStart = false,
                    IsFinish = false,
                    MaximumProcessingTime = 120,
                    Context = new NodeContext
                    {
                        OpRoutineStateId = Dom.OpRoutine.MixedFeedGuide.State.Idle,
                        TicketContainerId = null,
                        TicketId = null
                    }.ToJson(),
                    Config = new NodeConfig
                    {
                        DI = new Dictionary<string, NodeConfig.DiConfig> { },
                        DO = new Dictionary<string, NodeConfig.DoConfig> { },
                        Rfid = new Dictionary<string, NodeConfig.RfidConfig>
                        {
                            [Dom.Node.Config.Rfid.TableReader] = new NodeConfig.RfidConfig { DeviceId = 17100900, Timeout = 4 }
                        },
                        Scale = new Dictionary<string, NodeConfig.ScaleConfig> { },
                        Camera = new Dictionary<string, NodeConfig.CameraConfig> { }
                    }.ToJson(),
                    ProcessingMessage = new NodeProcessingMsg().ToJson()
                });
                
                context.SaveChanges();
            }
            #endregion
        }
    }
}
