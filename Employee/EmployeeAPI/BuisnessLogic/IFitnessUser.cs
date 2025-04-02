using EmployeeAPI.Models;
namespace EmployeeAPI.BuisnessLogic
{
    public interface IFitnessUser
    {
        void AddFitnessUser(FitnessUser user);
        List<FitnessUser> GetFitnessUsers();
        List<Exercise> GetExercises();
        List<UserExercise> GetUserExercises(int fitnessUserId);
    }
}
