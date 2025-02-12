using BuiTienQuatMVC.Models;

namespace BuiTienQuatMVC.Repositories
{
    public interface ISystemAccountRepository
    {
        public SystemAccount? GetByEmail(string email);
        public IEnumerable<SystemAccount> GetAll();
        public SystemAccount? GetById(short id);
        public void Add(SystemAccount account);
        public void Update(SystemAccount account);
        public void Delete(short id);

        //search
        public IEnumerable<SystemAccount> Search(string keyword);

        //sort
        public IEnumerable<SystemAccount> Sort(string orderBy, bool ascending);
    }
}

