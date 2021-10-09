using ECommerce.CommonLayer.Model;
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
    public class UserRL : IUserRL
    {
        public readonly ILogger<UserRL> _logger;

        public readonly IConfiguration _configuration;
        public readonly SqlConnection sqlConnection;
        public UserRL(IConfiguration configuration, ILogger<UserRL> logger)
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
        

       

        public async Task<UserAddResponse> UserAddress(UserAddRequest request)
        {
            UserAddResponse response = new UserAddResponse();
            response.IsSuccess = true;
            try
            {
                //String SqlQuery = SqlQueries.RegisterEmployee;
                SqlCommand sqlCommand = new SqlCommand("UserAddress", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 180;
                sqlCommand.Parameters.AddWithValue("@Address1", request.Address1);
                sqlCommand.Parameters.AddWithValue("@Address2", request.Address2);
                sqlCommand.Parameters.AddWithValue("@City", request.City);
                sqlCommand.Parameters.AddWithValue("@State", request.State);
                //string strkey = _configuration["SecurityKey"].ToString();
                //sqlCommand.Parameters.AddWithValue("@Password", Processor.PasswordProcessing.Encrypt(request.Password, strkey));
                sqlCommand.Parameters.AddWithValue("@DoC", request.DOC);
                sqlCommand.Parameters.AddWithValue("@DoM", request.DOM);
                sqlCommand.Parameters.AddWithValue("@IsActive", (request.IsActive));
               // sqlCommand.Parameters.AddWithValue("@Role", (request.Role));

                sqlConnection.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    response.IsSuccess = false;

                }
                else
                {
                   // response.IsActive = request.IsActive;
                    
                    response.Message = " User Address added Successfully";
                }

                

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
          
        }
    }
