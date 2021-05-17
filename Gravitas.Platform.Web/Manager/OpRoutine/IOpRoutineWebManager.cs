using System;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.MixedFeedManage;
using Gravitas.Platform.Web.ViewModel.OpRoutine.LoadPointGuide2;
using Gravitas.Platform.Web.ViewModel.OpRoutine.MixedFeedGuide;
using Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide;
using Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide2;

namespace Gravitas.Platform.Web.Manager.OpRoutine {

	public interface IOpRoutineWebManager {
		//TODO: Split methods to separate manager for each routine

		//SingleWindow
		void SingleWindow_GetTicket_New(long nodeId, string supplyBarCode);
		void SingleWindow_GetTicket_Change(long nodeId, string supplyCode);
		bool SingleWindow_GetTicket_Back(long nodeId);
		void SingleWindow_GetTicket_Detail(long nodeId, long ticketId);
		bool SingleWindow_GetTicket_Close(long nodeId);
		void SingleWindow_GetTicket_Delete(long nodeId, long ticketId);
		void SingleWindow_ShowTicketMenu_Exit(long nodeId);
		bool SingleWindow_Idle_SelectTicketContainer(long nodeId, long ticketContainerId);
		void SingleWindow_AddOperationVisa_Back(long nodeIdValue);
		SingleWindowVms.SingleWindowOpDataDetailVm SingleWindow_EditTicketForm_GetData(long nodeId);
		bool SingleWindow_ShowTicketMenu_Back(long nodeId);
		bool SingleWindow_ShowTicketMenu_Route(long nodeId);
		bool SingleWindow_ShowTicketMenu_Edit(long nodeId);
		bool SingleWindow_ShowTicketMenu_Commit(long nodeId);
		byte[] SingleWindow_ShowTicketMenu_PrintDoc(long nodeId, string printoutTypeId);
		bool SingleWindow_EditTicketForm_Save(SingleWindowVms.SingleWindowOpDataDetailVm data);
		bool SingleWindow_EditTicketForm_Back(long nodeId);

		bool SingleWindow_EditAddOpVisa_Back(long nodeId);

		bool SingleWindow_RouteEditData_Back(long nodeId);
		bool SingleWindow_RouteEditData_Save(SingleWindowVms.Route data);
		SingleWindowVms.Route SingleWindow_RouteEditDataVm(long nodeId);

	    SingleWindowVms.ProtocolPrintoutVm SingleWindow_ProtocolPrintout_GetVm(long nodeId, long? ticketIdExt = null);
	    SingleWindowVms.RoutePrintoutVm SingleWindow_RoutePrintout_GetVm(long nodeId, long ticketId);
		SingleWindowVms.TechnologicalRoutePrintoutVm SingleWindow_TechnologicalRoutePrintout_GetVm(long nodeId);

		void SingleWindow_DivideTicket(long nodeId, int newWeightValue);
	    void SingleWindow_DeleteTicketAddOpVisa_Back(long nodeId);



		bool SingleWindow_RouteAddOpVisa_Back(long nodeId);
		
		// SecurityIn
		bool SecurityIn_Entry_Cancelation(long nodeId);
		void SecurityIn_CheckOwnTransport_Next(long nodeId);
		void SecurityIn_CheckOwnTransport_Reject(long nodeId);

		// SecurityOut
		SecurityOutVms.ShowOperationsListVm SecurityOut_ShowOperationsList_GetData(long nodeId);
		bool SecurityOut_ShowOperationsList_Confirm(long nodeId, bool isConfirmed);
		bool SecurityOut_EditStampList_Back(long nodeId);
		bool SecurityOut_EditStampList(SecurityOutVms.EditStampListVm vm);
		void SecurityOut_CheckOwnTransport_Next(long nodeId);
		void SecurityOut_CheckOwnTransport_Reject(long nodeId);

