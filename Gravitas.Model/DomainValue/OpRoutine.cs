using System.Diagnostics.CodeAnalysis;
using Gravitas.Infrastructure.Common.Attribute;

// ReSharper disable once CheckNamespace
namespace Gravitas.Model {

	public static partial class Dom {

		public static class OpRoutine { 

			public static class Processor {
				public const int CoreService = 1;
				public const int WebUI = 2;
			}


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

                [SuppressMessage("ReSharper", "InconsistentNaming")]
                public static class Transition
                {
                    public const int Idle__CentralLabolatorySampleBindTray = 1300001;
                    public const int CentralLabSampleBindTray__CentralLabSampleAddOpVisa = 1300002;
                    public const int CentralLabSampleAddOpVisa__Idle = 1300003;
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

                [SuppressMessage("ReSharper", "InconsistentNaming")]
                public static class Transition
                {

                    public const int Idle__PrintDataDisclose = 140001;
                    public const int PrintDataDisclose__Idle = 140002;
                    public const int PrintDataDisclose__PrintAddOpVisa = 140003;
                    public const int PrintDataDisclose__PrintCollisionInitVisa = 140004;
                    public const int Idle_AddSample = 140006;
                    public const int AddSample_Idle = 140007;
                    public const int PrintCollisionInit_PrintDataDisclose = 140009;
                    public const int PrintDocument_Idle = 140011;
	                public const int AddSample_AddSampleVisa = 140012;
	                public const int AddSampleVisa__PrintLabel = 140013;
	                public const int PrintLabel_Idle = 140014;
	                public const int PrintAddOpVisa_PrintDocument = 140015;
	                public const int PrintDataDisclose__PrintCollisionStartVisa = 140016;
	                public const int PrintCollisionStartVisa__Idle = 140017;
	                public const int PrintCollisionInitVisa__PrintCollisionInit = 140018;
	                public const int PrintCollisionInit__Idle = 140019;
	                public const int PrintCollisionInitVisa__PrintDataDisclose = 140020;
	                public const int PrintAddOpVisa__PrintDataDisclose = 140021;
	                public const int PrintCollisionStartVisa__PrintDataDisclose = 140022;
                }
            }



            [OpControllerName("SingleWindow")]
			public static class SingleWindow {

				public const int Id = 1;

				public static class State {

					[OpActionName("Idle")]
					public const int Idle = 101;
					[OpActionName("GetTicket")]
					public const int GetTicket = 102;
					[OpActionName("ShowTicketMenu")]
					public const int ShowTicketMenu = 103;
					[OpActionName("ContainerCloseAddOpVisa")]
					public const int ContainerCloseAddOpVisa = 104;

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

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					
					//Common
					public const int Idle__GetTicket_Core = 100001;
					public const int Idle__GetTicket_Web = 100002;
					public const int GetTicket__ShowTicketMenu = 100003;
					public const int GetTicket__ContainerCloseAddOpVisa = 100004;
					public const int GetTicket__Idle = 100005;
					public const int ShowTicketMenu__GetTicket = 100006;
					public const int ShowTicketMenu__Idle = 100007;
					public const int ContainerCloseAddOpVisa__Idle = 100008;
					public const int ContainerCloseAddOpVisa__GetTicket = 100009;
					public const int ContainerCloseAddOpVisa__ShowTicketMenu = 100010;
					public const int ShowTicketMenu__SupplyChangeAddOpVisa = 100011;
					public const int SupplyChangeAddOpVisa__ShowTicketMenu = 100012;
					public const int ShowTicketMenu__DivideTicketAddOpVisa = 100013;
					public const int DivideTicketAddOpVisa__ShowTicketMenu = 100014;
					public const int GetTicket__DeleteTicketAddOpVisa = 100015;
					public const int DeleteTicketAddOpVisa__GetTicket = 100016;
					public const int DeleteTicketAddOpVisa__GetTicketCore = 100017;
					
