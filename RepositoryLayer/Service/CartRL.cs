using ECommerce.CommonLayer.Model;
using ECommerce.RepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.RepositoryLayer.Service
{
    public class CartRL : ICartRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection sqlConnection;
        public readonly ILogger<CartRL> _logger;
        private SqlConnection sqlConnectionVariable;
        const int ConnectionTimeout = 180;

        public CartRL(ILogger<CartRL> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            this.sqlConnectionVariable = new SqlConnection(_configuration["ConnectionStrings:DatabaseConnectionString"]);
        }
        //       Adding Product in cart
        public async Task<AddToCartResponse> AddToCart(AddToCartRequest request)
        {
            string Password = string.Empty;
            AddToCartResponse response = new AddToCartResponse()
            {
                IsSuccess = true
            };

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Cart) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Cart", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "AddtoCart");
                sqlCommand.Parameters.AddWithValue("@ProductId", request.ProductId);
                sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                sqlCommand.Parameters.AddWithValue("@Quantity", request.Quantity);
                sqlConnectionVariable.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    response.IsSuccess = false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Adding Product to cart):{ex}");
                response.IsSuccess = false;
            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return response;
        }


        //    Update Product in Cart
        public async Task<UpdateCartResponse> UpdateCart(UpdateCartRequest request)
        {
            UpdateCartResponse response = new UpdateCartResponse()
            {
                IsUpdateSuccess = true
            };

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (UpdateCart) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Cart", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "UpdateCart");
                sqlCommand.Parameters.AddWithValue("@CartId", request.CartId);
                sqlCommand.Parameters.AddWithValue("@productId", request.ProductId);
                sqlCommand.Parameters.AddWithValue("@userId", request.UserId);
                sqlCommand.Parameters.AddWithValue("@Quantity", request.Quantity);

                sqlConnectionVariable.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    response.IsUpdateSuccess = false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
                response.IsUpdateSuccess = false;
            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return response;
        }

        //Delete Cart Product
        public async Task<DeleteFromCartResponse> DeleteFromCart(DeleteFromCartRequest request)
        {
            DeleteFromCartResponse response = new DeleteFromCartResponse()
            {
                IsDeleteSuccess = true
            };

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Login) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Cart", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "DeleteFromCart");
                sqlCommand.Parameters.AddWithValue("@CartId", request.CartId);
                sqlConnectionVariable.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    response.IsDeleteSuccess = false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
                response.IsDeleteSuccess = false;
            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return response;
        }

        //    Get All Product list added in cart
        public async Task<List<GetAllCartResponse>> GetAllCart(GetAllCartRequest request)
        {
            var resultList = new List<GetAllCartResponse>();

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Get All Product list in cart) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Cart", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 180;
                sqlCommand.Parameters.AddWithValue("@StatementType", "GetAll");
                sqlCommand.Parameters.AddWithValue("@Id", request.UserId);
                sqlConnectionVariable.Open();
                using (DbDataReader db = await sqlCommand.ExecuteReaderAsync())
                {
                    if (db.HasRows)
                    {
                        while (await db.ReadAsync())
                            resultList.Add(new GetAllCartResponse()
                            {
                                ProductId = db["ProductId"] != DBNull.Value ? Convert.ToInt32(db["ProductId"]) : 0,
                                CartId = db["CartId"] != DBNull.Value ? Convert.ToInt32(db["CartId"]) : 0,
                                ProductName = db["ProductName"] != DBNull.Value ? (db["ProductName"]).ToString() : null,
                                Category = db["Category"] != DBNull.Value ? (db["Category"]).ToString() : null,
                                Price = db["Price"] != DBNull.Value ? Convert.ToDouble((db["Price"])) : 0.0,
                                Images = db["Images"] != DBNull.Value ? (db["Images"]).ToString() : null,
                                Quantity = db["Quantity"] != DBNull.Value ? Convert.ToInt32(db["Quantity"]) : 0,
                                IsAvailable = db["IsActive"] != DBNull.Value ? Convert.ToBoolean(db["IsActive"]) : false,
                                IsSuccess = true
                            });

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
            }
            finally
            {
                sqlConnectionVariable.Close();
            }

            return resultList;
        }

        // Save for later

        public async Task<SaveForLaterResponse> SaveForLater(SaveForLaterRequest request)
        {
            SaveForLaterResponse response = new SaveForLaterResponse()
            {
                IsUpdateSuccess = true
            };

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (UpdateCart) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Cart", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "SaveForLater");
                sqlCommand.Parameters.AddWithValue("@CartId", request.CartId);
                sqlCommand.Parameters.AddWithValue("@productId", request.ProductId);
                sqlCommand.Parameters.AddWithValue("@userId", request.UserId);
                sqlCommand.Parameters.AddWithValue("@Quantity", request.Quantity);

                sqlConnectionVariable.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    response.IsUpdateSuccess = false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
                response.IsUpdateSuccess = false;
            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return response;
        }


    }

}
