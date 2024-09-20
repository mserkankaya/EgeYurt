namespace ProductApi.Business.Interfaces
{
    public interface ITokenBlacklistService
    {
        /// <summary>
        /// bir tokenı kara listeye eklemek için kullanılır.
        /// </summary>
        /// <param name="token"></param>
        void BlacklistToken(string token);

        /// <summary>
        /// belirli bir tokenın kara listede olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsTokenBlacklisted(string token); 
    }
}
