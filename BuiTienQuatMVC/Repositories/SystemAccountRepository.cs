using BuiTienQuatMVC.Models;

namespace BuiTienQuatMVC.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly FunewsManagementContext _context;

        public SystemAccountRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<SystemAccount> GetAll()
        {
            return _context.SystemAccounts.ToList();
        }

        public SystemAccount? GetById(short id)
        {
            return _context.SystemAccounts.FirstOrDefault(s=> s.AccountId == id);
        }

        public void Add(SystemAccount account)
        {
            _context.SystemAccounts.Add(account);
            _context.SaveChanges();
        }

        public void Update(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            _context.SaveChanges();
        }

        public void Delete(short id)
        {
            var account = _context.SystemAccounts.Find(id);
            if (account != null)
            {
                _context.SystemAccounts.Remove(account);
                _context.SaveChanges();
            }
        }
        public IEnumerable<SystemAccount> Search(string keyword)
        {
            return _context.SystemAccounts
                .Where(a => (a.AccountName != null && a.AccountName.Contains(keyword)) ||
                            (a.AccountEmail != null && a.AccountEmail.Contains(keyword)))
                .ToList();
        }
        public IEnumerable<SystemAccount> Sort(string orderBy, bool ascending)
        {
            var query = _context.SystemAccounts.AsQueryable();
            orderBy = orderBy?.ToLower() ?? "id"; // Mặc định sắp xếp theo ID nếu orderBy bị null

            switch (orderBy)
            {
                case "id":
                    query = ascending ? query.OrderBy(a => a.AccountId) : query.OrderByDescending(a => a.AccountId);
                    break;
                case "email":
                    query = ascending
                        ? query.OrderBy(a => string.IsNullOrEmpty(a.AccountEmail) ? "" : a.AccountEmail)
                        : query.OrderByDescending(a => string.IsNullOrEmpty(a.AccountEmail) ? "" : a.AccountEmail);
                    break;
                case "role":
                    query = ascending ? query.OrderBy(a => a.AccountRole) : query.OrderByDescending(a => a.AccountRole);
                    break;
                default:
                    query = query.OrderBy(a => a.AccountId); // Nếu giá trị không hợp lệ, mặc định sắp xếp theo ID
                    break;
            }

            return query.ToList();
        }
        public SystemAccount? GetByEmail(string email)
        {
            return _context.SystemAccounts.FirstOrDefault(a => a.AccountEmail == email);
        }
    }
}
