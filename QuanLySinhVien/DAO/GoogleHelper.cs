using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyTrungTam
{
    public class GoogleHelper
    {
        // 1. Client ID và Secret bạn vừa gửi (Đã điền sẵn)
        private static string ClientId = "776601133322-2e05ag3b12sg7gna0eradtuait2ac61q.apps.googleusercontent.com";
        private static string ClientSecret = "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";
        public class KhongLuuLichSu : IDataStore
        {
            public Task ClearAsync() { return Task.CompletedTask; }
            public Task DeleteAsync<T>(string key) { return Task.CompletedTask; }
            public Task<T> GetAsync<T>(string key) { return Task.FromResult(default(T)); } // Luôn trả về null (chưa có token)
            public Task StoreAsync<T>(string key, T value) { return Task.CompletedTask; } // Không lưu gì cả
        }
        public static async Task<string> LoginGoogleAsync()
        {
            try
            {
                // Yêu cầu quyền truy cập
                UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = ClientId,
                        ClientSecret = ClientSecret
                    },
                    new[] { "email", "profile" },
                    "user",
                    CancellationToken.None,
                    new KhongLuuLichSu() // <--- THÊM THAM SỐ THỨ 5 NÀY VÀO
                );

                // Xác thực Token
                var payload = await GoogleJsonWebSignature.ValidateAsync(credential.Token.IdToken);
                return payload.Email;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}