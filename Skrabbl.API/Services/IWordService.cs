
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public interface IWordService
    {
        Task<bool> DoesWordExist(string word);
    }
}
