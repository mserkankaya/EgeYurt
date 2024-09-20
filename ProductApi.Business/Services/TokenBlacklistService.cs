using ProductApi.Business.Interfaces;

namespace ProductApi.Business.Services
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly HashSet<string> _blacklist = new HashSet<string>();

        /// <summary>
        /// Verilen tokenı kara listeye ekler.
        /// </summary>
        /// <param name="token"></param>
        public void BlacklistToken(string token)
        {
            _blacklist.Add(token);
        }

        /// <summary>
        /// Belirli bir tokenın kara listede olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsTokenBlacklisted(string token)
        {
            return _blacklist.Contains(token);
        }
    }
}
