using System;
using System.Collections.Generic;
using ConsumeWebAPI.Models;

namespace ConsumeWebAPI.Helper
{
    public interface IPartRestClient
    {
        void Add(PartModel partModel);
        void Delete(int id);
        IEnumerable<PartModel> GetSeveralParts();
        PartModel GetById(int id);
        PartModel GetByIP(int ip);
        PartModel GetByType(int type);
        void Copy(PartCopyModel part, int partKey);
    }
}
