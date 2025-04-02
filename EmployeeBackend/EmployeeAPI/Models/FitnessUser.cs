namespace EmployeeAPI.Models
{
    public class FitnessUser
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime? CreatedOn { get; set; }

    }

    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; } 
        public int Calories { get; set; }
      
    }

    public class UserExercise
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string ExerciseName { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Status { get; set; }
    }
}