					//Edit branch
					public const int ShowTicketMenu__EditTicketForm = 100101;
					public const int EditTicketForm__EditAddOpVisa = 100102;
					public const int EditAddOpVisa__EditPostApiData = 100103;
					public const int EditPostApiData__ShowTicketMenu = 100104;
					public const int EditTicketForm__ShowTicketMenu = 100105;

					public const int GetTicket__EditGetApiData = 100111;
					public const int ShowTicketMenu__EditGetApiData = 100112;
					public const int EditGetApiData__EditTicketForm = 100113;
					public const int EditGetApiData__ShowTicketMenu = 100114;
					public const int EditPostApiData__EditTicketForm = 100115;
					public const int EditAddOpVisa__EditTicketForm = 100121;
					
					//Route branch
					public const int ShowTicketMenu__RouteEditData = 100201;
					public const int RouteEditData__RouteAddOpVisa = 100202;
					public const int RouteAddOpVisa__ShowTicketMenu = 100211;
					public const int RouteEditData__ShowTicketMenu = 100212;
					public const int RouteAddOpVisa__RouteEditData = 100213;

					//Close branch
					public const int ShowTicketMenu__CloseAddOpVisa = 100301;
					public const int CloseAddOpVisa__ClosePostApiData = 100302;
					public const int ClosePostApiData__ShowTicketMenu = 100303;
					public const int CloseAddOpVisa__ShowTicketMenu = 100311;
				}
			}

			[OpControllerName("Queue")]
			public static class Queue {

				public const int Id = 2;

				public static class State {

					[OpActionName("Idle")]
					public const int Idle = 201;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {

				}
			}

			[OpControllerName("SecurityIn")]
			public static class SecurityIn {
				public const int Id = 3;

				public static class State {
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

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {

					public const int Idle__BindLongRangeRfid = 300001;
					public const int BindLongRangeRfid__AddOperationVisa = 300002;
					public const int AddOperationVisa__OpenBarrier = 300003;
					public const int OpenBarrier__GetCamSnapshot = 300004;
					public const int GetCamSnapshot__Idle = 300005;
				    public const int Idle__AddOperationVisa = 300006;
				    public const int Idle__CheckOwnTransport = 300007;
				    public const int CheckOwnTransport__Idle = 300008;
				    public const int CheckOwnTransport__AddOperationVisa = 300009;
				    public const int BindLongRangeRfid__Idle = 300010;
				    public const int AddOperationVisa__Idle = 300011;
				}
			}

			[OpControllerName("SecurityOut")]
			public static class SecurityOut {
				public const int Id = 4;

				public static class State {
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
					[OpActionName("GetCamSnapshot")]
					public const int GetCamSnapshot = 408;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {

					public const int Idle__ShowOperationsList = 400001;
					public const int ShowOperationsList__EditStampList = 400002;
					public const int EditStampList__AddRouteControlVisa = 400003;
					public const int AddRouteControlVisa__AddTransportInspectionVisa = 400004;
					public const int AddTransportInspectionVisa__OpenBarrier = 400005;
					public const int OpenBarrier__GetCamSnapshot = 400006;
					public const int GetCamSnapshot__Idle = 400007;
					public const int Idle__CheckOwnTransport = 400008;
					public const int CheckOwnTransport__AddRouteControlVisa = 400009;
					public const int CheckOwnTransport__Idle = 400010;
					public const int EditStampList__ShowOperationsList = 400203;
					public const int Idle__AddTransportInspectionVisa = 400204;
				}
			}

			[OpControllerName("LaboratoryIn")]
			public static class LabolatoryIn {
				public const int Id = 5;

