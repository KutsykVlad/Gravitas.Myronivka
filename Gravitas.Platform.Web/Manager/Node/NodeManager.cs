using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.Node
{
    public class NodeWebManager : INodeWebManager
    {
        private readonly INodeRepository _nodeRepository;
        private readonly GravitasDbContext _context;

        public NodeWebManager(INodeRepository nodeRepository, GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _context = context;
        }

        public NodeProgresVm GetNodeProgress(int id)
        {
            var nodeDto = _nodeRepository.GetNodeDto(id);
            if (nodeDto.Context.OpRoutineStateId == null) return null;

            var curOpRoutineState = _context.OpRoutineStates.First(x => x.Id == nodeDto.Context.OpRoutineStateId.Value);

            var resultVm = new NodeProgresVm
            {
                CurrentStatusName = curOpRoutineState.Name,
                CurrentStatusIndex = curOpRoutineState.StateNo,
                StatusCount = _context.OpRoutineStates.Count(e => e.OpRoutineId == nodeDto.OpRoutineId)
            };

            return resultVm;
        }
    }
}