using curso.api.Business.Entities;
using curso.api.Model.Inputs;
using curso.api.Model.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Business.Repositories
{
    public interface ICourseRepository
    {
        void Create(Course course);
        void Commit();
        IEnumerable<Course> GetList(int userCode);
    }
}
