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

        // Đăng nhập mới (Tự động xác định Role)
        public Account Login(string userName, string passWord)
        {
            // 1. Gọi DAO xác thực
            Account acc = AccountDAO.Instance.Login(userName, passWord);

            // 2. Kiểm tra tài khoản & Trạng thái
            if (acc != null)
            {
                // Logic check Status
                if (acc.TrangThai != "Hoạt động" && acc.TrangThai != "Active" && acc.TrangThai != "1" 
                    && acc.TrangThai != "Chờ xếp lớp" && acc.TrangThai != "Đang học")
                {
                    throw new Exception("Tài khoản đã bị khóa hoặc ngừng hoạt động! (" + acc.TrangThai + ")");
                }
                return acc;
            }
            return null; // Login thất bại
        }

        // [DEPRECATED] Giữ lại để tương thích ngược nếu cần
        public Account Login(string userName, string passWord, string role)
        {
            // Redirect sang hàm mới nếu muốn, hoặc giữ logic cũ
            if (AccountDAO.Instance.Login(userName, passWord, role))
            {
                Account acc = AccountDAO.Instance.GetAccountByUserName(userName);
                if (acc != null && acc.TrangThai != "Hoạt động") throw new Exception("Tài khoản đã bị khóa!");
                return acc;
            }
            return null;
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
