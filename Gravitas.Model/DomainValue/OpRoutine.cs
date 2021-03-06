using Gravitas.Infrastructure.Common.Attribute;

namespace Gravitas.Model.DomainValue
{
    public static class OpRoutine
    {

        [OpControllerName("CentralLaboratorySamples")]
        public static class CentralLaboratorySamples
        {
            public const int Id = 13;

            public static class State
            {
                [OpActionName("Idle")] 
                public const int Idle = 1301;

                [OpActionName("CentralLabSampleBindTray")]
                public const int CentralLabSampleBindTray = 1302;

                [OpActionName("CentralLabSampleAddOpVisa")]
                public const int CentralLabSampleAddOpVisa = 1303;
            }
        }

        [OpControllerName("CentralLaboratoryProcess")]
        public static class CentralLaboratoryProcess
        {
            public const int Id = 14;

            public static class State
            {
                [OpActionName("Idle")] 
                public const int Idle = 1401;
                
                [OpActionName("AddSample")] 
                public const int AddSample = 1402;
                
                [OpActionName("AddSampleVisa")] 
                public const int AddSampleVisa = 1403;
                
                [OpActionName("PrintLabel")] 
                public const int PrintLabel = 1404;
                
                [OpActionName("PrintDataDisclose")] 
                public const int PrintDataDisclose = 1405;
                
                [OpActionName("PrintCollisionStartVisa")]
                public const int PrintCollisionStartVisa = 1406;

                [OpActionName("PrintCollisionInit")]
                public const int PrintCollisionInit = 1407;

                [OpActionName("PrintCollisionInitVisa")]
                public const int PrintCollisionInitVisa = 1408;

                [OpActionName("PrintAddOpVisa")]
                public const int PrintAddOpVisa = 1409;
                
                [OpActionName("PrintDocument")]
                public const int PrintDocument = 1410;
            }
        }


        [OpControllerName("SingleWindow")]
        public static class SingleWindow
        {
            public const int Id = 1;

            public static class State
            {
                [OpActionName("Idle")] 
                public const int Idle = 101;
                
                [OpActionName("GetTicket")]
                public const int GetTicket = 102;
                
                [OpActionName("ShowTicketMenu")] 
                public const int ShowTicketMenu = 103;

                [OpActionName("ContainerCloseAddOpVisa")]
                public const int ContainerCloseAddOpVisa = 104;
                
                [OpActionName("AddOwnTransport")]
                public const int AddOwnTransport = 105;

                [OpActionName("EditTicketForm")] 
                public const int EditTicketForm = 111;
                
                [OpActionName("EditGetApiData")]
                public const int EditGetApiData = 112;
                
                [OpActionName("EditAddOpVisa")]
                public const int EditAddOpVisa = 113;
                
                [OpActionName("EditPostApiData")]
                public const int EditPostApiData = 114;

                [OpActionName("SupplyChangeAddOpVisa")]
                public const int SupplyChangeAddOpVisa = 115;

                [OpActionName("DivideTicketAddOpVisa")]
                public const int DivideTicketAddOpVisa = 116;

                [OpActionName("DeleteTicketAddOpVisa")]
                public const int DeleteTicketAddOpVisa = 117;

                [OpActionName("CloseAddOpVisa")]
                public const int CloseAddOpVisa = 121;
                
                [OpActionName("ClosePostApiData")]
                public const int ClosePostApiData = 122;

                [OpActionName("RouteEditData")]
                public const int RouteEditData = 131;
                
                [OpActionName("RouteAddOpVisa")]
                public const int RouteAddOpVisa = 132;
            }
        }

        [OpControllerName("SecurityIn")]
        public static class SecurityIn
        {
            public const int Id = 3;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 301;
                
                [OpActionName("CheckOwnTransport")]
                public const int CheckOwnTransport = 302;
                
                [OpActionName("BindLongRangeRfid")]
                public const int BindLongRangeRfid = 303;
                
                [OpActionName("AddOperationVisa")]
                public const int AddOperationVisa = 304;
                
                [OpActionName("OpenBarrier")]
                public const int OpenBarrier = 305;
                
                [OpActionName("GetCamSnapshot")]
                public const int GetCamSnapshot = 306;
            }
        }

        [OpControllerName("SecurityOut")]
        public static class SecurityOut
        {
            public const int Id = 4;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 401;
                
                [OpActionName("CheckOwnTransport")]
                public const int CheckOwnTransport = 402;
                
                [OpActionName("ShowOperationsList")]
                public const int ShowOperationsList = 403;
                
                [OpActionName("EditStampList")]
                public const int EditStampList = 404;
                
                [OpActionName("AddRouteControlVisa")]
                public const int AddRouteControlVisa = 405;

                [OpActionName("AddTransportInspectionVisa")]
                public const int AddTransportInspectionVisa = 406;

