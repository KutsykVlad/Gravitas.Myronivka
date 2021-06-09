using System;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.MixedFeedManage;
using Gravitas.Platform.Web.ViewModel.OpRoutine.DriverCheckIn;
using Gravitas.Platform.Web.ViewModel.OpRoutine.LoadPointGuide2;
using Gravitas.Platform.Web.ViewModel.OpRoutine.MixedFeedGuide;
using Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide;
using Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide2;

namespace Gravitas.Platform.Web.Manager.OpRoutine {

	public interface IOpRoutineWebManager {
		//TODO: Split methods to separate manager for each routine

		//SingleWindow
		void SingleWindow_GetTicket_New(int nodeId, string supplyBarCode);
		void SingleWindow_GetTicket_Change(int nodeId, string supplyCode);
		bool SingleWindow_GetTicket_Back(int nodeId);
		void SingleWindow_GetTicket_Detail(int nodeId, int ticketId);
		bool SingleWindow_GetTicket_Close(int nodeId);
		void SingleWindow_GetTicket_Delete(int nodeId, int ticketId);
		void SingleWindow_ShowTicketMenu_Exit(int nodeId);
		bool SingleWindow_Idle_SelectTicketContainer(int nodeId, int ticketContainerId);
		void SingleWindow_AddOperationVisa_Back(int nodeIdValue);
		SingleWindowVms.SingleWindowOpDataDetailVm SingleWindow_EditTicketForm_GetData(int nodeId);
		bool SingleWindow_ShowTicketMenu_Back(int nodeId);
		bool SingleWindow_ShowTicketMenu_Route(int nodeId);
		bool SingleWindow_ShowTicketMenu_Edit(int nodeId);
		bool SingleWindow_ShowTicketMenu_Commit(int nodeId);
		byte[] SingleWindow_ShowTicketMenu_PrintDoc(int nodeId, string printoutTypeId);
		bool SingleWindow_EditTicketForm_Save(SingleWindowVms.SingleWindowOpDataDetailVm data);
		bool SingleWindow_EditTicketForm_Back(int nodeId);

		bool SingleWindow_EditAddOpVisa_Back(int nodeId);

		bool SingleWindow_RouteEditData_Back(int nodeId);
		bool SingleWindow_RouteEditData_Save(SingleWindowVms.Route data);
		SingleWindowVms.Route SingleWindow_RouteEditDataVm(int nodeId);

	    SingleWindowVms.ProtocolPrintoutVm SingleWindow_ProtocolPrintout_GetVm(int nodeId, int? ticketIdExt = null);
	    SingleWindowVms.RoutePrintoutVm SingleWindow_RoutePrintout_GetVm(int nodeId, int ticketId);
		SingleWindowVms.TechnologicalRoutePrintoutVm SingleWindow_TechnologicalRoutePrintout_GetVm(int nodeId);

		void SingleWindow_DivideTicket(int nodeId, int newWeightValue);
	    void SingleWindow_DeleteTicketAddOpVisa_Back(int nodeId);
	    void OwnTransport_AddNew(int nodeId);
	    void OwnTransport_Update(int nodeId, int id);
	    void AddOwnTransport_Delete(int nodeId, int id);

	    bool SingleWindow_RouteAddOpVisa_Back(int nodeId);
		
		// SecurityIn
		bool SecurityIn_Entry_Cancelation(int nodeId);
		void SecurityIn_CheckOwnTransport_Next(int nodeId);
		void SecurityIn_CheckOwnTransport_Reject(int nodeId);

		// SecurityOut
		SecurityOutVms.ShowOperationsListVm SecurityOut_ShowOperationsList_GetData(int nodeId);
		bool SecurityOut_ShowOperationsList_Confirm(int nodeId, bool isConfirmed);
		bool SecurityOut_EditStampList_Back(int nodeId);
		bool SecurityOut_EditStampList(SecurityOutVms.EditStampListVm vm);
		void SecurityOut_CheckOwnTransport_Next(int nodeId);
		void SecurityOut_CheckOwnTransport_Reject(int nodeId);

