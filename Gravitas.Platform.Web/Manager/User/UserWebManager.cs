using System;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel.User;
using Dom = Gravitas.Model.DomainValue.Dom;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.Platform.Web.Manager.User
{
    public class UserWebManager : IUserWebManager
    {
        private readonly ICardRepository _cardRepository;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly GravitasDbContext _context;

        public UserWebManager(IExternalDataRepository externalDataRepository,
            ICardRepository cardRepository,
            IOpRoutineManager opRoutineManager, 
            GravitasDbContext context)
        {
            _externalDataRepository = externalDataRepository;
            _cardRepository = cardRepository;
            _opRoutineManager = opRoutineManager;
            _context = context;
        }

        public UserListVm GetUserList(string name, int pageNumber, int pageSize)
        {
            bool SearchFilter(ExternalData.ExternalUser item)
            {
                return IsContaining(item.FullName, name) || IsContaining(item.ShortName, name);
            }

            bool IsContaining(string mainStr, string searchStr)
            {
                return mainStr.ToUpper().Contains(searchStr != null ? searchStr.ToUpper() : string.Empty);
            }

            var list = _context.ExternalUsers.AsEnumerable().Where(SearchFilter).ToList();

            var listCount = list.Count;
            var items = list
                .OrderBy(item => item.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(item =>
                    new UserDetailsVm
                    {
                        Id = item.Id,
                        ShortName = item.ShortName, 
                        FullName = item.FullName
                    });

            var lastPage = (int) Math.Ceiling(decimal.Divide(listCount, pageSize));

            return new UserListVm
            {
                Items = items,
                PrevPage = Math.Max(pageNumber - 1, 1),
                NextPage = Math.Min(pageNumber + 1, lastPage),
                ItemsCount = listCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public UserDetailsVm GetUserDetails(string id)
        {
            var user = _externalDataRepository.GetExternalEmployeeDetail(id);
            var userCards = _context.Cards.Where(item => item.EmployeeId == user.Id)
                .Select(item => item.Id)
                .ToList();

            return new UserDetailsVm
            {
                Id = user.Id,
                ShortName = user.ShortName, 
                FullName = user.FullName, 
                CardIds = userCards
            };
        }

        public (bool, string) AssignCardToUser(string userId)
        {
            var rfidState = (RfidObidRwState) DeviceSyncManager.GetDeviceState(10000300);

            var card = _context.Cards.FirstOrDefault(e =>
                e.Id.Equals(rfidState.InData.Rifd, StringComparison.CurrentCultureIgnoreCase));

            if (card == null)
            {
                _cardRepository.Add<Card, string>(new Card
                {
                    Id = rfidState.InData.Rifd,
                    IsActive = true, 
                    TypeId = Dom.Card.Type.EmployeeCard, 
                    EmployeeId = userId
                });
                return (true, @"Картка створена та при'вязана успішно");
            }

            if (!_opRoutineManager.IsRfidCardValid(out var cardErrMsg, card, Dom.Card.Type.EmployeeCard))
                return (false, cardErrMsg.Text);

            if (card.EmployeeId != null) return (false, @"Картка прив'язана до іншого користувача");

            card.EmployeeId = userId;
            _cardRepository.Update<Card, string>(card);
            return (true, @"Картка при'вязана успішно");
        }
    }
}