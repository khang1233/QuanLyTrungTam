using QuanLyTrungTam.DAO;
using QuanLyTrungTam.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyTrungTam.BUS
{
    public class AccountBUS
    {
        private static AccountBUS instance;
        public static AccountBUS Instance
        {
            get { if (instance == null) instance = new AccountBUS(); return instance; }
        }
        private AccountBUS() { }

        public Account Login(string userName, string passWord, string role)
        {
            // 1. Gọi DAO để kiểm tra user/pass/role
            if (AccountDAO.Instance.Login(userName, passWord, role))
            {
                // 2. Lấy thông tin tài khoản
                Account acc = AccountDAO.Instance.GetAccountByUserName(userName);
                
                // 3. Kiểm tra logic trạng thái (Business Logic)
                if (acc != null && acc.TrangThai != "Hoạt động")
                {
                    throw new Exception("Tài khoản đã bị khóa hoặc ngừng hoạt động!");
                }

                return acc;
            }
            return null; // Login thất bại (sai pass/user/role)
        }

        public bool LoginGoogle(string email)
        {
            // Logic Google giữ nguyên, trả về bool hoặc có thể handle thêm object sau này
            return AccountDAO.Instance.LoginGoogle(email);
        }

        public Account GetAccountByEmail(string email)
        {
            return AccountDAO.Instance.GetAccountByEmail(email);
        }

        public Account GetAccountByUserName(string userName)
        {
            return AccountDAO.Instance.GetAccountByUserName(userName);
        }

        public bool UpdateStatus(string user, bool isActive)
        {
            return AccountDAO.Instance.UpdateStatus(user, isActive);
        }
        
        public bool UpdateStatus(string user, int status)
        {
             return AccountDAO.Instance.UpdateStatus(user, status);
        }

        public bool ResetPassword(string userName)
        {
            return AccountDAO.Instance.ResetPassword(userName);
        }
        
        public bool ResetPass(string user)
        {
            return AccountDAO.Instance.ResetPass(user);
        }

        public bool InsertAccount(string user, string pass, string quyen, string maNguoiDung)
        {
            // Có thể thêm check logic: password độ mạnh, user trùng (DAO đã check trùng)
            return AccountDAO.Instance.InsertAccount(user, pass, quyen, maNguoiDung);
        }

        public bool UpdatePassword(string userName, string passMoi)
        {
            return AccountDAO.Instance.UpdatePassword(userName, passMoi);
        }

        public void LockAccountByUserID(string maNguoiDung, bool khoa)
        {
            AccountDAO.Instance.LockAccountByUserID(maNguoiDung, khoa);
        }

        public DataTable GetListAccount()
        {
            return AccountDAO.Instance.GetListAccount();
        }

        public DataTable GetLoginHistory()
        {
            return AccountDAO.Instance.GetLoginHistory();
        }
    }
}
