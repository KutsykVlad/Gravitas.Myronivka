using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace SmsMobizonDemo {

	public static class SmsMobizonModelJson {

		public static class ErrorCode {
			/// <summary>
			/// Операция завершена успешно.
			/// </summary>
			public const int Ok = 0;

			/// <summary>
			/// Ошибка валидации передаваемых данных во время создания или обновления какой-либо сущности.В поле data представлена информация о том, какие поля заполнены неверно. Следует исправить ошибки и повторить запрос с новыми данными.
			/// </summary>
			public const int ValidationError = 1;

			/// <summary>
			/// Указанная запись не найдена. Скорее всего она была удалена, ID записи указан неверно или у пользователя, пытающегося получить доступ к этой записи, нет соответствующих прав доступа к этой записи.
			/// </summary>
			public const int RecordNotFoundError = 2;

			/// <summary>
			/// Неопознанная ошибка приложения.Обратитесь в службу поддержки и сообщите детали запроса, при котором она была получена.
			/// </summary>
			public const int UnknownError = 3;

			/// <summary>
			/// Неверно указан параметр module. Проверьте правильность написания параметра в документации к API.
			/// </summary>
			public const int ProviderNotFoundError = 4;

			/// <summary>
			/// Неверно указан параметр method. Проверьте правильность написания параметра в документации к API.
			/// </summary>
			public const int MethodNotFoundError = 5;

			/// <summary>
			/// Неверно указан параметр format. Проверьте правильность написания параметра в документации к API.
			/// </summary>
			public const int FormatNotSupportedError = 6;

			/// <summary>
			/// Ошибка входа в систему. Ошибка возникает в случаях: 1. Неправильно указанных данных для входа. 2. Когда во время работы с системой сессия пользователя истекла или была принудительно закрыта сервером. Более подробную информацию можно увидеть в поле message.
			/// </summary>
			public const int AuthenticationError = 8;

			/// <summary>
			/// Ошибка доступа к указанному методу
			/// </summary>
			public const int AuthorizationError = 9;

			/// <summary>
			/// Ошибка во время сохранения данных на сервере непосредственно в процессе выполнения данной операции. Обычно эта ошибка связана с одновременным доступом к данным из нескольких клиентов или изменением условий сохранения данных в процессе их сохранения.
			/// </summary>
			public const int DataUpdateError = 10;

			/// <summary>
			/// Некоторые обязательные параметры отсутствуют в запросе. Проверьте правильность написания параметров в документации к API и дополните запрос необходимыми параметрами.
			/// </summary>
			public const int ApiParamsError = 11;

			/// <summary>
			/// Входной параметр запроса не удовлетворяет установленным условиям или ограничениям.Данный код ошибки возникает в случаях, когда при выполнении запроса с параметрами какой-либо параметр нарушает ограничения. Похоже на ошибку валидации атрибутов, но может быть получено в запросах, которые не производять создание или изменение данных.
			/// </summary>
			public const int IncorrectParameterError = 12;

			/// <summary>
			/// Попытка сделать запрос к серверу апи, который не обслуживает данного пользователя.В случае получения этого кода правильный домен можно получить в поле data
			/// </summary>
			public const int IncorrectDomainError = 13;

			/// <summary>
			/// Данная ошибка возникает в случае если аккаунт пользователя заблокирован или удален
			/// </summary>
			public const int UserBlockedError = 14;

			/// <summary>
			/// Ошибка во время выполнения какой-либо операции, не связанной с обновлением данных.Детали данной ошибки указаны в поле message ответа API.
			/// </summary>
			public const int OperationError = 15;

			/// <summary>
			/// Ошибка превышения допустимого лимита операций в промежуток времени. Данная ошибка возникает при чрезмерно частых обращениях к одному и тому же методу API. В случае возникновения ошибки следует уменьшить частоту запросов.
			/// </summary>
			public const int ThrottlingExceededError = 30;

			/// <summary>
			/// Операция выполнена не в полном объеме, а только с частью данных.Обычно данный код возвращается при каких либо массовых операциях, во время выполнения которых некоторые элементы не были обработаны из-за ошибок или ограничений, но часть элементов обработана. В случае получения этого кода можно получить информацию о том, какие элементы были обработаны, а какие нет и с какими ошибками, получив содержимое поля data.
			/// </summary>
			public const int PartiallyDoneError = 98;

			/// <summary>
			/// Операция выполнена не в полном объеме, а только с частью данных.Обычно данный код возвращается при каких либо массовых операциях, во время выполнения которых некоторые элементы не были обработаны из-за ошибок или ограничений, но часть элементов обработана. В случае получения этого кода можно получить информацию о том, какие элементы были обработаны, а какие нет и с какими ошибками, получив содержимое поля data.
			/// </summary>
			public const int NothingDoneError = 99;

			/// <summary>
			/// Данный код не является ошибкой и означает, что операция была отправлена в фоновое выполнение.В этом случае поле data содержит ID фоновой операции, процесс и окончание которой можно отследить при помощи Taskqueue::GetStatus
			/// </summary>
			public const int BackgroundWait = 100;

			/// <summary>
			/// Общая ошибка.Детали можно получить в поле message.
			/// </summary>
			public const int CommonError = 999;
		}
		
		public static class GetBalance {
			public class Response {
				public class Data{
					[JsonProperty("balance")]
					public double Balance { get; set; }

					[JsonProperty("currency")]
					public string Currency { get; set; }
				}

				[JsonProperty("code")]
				public int Code { get; set; }

				[JsonProperty("data")]
				public Data DataArray { get; set; }

				[JsonProperty("message")]
				public string Message { get; set; }
			}
		}

		public static class GetMessageStatus
		{
			public class Response
			{
				public static class DomainValue
				{
					public static class MessageStatus
					{
						public static class New {
							public const string Name = "NEW";
							public const bool IsFinal = false;
						}

						public static class Enqueued {
							public const string Name = "ENQUEUD";
							public const bool IsFinal = false;
						}

						public static class Accepted {
							public const string Name = "ACCEPTD";
							public const bool IsFinal = false;
						}

						public static class Undelivered {
							public const string Name = "UNDELIV";
							public const bool IsFinal = true;
						}

						public static class Rejected {
							public const string Name = "REJECTD";
							public const bool IsFinal = true;
						}

						public static class PardialDlivered {
							public const string Name = "PDLIVRD";
							public const bool IsFinal = false;
						}

						public static class Delivered {
							public const string Name = "DELIVRD";
							public const bool IsFinal = true;
						}

						public static class Expired {
							public const string Name = "EXPIRED";
							public const bool IsFinal = true;
						}

						public static class Deleted {
							public const string Name = "DELETED";
							public const bool IsFinal = true;
						}
					}
				}

				public class Data
				{
					[JsonProperty("id")]
					public int Id { get; set; }

					[JsonProperty("status")]
					public string Status { get; set; }

					[JsonProperty("segCnt")]
					public int SegCnt { get; set; }

					[JsonProperty("startSendTs")]
					public string StartSendTs { get; set; }

					[JsonProperty("statusUpdateTs")]
					public string StatusUpdateTs { get; set; }
				}

				[JsonProperty("code")]
				public int Code { get; set; }

				[JsonProperty("data")]
				public Data[] DataArray { get; set; }

				[JsonProperty("message")]
				public string Message { get; set; }
			}
		}

		public static class SendMessage {
			[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
			public class Request {
				public class Param{
					[JsonProperty("name")]
					public string Name { get; set; }

					[JsonProperty("deferredToTs")]
					public string DeferredToTs { get; set; }

					[JsonProperty("mclass")]
					public int MsgClass { get; set; }

					[JsonProperty("validity")]
					public int Validity { get; set; }
				}

				[JsonProperty("recipient")]
				public string Recipient { get; set; }

				[JsonProperty("text")]
				public string Text { get; set; }

				[JsonProperty("from")]
				public string From { get; set; }

				//[JsonProperty("params")]
				[JsonProperty("params")]
				public List<Param> Params { get; set; }
			}

			public class Response{
				public static class DomainValue {
					public static class CampaignStatus {
						public const int OnModeration = 1;
						public const int SentWithoutModeration = 2;
					}
				}

				public class Data{
					[JsonProperty("campaignId")]
					public int CampaignId { get; set; }

					[JsonProperty("messageId")]
					public int MessageId { get; set; }

					[JsonProperty("status")]
					public int Status { get; set; }
				}

				[JsonProperty("code")]
				public int Code { get; set; }

				[JsonProperty("data")]
				public Data[] DataArray { get; set; }

				[JsonProperty("message")]
				public string Message { get; set; }
			}
		}

	}
	
	class Program
	{
		private const string Token = @"ua357b3f9bad72043a4ed2ca1665d0f5d1ef0886196e15bea09abb24d5bda2e9645291";
		private const string DomainUrl = @"https://api.mobizon.ua";

		static void Main(string[] args) {
			string str;

			var r = GetBalance();
			str = JsonConvert.SerializeObject(r);
			Console.WriteLine(str);
			Console.ReadKey();

			var rr = GetMessageStatus();
			str = JsonConvert.SerializeObject(rr);
			Console.WriteLine(str);
			Console.ReadKey();

			var rrr = SendSms();
			str = JsonConvert.SerializeObject(rrr);
			Console.WriteLine(str);
			Console.ReadKey();
		}

		static SmsMobizonModelJson.GetBalance.Response GetBalance() {
			using (var client = new HttpClient()) {
				var response = client.PostAsync($@"{DomainUrl}/service/user/getownbalance?apiKey={Token}", null).Result;
				string responseContentStr = response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<SmsMobizonModelJson.GetBalance.Response>(responseContentStr);
			}
		}

		static SmsMobizonModelJson.GetMessageStatus.Response GetMessageStatus()
		{
			using (var client = new HttpClient())
			{
				var response = client.PostAsync($@"{DomainUrl}/service/Message/GetSMSStatus?apiKey={Token}&ids=45372660&ids=45372425", null).Result;
				string responseContentStr = response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<SmsMobizonModelJson.GetMessageStatus.Response>(responseContentStr);
			}
		}

		static SmsMobizonModelJson.SendMessage.Response SendSms() {
			SmsMobizonModelJson.SendMessage.Request PostContent = new SmsMobizonModelJson.SendMessage.Request {
				Recipient = "380952137275",
				Text = "Булат Тест"
			};

			using (var client = new HttpClient()) {
				var response = client.PostAsync($@"{DomainUrl}/service/Message/SendSMSMessage?apiKey={Token}", new StringContent(JsonConvert.SerializeObject(PostContent))).Result;
				string responseContentStr = response.Content.ReadAsStringAsync().Result;
				return  JsonConvert.DeserializeObject<SmsMobizonModelJson.SendMessage.Response>(responseContentStr);
			}
		}
	}
}