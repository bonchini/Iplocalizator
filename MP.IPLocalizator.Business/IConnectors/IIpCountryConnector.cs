using System.Threading.Tasks;
using MP.IPLocalizator.Business.Data;

namespace MP.IPLocalizator.Business.IConnectors
{
    public interface IIpCountryConnector
    {
       Task<IpCountryData> GetIpCountryData(string ip);
    }
}
