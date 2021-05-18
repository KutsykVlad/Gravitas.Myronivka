using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gravitas.DAL;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.OneC;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Connect.Manager
{
	public class Api1CConnectManager {
		private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

		private OneCApiClient _oneCApiClient;
		private readonly IExternalDataRepository _externalDataRepository;
		private bool _addFilter;

		public Api1CConnectManager(IExternalDataRepository externalDataRepository)
		{
			_externalDataRepository = externalDataRepository;
		}

		private void UpdateItems<T>(List<T> list) where T : BaseEntity<string>
		{
			if (_addFilter && list.Count > 500) return;
			foreach (T item in list) {
				try {
					_externalDataRepository.AddOrUpdateWithoutSaveChanges<T, string>(item);
				}
				catch (Exception e)
				{
					Logger.Error(e);
				}
			}
			_externalDataRepository.Save();
		}

		public void SyncDictionaries(bool addFilter = false)
		{
			_addFilter = addFilter;
			Logger.Info("Sync started.");
			if (addFilter) Logger.Info("Records more than 500 items won't be stored.");
			// @"http://10.11.73.135/bu_dev_malinovskiy_k/hs/bulat"
			_oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);

			List<Task> syncTasks = new List<Task>();

			syncTasks.Add(new Task(() => {
					Logger.Info("AcceptancePoint");
					var list = _oneCApiClient.GetAcceptancePointModifiedItems();
					Logger.Info($"AcceptancePoint. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"AcceptancePoint. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Budget");
					var list = _oneCApiClient.GetBudgetModifiedItems();
					Logger.Info($"Budget. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Budget. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Contract");
					var list = _oneCApiClient.GetContractModifiedItems();
					Logger.Info($"Contract. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Contract. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Employee");
					var list = _oneCApiClient.GetEmployeeModifiedItems();
					Logger.Info($"Employee. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Employee. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("FixedAsset");
					var list = _oneCApiClient.GetFixedAssetModifiedItems();
					Logger.Info($"FixedAsset. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"FixedAsset. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Organisation");
					var list = _oneCApiClient.GetOrganisationModifiedItems();
					Logger.Info($"Organisation. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Organisation. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Partner");
					var list = _oneCApiClient.GetPartnerModifiedItems();
					Logger.Info($"Partner. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Partner. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Product");
					var list = _oneCApiClient.GetProductModifiedItems();
					Logger.Info($"Product. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Product. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("ReasonForRefund");
					var list = _oneCApiClient.GetReasonForRefundModifiedItems();
					Logger.Info($"ReasonForRefund. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"ReasonForRefund. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Route");
					var list = _oneCApiClient.GetRouteModifiedItems();
					Logger.Info($"Route. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Route. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Stock");

					var list = _oneCApiClient.GetStockModifiedItems();
					Logger.Info($"Stock. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Stock. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Subdivision");
					var list = _oneCApiClient.GetSubdivisionModifiedItems();
					Logger.Info($"Subdivision. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Subdivision. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("ExternalUser");
					var list = _oneCApiClient.GetUserModifiedItems();
					Logger.Info($"ExternalUser. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"ExternalUser. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("YearOfHarvest");
					var list = _oneCApiClient.GetYearOfHarvestModifiedItems();
					Logger.Info($"YearOfHarvest. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"YearOfHarvest. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("MeasureUnit");
					var list = _oneCApiClient.GetUnitModifiedItems();
					Logger.Info($"MeasureUnit. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"MeasureUnit. Done - {list.Count}");
				})
			);

			syncTasks.Add(new Task(() => {
					Logger.Info("Crop");
					var list = _oneCApiClient.GetCropModifiedItems();
					Logger.Info($"Crop. Items to be processed - {list.Count}");
					UpdateItems(list);
					Logger.Info($"Crop. Done - {list.Count}");
				})
			);
			
			foreach (Task task in syncTasks) {
				task.RunSynchronously();
				//Thread.Sleep(60*1000);
			}

			while (!syncTasks.All(e => e.IsCanceled || e.IsCompleted || e.IsFaulted)) {

				Thread.Sleep(10 * 1000);
			}

			Logger.Info("Sync completed.");
		}
	}
}
