using ECommerce.CommonLayer.Model;
using ECommerce.Helper;
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
    public class ProductRL : IProductRL
    {

        public readonly ILogger<ProductRL> _logger;

        public readonly IConfiguration _configuration;
        public readonly SqlConnection sqlConnection;
        public ProductRL(IConfiguration configuration, ILogger<ProductRL> logger)
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

        public async Task<AddProductResponse> AddProduct(AddProductRequest request)
        {
            AddProductResponse response = new AddProductResponse()
            {
                IsSuccess = true
            };

            try
            {
                SqlCommand sqlCommand = new SqlCommand("sp_Products", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 180;

                sqlCommand.Parameters.AddWithValue("@StatementType", "Add");

                sqlCommand.Parameters.AddWithValue("@ProductName", request.ProductName);
                sqlCommand.Parameters.AddWithValue("@Category", request.Category);
                sqlCommand.Parameters.AddWithValue("@Price", request.Price);

                sqlConnection.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    response.IsSuccess = false;
                }
                //else
                //{

                //    response.Message = "Product Added  Successfully";
                //}

            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Adding Product):{ex}");
                response.IsSuccess = false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return response;


        }

        public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request)
        {
            UpdateProductResponse response = new UpdateProductResponse()
            {
                IsSuccess = true
            };


            try
            {
                // String SqlQuery = SqlQueries.AddProduct;
                SqlCommand sqlCommand = new SqlCommand("sp_Products", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 180;

                sqlCommand.Parameters.AddWithValue("@StatementType", "Update");

                sqlCommand.Parameters.AddWithValue("@ProductName", request.ProductName);
                sqlCommand.Parameters.AddWithValue("@Category", request.Category);
                sqlCommand.Parameters.AddWithValue("@Price", request.Price);

                sqlConnection.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    response.IsSuccess = false;
                }
                else
                {

                    response.Message = "Product Updated  Successfully";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Adding Product):{ex}");
                response.IsSuccess = false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return response;


        }

        public async Task<List<GetAllProductsResponse>> GetAllProducts()
        {
            var resultList = new List<GetAllProductsResponse>();

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Get All Product list) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Products", this.sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 180;
                sqlCommand.Parameters.AddWithValue("@StatementType", "GetAll");
                sqlConnection.Open();

                using (DbDataReader db = await sqlCommand.ExecuteReaderAsync())
                {
                    if (db.HasRows)
                    {
                        while (await db.ReadAsync())

                            resultList.Add(new GetAllProductsResponse()
                            {
                                ProductId = db["ProductId"] != DBNull.Value ? Convert.ToInt32(db["ProductId"]) : 0,
                                ProductName = db["ProductName"] != DBNull.Value ? (db["ProductName"]).ToString() : null,
                                //Brand = db["Brand"] != DBNull.Value ? (db["Brand"]).ToString() : null,
                                //Color = db["Color"] != DBNull.Value ? (db["Color"]).ToString() : null,
                                // Dimensions = db["Dimensions"] != DBNull.Value ? (db["Dimensions"]).ToString() : null,
                                //Category = db["Category"] != DBNull.Value ? (db["Category"]).ToString() : null,
                                Price = db["Price"] != DBNull.Value ? Convert.ToDouble((db["Price"])) : 0.0,
                                //Images = db["Images"] != DBNull.Value ? (db["Images"]).ToString() : null,
                                IsActive = db["IsActive"] != DBNull.Value ? Convert.ToBoolean(db["IsActive"]) : false,
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
                sqlConnection.Close();
            }

            return resultList;
        }

        public async Task<GetProductByNameResponse> GetProductByName(GetProductByNameRequest request)
        {
            GetProductByNameResponse response = new GetProductByNameResponse()
            {
                IsSuccess = true,

                ProductName = null,
                Category = null,
                Price = 0,
                IsActive = false
                //Brand = null,Color = null,Dimensions = null,CreatedByAdminId = 0,Images = null,

            };

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Login) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Products", this.sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 180;
                sqlCommand.Parameters.AddWithValue("@StatementType", "GetId");
                sqlCommand.Parameters.AddWithValue("@ProductName", request.ProductName);
                sqlConnection.Open();
                using (DbDataReader dc = await sqlCommand.ExecuteReaderAsync())
                {
                    if (dc.HasRows)
                    {
                        await dc.ReadAsync();
                        response.ProductId = dc["ProductId"] != DBNull.Value ? Convert.ToInt32(dc["ProductId"]) : 0;
                        response.ProductName = dc["ProductName"] != DBNull.Value ? (dc["ProductName"]).ToString() : null;
                        //              res.Brand = dc["Brand"] != DBNull.Value ? (dc["Brand"]).ToString() : null;
                        //    //                res.Color = dc["Color"] != DBNull.Value ? (dc["Color"]).ToString() : null;
                        //    //                res.Dimensions = dc["Dimensions"] != DBNull.Value ? (dc["Dimensions"]).ToString() : null;
                        response.Category = dc["Category"] != DBNull.Value ? (dc["Category"]).ToString() : null;
                        response.Price = dc["Price"] != DBNull.Value ? Convert.ToDouble(dc["Price"]) : 0;
                        //    //                //res.CreatedByAdminId = dc["CreatedByAdminId"] != DBNull.Value ? Convert.ToInt32(dc["CreatedByAdminId"]) : 0;
                        //    //                res.Images = dc["Images"] != DBNull.Value ? (dc["Images"]).ToString() : null;
                        response.IsActive = dc["IsActive"] != DBNull.Value ? Convert.ToBoolean(dc["IsActive"]) : false;
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
                response.IsSuccess = false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return response;
        }

        public async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request)
        {
            {
                DeleteProductResponse response = new DeleteProductResponse()
                {
                    IsSuccess = true
                };

                try
                {
                    _logger.LogInformation("Enter in Repository Layer of (Login) ");
                    SqlCommand sqlCommand = new SqlCommand("sp_Products", this.sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@StatementType", "DeleteById");
                    sqlCommand.Parameters.AddWithValue("@ProductId", request.ProductId);
                    sqlConnection.Open();
                    int status = await sqlCommand.ExecuteNonQueryAsync();
                    bool statuss = (response.IsSuccess);
                    if (status <= 0)
                    {
                        response.IsSuccess = false;
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Repository layer exception (Updating Product):{ex}");
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
}






