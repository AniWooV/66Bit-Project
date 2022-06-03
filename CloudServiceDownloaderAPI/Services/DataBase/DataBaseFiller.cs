using System.Threading.Tasks;
using CloudServiceDownloaderAPI.Contexts;
using CloudServiceDownloaderAPI.Models;

namespace CloudServiceDownloaderAPI.Services.DataBase
{
    public class DataBaseFiller
    {
        private readonly APIContext _context;

        public DataBaseFiller(APIContext context)
        {
            _context = context;
        }

        public async Task AddFile(File file)
        {
            _context.Files.Add(file);

            await _context.SaveChangesAsync();
        }
    }
}
