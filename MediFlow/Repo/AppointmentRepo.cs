using MediFlow.Controllers;
using MediFlow.DTO;
using MediFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Repo
{
    public class AppointmentRepo
    {
        private readonly AppDbContext _context;

        public AppointmentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Models.Appointment> CreateAppointment(Models.Appointment appointment)
        {
            var status = await _context.appointments.AddAsync(appointment);
            if(status.State == EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return status.Entity;
            }
            return null;
        }

        public async Task<List<Models.Appointment>> GetAppointment()
        {
            var status = await _context.appointments.Include(d => d.doctor).Include(p => p.patient).ToListAsync();
            if (status == null)
                return null;
            return status;
        }

        public async Task<Models.Appointment> GetAppointmentById(int id)
        {
            var status = await _context.appointments.Include(d => d.doctor).Include(p => p.patient).FirstOrDefaultAsync(d => d.Id == id);
            return status;
        }

        public async Task<Models.Appointment> UpdateAppointment(
            Models.Appointment appointment)
        {
            var data = await _context.appointments
                .FindAsync(appointment.Id);

            if (data != null)
            {
                _context.Entry(data)
                    .CurrentValues
                    .SetValues(appointment);

                int count = await _context.SaveChangesAsync();

                if (count > 0)
                    return data;
            }

            return null;
        }

        public async Task<bool> DeleteAppointment(int id)
        {
            var res = await _context.appointments
                .FirstOrDefaultAsync(obj => obj.Id == id);

            if (res == null)
            {
                return false;
            }

            var status = _context.appointments.Remove(res);

            if (status.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    
        public async Task<Models.Appointment> GetDoctorAppointments(int id, DateTime date)
        {
            var status = await _context.appointments.Include(d => d.doctor).FirstOrDefaultAsync(obj => obj.DoctorId == id && obj.AppointmentDate.Date == date);
            return status;
        }

        public async Task<Models.Appointment> GetPatientAppointments(int id)
        {
            var status = await _context.appointments.Include(p => p.patient).FirstOrDefaultAsync(obj => obj.PatientId == id);
            return status;
        }
    }
}