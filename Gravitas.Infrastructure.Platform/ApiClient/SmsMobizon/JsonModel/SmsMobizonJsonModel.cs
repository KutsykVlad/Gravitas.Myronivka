﻿namespace Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon
{
    public partial class SmsMobizonApiClient
    {
        public static class ErrorCode
        {
	        /// <summary>
	        ///     Операция завершена успешно.
	        /// </summary>
	        public const int Ok = 0;

	        /// <summary>
	        ///     Ошибка валидации передаваемых данных во время создания или обновления какой-либо сущности.В поле data представлена информация о том, какие поля
	        ///     заполнены неверно. Следует исправить ошибки и повторить запрос с новыми данными.
	        /// </summary>
	        public const int ValidationError = 1;

	        /// <summary>
	        ///     Указанная запись не найдена. Скорее всего она была удалена, ID записи указан неверно или у пользователя, пытающегося получить доступ к этой
	        ///     записи, нет соответствующих прав доступа к этой записи.
	        /// </summary>
	        public const int RecordNotFoundError = 2;

	        /// <summary>
	        ///     Неопознанная ошибка приложения.Обратитесь в службу поддержки и сообщите детали запроса, при котором она была получена.
	        /// </summary>
	        public const int UnknownError = 3;

	        /// <summary>
	        ///     Неверно указан параметр module. Проверьте правильность написания параметра в документации к API.
	        /// </summary>
	        public const int ProviderNotFoundError = 4;

	        /// <summary>
	        ///     Неверно указан параметр method. Проверьте правильность написания параметра в документации к API.
	        /// </summary>
	        public const int MethodNotFoundError = 5;

	        /// <summary>
	        ///     Неверно указан параметр format. Проверьте правильность написания параметра в документации к API.
	        /// </summary>
	        public const int FormatNotSupportedError = 6;

	        /// <summary>
	        ///     Ошибка входа в систему. Ошибка возникает в случаях: 1. Неправильно указанных данных для входа. 2. Когда во время работы с системой сессия
	        ///     пользователя истекла или была принудительно закрыта сервером. Более подробную информацию можно увидеть в поле message.
	        /// </summary>
	        public const int AuthenticationError = 8;

	        /// <summary>
	        ///     Ошибка доступа к указанному методу
	        /// </summary>
	        public const int AuthorizationError = 9;

	        /// <summary>
	        ///     Ошибка во время сохранения данных на сервере непосредственно в процессе выполнения данной операции. Обычно эта ошибка связана с одновременным
	        ///     доступом к данным из нескольких клиентов или изменением условий сохранения данных в процессе их сохранения.
	        /// </summary>
	        public const int DataUpdateError = 10;

	        /// <summary>
	        ///     Некоторые обязательные параметры отсутствуют в запросе. Проверьте правильность написания параметров в документации к API и дополните запрос
	        ///     необходимыми параметрами.
	        /// </summary>
	        public const int ApiParamsError = 11;

	        /// <summary>
	        ///     Входной параметр запроса не удовлетворяет установленным условиям или ограничениям.Данный код ошибки возникает в случаях, когда при выполнении
	        ///     запроса с параметрами какой-либо параметр нарушает ограничения. Похоже на ошибку валидации атрибутов, но может быть получено в запросах, которые
	        ///     не производять создание или изменение данных.
	        /// </summary>
	        public const int IncorrectParameterError = 12;

	        /// <summary>
	        ///     Попытка сделать запрос к серверу апи, который не обслуживает данного пользователя.В случае получения этого кода правильный домен можно получить в
	        ///     поле data
	        /// </summary>
	        public const int IncorrectDomainError = 13;

	        /// <summary>
	        ///     Данная ошибка возникает в случае если аккаунт пользователя заблокирован или удален
	        /// </summary>
	        public const int UserBlockedError = 14;

	        /// <summary>
	        ///     Ошибка во время выполнения какой-либо операции, не связанной с обновлением данных.Детали данной ошибки указаны в поле message ответа API.
	        /// </summary>
	        public const int OperationError = 15;

	        /// <summary>
	        ///     Ошибка превышения допустимого лимита операций в промежуток времени. Данная ошибка возникает при чрезмерно частых обращениях к одному и тому же
	        ///     методу API. В случае возникновения ошибки следует уменьшить частоту запросов.
	        /// </summary>
	        public const int ThrottlingExceededError = 30;

	        /// <summary>
	        ///     Операция выполнена не в полном объеме, а только с частью данных.Обычно данный код возвращается при каких либо массовых операциях, во время
	        ///     выполнения которых некоторые элементы не были обработаны из-за ошибок или ограничений, но часть элементов обработана. В случае получения этого
	        ///     кода можно получить информацию о том, какие элементы были обработаны, а какие нет и с какими ошибками, получив содержимое поля data.
	        /// </summary>
	        public const int PartiallyDoneError = 98;

	        /// <summary>
	        ///     Операция выполнена не в полном объеме, а только с частью данных.Обычно данный код возвращается при каких либо массовых операциях, во время
	        ///     выполнения которых некоторые элементы не были обработаны из-за ошибок или ограничений, но часть элементов обработана. В случае получения этого
	        ///     кода можно получить информацию о том, какие элементы были обработаны, а какие нет и с какими ошибками, получив содержимое поля data.
	        /// </summary>
	        public const int NothingDoneError = 99;

	        /// <summary>
	        ///     Данный код не является ошибкой и означает, что операция была отправлена в фоновое выполнение.В этом случае поле data содержит ID фоновой
	        ///     операции, процесс и окончание которой можно отследить при помощи Taskqueue::GetStatus
	        /// </summary>
	        public const int BackgroundWait = 100;

	        /// <summary>
	        ///     Общая ошибка.Детали можно получить в поле message.
	        /// </summary>
	        public const int CommonError = 999;
        }
    }
}