using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using SchedulerModel;

namespace ApiRepo
{
    public interface IRepository<T>
    {
        List<T> FindAll();
        T Find(string id);
        bool Add(T x);
        bool Update(T x);
        bool Remove(T x);
    }

    public class coursesRepo : IRepository<Course>
    {
        string _root;

        public coursesRepo(string root)
        {
            _root = root;
        }

        List<Course> IRepository<Course>.FindAll()
        {
            List<Course> courses;
            string path = @_root + "CourseList";

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(path).Result;

                if (response.IsSuccessStatusCode)
                {
                    HttpContent responseContent = response.Content;

                    courses = responseContent.ReadAsAsync<List<Course>>().Result;

                    return courses;
                }
            }

            return null;
        }

        public Course Find(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Course x)
        {
            throw new NotImplementedException();
        }

        public bool Add(Course x)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Course x)
        {
            throw new NotImplementedException();
        }
    }
}
