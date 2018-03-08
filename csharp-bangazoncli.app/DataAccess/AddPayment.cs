using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_bangazoncli.app.DataAccess
{
    class AddPayment
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["BangazonCLI"].ConnectionString;

        public bool AddPaymentType(string pmtType, int customerId, int acctNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO [dbo].[PmtType]
                           ([PmtType]
                           ,[CustomerId]
                           ,[AcctNumber])
                     VALUES
                           (@pmtType), @customerId, @acctNumber)";
                           
 
                var pmtTypeParam = new SqlParameter("@pmtType", SqlDbType.NVarChar);
                pmtTypeParam.Value = pmtType;
                cmd.Parameters.Add(pmtTypeParam);

                var customerIdParam = new SqlParameter("@customerId", SqlDbType.Int);
                customerIdParam.Value = customerId;
                cmd.Parameters.Add(customerIdParam);

                var acctNumberParam = new SqlParameter("@acctNumber", SqlDbType.Int);
                acctNumberParam.Value = acctNumber;
                cmd.Parameters.Add(acctNumberParam);

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }
    }
}
