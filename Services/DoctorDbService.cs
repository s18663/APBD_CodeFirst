using APBD_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_CodeFirst.Services
{
    public class DoctorDbService : IDbService
    {
        private readonly CodeFirstContext _dbService;
        public DoctorDbService(CodeFirstContext dbcontext)
        {
            _dbService = dbcontext;
        }
        public bool AddDoctor(Doctor doctor)
        {
            _dbService.Add(doctor);
            try
            {
                _dbService.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _dbService.Doctor.ToList();
        }

        public bool RemoveDoctor(Doctor doctor)
        {
            _dbService.Remove(doctor);
            try
            {
                _dbService.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            _dbService.Update(doctor);
            try
            {
                _dbService.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
