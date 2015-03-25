using System.Collections.Generic;
using AuthZ.DataLayer;

namespace AuthZ.Datalayer
{
    public interface IDataLayer
    {
        AudienceDto GetAudience(string clientId);
        IEnumerable<AudienceDto> GetAll();
    }
}