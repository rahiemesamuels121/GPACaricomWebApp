using GPACARICOM.Models;

namespace GPACARICOM.Services.Interfaces
{
    public interface IProgramService
    {
        public Task<List<ProgramModel>> GetProgramsAsync();
        public Task<ProgramModel> GetProgram(int id);
        public Task<bool> AddNewProgram(ProgramModel program);
        public Task<bool> UpdateProgram(ProgramModel article);
        public Task<bool> DeleteProgram(int id);
    }
}
