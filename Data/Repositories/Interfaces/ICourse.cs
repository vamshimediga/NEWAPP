using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICourse
    {
        List<Course> Courses();
        Course GetById(int id);
        int insert(Course course);

        int update(Course course);  

       bool delete(int id);


    }
}