				public static class State {
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

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {

					public const int Idle__SampleReadTruckRfid = 500001;
					public const int SampleReadTruckRfid__SampleBindTray = 500002;
					public const int SampleBindTray__SampleBindAnalysisTray = 500003;
					public const int SampleBindAnalysisTray__SampleAddOpVisa = 500004;
					public const int SampleAddOpVisa__Idle = 500005;

					public const int Idle__ResultReadTrayRfid = 500101;
					public const int ResultReadTrayRfid__ResultEditAnalysis = 500102;
					public const int ResultEditAnalysis__ResultAddOpVisa = 500103;
					public const int ResultAddOpVisa__Idle = 500104;

					public const int Idle__PrintReadTrayRfid = 500201;
					public const int PrintReadTrayRfid__PrintAnalysisResults = 500202;
					public const int PrintAnalysisResults__PrintAnalysisAddOpVisa = 500203;
					public const int PrintAnalysisAddOpVisa__PrintDataDisclose = 500204;
					public const int PrintDataDisclose__PrintCollisionInit = 500205;
					public const int PrintDataDisclose__PrintAddOpVisa = 500206;
					public const int Idle__PrintCollisionManage = 500207;
					public const int PrintCollisionInit__Idle = 500208;
					public const int PrintCollisionManage__PrintAddOpVisa = 500209;
					public const int PrintAddOpVisa__Idle = 500210;
					public const int PrintCollisionManage__Idle = 500211;
					public const int PrintCollisionInit__PrintDataDisclose = 500212;
					public const int PrintCollisionManage__SampleReadTruckRfid = 500213;
					public const int PrintAddOpVisa__PrintLaboratoryProtocol = 500214;
					public const int PrintDataDisclose__PrintLaboratoryProtocol = 500215;
					public const int PrintLaboratoryProtocol__Idle = 500216;

					public const int SampleReadTruckRfid__Idle = 500301;
					public const int SampleBindTray__Idle = 500302;
					public const int ResultReadTrayRfid__Idle = 500303;
					public const int PrintReadTrayRfid__Idle = 500304;
					public const int PrintAnalysisAddOpVisa__PrintAnalysisResults = 500305;
					public const int Idle_PrintCollisionInit = 500306;

				}
			}

			[OpControllerName("LabolatoryOut")]
			public static class LabolatoryOut {
				public const int Id = 6;

				public static class State {
					[OpActionName("Idle")]
					public const int Idle = 601;
				}
			}

			[OpControllerName("Weighbridge")]
			public static class Weighbridge {
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

			    [SuppressMessage("ReSharper", "InconsistentNaming")]
			    public static class Transition
			    {

			        public const int Idle__GetScaleZero = 700001;
			        public const int GetScaleZero__OpenBarrierIn = 700002;
			        public const int OpenBarrierIn__CheckScaleNotEmpty = 700003;
			        public const int CheckScaleNotEmpty__GetTicketCard = 700004;
			        public const int GetTicketCard__DriverTrailerEnableCheck = 700005;
			        public const int GetTicketCard__GuardianCardPrompt = 700006;
			        public const int GetTicketCard__Idle = 700007;
			        public const int DriverTrailerEnableCheck__GuardianCardPrompt = 700008;
			        public const int DriverTrailerEnableCheck__TruckWeightPrompt = 700009;
			        public const int GuardianCardPrompt__GuardianTruckVerification = 700010;
			        public const int GuardianTruckVerification__GuardianTrailerEnableCheck = 700011;
			        public const int GuardianTruckVerification__Idle = 700012;
			        public const int GuardianTrailerEnableCheck__TruckWeightPrompt = 700013;
			        public const int TruckWeightPrompt__GetGuardianTruckWeightPermission = 700014;
			        public const int TruckWeightPrompt__Idle = 700015;
			        public const int TruckWeightPrompt__GetTruckWeight = 700016;
                    public const int GetTruckWeight__TrailerWeightPrompt = 700017;
			        public const int GetTruckWeight__WeightResultsValidation = 700018;
			        public const int TrailerWeightPrompt__GetGuardianTrailerWeightPermission = 700019;
			        public const int TrailerWeightPrompt__Idle = 700020;
			        public const int TrailerWeightPrompt__GetTrailerWeight = 700021;
                    public const int GetTrailerWeight__WeightResultsValidation = 700022;
			        public const int WeightResultsValidation__OpenBarrierOut = 700023;
			        public const int WeightResultsValidation__Idle = 700024;
			        public const int OpenBarrierOut__CheckScaleEmpty = 700025;
			        public const int CheckScaleEmpty__Idle = 700026;

