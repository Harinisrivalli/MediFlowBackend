using MediFlow.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Repo
{
    public class DoctorRepo
    {
        private AppDbContext _context;

        public DoctorRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CreateDoctor> CreateDoctor(CreateDoctor createDoctor)
        {
            var status = await _context.doctors.AddAsync(createDoctor);

            if (status.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return status.Entity;
            }

            return null;
        }

        public async Task<CreateDoctor> GetDoctor(int id)
        {
            return await _context.doctors.FindAsync(id);
        }

        public async Task<List<CreateDoctor>> GetDoctors()
        {
            return await _context.doctors.Include(d=> d.availabilitySlot).ToListAsync();
        }

        public async Task<UpdateDoctor> UpdateDoctor(UpdateDoctor updateDoctor)
        {
            var doctor = await _context.doctors.Include(d=>d.availabilitySlot).FirstOrDefaultAsync(d => d.Id == updateDoctor.Id);

            if (doctor == null)
            {
                return null;
            }

            _context.Entry(doctor).CurrentValues.SetValues(updateDoctor);

            if(updateDoctor.availabilitySlot != null)
            {
                foreach (var upddoctor in updateDoctor.availabilitySlot)
                {
                    var existingSlot = doctor.availabilitySlot.FirstOrDefault(d => d.day == upddoctor.day);
                    if (existingSlot != null)
                    {
                        existingSlot.day = upddoctor.day;
                        existingSlot.startTime = upddoctor.startTime;
                        existingSlot.endTime = upddoctor.endTime;
                        existingSlot.isAvailable = upddoctor.isAvailable;
                    }
                    else
                    {
                        doctor.availabilitySlot.Add(new AvailabilitySlot
                        {
                            day = upddoctor.day,
                            startTime = upddoctor.startTime,
                            endTime = upddoctor.endTime,
                            isAvailable = upddoctor.isAvailable,
                            CreateDoctorId = doctor.Id
                        });
                    }
                }
            }

            doctor.updatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return updateDoctor;
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            var doctor = await _context.doctors.FindAsync(id);

            if (doctor == null)
                return false;

            doctor.isDeleted = true;
            doctor.isActive = false;

            var status = _context.doctors.Update(doctor);

            if (status.State == EntityState.Modified)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}