        //CentralLaboratoryProcess
	    CentralLaboratoryProcess.IdleVm CentralLaboratoryProcess_Idle_GetVm(int nodeId);
	    void CentralLaboratoryProcess_Idle_SelectSample(int nodeId, Guid opDataId);
        CentralLaboratoryProcess.PrintDataDiscloseVm CentralLaboratoryProcess_PrintDataDisclose_GetVm(int nodeId);
		void CentralLaboratoryProcess_PrintCollisionInit_Send(int nodeId, string comment);
	    void CentralLaboratoryProcess_PrintDataDisclose_Back(int nodeId);
	    void CentralLaboratoryProcess_Idle_AddSample(int nodeId);
	    void CentralLaboratoryProcess_Idle_AddSample_Back(int nodeId);
        bool CentralLaboratoryProcess_PrintDataDisclose_Confirm(int nodeId);
	    CentralLaboratoryProcess.PrintCollisionInitVm CentralLaboratory_GetCollisionInitVm(int nodeId);
	    CentralLaboratoryProcess.PrintDocumentVm CentralLaboratory_GetPrintDocumentVm(int nodeId);
        bool CentralLaboratoryProcess_SendToCollision(CentralLaboratoryProcess.PrintCollisionInitVm vm);
	    bool CentralLaboratoryProcess_PrintCollisionInit_Return(int nodeId);
	    bool CentralLaboratoryProcess_PrintCollisionManage_ReturnToCollectSamples(int nodeId, string comment);
	    void CentralLaboratoryProcess_PrintDocument_Confirm(int nodeId);
	    CentralLaboratoryProcess.CentralLabolatorySamplePrintoutVm CentralLaboratory_GetSamplePrintoutVm(int ticketId);
		void CentralLaboratoryProcess_PrintDataDisclose_MoveWithLoad(int nodeId);
		void CentralLaboratoryProcess_PrintDataDisclose_UnloadToStoreWithLoad(int nodeId);
		void CentralLaboratoryProcess_PrintDataDisclose_ToCollisionInit(int nodeId);
		CentralLaboratoryProcess.CentralLaboratoryLabelVm CentralLaboratory_GetLabelPrintoutVm(Guid id);
		void CentralLaboratoryProcess_PrintDataDisclose_DeleteFile(int nodeId);
		void CentralLaboratoryProcess_PrintCollisionVisa_Back(int nodeId);
		void CentralLaboratoryProcess_PrintLabel_Confirm(int nodeId);
		void CentralLaboratoryProcess_PrintAddOpVisa_Back(int nodeId);
		void CentralLaboratoryProcess_PrintDataDiscloseVisa_Back(int nodeId);

		//LabolatoryIn
        bool LaboratoryIn_Idle_СollectSample(int nodeId);
		bool LaboratoryIn_Idle_EditAnalysisResult(int nodeId);
		bool LaboratoryIn_Idle_PrintAnalysisResult(int nodeId);
		bool LaboratoryIn_Idle_PrintCollisionInit(int nodeId, int ticketId);
		bool LaboratoryIn_Idle_SelectTicketContainer(int nodeId, int ticketContainerId);
		bool LaboratoryIn_SampleReadTruckRfid_Back(int nodeId);
		bool LaboratoryIn_SampleBindTray_Back(int nodeId);
		LaboratoryInVms.SampleBindAnalysisTrayVm LaboratoryIn_SampleBindAnalysisTray_GetVmData(int nodeId);
		bool LaboratoryIn_SampleBindAnalysisTray_Next(int nodeId);
		bool LaboratoryIn_SampleBindAnalysisTray(LaboratoryInVms.SampleBindAnalysisTrayVm vmData);
		bool LaboratoryIn_ResultReadTrayRfid_Back(int nodeId);
		LaboratoryInVms.ResultEditAnalysisVm LaboratoryIn_ResultEditAnalysis_GetVm(int nodeId);
		bool LaboratoryIn_ResultEditAnalysis_Back(int nodeId);
		bool LaboratoryIn_ResultEditAnalysis_Save(LaboratoryInVms.LabFacelessOpDataComponentDetailVm vm);
		bool LaboratoryIn_ResultEditAnalysis_SaveFromDevice(DeviceStateVms.LabAnalyserStateDialogVm vm);
		bool LaboratoryIn_PrintReadTrayRfid_Back(int nodeId);
		LaboratoryInVms.PrintAnalysisResultsVm LaboratoryIn_PrintAnalysisResults_GetVmData(int nodeId);
		bool LaboratoryIn_PrintAnalysisResult_Save(LaboratoryInVms.LabFacelessOpDataDetailVm vm);
		bool LaboratoryIn_PrintAnalysisResult_Back(int nodeId);
		LaboratoryInVms.PrintDataDiscloseVm LaboratoryIn_PrintDataDisclose_GetVm(int nodeId);
		bool LaboratoryIn_PrintDataDisclose_Confirm(int nodeId, bool isConfirmed);
		bool LaboratoryIn_PrintCollisionManage_SetReturnRoute(int nodeId, string indexRefundReason);
		bool LaboratoryIn_PrintCollisionManage_ReturnToCollectSamples(int nodeId);
		bool LaboratoryIn_SendToCollision(LaboratoryInVms.PrintCollisionInitVm vm);
		LaboratoryInVms.PrintCollisionInitVm LaboratoryIn_GetCollisionInitVm(int nodeId);
		LaboratoryInVms.PrintCollisionManageVm GetLaboratoryIn_PrintCollisionManageVm(int nodeId);
		bool LaboratoryIn_PrintCollisionInit_Return(int nodeId);
		LaboratoryInVms.PrintLaboratoryProtocol PrintLaboratoryProtocol_GetVm(int nodeId);
		void PrintLaboratoryProtocol_Next(int nodeId);
		LaboratoryInVms.SamplePrintoutVm LaboratoryIn_SamplePrintout_GetVm(Guid opDataId);
		void LaboratoryIn_PrintCollisionManage_SetReloadRoute(int nodeId);

