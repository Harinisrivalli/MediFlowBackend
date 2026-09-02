using MediFlow.DTO;
using MediFlow.Repo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MediFlow.Service
{
    public class AppointmentService
    {
        private readonly AppointmentRepo _appointmentRepo;
        private readonly PatientService _patientService;
        private readonly DoctorService _doctorService;

        public AppointmentService(AppointmentRepo appointmentRepo, PatientService patientService, DoctorService doctorService)
        {
            _appointmentRepo = appointmentRepo;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        public async Task<DTO.AppointmentDTOResponse> CreateAppointment(
            DTO.AppointmentDTO appointment)
        {
            Models.Appointment appointment1 = new Models.Appointment()
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                Status = appointment.Status.ToString(),
                AppointmentDate = appointment.AppointmentDate,
                Notes = appointment.Notes,
                Reason = appointment.Reason,
                ConsultationType = appointment.ConsultationType,
                SelectedSlots = appointment.SelectedSlots
            };

            var status = await _appointmentRepo
                .CreateAppointment(appointment1);

            if (status != null)
            {
                var resp = new DTO.AppointmentDTOResponse
                {
                    Id = appointment1.Id,
                    PatientId = appointment1.PatientId,
                    DoctorId = appointment1.DoctorId,
                    Status = Enum.Parse<DTO.Status>(appointment1.Status),
                    AppointmentDate = appointment1.AppointmentDate,
                    Notes = appointment1.Notes,
                    Reason = appointment1.Reason,
                    ConsultationType = appointment1.ConsultationType,
                    SelectedSlots = appointment1.SelectedSlots,
                };
                return resp;
            }

            return null;
        }

        public async Task<List<DTO.AppointmentDTOResponse>> GetAppointment()
        {
            List<Models.Appointment> status = await _appointmentRepo.GetAppointment();

            if (status == null)
                return null;

            var tasks = status.Select(async obj =>
            {
                return new DTO.AppointmentDTOResponse
                {
                    Id = obj.Id,
                    PatientId = obj.PatientId,
                    DoctorId = obj.DoctorId,
                    AppointmentDate = obj.AppointmentDate,
                    Status = Enum.Parse<DTO.Status>(obj.Status),
                    Notes = obj.Notes,
                    Reason = obj.Reason,
                    ConsultationType = obj.ConsultationType,
                    SelectedSlots = obj.SelectedSlots,
                    doctor = new CreateDoctorDTOResp
                    {
                        Id = obj.doctor.Id,
                        fullName = obj.doctor.fullName,
                        email = obj.doctor.email,
                        licenseNo = obj.doctor.licenseNo,
                        specialization = obj.doctor.specialization,
                        experience = obj.doctor.experience,
                        phoneNo = obj.doctor.phoneNo,
                        gender = obj.doctor.gender,
                        dob = obj.doctor.dob,
                        profilePhoto = obj.doctor.profilePhoto,
                        qualification = obj.doctor.qualification,
                        consultationFee = obj.doctor.consultationFee,
                        about = obj.doctor.about,
                        status = obj.doctor.status,
                        isActive = obj.doctor.isActive,
                        isDeleted = obj.doctor.isDeleted,
                        createdAt = obj.doctor.createdAt,
                        updatedAt = obj.doctor.updatedAt
                    },
                    patient = new PatientDTOResponse
                    {
                        Id = obj.patient.Id,
                        fullName = obj.patient.FullName,
                        email = obj.patient.Email,
                        phoneNo = obj.patient.PhoneNo,
                        age = obj.patient.Age,
                        gender = obj.patient.Gender,
                        city = obj.patient.City,
                        state = obj.patient.State,
                        pincode = obj.patient.Pincode,
                        bloodGroup = obj.patient.BloodGroup,
                        isActive = obj.patient.IsActive,
                        isDeleted = obj.patient.IsDeleted,
                        createdAt = obj.patient.CreatedAt,
                        updatedAt = obj.patient.UpdatedAt
                    }
                };
            });
            List<DTO.AppointmentDTOResponse> list1 = (await Task.WhenAll(tasks)).ToList();
            return list1;
        }

        public async Task<DTO.AppointmentDTOResponse> GetAppointmentById(int id)
        {
            var status = await _appointmentRepo.GetAppointmentById(id);

            if (status != null)
            {
                return new DTO.AppointmentDTOResponse
                {
                    Id = status.Id,
                    PatientId = status.PatientId,
                    DoctorId = status.DoctorId,
                    AppointmentDate = status.AppointmentDate,
                    Status = Enum.Parse<DTO.Status>(status.Status),
                    Notes = status.Notes,
                    Reason = status.Reason,
                    ConsultationType = status.ConsultationType,
                    SelectedSlots = status.SelectedSlots,
                    doctor = new CreateDoctorDTOResp
                    {
                        Id = status.doctor.Id,
                        fullName = status.doctor.fullName,
                        email = status.doctor.email,
                        licenseNo = status.doctor.licenseNo,
                        specialization = status.doctor.specialization,
                        experience = status.doctor.experience,
                        phoneNo = status.doctor.phoneNo,
                        gender = status.doctor.gender,
                        dob = status.doctor.dob,
                        profilePhoto = status.doctor.profilePhoto,
                        qualification = status.doctor.qualification,
                        consultationFee = status.doctor.consultationFee,
                        about = status.doctor.about,
                        status = status.doctor.status,
                        isActive = status.doctor.isActive,
                        isDeleted = status.doctor.isDeleted,
                        createdAt = status.doctor.createdAt,
                        updatedAt = status.doctor.updatedAt
                    },
                    patient = new PatientDTOResponse
                    {
                        Id = status.patient.Id,
                        fullName = status.patient.FullName,
                        email = status.patient.Email,
                        phoneNo = status.patient.PhoneNo,
                        age = status.patient.Age,
                        gender = status.patient.Gender,
                        city = status.patient.City,
                        state = status.patient.State,
                        pincode = status.patient.Pincode,
                        bloodGroup = status.patient.BloodGroup,
                        isActive = status.patient.IsActive,
                        isDeleted = status.patient.IsDeleted,
                        createdAt = status.patient.CreatedAt,
                        updatedAt = status.patient.UpdatedAt
                    }
                };
            }
            return null;
        }

        public async Task<DTO.AppointmentDTOResponse> UpdateAppointment(
            DTO.AppointmentDTO appointment)
        {
            Models.Appointment appointment1 = new Models.Appointment
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status.ToString(),
                Notes = appointment.Notes,
                Reason = appointment.Reason,
                ConsultationType = appointment.ConsultationType,
                SelectedSlots = appointment.SelectedSlots
            };

            var data = await _appointmentRepo
                .UpdateAppointment(appointment1);

            if (data != null)
            {
                return new DTO.AppointmentDTOResponse
                {
                    Id = data.Id,
                    PatientId = data.PatientId,
                    DoctorId = data.DoctorId,
                    AppointmentDate = data.AppointmentDate,
                    Status = Enum.Parse<DTO.Status>(data.Status),
                    Notes = appointment.Notes,
                    Reason = appointment.Reason,
                    ConsultationType = appointment.ConsultationType,
                    SelectedSlots = appointment.SelectedSlots,
                    doctor = await _doctorService.GetDoctorById(appointment.DoctorId ?? 0),
                    patient = await _patientService.GetPatientById(appointment.PatientId ?? 0)
                };
            }

            return null;
        }

        public async Task<bool> DeleteAppointment(int id)
        {
            var data = await _appointmentRepo.DeleteAppointment(id);

            if (data == false)
                return false;

            return data;
        }
    
        public async Task<DTO.AppointmentDTOResponse> GetDoctorAppointments(int id, DateTime date)
        {
            var status = await _appointmentRepo.GetDoctorAppointments(id, date);
            if(status != null)
            {
                return new DTO.AppointmentDTOResponse
                {
                    Id = status.Id,
                    PatientId = status.PatientId,
                    DoctorId = status.DoctorId,
                    AppointmentDate = status.AppointmentDate,
                    Status = Enum.Parse<DTO.Status>(status.Status),
                    Notes = status.Notes,
                    Reason = status.Reason,
                    ConsultationType = status.ConsultationType,
                    SelectedSlots = status.SelectedSlots,
                    doctor = new CreateDoctorDTOResp
                    {
                        Id = status.doctor.Id,
                        fullName = status.doctor.fullName,
                        email = status.doctor.email,
                        licenseNo = status.doctor.licenseNo,
                        specialization = status.doctor.specialization,
                        experience = status.doctor.experience,
                        phoneNo = status.doctor.phoneNo,
                        gender = status.doctor.gender,
                        dob = status.doctor.dob,
                        profilePhoto = status.doctor.profilePhoto,
                        qualification = status.doctor.qualification,
                        consultationFee = status.doctor.consultationFee,
                        about = status.doctor.about,
                        status = status.doctor.status,
                        isActive = status.doctor.isActive,
                        isDeleted = status.doctor.isDeleted,
                        createdAt = status.doctor.createdAt,
                        updatedAt = status.doctor.updatedAt
                    }
                };
            }
            return null;
        }

        public async Task<DTO.AppointmentDTOResponse> GetPatientAppointments(int id)
        {
            var status = await _appointmentRepo.GetPatientAppointments(id);
            if (status == null)
                return null;
            return new DTO.AppointmentDTOResponse
            {
                Id = status.Id,
                PatientId = status.PatientId,
                DoctorId = status.DoctorId,
                AppointmentDate = status.AppointmentDate,
                Status = Enum.Parse<DTO.Status>(status.Status),
                Notes = status.Notes,
                Reason = status.Reason,
                ConsultationType = status.ConsultationType,
                SelectedSlots = status.SelectedSlots,
                patient = new PatientDTOResponse
                {
                    Id = status.patient.Id,
                    fullName = status.patient.FullName,
                    email = status.patient.Email,
                    phoneNo = status.patient.PhoneNo,
                    age = status.patient.Age,
                    gender = status.patient.Gender,
                    city = status.patient.City,
                    state = status.patient.State,
                    pincode = status.patient.Pincode,
                    bloodGroup = status.patient.BloodGroup,
                    isActive = status.patient.IsActive,
                    isDeleted = status.patient.IsDeleted,
                    createdAt = status.patient.CreatedAt,
                    updatedAt = status.patient.UpdatedAt
                }
            };
        }
    }
}