        //CentralLaboratoryProcess
	    CentralLaboratoryProcess.IdleVm CentralLaboratoryProcess_Idle_GetVm(long nodeId);
	    void CentralLaboratoryProcess_Idle_SelectSample(long nodeId, Guid opDataId);
        CentralLaboratoryProcess.PrintDataDiscloseVm CentralLaboratoryProcess_PrintDataDisclose_GetVm(long nodeId);
		void CentralLaboratoryProcess_PrintCollisionInit_Send(long nodeId, string comment);
	    void CentralLaboratoryProcess_PrintDataDisclose_Back(long nodeId);
	    void CentralLaboratoryProcess_Idle_AddSample(long nodeId);
	    void CentralLaboratoryProcess_Idle_AddSample_Back(long nodeId);
        bool CentralLaboratoryProcess_PrintDataDisclose_Confirm(long nodeId);
	    CentralLaboratoryProcess.PrintCollisionInitVm CentralLaboratory_GetCollisionInitVm(long nodeId);
	    CentralLaboratoryProcess.PrintDocumentVm CentralLaboratory_GetPrintDocumentVm(long nodeId);
        bool CentralLaboratoryProcess_SendToCollision(CentralLaboratoryProcess.PrintCollisionInitVm vm);
	    bool CentralLaboratoryProcess_PrintCollisionInit_Return(long nodeId);
	    bool CentralLaboratoryProcess_PrintCollisionManage_ReturnToCollectSamples(long nodeId, string comment);
	    void CentralLaboratoryProcess_PrintDocument_Confirm(long nodeId);
	    CentralLaboratoryProcess.CentralLabolatorySamplePrintoutVm CentralLaboratory_GetSamplePrintoutVm(long ticketId);
		void CentralLaboratoryProcess_PrintDataDisclose_MoveWithLoad(long nodeId);
		void CentralLaboratoryProcess_PrintDataDisclose_UnloadToStoreWithLoad(long nodeId);
		void CentralLaboratoryProcess_PrintDataDisclose_ToCollisionInit(long nodeId);
		CentralLaboratoryProcess.CentralLaboratoryLabelVm CentralLaboratory_GetLabelPrintoutVm(Guid id);
		void CentralLaboratoryProcess_PrintDataDisclose_DeleteFile(long nodeId);
		void CentralLaboratoryProcess_PrintCollisionVisa_Back(long nodeId);
		void CentralLaboratoryProcess_PrintLabel_Confirm(long nodeId);
		void CentralLaboratoryProcess_PrintAddOpVisa_Back(long nodeId);
		void CentralLaboratoryProcess_PrintDataDiscloseVisa_Back(long nodeId);

		//LabolatoryIn
        bool LaboratoryIn_Idle_СollectSample(long nodeId);
		bool LaboratoryIn_Idle_EditAnalysisResult(long nodeId);
		bool LaboratoryIn_Idle_PrintAnalysisResult(long nodeId);
		bool LaboratoryIn_Idle_PrintCollisionInit(long nodeId, int ticketId);
		bool LaboratoryIn_Idle_SelectTicketContainer(long nodeId, long ticketContainerId);
		bool LaboratoryIn_SampleReadTruckRfid_Back(long nodeId);
		bool LaboratoryIn_SampleBindTray_Back(long nodeId);
		LaboratoryInVms.SampleBindAnalysisTrayVm LaboratoryIn_SampleBindAnalysisTray_GetVmData(long nodeId);
		bool LaboratoryIn_SampleBindAnalysisTray_Next(long nodeId);
		bool LaboratoryIn_SampleBindAnalysisTray(LaboratoryInVms.SampleBindAnalysisTrayVm vmData);
		bool LaboratoryIn_ResultReadTrayRfid_Back(long nodeId);
		LaboratoryInVms.ResultEditAnalysisVm LaboratoryIn_ResultEditAnalysis_GetVm(long nodeId);
		bool LaboratoryIn_ResultEditAnalysis_Back(long nodeId);
		bool LaboratoryIn_ResultEditAnalysis_Save(LaboratoryInVms.LabFacelessOpDataComponentDetailVm vm);
		bool LaboratoryIn_ResultEditAnalysis_SaveFromDevice(DeviceStateVms.LabAnalyserStateDialogVm vm);
		bool LaboratoryIn_PrintReadTrayRfid_Back(long nodeId);
		LaboratoryInVms.PrintAnalysisResultsVm LaboratoryIn_PrintAnalysisResults_GetVmData(long nodeId);
		bool LaboratoryIn_PrintAnalysisResult_Save(LaboratoryInVms.LabFacelessOpDataDetailVm vm);
		bool LaboratoryIn_PrintAnalysisResult_Back(long nodeId);
		LaboratoryInVms.PrintDataDiscloseVm LaboratoryIn_PrintDataDisclose_GetVm(long nodeId);
		bool LaboratoryIn_PrintDataDisclose_Confirm(long nodeId, bool isConfirmed);
		bool LaboratoryIn_PrintCollisionManage_SetReturnRoute(long nodeId, string indexRefundReason);
		bool LaboratoryIn_PrintCollisionManage_ReturnToCollectSamples(long nodeId);
		bool LaboratoryIn_SendToCollision(LaboratoryInVms.PrintCollisionInitVm vm);
		LaboratoryInVms.PrintCollisionInitVm LaboratoryIn_GetCollisionInitVm(long nodeId);
		LaboratoryInVms.PrintCollisionManageVm GetLaboratoryIn_PrintCollisionManageVm(long nodeId);
		bool LaboratoryIn_PrintCollisionInit_Return(long nodeId);
		LaboratoryInVms.PrintLaboratoryProtocol PrintLaboratoryProtocol_GetVm(long nodeId);
		void PrintLaboratoryProtocol_Next(long nodeId);
		LaboratoryInVms.SamplePrintoutVm LaboratoryIn_SamplePrintout_GetVm(Guid opDataId);
		void LaboratoryIn_PrintCollisionManage_SetReloadRoute(long nodeId);