		// Weighbridge
		WeightbridgeVms.BaseWeightPromptVm Weighbridge_GetWeightPrompt_GetData(int nodeId);
	    void Weighbridge_DriverTrailerAccepted(int nodeId, bool isTrailerAccepted);
	    void Weighbridge_GetWeightPrompt_Process(int nodeId, bool isWeightAccepted, bool isTruckWeighting);
		void Weighbridge_ResetWeighbridge(int nodeId);
	    void Weighbridge_GuardianTruckVerification_Process(int nodeId, bool isWeighingAllowed);
	    void Weighbridge_GuardianTrailerEnable_Process(int nodeId, bool isTrailerEnabled);
		WeightbridgeVms.OpenBarrierOutVm Weighbridge_OpenBarrierOutVm(int nodeIdValue);
	    WeightbridgeVms.TruckWeightPromptVm WeighbridgeGetTruckWeightPromptVm(int nodeId);
	    WeightbridgeVms.GetGuardianTruckWeightPermissionVm Weighbridge__GetGuardianTruckWeightPermissionVm(int nodeId);
        WeightbridgeVms.TrailerWeightPromptVm Weighbridge__GetTrailerWeightPromptVm(int nodeId);
        WeightbridgeVms.GetGuardianTrailerWeightPermissionVm Weighbridge__GetGuardianTrailerWeightPermissionVm(int nodeId);
        
	    // UnloadPointType1
        bool UnloadPointType1_ConfirmOperation_Next(int nodeId);
		bool UnloadPointType1_AddOperationVisa_Back(int nodeId);
		bool UnloadPointType1_IdleWorkstation_Back(int nodeId);
		bool UnloadPointType1_Workstation_Process(int nodeId);
		UnloadPointType1Vms.IdleVm UnloadPointType1_IdleVm(int nodeId);
		void UnloadPointType1_Workstation_SetNodeActive(int nodeId);
		void UnloadPointType1_AddChangeStateVisa_Back(int nodeId);
		void UnloadPointType1_Idle_ChangeState(int nodeId);
		void UnloadPointType1_Idle_GetTareValue(int nodeId);

		//UnloadPointType2

		void UnloadPointType2_Idle_ChangeState(int nodeId);
		void UnloadPointType2_AddChangeStateVisa_Back(int nodeId);
		void UnloadPointType2_Workstation_SetNodeActive(int nodeId);
		UnloadPointType2Vms.IdleVm UnloadPointType2_IdleVm(int nodeId);
		bool UnloadPointType2_Workstation_Process(int nodeId);
		bool UnloadPointType2_IdleWorkstation_Back(int nodeId);
		bool UnloadPointType2_AddOperationVisa_Back(int nodeId);
		bool UnloadPointType2_ConfirmOperation_Next(int nodeId, string acceptancePointCode);



		// LoadPointType1
		bool LoadPointType1_ConfirmOperation_Next(int nodeId);
		bool LoadPointType1_AddOperationVisa_Back(int nodeId);
		bool LoadPointType1_IdleWorkstation_Back(int nodeId);
		bool LoadPointType1_Workstation_Process(int nodeId);
		LoadPointType1Vms.IdleVm LoadPointType1_IdleVm(int nodeId);
		void LoadPointType1_Workstation_SetNodeActive(int nodeId);
		void LoadPointType1_ConfirmOperation_Cancel(int nodeId);
		void LoadPointType1_ConfirmOperation_Reject(int nodeId);
		void LoadPointType1_AddChangeStateVisa_Back(int nodeId);
		void LoadPointType1_Idle_ChangeState(int nodeId);
		void LoadPointType1_Idle_GetTareValue(int nodeId);

