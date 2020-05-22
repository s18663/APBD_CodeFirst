using APBD_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_CodeFirst.Services
{
    interface IDbService
    {
        public IEnumerable<Doctor> GetDoctors();
        public bool AddDoctor(Doctor doctor);
        public bool RemoveDoctor(Doctor doctor);
        public bool UpdateDoctor(Doctor doctor);
    }
}
