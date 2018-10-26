using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using Dapper;

namespace Cat.Automation.UI.Utilities
{
    public class DB_Data
    {
        private static Configuration appConfig;
        public static SqlConnection sql;
        public static string HarvesterDbConnString;
        public static int ServiceProviderData_id;
        public static int RegisteredAsset_count;
        List<ServiceproviderDBModel> DBData_Feed = new List<ServiceproviderDBModel>();
      //  List<DBData_ED_CCDSID> DBData_Feed = new List<DBData_ED_CCDSID>();

        DBData_ED_CCDSID DBData_EdCCDsId = new DBData_ED_CCDSID();
        List<ServiceProvider> DBData_ListFeed = new List<ServiceProvider>();
        FeedConfig_DBModel FeedConfig_SavedDBData = new FeedConfig_DBModel();
        DBFeed_URLSchema DataFeed_URLSchema = new DBFeed_URLSchema();
        DBData_ED_CCDSID DBData_Serviceproviderasset = new DBData_ED_CCDSID();
        

        static DB_Data()
        {
            appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            HarvesterDbConnString = appConfig.AppSettings.Settings["HarvesterDB"].Value;
             sql = new SqlConnection(HarvesterDbConnString);
       }
        public class FeedConfig_DBModel
        {
            public string UserName;
            public string Password;
            public string OptionalName;
            public string Url;
        }
        public class ServiceproviderDBModel
        {
            public string Provider;
            public string Url;
            public int FeedSchema_id;
            public string UserName;
            public string Password;
            public string OptionalName;
        }
        public class DBData_ED_CCDSID
        {
            public string EdrefId;
            public string  CCDSRefid;
            public string status;
            public string   DealerDescription;
            public string BusinessUnitDescription;
            public string year;
            public string VIN;
            public string CATMakeCode;
        }

        public class DBFeed_URLSchema
        {
            public string Url;
            public int FeedSchema_id;
        }
        public class ServiceProvider
        {
        public string Feed;
        }
        public int RegisteredAssetcount(string username, string OrganizationId)
        {
            try
            {
                sql.Open();
                string Query = @"select count(id)  from serviceproviderdataasset where status =3 and ServiceProviderData_id in (select id from serviceproviderdata where UserName = @Uname and Organization_id = @OrgId)";
                RegisteredAsset_count = sql.Query<int>(Query, new { Uname = username, OrgId = OrganizationId }).FirstOrDefault();

            }
            catch (Exception e)
            { }
            finally
            {
                sql.Close();
            }
            return RegisteredAsset_count;
        }
        public DBData_ED_CCDSID DBdata_Ed_CCDSId(string username, string SerialNo, string OrganizationId)
        {
            //username = "aemp@aertssen.be";
            //SerialNo = "A25F013057";
            //OrganizationId = "2772";
            try
            {
                sql.Open();
                string serviceprovider = @"select EdrefId,CCDSRefid,status,DealerDescription,BusinessUnitDescription,CATMakeCode,year,VIN from Serviceproviderdataasset where serialnumber = @Sno and ServiceProviderData_id in (select id from serviceproviderdata where UserName = @Uname and Organization_id = @OrgId)";
                DBData_EdCCDsId = sql.Query<DBData_ED_CCDSID>(serviceprovider, new { Uname = username, Sno = SerialNo, OrgId = OrganizationId }).FirstOrDefault();

            }
            catch (Exception e)
            {

            }
            finally
            {
                sql.Close();
            }

            return DBData_EdCCDsId;
        }

        public List<ServiceproviderDBModel> FetchDBData(string ServiceProvider)
        {

            try
            {
                sql.Open();
                string serviceprovider = @"Select SP.Feed as Provider, SP.Url, SP.FeedSchema_id,SPD.UserName, SPD.Password,SPD.OptionalName from ServiceProvider SP
left join  Serviceproviderdata SPD
on SP.Id = SPD.serviceProvider_id
where SP.Feed = @ServcProvider";
                DBData_Feed = sql.Query<ServiceproviderDBModel>(serviceprovider, new { ServcProvider = ServiceProvider }).ToList();

            }
            catch (Exception e)
            {

            }
            finally
            {
                sql.Close();
            }

            return DBData_Feed;
        }