		// Weighbridge
		WeightbridgeVms.BaseWeightPromptVm Weighbridge_GetWeightPrompt_GetData(long nodeId);
	    void Weighbridge_DriverTrailerAccepted(long nodeId, bool isTrailerAccepted);
	    void Weighbridge_GetWeightPrompt_Process(long nodeId, bool isWeightAccepted, bool isTruckWeighting);
		void Weighbridge_ResetWeighbridge(long nodeId);
	    void Weighbridge_GuardianTruckVerification_Process(long nodeId, bool isWeighingAllowed);
	    void Weighbridge_GuardianTrailerEnable_Process(long nodeId, bool isTrailerEnabled);
		WeightbridgeVms.OpenBarrierOutVm Weighbridge_OpenBarrierOutVm(long nodeIdValue);
	    WeightbridgeVms.TruckWeightPromptVm WeighbridgeGetTruckWeightPromptVm(long nodeId);
	    WeightbridgeVms.GetGuardianTruckWeightPermissionVm Weighbridge__GetGuardianTruckWeightPermissionVm(long nodeId);
        WeightbridgeVms.TrailerWeightPromptVm Weighbridge__GetTrailerWeightPromptVm(long nodeId);
        WeightbridgeVms.GetGuardianTrailerWeightPermissionVm Weighbridge__GetGuardianTrailerWeightPermissionVm(long nodeId);
        
	    // UnloadPointType1
        bool UnloadPointType1_ConfirmOperation_Next(long nodeId);
		bool UnloadPointType1_AddOperationVisa_Back(long nodeId);
		bool UnloadPointType1_IdleWorkstation_Back(long nodeId);
		bool UnloadPointType1_Workstation_Process(long nodeId);
		UnloadPointType1Vms.IdleVm UnloadPointType1_IdleVm(long nodeId);
		void UnloadPointType1_Workstation_SetNodeActive(long nodeId);
		void UnloadPointType1_AddChangeStateVisa_Back(long nodeId);
		void UnloadPointType1_Idle_ChangeState(long nodeId);
		void UnloadPointType1_Idle_GetTareValue(long nodeId);

		//UnloadPointType2

		void UnloadPointType2_Idle_ChangeState(long nodeId);
		void UnloadPointType2_AddChangeStateVisa_Back(long nodeId);
		void UnloadPointType2_Workstation_SetNodeActive(long nodeId);
		UnloadPointType2Vms.IdleVm UnloadPointType2_IdleVm(long nodeId);
		bool UnloadPointType2_Workstation_Process(long nodeId);
		bool UnloadPointType2_IdleWorkstation_Back(long nodeId);
		bool UnloadPointType2_AddOperationVisa_Back(long nodeId);
		bool UnloadPointType2_ConfirmOperation_Next(long nodeId, string acceptancePointCode);



		// LoadPointType1
		bool LoadPointType1_ConfirmOperation_Next(long nodeId);
		bool LoadPointType1_AddOperationVisa_Back(long nodeId);
		bool LoadPointType1_IdleWorkstation_Back(long nodeId);
		bool LoadPointType1_Workstation_Process(long nodeId);
		LoadPointType1Vms.IdleVm LoadPointType1_IdleVm(long nodeId);
		void LoadPointType1_Workstation_SetNodeActive(long nodeId);
		void LoadPointType1_ConfirmOperation_Cancel(long nodeId);
		void LoadPointType1_ConfirmOperation_Reject(long nodeId);
		void LoadPointType1_AddChangeStateVisa_Back(long nodeId);
		void LoadPointType1_Idle_ChangeState(long nodeId);
		void LoadPointType1_Idle_GetTareValue(long nodeId);