			        public const int TruckWeightPrompt__OpenBarrierOut = 700027;
			        public const int TrailerWeightPrompt__OpenBarrierOut = 700028;
			        public const int GetGuardianTruckWeightPermission__GetTruckWeight = 700029;
			        public const int GuardianTruckVerification__TruckWeightPrompt = 700030;
			        public const int GetGuardianTrailerWeightPermission__GetTrailerWeight = 700031;
			        public const int GuardianTruckVerification__GetTruckWeight = 700032;

			        public const int GetTicketCard__TruckWeightPrompt = 700033;

			        public const int GetTicketCard__Idle_WebUi = 700034;
			        public const int CheckScaleNotEmpty__Idle_WebUi = 700035;
			        public const int CheckScaleNotEmpty__Idle = 700036;

			        public const int DriverTrailerEnableCheck__Idle_Core = 700037;
			        public const int TruckWeightPrompt__Idle_Core = 700038;
			        public const int TrailerWeightPrompt__Idle_Core = 700039;
			        public const int GetTruckWeight__OpenBarrierOut = 700040;
			        public const int GetTrailerWeight__OpenBarrierOut = 700041;
			        public const int DriverTrailerEnableCheck__Idle = 700042;
			        public const int GuardianCardPrompt__Idle = 700043;
			        public const int GetGuardianTruckWeightPermission__Idle = 700044;
			        public const int GetTruckWeight__Idle = 700045;
			        public const int GetGuardianTrailerWeightPermission__Idle = 700046;
			        public const int GetTrailerWeight__Idle = 700047;
			        public const int GuardianTruckVerification__Idle_Core = 700048;


                }
			}

			[OpControllerName("UnloadPointGuide")]
			public static class UnloadPointGuide {
				public const int Id = 8;

				public static class State {
					[OpActionName("Idle")]
					public const int Idle = 801;
					[OpActionName("BindUnloadPoint")]
					public const int BindUnloadPoint = 802;
					[OpActionName("AddOpVisa")]
					public const int AddOpVisa = 803;
					[OpActionName("EntryAddOpVisa")]
					public const int EntryAddOpVisa = 804;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Idle__BindUnloadPoint = 800001;
					public const int BindUnloadPoint__AddOpVisa = 800002;
					public const int AddOpVisa__Idle = 800003;
					public const int BindUnloadPoint__Idle = 800004;
					public const int EntryAddOpVisa__Idle = 800005;
					public const int Idle__EntryAddOpVisa = 800006;
					public const int EntryAddOpVisa__IdleCore = 800007;
				}
			}
			
			[OpControllerName("UnloadPointGuide2")]
			public static class UnloadPointGuide2 {
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

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Idle__BindUnloadPoint = 800011;
					public const int BindUnloadPoint__AddOpVisa = 800012;
					public const int AddOpVisa__Idle = 800013;
					public const int BindUnloadPoint__Idle = 800014;
					public const int Idle__EntryAddOpVisa = 800016;
				}
			}

			[OpControllerName("UnloadPointType1")]
			public static class UnloadPointType1 {
				public const int Id = 9;

				public static class State {
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

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Workstation__Idle = 900001;
					public const int Idle__AddOperationVisa = 900002;
					public const int AddOperationVisa__Idle = 900003;
					public const int Idle__Workstation = 900004;
					public const int AddOperationVisa__Workstation = 900005;
					public const int Workstation__Idle__Core = 900006;
					public const int Idle__AddChangeStateVisa = 900007;
					public const int AddChangeStateVisa__Workstation = 900008;
					public const int AddChangeStateVisa__Idle = 900009;
					public const int Idle__GetTareValue = 900010;
					public const int GetTareValue__Idle = 900011;
				}
			}
			
