using MediFlow.DTO;
using MediFlow.Models;
using MediFlow.Repo;

namespace MediFlow.Service
{
    public class DoctorService
    {
        private readonly DoctorRepo _repo;

        public DoctorService(DoctorRepo repo)
        {
            _repo = repo;
        }

        public async Task<CreateDoctorDTOResp> CreateDoctor(CreateDoctorDTO dto)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/doctors");
            if (Directory.Exists(uploadFolder) == false)
            {
                Directory.CreateDirectory(uploadFolder);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.profilePhoto.FileName);
            string fname = Path.Combine(uploadFolder, fileName);
            FileStream fs = new FileStream(fname, FileMode.Create);
            await dto.profilePhoto.CopyToAsync(fs);
            Doctor createdDoctor = new Doctor
            {
                Id = dto.Id,
                fullName = dto.fullName,
                dob = dto.dob,
                isDeleted = false,
                isActive = true,
                consultationFee = dto.consultationFee,
                email = dto.email,
                password = dto.password,
                phoneNo = dto.phoneNo,
                gender = dto.gender,
                profilePhoto = "uploads/doctors/" + fileName,
                specialization = dto.specialization,
                qualification = dto.qualification,
                licenseNo = dto.licenseNo,
                experience = dto.experience,
                about = dto.about,
                status = dto.status,

                availabilitySlot = dto.availabilitySlot.Select(obj => new Models.AvailabilitySlot
                {
                    isAvailable = obj.isAvailable,
                    day = obj.day,
                    startTime = obj.startTime,
                    endTime = obj.endTime
                }).ToList()
            };

            createdDoctor = await _repo.CreateDoctor(createdDoctor);

            if (createdDoctor == null)
                return null;

            CreateDoctorDTOResp resp = new CreateDoctorDTOResp
            {
                Id = createdDoctor.Id,
                fullName = createdDoctor.fullName,
                dob = createdDoctor.dob,
                isDeleted = createdDoctor.isDeleted,
                isActive = createdDoctor.isActive,
                consultationFee = createdDoctor.consultationFee,
                email = createdDoctor.email,
                password = createdDoctor.password,
                phoneNo = createdDoctor.phoneNo,
                gender = createdDoctor.gender,
                profilePhoto = createdDoctor.profilePhoto,
                specialization = createdDoctor.specialization,
                qualification = createdDoctor.qualification,
                licenseNo = createdDoctor.licenseNo,
                experience = createdDoctor.experience,
                about = createdDoctor.about,
                status = createdDoctor.status,

                availabilitySlot = createdDoctor.availabilitySlot.Select(obj => new DTO.AvailabilitySlot
                {
                    isAvailable = obj.isAvailable,
                    day = obj.day,
                    startTime = obj.startTime,
                    endTime = obj.endTime
                }).ToList()
            };

            return resp;
        }

        public async Task<CreateDoctorDTOResp> GetDoctorById(int id)
        {
            Doctor doctor = await _repo.GetDoctor(id);

            if (doctor == null)
                return null;

            return new CreateDoctorDTOResp
            {
                Id = doctor.Id,
                fullName = doctor.fullName,
                dob = doctor.dob,
                isDeleted = doctor.isDeleted,
                isActive = doctor.isActive,
                consultationFee = doctor.consultationFee,
                email = doctor.email,
                password = doctor.password,
                phoneNo = doctor.phoneNo,
                gender = doctor.gender,
                profilePhoto = doctor.profilePhoto,
                specialization = doctor.specialization,
                qualification = doctor.qualification,
                licenseNo = doctor.licenseNo,
                experience = doctor.experience,
                about = doctor.about,
                status = doctor.status,

                availabilitySlot = doctor.availabilitySlot.Select(obj => new DTO.AvailabilitySlot
                {
                    isAvailable = obj.isAvailable,
                    day = obj.day,
                    startTime = obj.startTime,
                    endTime = obj.endTime
                }).ToList()
            };
        }

        public async Task<List<CreateDoctorDTOResp>> GetDoctor()
        {
            List<Doctor> data = await _repo.GetDoctors();

            if (data == null)
                return null;

            return data.Select(obj => new CreateDoctorDTOResp
            {
                Id = obj.Id,
                fullName = obj.fullName,
                dob = obj.dob,
                isDeleted = obj.isDeleted,
                isActive = obj.isActive,
                consultationFee = obj.consultationFee,
                email = obj.email,
                password = obj.password,
                phoneNo = obj.phoneNo,
                gender = obj.gender,
                profilePhoto = obj.profilePhoto,
                specialization = obj.specialization,
                qualification = obj.qualification,
                licenseNo = obj.licenseNo,
                experience = obj.experience,
                about = obj.about,
                status = obj.status,

                availabilitySlot = obj.availabilitySlot.Select(x => new DTO.AvailabilitySlot
                {
                    isAvailable = x.isAvailable,
                    day = x.day,
                    startTime = x.startTime,
                    endTime = x.endTime
                }).ToList()

            }).ToList();
        }

        public async Task<bool> DeleteDoctor(int id)
        {
            return await _repo.DeleteDoctor(id);
        }

        public async Task<UpdateDoctorDTO> UpdateDoctor(UpdateDoctorDTO dto)
        {
            var updatedDoctor = await _repo.UpdateDoctor(new Models.UpdateDoctor
            {
                Id = dto.Id,
                fullName = dto.fullName,
                dob = dto.dob,
                isDeleted = false,
                isActive = true,
                consultationFee = dto.consultationFee,
                email = dto.email,
                password = dto.password,
                phoneNo = dto.phoneNo,
                gender = dto.gender,
                profilePhoto = dto.profilePhoto,
                specialization = dto.specialization,
                qualification = dto.qualification,
                licenseNo = dto.licenseNo,
                experience = dto.experience,
                about = dto.about,
                status = dto.status,

                availabilitySlot = dto.availabilitySlot.Select(obj => new Models.AvailabilitySlot
                {
                    isAvailable = obj.isAvailable,
                    day = obj.day,
                    startTime = obj.startTime,
                    endTime = obj.endTime
                }).ToList()
            });

            if (updatedDoctor == null)
                return null;

            dto = new UpdateDoctorDTO
            {
                Id = updatedDoctor.Id,
                fullName = updatedDoctor.fullName,
                dob = updatedDoctor.dob,
                isDeleted = updatedDoctor.isDeleted,
                isActive = updatedDoctor.isActive,
                consultationFee = updatedDoctor.consultationFee,
                email = updatedDoctor.email,
                password = updatedDoctor.password,
                phoneNo = updatedDoctor.phoneNo,
                gender = updatedDoctor.gender,
                profilePhoto = updatedDoctor.profilePhoto,
                specialization = updatedDoctor.specialization,
                qualification = updatedDoctor.qualification,
                licenseNo = updatedDoctor.licenseNo,
                experience = updatedDoctor.experience,
                about = updatedDoctor.about,
                status = updatedDoctor.status,

                availabilitySlot = updatedDoctor.availabilitySlot.Select(obj => new DTO.AvailabilitySlot
                {
                    isAvailable = obj.isAvailable,
                    day = obj.day,
                    startTime = obj.startTime,
                    endTime = obj.endTime
                }).ToList()
            };

            return dto;
        }
    }
}