		// UnloadPointGuide
		bool UnloadPointGuide_Idle_SelectTicketContainer(long nodeId, long ticketContainerId);
		UnloadPointGuideVms.BindUnloadPointVm UnloadPointGuide_BindUnloadPoint_GetVm(long nodeId);
		bool UnloadPointGuide_BindUnloadPoint_Back(long nodeId);
		bool UnloadPointGuide_BindUnloadPoint_Next(UnloadPointGuideVms.BindUnloadPointVm vm);
		void UnloadPointGuide_Idle_AskFromQueue(long nodeId, long ticketContainerId);
		bool UnloadPointGuide_Idle_AskFromQueue_Back(long nodeId);
		
		// UnloadPointGuide2
		bool UnloadPointGuide2_Idle_SelectTicketContainer(long nodeId, long ticketContainerId);
		UnloadPointGuide2Vms.BindUnloadPointVm UnloadPointGuide2_BindUnloadPoint_GetVm(long nodeId);
		bool UnloadPointGuide2_BindUnloadPoint_Back(long nodeId);
		bool UnloadPointGuide2_BindUnloadPoint_Next(UnloadPointGuide2Vms.BindUnloadPointVm vm);

		// LoadPointGuide
		bool LoadPointGuide_Idle_SelectTicketContainer(long nodeId, long ticketContainerId);
		LoadPointGuideVms.BindDestPointVm LoadPointGuide_BindLoadPoint_GetVm(long nodeId);
		bool LoadPointGuide_BindLoadPoint_Back(long nodeId);
		bool LoadPointGuide_BindLoadPoint_Next(LoadPointGuideVms.BindDestPointVm vm);
		bool LoadPointGuide_Idle_SelectRejectedForUnload(long nodeId, long ticketContainerId);
		bool LoadPointGuide_Idle_SelectRejectedForLoad(long nodeId, long ticketContainerId);
		void AddOpVisa_Back(long nodeId);
		
		// LoadPointGuide2
		bool LoadPointGuide2_Idle_SelectTicketContainer(long nodeId, long ticketContainerId);
		LoadPointGuide2Vms.BindDestPointVm LoadPointGuide2_BindLoadPoint_GetVm(long nodeId);
		bool LoadPointGuide2_BindLoadPoint_Back(long nodeId);
		bool LoadPointGuide2_BindLoadPoint_Next(LoadPointGuide2Vms.BindDestPointVm vm);
		// void LoadPointGuide2_AddOpVisa_Back(long nodeId);
		
		// LoadCheckPoint
//		void LoadCheckPoint_GetTareValue_Confirm(LoadCheckPointVms.GetTareValue vm);
//		void LoadCheckPoint_AddOperationVisa_Back(long nodeId);
//		LoadCheckPointVms.GetTareValue LoadCheckPoint_GetTareValue_GetVm(long nodeId);
		
		// UnloadCheckPoint
//		void UnloadCheckPoint_AddOperationVisa_Back(long nodeId);
		
		// MixedFeedGuide
		bool MixedFeedGuide_Idle_SelectTicketContainer(long nodeId, long ticketContainerId);
		MixedFeedGuideVms.BindDestPointVm MixedFeedGuide_BindLoadPoint_GetVm(long nodeId);
		bool MixedFeedGuide_BindLoadPoint_Back(long nodeId);
		bool MixedFeedGuide_BindLoadPoint_Next(MixedFeedGuideVms.BindDestPointVm vm);
		MixedFeedProtocolVm MixedFeed_ProtocolPrintout_GetVm(long nodeId);
		
		// MixedFeedLoad
		bool MixedFeedLoad_ConfirmOperation_Next(long nodeId);
		bool MixedFeedLoad_AddOperationVisa_Back(long nodeId);
		bool MixedFeedLoad_IdleWorkstation_Back(long nodeId);
		bool MixedFeedLoad_Workstation_Process(long nodeId);
		MixedFeedLoadVms.IdleVm MixedFeedLoad_IdleVm(long nodeId);
		void MixedFeedLoad_Workstation_SetNodeActive(long nodeId);
		void MixedFeedLoad_Cleanup_Start(MixedFeedLoadVms.CleanupVm vm);
		void MixedFeedLoad_Cleanup_Back(long nodeId);
		void MixedFeedLoad_Workstation_Cleanup(long nodeId);
		void MixedFeedLoad_ConfirmOperation_Cancel(long nodeId);
		void MixedFeedLoad_ConfirmOperation_Reject(long nodeId);
		void MixedFeedLoad_AddChangeStateVisa_Back(long nodeId);
		void MixedFeedLoad_Idle_ChangeState(long nodeId);
	}
}