                [OpActionName("OpenBarrier")]
                public const int OpenBarrier = 407;
            }
        }

        [OpControllerName("LaboratoryIn")]
        public static class LaboratoryIn
        {
            public const int Id = 5;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 501;

                [OpActionName("SampleReadTruckRfid")]
                public const int SampleReadTruckRfid = 502;
                
                [OpActionName("SampleBindTray")]
                public const int SampleBindTray = 503;

                [OpActionName("SampleBindAnalysisTray")]
                public const int SampleBindAnalysisTray = 504;

                [OpActionName("SampleAddOpVisa")]
                public const int SampleAddOpVisa = 505;

                [OpActionName("ResultReadTrayRfid")]
                public const int ResultReadTrayRfid = 511;
                
                [OpActionName("ResultEditAnalysis")]
                public const int ResultEditAnalysis = 512;
                
                [OpActionName("ResultAddOpVisa")] 
                public const int ResultAddOpVisa = 513;

                [OpActionName("PrintReadTrayRfid")] 
                public const int PrintReadTrayRfid = 521;
                
                [OpActionName("PrintAnalysisResult")]
                public const int PrintAnalysisResults = 522;

                [OpActionName("PrintAnalysisAddOpVisa")]
                public const int PrintAnalysisAddOpVisa = 523;

                [OpActionName("PrintDataDisclose")]
                public const int PrintDataDisclose = 524;
                
                [OpActionName("PrintCollisionInit")]
                public const int PrintCollisionInit = 525;
                
                [OpActionName("PrintCollisionManage")]
                public const int PrintCollisionManage = 526;
                
                [OpActionName("PrintAddOpVisa")]
                public const int PrintAddOpVisa = 527;

                [OpActionName("PrintLaboratoryProtocol")]
                public const int PrintLaboratoryProtocol = 528;
            }
        }

        [OpControllerName("Weighbridge")]
        public static class Weighbridge
        {
            public const int Id = 7;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 701;
                
                [OpActionName("GetScaleZero")]
                public const int GetScaleZero = 702;
                
                [OpActionName("OpenBarrierIn")]
                public const int OpenBarrierIn = 703;
                
                [OpActionName("CheckScaleNotEmpty")]
                public const int CheckScaleNotEmpty = 704;
                
                [OpActionName("GetTicketCard")]
                public const int GetTicketCard = 705;

                [OpActionName("DriverTrailerEnableCheck")]
                public const int DriverTrailerEnableCheck = 706;

                [OpActionName("GuardianCardPrompt")] 
                public const int GuardianCardPrompt = 707;

                [OpActionName("GuardianTruckVerification")]
                public const int GuardianTruckVerification = 708;

                [OpActionName("GuardianTrailerEnableCheck")]
                public const int GuardianTrailerEnableCheck = 709;

                [OpActionName("TruckWeightPrompt")]
                public const int TruckWeightPrompt = 710;

                [OpActionName("GetGuardianTruckWeightPermission")]
                public const int GetGuardianTruckWeightPermission = 711;

                [OpActionName("GetTruckWeight")] 
                public const int GetTruckWeight = 712;

                [OpActionName("TrailerWeightPrompt")]
                public const int TrailerWeightPrompt = 713;

                [OpActionName("GetGuardianTrailerWeightPermission")]
                public const int GetGuardianTrailerWeightPermission = 714;

                [OpActionName("GetTrailerWeight")] 
                public const int GetTrailerWeight = 715;

                [OpActionName("WeightResultsValidation")]
                public const int WeightResultsValidation = 716;

                [OpActionName("OpenBarrierOut")]
                public const int OpenBarrierOut = 717;

                [OpActionName("CheckScaleEmpty")]
                public const int CheckScaleEmpty = 718;
            }
        }

        [OpControllerName("UnloadPointGuide")]
        public static class UnloadPointGuide
        {
            public const int Id = 8;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 801;
                
                [OpActionName("BindUnloadPoint")]
                public const int BindUnloadPoint = 802;
                
                [OpActionName("AddOpVisa")]
                public const int AddOpVisa = 803;
                
                [OpActionName("EntryAddOpVisa")]
                public const int EntryAddOpVisa = 804;
            }
        }

        [OpControllerName("UnloadPointGuide2")]
        public static class UnloadPointGuide2
        {
            public const int Id = 81;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 811;
                
                [OpActionName("BindUnloadPoint")]
                public const int BindUnloadPoint = 812;
                
                [OpActionName("AddOpVisa")]
                public const int AddOpVisa = 813;
            }
        }

        [OpControllerName("UnloadPointType1")]
        public static class UnloadPointType1
        {
            public const int Id = 9;

            public static class State
            {
                [OpActionName("Workstation")] 
                public const int Workstation = 901;
                
                [OpActionName("Idle")] 
                public const int Idle = 902;
                
                [OpActionName("GetTareValue")] 
                public const int GetTareValue = 903;
                
                [OpActionName("AddOperationVisa")] 
                public const int AddOperationVisa = 904;
                
                [OpActionName("AddChangeStateVisa")]
                public const int AddChangeStateVisa = 905;
            }
        }

        [OpControllerName("UnloadPointType2")]
        public static class UnloadPointType2
        {
            public const int Id = 92;

            public static class State
            {
                [OpActionName("Workstation")] 
                public const int Workstation = 9201;
                
                [OpActionName("Idle")]
                public const int Idle = 9202;

                [OpActionName("SelectAcceptancePoint")]
                public const int SelectAcceptancePoint = 9203;

                [OpActionName("AddOperationVisa")] 
                public const int AddOperationVisa = 9204;
                
                [OpActionName("AddChangeStateVisa")]
                public const int AddChangeStateVisa = 9205;
            }
        }

        [OpControllerName("LoadPointGuide")]
        public static class LoadPointGuide
        {
            public const int Id = 11;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 1101;
                
                [OpActionName("BindLoadPoint")]
                public const int BindLoadPoint = 1102;
                
                [OpActionName("AddOpVisa")]
                public const int AddOpVisa = 1103;
            }
        }

        [OpControllerName("LoadPointGuide2")]
        public static class LoadPointGuide2
        {
            public const int Id = 111;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 1111;
                
                [OpActionName("BindLoadPoint")]
                public const int BindLoadPoint = 1112;
                
                [OpActionName("AddOpVisa")] 
                public const int AddOpVisa = 1113;
            }
        }

        [OpControllerName("LoadCheckPoint")]
        public static class LoadCheckPoint
        {
            public const int Id = 12;

            public static class State
            {
                [OpActionName("Idle")] 
                public const int Idle = 1201;
            }
        }

        [OpControllerName("UnloadCheckPoint")]
        public static class UnloadCheckPoint
        {
            public const int Id = 19;

            public static class State
            {
                [OpActionName("Idle")] 
                public const int Idle = 1901;
            }
        }

        [OpControllerName("LoadPointType1")]
        public static class LoadPointType1
        {
            public const int Id = 15;

            public static class State
            {
                [OpActionName("Workstation")]
                public const int Workstation = 1501;
                
                [OpActionName("Idle")]
                public const int Idle = 1502;
                
                [OpActionName("GetTareValue")] 
                public const int GetTareValue = 1503;
                
                [OpActionName("AddOperationVisa")]
                public const int AddOperationVisa = 1504;
                
                [OpActionName("AddChangeStateVisa")]
                public const int AddChangeStateVisa = 1505;
            }
        }

        [OpControllerName("MixedFeedManage")]
        public static class MixedFeedManage
        {
            public const int Id = 16;

            public static class State
            {
                [OpActionName("Workstation")] 
                public const int Workstation = 1601;
                
                [OpActionName("Edit")]
                public const int Edit = 1602;
                
                [OpActionName("AddOperationVisa")]
                public const int AddOperationVisa = 1603;
            }
        }

        [OpControllerName("MixedFeedLoad")]
        public static class MixedFeedLoad
        {
            public const int Id = 17;

            public static class State
            {
                [OpActionName("Workstation")]
                public const int Workstation = 1701;
                
                [OpActionName("Idle")]
                public const int Idle = 1702;
                
                [OpActionName("AddOperationVisa")]
                public const int AddOperationVisa = 1703;
                
                [OpActionName("Cleanup")]
                public const int Cleanup = 1704;
                
                [OpActionName("AddCleanupVisa")]
                public const int AddCleanupVisa = 1705;
                
                [OpActionName("AddChangeStateVisa")]
                public const int AddChangeStateVisa = 1706;
            }
        }

        [OpControllerName("MixedFeedGuide")]
        public static class MixedFeedGuide
        {
            public const int Id = 18;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 1801;
                
                [OpActionName("BindLoadPoint")]
                public const int BindLoadPoint = 1802;
                
                [OpActionName("AddOpVisa")]
                public const int AddOpVisa = 1803;
            }
        }

        [OpControllerName("SecurityReview")]
        public static class SecurityReview
        {
            public const int Id = 31;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 3101;
                
                [OpActionName("AddOperationVisa")] 
                public const int AddOperationVisa = 3102;
            }
        }

        [OpControllerName("DriverCheckIn")]
        public static class DriverCheckIn
        {
            public const int Id = 55;

            public static class State
            {
                [OpActionName("Idle")]
                public const int Idle = 551;
                
                [OpActionName("AddDriver")] 
                public const int AddDriver = 552;
                
                [OpActionName("DriverInfoCheck")] 
                public const int DriverInfoCheck = 553;
                
                [OpActionName("RegistrationConfirm")] 
                public const int RegistrationConfirm = 554;
            }
        }
    }
}