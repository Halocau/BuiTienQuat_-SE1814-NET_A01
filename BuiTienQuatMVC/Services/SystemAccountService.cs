using BuiTienQuatMVC.Models;
using BuiTienQuatMVC.Repositories;

namespace BuiTienQuatMVC.Services
{
    public class SystemAccountService
    {
        private readonly ISystemAccountRepository _accountRepository;

        public SystemAccountService(ISystemAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // Xác thực tài khoản
        public SystemAccount Authenticate(string email, string password)
        {
            var account = _accountRepository.GetByEmail(email);
            if (account != null && account.AccountPassword == password)
            {
                return account;
            }
            return null;
        }

        // Lấy tất cả tài khoản
        public IEnumerable<SystemAccount> GetAllSystemAccounts()
        {
            return _accountRepository.GetAll();
        }

        // Lấy tài khoản theo ID
        public SystemAccount? GetSystemAccountById(short id)
        {
            return _accountRepository.GetById(id);
        }

        // Thêm tài khoản mới
        public void AddSystemAccount(SystemAccount account)
        {
            _accountRepository.Add(account);
        }

        // Cập nhật tài khoản
        public void UpdateSystemAccount(SystemAccount account)
        {
            _accountRepository.Update(account);
        }

        // Xóa tài khoản
        public void DeleteSystemAccount(short id)
        {
            _accountRepository.Delete(id);
        }

        // Tìm kiếm tài khoản theo tên hoặc email
        public IEnumerable<SystemAccount> Search(string keyword)
        {
            return _accountRepository.Search(keyword);
        }

        // Sắp xếp danh sách tài khoản theo id, email, hoặc role
        public IEnumerable<SystemAccount> Sort(string orderBy, bool ascending)
        {
            return _accountRepository.Sort(orderBy, ascending);
        }
    }
}
