using BuiTienQuatMVC.Models;
using BuiTienQuatMVC.Services;
using BuiTienQuatMVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuiTienQuatMVC.Controllers
{
    public class SystemAccountController : Controller
    {
        private readonly SystemAccountService _accountService;

        public SystemAccountController(SystemAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
         
            var user = _accountService.Authenticate(model.Email, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
                return View(model);
            }

            // Lưu session
            HttpContext.Session.SetString("UserEmail", user.AccountEmail);
            HttpContext.Session.SetInt32("Role", (int)user.AccountRole);
            HttpContext.Session.SetInt32("UserId", user.AccountId);
            HttpContext.Session.SetString("UserName", user.AccountName);
            return RedirectToAction("Index", "NewsArticle");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        // Action để hiển thị danh sách tài khoản
        public IActionResult Index()
        {
            var accounts = _accountService.GetAllSystemAccounts() ?? new List<SystemAccount>(); // Đảm bảo không null
            return View(accounts);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                _accountService.AddSystemAccount(account);
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }


        // Lấy thông tin tài khoản theo ID
        public ActionResult GetAccount(short id)
        {
            var account = _accountService.GetSystemAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Json(account);
        }

        public IActionResult Edit(short id)
        {
            var account = _accountService.GetSystemAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(short id, SystemAccount account)
        {
            if (id != account.AccountId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAccount = _accountService.GetSystemAccountById(id);
                    if (existingAccount == null)
                    {
                        return NotFound();
                    }
                    // Cập nhật từng trường cần thiết
                    existingAccount.AccountName = account.AccountName;
                    existingAccount.AccountEmail = account.AccountEmail;
                    existingAccount.AccountRole = account.AccountRole;

                    // Chỉ cập nhật mật khẩu nếu người dùng nhập mới
                    if (!string.IsNullOrEmpty(account.AccountPassword))
                    {
                        existingAccount.AccountPassword = account.AccountPassword;
                    }

                    _accountService.UpdateSystemAccount(existingAccount);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return View(account);
        }

        // Xóa tài khoản
        public ActionResult Delete(short id)
        {
            _accountService.DeleteSystemAccount(id);
            return RedirectToAction(nameof(Index));
        }

        // Tìm kiếm tài khoản theo tên hoặc email
        public ActionResult Search(string query)
        {
            var results = _accountService.Search(query);
            return View("Index", results); // Truyền danh sách kết quả vào Model
        }


        // Sắp xếp danh sách tài khoản theo id, email, role
        public ActionResult Sort(string orderBy, bool ascending)
        {
            var sortedAccounts = _accountService.Sort(orderBy, ascending) ?? new List<SystemAccount>();
            return View("Index", sortedAccounts);
        }

    }
}
