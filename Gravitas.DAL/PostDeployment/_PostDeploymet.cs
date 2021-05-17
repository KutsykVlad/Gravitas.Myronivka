namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {
        public static void Deploy(GravitasDbContext context)
        {
//			Card.Type(context);
//            Roles.Type(context);
//            Roles.Assigment(context);
//            Device.Type(context);
//            Ticket.Status(context);
//            TicketContainer.Status(context);
//            TicketFile.Type(context);
//            OrganizationUnit.Type(context);
//            OrganizationUnit.Data(context);
//            QueueItemPriority.PriorityType(context);
//            QueueItemCategory.CategoryType(context);
            OpRoutine.Items(context);
//            OpRoutine.Processor(context);
//            SmsTemplates.SmsTemplate(context);
//            Routes.RouteMapState(context);
//            Routes.RouteTemplates(context);
//            Routes.RouteMaps(context);
//            PhoneNumbers(context);
            OpRoutineState.SingleWindow(context);
            OpRoutineState.SecurityIn(context);
            OpRoutineState.SecurityOut(context);
            OpRoutineState.SecurityReview(context);
            OpRoutineState.CentralLaboratorySample(context);
            OpRoutineState.CentralLaboratoryProcess(context);
            OpRoutineState.LaboratoryIn(context);
            OpRoutineState.Weightbridge(context);
            OpRoutineState.UnloadPointGuide(context);
            OpRoutineState.UnloadPointGuide2(context);
            OpRoutineState.UnloadPointType1(context);
            OpRoutineState.UnloadPointType2(context);
            OpRoutineState.LoadCheckPoint(context);
            OpRoutineState.UnloadCheckPoint(context);
            OpRoutineState.LoadPointType1(context);
            OpRoutineState.LoadPointGuide(context);
            OpRoutineState.LoadPointGuide2(context);
            OpRoutineState.MixedFeedLoad(context);
            OpRoutineState.MixedFeedGuide(context);
            OpRoutineState.MixedFeedManage(context);
            OpRoutineTransition.SingleWindow(context);
            OpRoutineTransition.SecurityIn(context);
            OpRoutineTransition.SecurityOut(context);
            OpRoutineTransition.SecurityReview(context);
            OpRoutineTransition.CentralLabolatorySamples(context);
            OpRoutineTransition.CentralLabolatoryProcess(context);
            OpRoutineTransition.LaboratoryIn(context);
            OpRoutineTransition.LabolatoryOut(context);
            OpRoutineTransition.WeightbridgeTare(context);
            OpRoutineTransition.UnloadPointGuide(context);
            OpRoutineTransition.UnloadPointGuide2(context);
            OpRoutineTransition.UnloadPointType1(context);
            OpRoutineTransition.UnloadPointType2(context);
            OpRoutineTransition.LoadCheckPoint(context);
            OpRoutineTransition.UnloadCheckPoint(context);
            OpRoutineTransition.LoadPointType1(context);
            OpRoutineTransition.LoadPointGuide(context);
            OpRoutineTransition.LoadPointGuide2(context);
            OpRoutineTransition.MixedFeedLoad(context);
            OpRoutineTransition.MixedFeedGuide(context);
            OpRoutineTransition.MixedFeedManage(context);
//            OpDataState.Items(context);
            Nodes.Security(context);
            //Nodes.Weighbridges(context);
            //Nodes.CentralLaboratory(context);
            //Nodes.LoadEl23(context);
            //Nodes.LoadEl45(context);
            //Nodes.LoadOil(context);
            //Nodes.LoadShrotHuskOil(context);
            //Nodes.MixedFeed(context);
            //Nodes.UnloadEl23(context);
            //Nodes.UnloadEl45(context);
            //Nodes.CheckPoints(context);
            //Nodes.UnloadLaboratory(context);
            //Nodes.LoadTareWarehouse(context);
            //Nodes.UnloadTareWarehouse(context);
            Nodes.SingleWindow(context);
            //Nodes.UnloadShrot(context);
            //Nodes.UnloadLowerTerritory(context);
            //Nodes.LoadMPZ(context);
            //Nodes.UnloadStores(context);
            //Nodes.LoadStores(context);
            //Nodes.LoadLowerTerritory(context);
//            EmployeeRoles.Roles(context);
//            MixedFeed.Silo(context);
//            EndPointNodes(context);
        }
    }
}