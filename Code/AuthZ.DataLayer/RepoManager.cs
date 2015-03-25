using System;

namespace AuthZ.Datalayer
{
    public class RepoManager
    {
        public readonly IDataLayer DataLayer;

        public RepoManager(IDataLayer dataLayer)
        {
            if (dataLayer == null)
            {
                throw new ArgumentException("please pass an IDataLayer concrete implementation");
            }
            
            this.DataLayer = dataLayer;            
        }
    }
}