			[OpControllerName("UnloadPointType2")]
			public static class UnloadPointType2 {
				public const int Id = 92;

				public static class State {
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

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Workstation__Idle = 920001;
					public const int Idle__SelectAcceptancePoint = 920002;
					public const int SelectAcceptancePoint__AddOperationVisa = 920003;
					public const int AddOperationVisa__Idle = 920004;
					public const int AddOperationVisa__Idle__Core = 920005;
					public const int Idle__AddOperationVisa = 920006;
					public const int Idle__Workstation = 920007;
					public const int Workstation__Idle__Core = 920008;
					public const int AddOperationVisa__SelectAcceptancePoint = 920009;
					public const int AddOperationVisa__Workstation = 920010;
					public const int Idle__AddChangeStateVisa = 920011;
					public const int AddChangeStateVisa__Idle = 920012;
					public const int AddChangeStateVisa__Workstation = 920013;
					public const int SelectAcceptancePoint__Idle = 920014;
					public const int SelectAcceptancePoint__Idle__Core = 920015;
					public const int Idle__Workstation__Core = 920016;
					public const int AddChangeStateVisa__Idle__Core = 920017;
				}
			}

			[OpControllerName("LoadPointGuide")]
			public static class LoadPointGuide {
				public const int Id = 11;

				public static class State {
					[OpActionName("Idle")]
					public const int Idle = 1101;
					[OpActionName("BindLoadPoint")]
					public const int BindLoadPoint = 1102;
					[OpActionName("AddOpVisa")]
					public const int AddOpVisa = 1103;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Idle__BindUnloadPoint = 1100001;
					public const int BindUnloadPoint__AddOpVisa = 1100002;
					public const int AddOpVisa__Idle = 1100003;
					public const int BindLoadPoint__Idle = 1100004;
					public const int AddOpVisa__BindLoadPoint = 1100005;
				}
			}
			
			[OpControllerName("LoadPointGuide2")]
			public static class LoadPointGuide2 {
				public const int Id = 111;

				public static class State {
					[OpActionName("Idle")]
					public const int Idle = 1111;
					[OpActionName("BindLoadPoint")]
					public const int BindLoadPoint = 1112;
					[OpActionName("AddOpVisa")]
					public const int AddOpVisa = 1113;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Idle__BindUnloadPoint = 1100011;
					public const int BindUnloadPoint__AddOpVisa = 1100012;
					public const int AddOpVisa__Idle = 1100013;
					public const int BindLoadPoint__Idle = 1100014;
					public const int AddOpVisa__BindLoadPoint = 1100015;
				}
			}
			
			[OpControllerName("LoadCheckPoint")]
			public static class LoadCheckPoint {
				public const int Id = 12;

				public static class State
				{
					[OpActionName("Idle")] public const int Idle = 1201;
//					[OpActionName("GetTareValue")] public const int GetTareValue = 1202;
//					[OpActionName("AddOperationVisa")] public const int AddOperationVisa = 1203;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
//					public const int Idle__GetTareValue = 1200001;
//					public const int GetTareValue__AddOperationVisa = 1200002;
//					public const int AddOperationVisa__Idle = 1200003;
//					public const int AddOperationVisa__GetTareValue = 1200004;
				}
			}
			
			[OpControllerName("UnloadCheckPoint")]
			public static class UnloadCheckPoint {
				public const int Id = 19;

				public static class State
				{
					[OpActionName("Idle")] public const int Idle = 1901;
//					[OpActionName("GetTareValue")] public const int GetTareValue = 1902;
//					[OpActionName("AddOperationVisa")] public const int AddOperationVisa = 1903;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
//					public const int Idle__GetTareValue = 1900001;
//					public const int GetTareValue__AddOperationVisa = 1900002;
//					public const int AddOperationVisa__Idle = 1900003;
//					public const int AddOperationVisa__GetTareValue = 1900004;
				}
			}
			