		// UnloadPointGuide
		bool UnloadPointGuide_Idle_SelectTicketContainer(int nodeId, int ticketContainerId);
		UnloadPointGuideVms.BindUnloadPointVm UnloadPointGuide_BindUnloadPoint_GetVm(int nodeId);
		bool UnloadPointGuide_BindUnloadPoint_Back(int nodeId);
		bool UnloadPointGuide_BindUnloadPoint_Next(UnloadPointGuideVms.BindUnloadPointVm vm);
		void UnloadPointGuide_Idle_AskFromQueue(int nodeId, int ticketContainerId);
		bool UnloadPointGuide_Idle_AskFromQueue_Back(int nodeId);
		
		// UnloadPointGuide2
		bool UnloadPointGuide2_Idle_SelectTicketContainer(int nodeId, int ticketContainerId);
		UnloadPointGuide2Vms.BindUnloadPointVm UnloadPointGuide2_BindUnloadPoint_GetVm(int nodeId);
		bool UnloadPointGuide2_BindUnloadPoint_Back(int nodeId);
		bool UnloadPointGuide2_BindUnloadPoint_Next(UnloadPointGuide2Vms.BindUnloadPointVm vm);

		// LoadPointGuide
		bool LoadPointGuide_Idle_SelectTicketContainer(int nodeId, int ticketContainerId);
		LoadPointGuideVms.BindDestPointVm LoadPointGuide_BindLoadPoint_GetVm(int nodeId);
		bool LoadPointGuide_BindLoadPoint_Back(int nodeId);
		bool LoadPointGuide_BindLoadPoint_Next(LoadPointGuideVms.BindDestPointVm vm);
		bool LoadPointGuide_Idle_SelectRejectedForUnload(int nodeId, int ticketContainerId);
		bool LoadPointGuide_Idle_SelectRejectedForLoad(int nodeId, int ticketContainerId);
		void AddOpVisa_Back(int nodeId);
		
		// LoadPointGuide2
		bool LoadPointGuide2_Idle_SelectTicketContainer(int nodeId, int ticketContainerId);
		LoadPointGuide2Vms.BindDestPointVm LoadPointGuide2_BindLoadPoint_GetVm(int nodeId);
		bool LoadPointGuide2_BindLoadPoint_Back(int nodeId);
		bool LoadPointGuide2_BindLoadPoint_Next(LoadPointGuide2Vms.BindDestPointVm vm);
		
		// MixedFeedGuide
		bool MixedFeedGuide_Idle_SelectTicketContainer(int nodeId, int ticketContainerId);
		MixedFeedGuideVms.BindDestPointVm MixedFeedGuide_BindLoadPoint_GetVm(int nodeId);
		bool MixedFeedGuide_BindLoadPoint_Back(int nodeId);
		bool MixedFeedGuide_BindLoadPoint_Next(MixedFeedGuideVms.BindDestPointVm vm);
		MixedFeedProtocolVm MixedFeed_ProtocolPrintout_GetVm(int nodeId);
		
		// MixedFeedLoad
		bool MixedFeedLoad_ConfirmOperation_Next(int nodeId);
		bool MixedFeedLoad_AddOperationVisa_Back(int nodeId);
		bool MixedFeedLoad_IdleWorkstation_Back(int nodeId);
		bool MixedFeedLoad_Workstation_Process(int nodeId);
		MixedFeedLoadVms.IdleVm MixedFeedLoad_IdleVm(int nodeId);
		void MixedFeedLoad_Workstation_SetNodeActive(int nodeId);
		void MixedFeedLoad_Cleanup_Start(MixedFeedLoadVms.CleanupVm vm);
		void MixedFeedLoad_Cleanup_Back(int nodeId);
		void MixedFeedLoad_Workstation_Cleanup(int nodeId);
		void MixedFeedLoad_ConfirmOperation_Cancel(int nodeId);
		void MixedFeedLoad_ConfirmOperation_Reject(int nodeId);
		void MixedFeedLoad_AddChangeStateVisa_Back(int nodeId);
		void MixedFeedLoad_Idle_ChangeState(int nodeId);
		
		// DriverCheckIn
		void DriverCheckIn_Idle_CheckIn(int nodeId);
		void DriverCheckIn_CheckIn_Idle(int nodeId);
		void DriverCheckIn_CheckIn_DriverInfoCheck(int nodeId);
		void DriverCheckIn_DriverInfoCheck_RegistrationConfirm(int nodeId);
		void DriverCheckIn_AddDriver(DriverCheckInVms.AddDriverVm model);
		DriverCheckInVms.DriverInfoCheckVm GetDriverInfoCheckModel(int nodeId);
		void DriverCheckIn_DriverInfoCheck(DriverCheckInVms.DriverInfoCheckVm model);
		DriverCheckInVms.RegistrationConfirmVm GetRegistrationConfirm(int nodeId);
	}
}