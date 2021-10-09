using ECommerce.CommonLayer.Model;
using ECommerce.Processor;
using ECommerce.RepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.RepositoryLayer.Service
{
    public class AdminRL : IAdminRL
    {

        public readonly ILogger<AdminRL> _logger;

        public readonly IConfiguration _configuration;
        public readonly SqlConnection sqlConnection;
        public AdminRL(IConfiguration configuration, ILogger<AdminRL> logger)
        {
            _configuration = configuration;
            var ConfigurationDatabase = this.GetDatabaseConfiguration();
            this.sqlConnection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DatabaseConnectionString").Value);
            _logger = logger;
        }

        private IConfigurationRoot GetDatabaseConfiguration()
        {
            var DatabaseConnectionBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return DatabaseConnectionBuilder.Build();
        }

        //Register

        public async Task<AdminRegResponse> AdminRegister(AdminRegRequest request)
        {
            AdminRegResponse response = new AdminRegResponse();
            response.IsSuccess = true;
            try
            {
                //String SqlQuery = SqlQueries.RegisterEmployee;
                SqlCommand sqlCommand = new SqlCommand("Register", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 180;
                sqlCommand.Parameters.AddWithValue("@Name", request.Name);
                sqlCommand.Parameters.AddWithValue("@Mobile", request.Mobile);
                sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                sqlCommand.Parameters.AddWithValue("@Gender", request.Gender);
                string strkey = _configuration["SecurityKey"].ToString();
                sqlCommand.Parameters.AddWithValue("@Password", Processor.PasswordProcessing.Encrypt(request.Password, strkey));
                //sqlCommand.Parameters.AddWithValue("@DoC", req.DoC);
                sqlCommand.Parameters.AddWithValue("@DoB", request.DoB);
                sqlCommand.Parameters.AddWithValue("@IsActive", (request.IsActive));
                sqlCommand.Parameters.AddWithValue("@Role", (request.Role));

                sqlConnection.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    response.IsSuccess = false;

                }
                else
                {
                    response.Name = request.Name;
                    response.Email = request.Email;
                    response.Mobile = request.Mobile;
                    response.Gender = request.Gender;
                    response.DoB = request.DoB;
                    response.IsActive = request.IsActive;
                    response.Role = request.Role;
                    response.Message = "Registered Successfully";
                }

                //else
                //{
                //    response.IsSuccess = false;

                //}

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                _logger.LogError($"Error Occur in register part");
            }
            finally
            {
                sqlConnection.Close();
            }
            return response;
        }

        public async Task<AdminLoginResponse> AdminLogin(AdminLoginRequest request)
        {

            AdminLoginResponse response = new AdminLoginResponse();
            response.IsSuccess = true;
            response.Email = null;

            try
            {

                {
                    //String SqlQuery = SqlQueries.LoginEmployee;
                    SqlCommand sqlCommand = new SqlCommand("Login", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 180;
                    // sqlCommand.Parameters.AddWithValue("@AdminId", request.AdminId);

                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);

                    sqlCommand.Parameters.AddWithValue("@Password", Processor.PasswordProcessing.Encrypt(request.Password, _configuration["SecurityKey"]).ToString());
                    sqlConnection.Open();
                    using (DbDataReader db = await sqlCommand.ExecuteReaderAsync())
                        if (db.HasRows)
                        {
                            await db.ReadAsync();
                            response.Id = db["Id"] != DBNull.Value ? Convert.ToInt32(db["Id"]) : 0;
                            response.Email = db["Email"] != DBNull.Value ? db["Email"].ToString() : null;
                            response.Role = db["Role"] != DBNull.Value ? db["Role"].ToString() : null;
                            response.IsActive = db["IsActive"] != DBNull.Value ? Convert.ToBoolean(db["IsActive"]) : true;
                            response.Message = "Login Successfully";
                        }
                        else
                        {
                            response.IsSuccess = false;
                        }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                _logger.LogError($"Error Occur in login part");

            }
            finally
            {
                sqlConnection.Close();
            }
            return response;
        }


        //Forget Password
        public async Task<ForgetPasswordResponse> ForgetPassword(ForgetPasswordRequest request)
        {
            string Password = string.Empty;
            ForgetPasswordResponse response = new ForgetPasswordResponse()
            {
                IsSuccess = true,
                Email = null,

            };
            try
            {
               // if (sqlConnection != null)
               // {
                    Random r = new Random();
                    response.OTP = r.Next(100000, 999999);

                    _logger.LogInformation("Enter in Repository Layer of (ForgetPassword) ");
                    SqlCommand sqlCommand = new SqlCommand("sp_ForgetPassword", this.sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@OTP", response.OTP);
                    //sqlCommand.Parameters.AddWithValue("@flag","Status");
                    sqlConnection.Open();
                    DbDataReader db = await sqlCommand.ExecuteReaderAsync();
                    if (db.HasRows)
                    {
                        await db.ReadAsync();
                        int status = Convert.ToInt32(db["Status"]);
                        if (status == 1)
                        {
                            response.Id = db["FId"] != DBNull.Value ? Convert.ToInt32(db["FId"]) : 0;
                            response.Email = request.Email;
                            //res.Email = db["Email"] != DBNull.Value ? db["Email"].ToString() : null;

                            response.Role = db["Roles"] != DBNull.Value ? db["Roles"].ToString() : string.Empty;

                            //res.Message = "OK";
                        }
                        else
                        {
                            // res.Message = "Please check the EmailID";
                            response.IsSuccess = false;
                        }
                    }
                                //else
                            //{
                         //    response.IsSuccess = true;
                        //}
                        //}
                else
                {
                    response.IsSuccess = false;
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Forget password):{ex}");
                response.IsSuccess = false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return response;
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            string Password = string.Empty;
            ResetPasswordResponse response = new ResetPasswordResponse()
            {
                IsSuccess = true,
                Email = null,

            };

            try
            {
                if (sqlConnection != null)
                {
                    _logger.LogInformation("Enter in Repository Layer of (Login) ");
                    SqlCommand sqlCommand = new SqlCommand("sp_ResetPassword", this.sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    if (request.NewPassword == request.ConfirmPassword)
                    {
                        Password = PasswordProcessing.Encrypt(request.NewPassword.ToString(), _configuration["SecurityKey"]);
                        sqlCommand.Parameters.AddWithValue("@Password", Password);
                        sqlConnection.Open();
                        DbDataReader db = await sqlCommand.ExecuteReaderAsync();
                        await db.ReadAsync();
                        //response.Id = db["FId"] != DBNull.Value ? Convert.ToInt32(db["FId"]) : 0;
                        response.Email = db["Email"] != DBNull.Value ? db["Email"].ToString() : null;
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }

                }

            }
                    catch (Exception ex)
                {
                _logger.LogError($"Repository layer exception (Forget password):{ex}");
                response.IsSuccess = false;
                }
            finally
            {
                sqlConnection.Close();
            }
            return response;
        }

    }
}

