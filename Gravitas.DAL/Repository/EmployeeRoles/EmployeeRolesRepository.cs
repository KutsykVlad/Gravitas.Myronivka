using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;
using Gravitas.Model.DomainModel.EmployeeRoles.DTO;

namespace Gravitas.DAL.Repository.EmployeeRoles
{
    public class EmployeeRolesRepository : BaseRepository, IEmployeeRolesRepository
    {
        private readonly GravitasDbContext _context;

        public EmployeeRolesRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public RolesDto GetEmployeeRoles(Guid employeeId)
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

        private RoleDetail GetRoleDetail(int roleId)
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
            var roles = GetQuery<Role, int>().Select(t => t.Id).ToList();

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
                Update<Role, int>(new Role { Name = roleDetail.Name, Id = roleDetail.RoleId });

                var assignments = GetRoleDetail(roleDetail.RoleId).Nodes;

                foreach (var assignment in assignments)
                {
                    if (!roleDetail.Nodes.Contains(assignment))
                    {
                        var tmp = _context.RoleAssignments.FirstOrDefault(t => t.RoleId == roleDetail.RoleId && t.NodeId == assignment);
                        Delete<RoleAssignment, int>(tmp);
                    }
                }

                foreach (var assignment in roleDetail.Nodes)
                {
                    if (!assignments.Contains(assignment))
                    {
                        Add<RoleAssignment, int>(new RoleAssignment
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
            Add<Role, int>(new Role { Name = roleDetail.Name });
            var role = _context.Roles.Where(t => t.Name == roleDetail.Name)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            if (role != null)
            {
                foreach (var node in roleDetail.Nodes)
                {
                    Add<RoleAssignment, int>(new RoleAssignment
                    {
                        NodeId = node,
                        RoleId = role.Id
                    });
                }
            }
        }

        public void DeleteRole(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Id == roleId);

            if (role == null)
                return;

            var records = _context.EmployeeRoles.Where(t => t.RoleId == roleId).ToList();

            foreach (var record in records)
            {
                Delete<EmployeeRole, int>(record);
            }

            var assignments = _context.RoleAssignments.Where(t => t.RoleId == roleId).ToList();

            foreach (var record in assignments)
            {
                Delete<RoleAssignment, int>(record);
            }

            Delete<Role, int>(role);
        }

        public void ApplyEmployeeRoles(RolesDto employeeRoles, Guid employeeId)
        {
            var roles = _context.EmployeeRoles.Where(t => t.EmployeeId == employeeId).ToList();
            foreach (var role in roles)
            {
                if (!employeeRoles.Items.Select(t => t.RoleId).ToList().Contains(role.RoleId))
                {
                    Delete<EmployeeRole, int>(role);
                }
            }

            foreach (var role in employeeRoles.Items)
            {
                if (!roles.Select(t => t.RoleId).ToList().Contains(role.RoleId))
                {
                    Add<EmployeeRole, int>(new EmployeeRole
                    {
                        EmployeeId = employeeId,
                        RoleId = role.RoleId
                    });
                }
            }
        }
    }
}
