using System;
using System.Data;
using Microsoft.Data.SqlClient;
using EmployeeAPI.Models;
using Microsoft.Extensions.Configuration;

namespace EmployeeAPI.BuisnessLogic
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DatabaseHelper _dbHelper;

        public EmployeeService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }


        public int InsertEmployee(Employee employee)
        {
            using (SqlConnection conn = _dbHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("InsertEmployee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@JobRoleID", employee.JobRoleID);
                    cmd.Parameters.AddWithValue("@JobTitleID", employee.JobTitleID);
                    cmd.Parameters.AddWithValue("@EffectiveFrom", employee.EffectiveFrom);
                    cmd.Parameters.AddWithValue("@DepartureDate", (object)employee.DepartureDate ?? DBNull.Value);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = _dbHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetEmployeesWithJobDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                JobRole = reader["JobRole"].ToString(),
                                JobTitle = (reader["JobTitle"]).ToString(),
                                EffectiveFrom = Convert.ToDateTime(reader["EffectiveFrom"]),
                                DepartureDate = reader["DepartureDate"] != DBNull.Value ? Convert.ToDateTime(reader["DepartureDate"]) : (DateTime?)null
                            });
                        }
                    }
                }
            }
            return employees;
        }
        public List<JobTitleDto> GetJobTitles()
        {
            var jobTitles = new List<JobTitleDto>();

            using (SqlConnection conn = _dbHelper.GetConnection()) 
            using (SqlCommand cmd = new SqlCommand("GetJobTitles", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();  // Open DB connection synchronously

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jobTitles.Add(new JobTitleDto
                        {
                            TitleID = reader.GetInt32(0),
                            Title = reader.GetString(1)
                        });
                    }
                }
            }

            return jobTitles;
        }

        public List<JobRoleDto> GetJobRoles()
        {
            var jobRoles = new List<JobRoleDto>();

            using (SqlConnection conn = _dbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetJobRoles", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();  

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jobRoles.Add(new JobRoleDto
                        {
                            RoleID = reader.GetInt32(0),
                            Role = reader.GetString(1)
                        });
                    }
                }
            }

            return jobRoles;
        }


        public bool UpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = _dbHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UpdateEmployees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@JobRoleID", employee.JobRoleID);
                    cmd.Parameters.AddWithValue("@JobTitleID", employee.JobTitleID);
                    cmd.Parameters.AddWithValue("@EffectiveFrom", employee.EffectiveFrom);
                    cmd.Parameters.AddWithValue("@DepartureDate", (object)employee.DepartureDate ?? DBNull.Value);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected != 0 ;
                }
            }
        }   
    }
}