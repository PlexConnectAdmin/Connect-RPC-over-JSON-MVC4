using System;
using System.Collections.Generic;
using ConsumeWebAPI.Models;

namespace ConsumeWebAPI.Helper
{
    public interface IPartRestClient
    {
        void Add(PartAddModel partModel);
        void Delete(int id);
        IEnumerable<PartModel> GetSeveralParts();
        PartModel GetById(int id);
        void Copy(PartCopyModel part, int partKey);
    }
}
