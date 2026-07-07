using MediFlow.Database;
using MediFlow.DTO;
using MediFlow.Repo;

namespace MediFlow.Service
{
    public class PatientService
    {
        private readonly PatientRepo _patientRepo;

        public PatientService(PatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public async Task<List<PatientDTOResponse>> GetPatients()
        {
            List<PatientData> patients = await _patientRepo.GetPatients();
            if(patients.Count > 0)
            {
                List<PatientDTOResponse> patientsdto = patients.Select(obj => new PatientDTOResponse
                {
                    id = obj.Id,
                    fullName = obj.FullName,
                    age = obj.Age,
                    email = obj.Email,
                    phoneNo = obj.PhoneNo,
                    city = obj.City,
                    state = obj.State,
                    pincode = obj.Pincode,
                    gender = obj.Gender,
                    bloodGroup = obj.BloodGroup,
                    profilePhoto = obj.ProfilePhoto,
                    isActive = obj.IsActive,
                    isDeleted = obj.IsDeleted,
                    createdAt = obj.CreatedAt,
                    updatedAt = obj.UpdatedAt
                }).ToList();
                return patientsdto;
            }
            return null;
        }

        public async Task<PatientDTOResponse> CreatePatient(CreatePatientDTO createPatientDTO)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (Directory.Exists(uploadFolder) == false)
            {
                Directory.CreateDirectory(uploadFolder);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(createPatientDTO.profilePhoto.FileName);
            string fname = Path.Combine(uploadFolder, fileName);
            FileStream fs = new FileStream(fname, FileMode.Create);
            await createPatientDTO.profilePhoto.CopyToAsync(fs);
            PatientData patientData = new PatientData
            {
                FullName = createPatientDTO.fullName,
                Age = createPatientDTO.age,
                Email = createPatientDTO.email,
                PhoneNo = createPatientDTO.phoneNo,
                City = createPatientDTO.city,
                State = createPatientDTO.state,
                Pincode = createPatientDTO.pincode,
                Gender = createPatientDTO.gender,
                BloodGroup = createPatientDTO.bloodGroup,
                ProfilePhoto = "uploads/" + fileName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsActive = createPatientDTO.isActive
            };
            patientData = await _patientRepo.CreatePatient(patientData);
            if(patientData != null)
            {
                return new PatientDTOResponse
                {
                    id = patientData.Id,
                    fullName = patientData.FullName,
                    age = patientData.Age,
                    email = patientData.Email,
                    phoneNo = patientData.PhoneNo,
                    city = patientData.City,
                    state = patientData.State,
                    pincode = patientData.Pincode,
                    gender = patientData.Gender,
                    bloodGroup = patientData.BloodGroup,
                    profilePhoto = patientData.ProfilePhoto,
                    isActive = patientData.IsActive,
                    isDeleted = patientData.IsDeleted,
                    createdAt = patientData.CreatedAt,
                    updatedAt = patientData.UpdatedAt
                };
            }
            return null;
        }

        public async Task<bool> UpdatePatient(int id, UpdatePatientDTO updatePatientDTO)
        {
            PatientData patientData = new PatientData
            {
                Id = id,
                FullName = updatePatientDTO.fullName,
                Age = updatePatientDTO.age,
                Email = updatePatientDTO.email,
                PhoneNo = updatePatientDTO.phoneNo,
                City = updatePatientDTO.city,
                State = updatePatientDTO.state,
                Pincode = updatePatientDTO.pincode,
                Gender = updatePatientDTO.gender,
                BloodGroup = updatePatientDTO.bloodGroup,
                ProfilePhoto = updatePatientDTO.profilePhoto,
                IsActive = updatePatientDTO.isActive,
                IsDeleted = updatePatientDTO.isDeleted,
            };
            patientData = await _patientRepo.UpdatePatient(patientData);
            if(patientData != null)
            {
                return true;
            }
            return false;
        }
    
        public async Task<bool> DeletePatient(int id)
        {
            return await _patientRepo.DeletePatient(id);

        }
    }
}