			[OpControllerName("LoadPointType1")]
			public static class LoadPointType1 {
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

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Workstation__Idle = 1500001;
					public const int Idle__AddOperationVisa = 1500002;
					public const int AddOperationVisa__Idle = 1500003;
					public const int Idle__Workstation = 1500004;
					public const int AddOperationVisa__Workstation = 1500005;
					public const int Workstation__Idle__Core = 1500006;
					public const int Idle__AddChangeStateVisa = 1500007;
					public const int AddChangeStateVisa__Workstation = 1500008;
					public const int AddChangeStateVisa__Idle = 1500009;
					public const int Idle__GetTareValue = 1500010;
					public const int GetTareValue__Idle = 1500011;
				}
			}
			
			[OpControllerName("MixedFeedManage")]
			public static class MixedFeedManage {
				public const int Id = 16;

				public static class State {
					[OpActionName("Workstation")]
					public const int Workstation = 1601;
					[OpActionName("Edit")]
					public const int Edit = 1602;
					[OpActionName("AddOperationVisa")]
					public const int AddOperationVisa = 1603;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Workstation__Edit = 1600001;
					public const int Edit__AddOperationVisa = 1600002;
					public const int AddOperationVisa__Workstation = 1600003;
					public const int Edit__Workstation = 1600004;
				}
			}
			
			[OpControllerName("MixedFeedLoad")]
			public static class MixedFeedLoad {
				public const int Id = 17;

				public static class State
				{
					[OpActionName("Workstation")] public const int Workstation = 1701;
					[OpActionName("Idle")] public const int Idle = 1702;
					[OpActionName("AddOperationVisa")] public const int AddOperationVisa = 1703;
					[OpActionName("Cleanup")] public const int Cleanup = 1704;
					[OpActionName("AddCleanupVisa")] public const int AddCleanupVisa = 1705;
					[OpActionName("AddChangeStateVisa")] public const int AddChangeStateVisa = 1706;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Workstation__Idle = 1700001;
					public const int Idle__AddOperationVisa = 1700002;
					public const int AddOperationVisa__Idle = 1700003;
					public const int Idle__Workstation = 1700004;
					public const int AddOperationVisa__Workstation = 1700005;
					public const int Workstation__Idle__Core = 1700006;
					public const int Workstation__Cleanup = 1700007;
					public const int Cleanup__AddCleanupVisa = 1700008;
					public const int Cleanup__Workstation = 1700009;
					public const int AddCleanupVisa__Workstation = 1700010;
					public const int Workstation__AddOperationVisa = 1700011;
					public const int Idle__AddOperationVisaCore = 1700012;
					public const int Idle__AddChangeStateVisa = 1700013;
					public const int AddChangeStateVisa__Workstation = 1700014;
					public const int AddChangeStateVisa__Idle = 1700015;
				}
			}
			
			[OpControllerName("MixedFeedGuide")]
			public static class MixedFeedGuide {
				public const int Id = 18;

				public static class State {
					[OpActionName("Idle")]
					public const int Idle = 1801;
					[OpActionName("BindLoadPoint")]
					public const int BindLoadPoint = 1802;
					[OpActionName("AddOpVisa")]
					public const int AddOpVisa = 1803;
				}

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition {
					public const int Idle__BindUnloadPoint = 1800001;
					public const int BindUnloadPoint__AddOpVisa = 1800002;
					public const int AddOpVisa__Idle = 1800003;
					public const int BindLoadPoint__Idle = 1800004;
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

				[SuppressMessage("ReSharper", "InconsistentNaming")]
				public static class Transition
				{
					public const int Idle__AddOperationVisa = 3100001;
					public const int AddOperationVisa__Idle = 3100002;
					public const int AddOperationVisa__Idle__Core = 3100003;
				}
			}
		}
	}
}
