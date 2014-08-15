using LTIS.Lib.Repository;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace LTIS.Controllers
{
    public class ContactCountController : ApiController
    {
        public async Task<int> Get()
        {
            return await LTRepository.ContactCount();
        }
	}
}