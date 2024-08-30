using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IMediaWithCategory
    {
        List<MediaWithCategory> GetMediaWithCategories(); 
        MediaWithCategory GetMediaWithCategoryById(int id);

        int insert(MediaWithCategory mediaWithCategory);
        
        int update(MediaWithCategory mediaWithCategory);

        int delete(int id);
    }
}
