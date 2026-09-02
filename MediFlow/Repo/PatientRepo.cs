using MediFlow.Database;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Repo
{
    public class PatientRepo
    {
        private readonly AppDbContext _context;
        public PatientRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PatientData>> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients;
        }

        public async Task<PatientData> CreatePatient(PatientData patientData)
        {
            var createdPatient = await _context.Patients.AddAsync(patientData);
            if(createdPatient.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return createdPatient.Entity;
            }
            return null;
        }

        public async Task<PatientData> UpdatePatient(PatientData patientData)
        {
            var patientTobeUpdated = await _context.Patients.FindAsync(patientData.Id);
            if(patientTobeUpdated == null)
            {
                return null;
            }
            patientTobeUpdated.FullName = patientData.FullName;
            patientTobeUpdated.Age = patientData.Age;
            patientTobeUpdated.Email = patientData.Email;
            patientTobeUpdated.PhoneNo = patientData.PhoneNo;
            patientTobeUpdated.City = patientData.City; 
            patientTobeUpdated.State = patientData.State;
            patientTobeUpdated.Pincode = patientData.Pincode;
            patientTobeUpdated.Gender = patientData.Gender;
            patientTobeUpdated.BloodGroup = patientData.BloodGroup;
            patientTobeUpdated.ProfilePhoto = patientData.ProfilePhoto;
            patientTobeUpdated.IsActive = patientData.IsActive;
            patientTobeUpdated.IsDeleted = patientData.IsDeleted;
            patientTobeUpdated.UpdatedAt = DateTime.Now;
            patientData = _context.Patients.Update(patientTobeUpdated).Entity;
            await _context.SaveChangesAsync();
            return patientData;
        }

        public async Task<bool> DeletePatient(int id)
        {
            var patientTobeDeleted = await _context.Patients.FindAsync(id);
            if(patientTobeDeleted == null)
            {
                return false;
            }
            patientTobeDeleted.IsDeleted = true;
            patientTobeDeleted.IsActive = false;
            patientTobeDeleted.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PatientData> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return null;
            return patient;
        }
    }
}
