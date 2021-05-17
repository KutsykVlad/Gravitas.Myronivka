namespace Gravitas.DAL
{
    public interface ICardRepository : IBaseRepository<GravitasDbContext>
    {
        string GetContainerCardNo(long id);
    }
}