        public List<ServiceProvider> List_Provider()
        {

            try
            {
                sql.Open();
                string Query = @"select Feed from serviceprovider where status =1";
                DBData_ListFeed = sql.Query<ServiceProvider>(Query).ToList();

            }
            catch (Exception e)
            {  }
            finally
            {
                sql.Close();
            }

            return DBData_ListFeed;
        }
        public int ServiceProviderDataId_UserName(string username)
        {
            try
            {
                sql.Open();
                string Query = @"select top 1 id from serviceproviderdata where UserName = @Uname";
                ServiceProviderData_id = sql.Query<int>(Query, new { Uname = username }).FirstOrDefault();

            }
            catch (Exception e)
            { }
            finally
            {
                sql.Close();
            }
            return ServiceProviderData_id;
        }

        public FeedConfig_DBModel Verify_FeddConfig_DBData(string Username)
        {          
            try
            {
                sql.Open();
                string Query = @"select SPD.UserName,SPD.Password, SPD.OptionalName,SPCD.Url from serviceproviderdata SPD
left join  Serviceprovidercustomdata SPCD
on SPD.Id = SPCD.serviceProviderData_id
where SPD.UserName= @Uname";
                FeedConfig_SavedDBData = sql.Query<FeedConfig_DBModel>(Query, new { Uname = Username }).FirstOrDefault();

            }
            catch (Exception e)
            { }
            finally
            {
                sql.Close();
            }

            return FeedConfig_SavedDBData;
        }

        public FeedConfig_DBModel Verify_UpadtedFeddConfig_DBData(int Id)
        {
            try
            {
                sql.Open();       
                string Query = @"select SPD.UserName,SPD.Password, SPD.OptionalName,SPCD.Url from serviceproviderdata SPD
left join  Serviceprovidercustomdata SPCD
on SPD.Id = SPCD.serviceProviderData_id
where SPD.Id= @SPDId";
                FeedConfig_SavedDBData = sql.Query<FeedConfig_DBModel>(Query, new { SPDId = Id }).FirstOrDefault();

            }
            catch (Exception e)
            { }
            finally
            {
                sql.Close();
            }

            return FeedConfig_SavedDBData;
        }

        public int Verify_UserName_Count(string Username)
        {
            int count =0;
            try
            {
                sql.Open();
                string Query = @"select count(UserName) from Serviceproviderdata where UserName = @Uname";

                 count = sql.Query<int>(Query, new { Uname = Username }).FirstOrDefault();

            }
            catch (Exception e)
            { }
            finally
            {
                sql.Close();
            }

            return count;
        }

        public DBFeed_URLSchema DB_Feed_URLSchema(string Feed_Provider)
        {
            try
            {
                sql.Open();
                string Query = @"select URL,FeedSchema_id from serviceprovider where Feed = @ServcProvider and status =1";
                DataFeed_URLSchema = sql.Query<DBFeed_URLSchema>(Query, new { ServcProvider = Feed_Provider }).FirstOrDefault();

            }
            catch (Exception e)
            { }
            finally
            {
                sql.Close();
            }

            return DataFeed_URLSchema;
        }

        public void Get_Json(string FeedId)
        {
             try
            {
                sql.Open();
                string Query = @"select XmlDraft from rawdataqueue where id = @Feed";

             string Json = sql.Query<string>(Query, new { Feed = FeedId }).FirstOrDefault();

            }
            catch (Exception e)
            { }
            finally
            {
                sql.Close();
            }
        }
        public string Get_DuplicateUser(string serviceprovider)
        {
            string UserName = "";
            int id = Get_ServiceProviderId(serviceprovider); 
            try
            {
                sql.Open();
                string Query = @"select top 1 username from serviceproviderdata where Status = 1 and ServiceProvider_id = @Serviceproviderid";

                UserName = sql.Query<string>(Query, new  { Serviceproviderid = id }).FirstOrDefault();

            }
            catch (Exception e)
            { }
            finally
            {
                sql.Close();
            }
            return UserName;
        }

        public int Get_ServiceProviderId(string ServiceProvider)
        {
            int ServiceProviderId = 0;
            try
            {
                sql.Open();
                string Q = @"select id from serviceprovider where feed = @ServcProvider";
                ServiceProviderId = sql.Query<int>(Q, new { ServcProvider = ServiceProvider }).Single();
            }
            catch (Exception e)
            {

            }
            finally
            {
                sql.Close();
            }

            return ServiceProviderId;


        }
    }
}
