//namespace BuiTienQuatMVC.Models
//{
//    public class AdminAccount
//    {
//        public static void SeedAdminAccount(FunewsManagementContext context, IConfiguration configuration)
//        {
//            var adminEmail = configuration["AdminAccount:Email"];
//            var adminPassword = configuration["AdminAccount:Password"];

//            // Kiểm tra xem Admin đã tồn tại chưa
//            if (!context.SystemAccounts.Any(a => a.AccountEmail == adminEmail))
//            {
//                var admin = new SystemAccount
//                {
//                    AccountName = "Administrator",
//                    AccountEmail = adminEmail,
//                    AccountPassword = adminPassword,
//                    AccountRole = 0,
//                };

//                context.SystemAccounts.Add(admin);
//                context.SaveChanges();
//            }
//        }
//    }
//}
