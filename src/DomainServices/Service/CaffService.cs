using ShoppingLikeFiles.DomainServices.Model;

namespace ShoppingLikeFiles.DomainServices.Service
{
    internal class CaffService : ICaffService
    {
        public string GetThumbnail(string fileName)
        {
            throw new NotImplementedException();
        }

        public string Ping()
        {
            return "pong";
        }

        public CaffCredit? ValidateFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
