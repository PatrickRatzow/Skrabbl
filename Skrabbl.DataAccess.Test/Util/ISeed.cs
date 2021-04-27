using System.Threading.Tasks;

namespace Skrabbl.DataAccess.Test
{
    internal interface ISeed
    {
        Task Up();
        Task Down();
    }
}