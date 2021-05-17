using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;
using Gravitas.Model.DomainModel.EmployeeRoles.DTO;
using Gravitas.Model.Dto;

namespace Gravitas.DAL
{
    public class EmployeeRolesRepository : BaseRepository<GravitasDbContext>, IEmployeeRolesRepository
    {
        private readonly GravitasDbContext _context;

        public EmployeeRolesRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public RolesDto GetEmployeeRoles(string employeeId)
        {
            var items = _context.EmployeeRoles
                .AsNoTracking()
                .Where(role => role.EmployeeId == employeeId)
                .AsEnumerable()
                .Select(item => GetRoleDetail(item.RoleId))
                .ToList();
            return new RolesDto
            {
                Items = items,
                Count = items.Count
            };
        }

        private RoleDetail GetRoleDetail(long roleId)
        {
            var role = _context.Roles.AsNoTracking().FirstOrDefault(x => x.Id == roleId);
            if (role == null)
            {
                return null;
            }

            return new RoleDetail
            {
                RoleId = roleId,
                Name = role.Name,
                Nodes = role.Assignments.Select(t=> t.NodeId).ToList()
            };
        }

        public RolesDto GetRoles()
        {
            var roles = GetQuery<Role, long>().Select(t => t.Id).ToList();

            var result = new RolesDto { Count = roles.Count};
            roles.ForEach(t =>
            {
                result.Items.Add(GetRoleDetail(t));
            });
            return result;
        }

        public void UpdateRole(RoleDetail roleDetail)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == roleDetail.RoleId);

            if (role != null)
            {
                Update<Role, long>(new Role { Name = roleDetail.Name, Id = roleDetail.RoleId });

                var assignments = GetRoleDetail(roleDetail.RoleId).Nodes;

                foreach (var assignment in assignments)
                {
                    if (!roleDetail.Nodes.Contains(assignment))
                    {
                        var tmp = _context.RoleAssignments.FirstOrDefault(t => t.RoleId == roleDetail.RoleId && t.NodeId == assignment);
                        Delete<RoleAssignment, long>(tmp);
                    }
                }

                foreach (var assignment in roleDetail.Nodes)
                {
                    if (!assignments.Contains(assignment))
                    {
                        Add<RoleAssignment, long>(new RoleAssignment
                        {
                            NodeId = assignment,
                            RoleId = role.Id
                        });
                    }
                }
            }
        }

        public void AddRole(RoleDetail roleDetail)
        {
            Add<Role, long>(new Role { Name = roleDetail.Name });
            var role = _context.Roles.Where(t => t.Name == roleDetail.Name)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            if (role != null)
            {
                foreach (var node in roleDetail.Nodes)
                {
                    Add<RoleAssignment, long>(new RoleAssignment
                    {
                        NodeId = node,
                        RoleId = role.Id
                    });
                }
            }
        }

        public void DeleteRole(long roleId)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == roleId);

            if (role == null)
                return;

            var records = _context.EmployeeRoles.Where(t => t.RoleId == roleId).ToList();

            foreach (var record in records)
            {
                Delete<EmployeeRole, long>(record);
            }

            var assignments = _context.RoleAssignments.Where(t => t.RoleId == roleId).ToList();

            foreach (var record in assignments)
            {
                Delete<RoleAssignment, long>(record);
            }

            Delete<Role, long>(role);
        }

        public void ApplyEmployeeRoles(RolesDto employeeRoles, string employeeId)
        {
            var roles = _context.EmployeeRoles.Where(t => t.EmployeeId == employeeId).ToList();
            foreach (var role in roles)
            {
                if (!employeeRoles.Items.Select(t => t.RoleId).ToList().Contains(role.RoleId))
                {
                    Delete<EmployeeRole, long>(role);
                }
            }

            foreach (var role in employeeRoles.Items)
            {
                if (!roles.Select(t => t.RoleId).ToList().Contains(role.RoleId))
                {
                    Add<EmployeeRole, long>(new EmployeeRole
                    {
                        EmployeeId = employeeId,
                        RoleId = role.RoleId
                    });
                }
            }
        }
    }
}
