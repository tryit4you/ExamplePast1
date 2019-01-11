using ExamplePast1.Data.Models;
using ExamplePast1.Shared;
using ExamplePast1.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ExamplePast1.Data.Dao
{
    public class DataBaseDao
    {
        ConnectDatabase connect = null;
        public DataBaseDao()
        {
            connect = new ConnectDatabase();
        }
        public IEnumerable<DataListViewModel> GetListDataBase()
        {
            List<DataListViewModel> listData = new List<DataListViewModel>();

            var query = "SELECT * FROM master.sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb') ORDER BY NAME ASC";
            SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);

            SqlDataReader r = command.ExecuteReader();
            while (r.Read())
            {
                string dbName = r["name"].ToString();
                string dbId = r["database_id"].ToString();
                string dbCreatedate = r["create_date"].ToString();
                var model = new DataListViewModel
                {
                    DatabaseId = dbId,
                    DatabaseName = dbName,
                    CreateDate = dbCreatedate
                };
                listData.Add(model);
            }
            r.Close();
            return listData;
        }
        public bool CreateDatabase(string databaseName)
        {
            var query = $"create database {databaseName}";
            SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);
            try
            {
                int r = command.ExecuteNonQuery();
                if (r != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException e)
            {
                return false;
            }
        }
        public bool CheckExist()
        {
            return false;
        }
        public bool CreateTable(string dataName, string tableName)
        {

            var useQuery = $"USE {dataName}";
            var query = $"create TABLE  {tableName} " +
               $"(ID_DONVIHANHCHINH int  NOT NULL DEFAULT(0)," +
               $"TEN_CAYXANH VARCHAR(100)," +
               $"THONGTINCHITIET NVARCHAR(500)," +
               $"TEN_LOAICAYXANH NVARCHAR(100)," +
               $"BANKINHTANLA FLOAT NOT NULL DEFAULT(0)," +
               $"IS_BOVIEN BIT NOT NULL DEFAULT(0)," +
               $"IS_PLANT BIT NOT NULL DEFAULT(0)," +
               $"IS_ONGNUOC BIT NOT NULL DEFAULT(0)," +
               $"CAYXANH VARCHAR(100)," +
               $"IS_CAOHONVIAHE BIT NOT NULL DEFAULT(0)," +
               $"IS_ONGCONG BIT NOT NULL DEFAULT(0)," +
               $"ID_HINHDANGHO INT NOT NULL DEFAULT(0)," +
               $"COUNT INT NOT NULL DEFAULT(0)," +
               $"TENCONGTRINHGANNHAT NVARCHAR(100)," +
               $"ID_TINHTRANGSINHTRUONG INT NOT NULL DEFAULT(0)," +
               $"LONGITUDE_HOTRONGCAY FLOAT NOT NULL DEFAULT(0)," +
               $"NAMBANGIAOCONGTRINH VARCHAR(50)," +
               $"TEN_PHANLOAIQUYHOACH NVARCHAR(100)," +
               $"IMAGE NVARCHAR(100)," +
               $"DUONGKINHGOC FLOAT NOT NULL DEFAULT(0.0)," +
               $"ID_LOAICAYXANH INT NOT NULL DEFAULT(0)," +
               $"TEN_CHUNGLOAICAYXANH NVARCHAR(50)," +
               $"DIENTICHTANLA FLOAT NOT NULL DEFAULT(0)," +
               $"ID_CAYXANH INT NOT NULL DEFAULT(0)," +
               $"CHIEUCAOVUTNGON FLOAT NOT NULL DEFAULT(0)," +
               $"GHICHU NVARCHAR(500)," +
               $"ID_HOTRONGCAY INT NOT NULL DEFAULT(0)," +
               $"DUONGPHO NVARCHAR(100)," +
               $"ID_KHUVUCCAYXANH INT NOT NULL DEFAULT(0)," +
               $"KHUVUCCAYXANH NVARCHAR," +
               $"NAMTRONGCAY VARCHAR(50)," +
               $"DONVIHANHCHINH NVARCHAR(100)," +
               $"SOLANCAPNHAT INT NOT NULL DEFAULT(0)," +
               $"ID_NHOMNGUYCO INT NOT NULL DEFAULT(0)," +
               $"IS_DAYDIEN BIT NOT NULL DEFAULT(0)," +
               $"NGAYTAO VARCHAR(50)," +
               $"ID_LOAIBOVIEN INT NOT NULL DEFAULT(0)," +
               $"SONHA NVARCHAR(100) ," +
               $"KICHTHUOCHO FLOAT NOT NULL DEFAULT(0)," +
               $"DIENTICHHO INT NOT NULL DEFAULT(0)," +
               $"IS_BORAO BIT NOT NULL DEFAULT(0)," +
               $"TRANGTHAIBOVIEN NVARCHAR(10)," +
               $"LATITUDE_HOTRONGCAY FLOAT NOT NULL DEFAULT(0)," +
               $"ENDX INT NOT NULL DEFAULT(0)," +
               $"STARTX INT NOT NULL DEFAULT(0)," +
               $"ENDY INT NOT NULL DEFAULT(0)," +
               $"STARTY INT NOT NULL DEFAULT(0)," +
               $"LOAIBOVIEN NVARCHAR(50)," +
               $"KHOANGCACHHOVIA FLOAT NOT NULL DEFAULT(0.0)," +
               $"MA_CAYXANH NVARCHAR(100)," +
               $"ID_TRANGTHAIBOVIEN INT NOT NULL DEFAULT(0)," +
               $"TEN_NHOMNGUYCO NVARCHAR(100)," +
               $"HINHDANGHO NVARCHAR(50)," +
               $"IS_CAPNGAM BIT NOT NULL DEFAULT(0)," +
               $"ID_NHOMCHUNGLOAI INT NOT NULL DEFAULT(0)," +
               $"MA_HOTRONGCAY VARCHAR(50)," +
               $"ID_PHANLOAIQUYHOACH INT NOT NULL DEFAULT(0)," +
               $"TEN_TINHTRANGSINHTRUONG NVARCHAR(100)" +
               $" )";
            using (SqlCommand cmd = new SqlCommand(useQuery, ConnectDatabase.connect))
            {
                cmd.ExecuteNonQuery();
            }


            SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);
            try
            {
                int r = command.ExecuteNonQuery();
                if (r != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException e)
            {
                Debug.Write("Lỗi:" + e.Message);
                return false;
            }
        }
        public bool DeleteDatabase(string databaseName)
        {
            var query = $"use master\n" +
                $" drop database {databaseName}";
            SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);
            try
            {
                int r = command.ExecuteNonQuery();
                if (r != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException e)
            {
                Debug.Write(e);
                return false;
            }
        }
        public bool DeleteTable(string tableName, string dataName)
        {
            var query = $"use {dataName}\n" +
                $" drop table {tableName}";
            SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);
            try
            {
                int r = command.ExecuteNonQuery();
                if (r != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException e)
            {
                Debug.Write(e);
                return false;
            }
        }

        public IEnumerable<TableListViewModel> GetListTable(string databaseName)
        {
            List<TableListViewModel> listData = new List<TableListViewModel>();
            var query = $"SELECT * FROM {databaseName}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);
            SqlDataReader r = command.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    string tb_catalog = r["TABLE_CATALOG"].ToString();
                    string tb_name = r["TABLE_NAME"].ToString();
                    var model = new TableListViewModel
                    {
                        DatabaseName = tb_catalog,
                        TableName = tb_name
                    };
                    listData.Add(model);
                }
                r.Close();
                return listData;

            }
            catch (SqlException e)
            {
                return null;
            }
        }
        public ImportResultViewModel ImportData(string tableName, string dataName, List<Map4D> map4Ds)
        {
            var watch = Stopwatch.StartNew();
            int r = 0, i = 0;
            foreach (var mapdata in map4Ds)
            {
                var xyvn2000 = Coordinate.xyvn2000_2_blwgs84(mapdata.LATITUDE_HOTRONGCAY, mapdata.LONGITUDE_HOTRONGCAY, MapParams.pdo, MapParams.pphut, MapParams.zone);
                #region query
                var query = $"use {dataName}\n" + "INSERT INTO " + tableName + "(ID_DONVIHANHCHINH ,TEN_CAYXANH,THONGTINCHITIET,TEN_LOAICAYXANH,BANKINHTANLA , IS_BOVIEN , " +
                    "IS_PLANT ,  IS_ONGNUOC , CAYXANH , IS_CAOHONVIAHE , IS_ONGCONG , ID_HINHDANGHO , COUNT , " +
                    "TENCONGTRINHGANNHAT,ID_TINHTRANGSINHTRUONG,LONGITUDE_HOTRONGCAY,NAMBANGIAOCONGTRINH,TEN_PHANLOAIQUYHOACH,IMAGE," +
                    "DUONGKINHGOC,ID_LOAICAYXANH,TEN_CHUNGLOAICAYXANH,DIENTICHTANLA,ID_CAYXANH,CHIEUCAOVUTNGON,GHICHU," +
                    "ID_HOTRONGCAY,DUONGPHO,ID_KHUVUCCAYXANH, KHUVUCCAYXANH , NAMTRONGCAY, DONVIHANHCHINH,SOLANCAPNHAT," +
                    "ID_NHOMNGUYCO,IS_DAYDIEN,NGAYTAO,ID_LOAIBOVIEN,SONHA,KICHTHUOCHO , DIENTICHHO , IS_BORAO , TRANGTHAIBOVIEN " +
                    ", LATITUDE_HOTRONGCAY , ENDX , STARTX , ENDY , STARTY , LOAIBOVIEN , KHOANGCACHHOVIA , " +
                    "MA_CAYXANH,ID_TRANGTHAIBOVIEN,TEN_NHOMNGUYCO,HINHDANGHO, IS_CAPNGAM, ID_NHOMCHUNGLOAI , MA_HOTRONGCAY, " +
                    "ID_PHANLOAIQUYHOACH,TEN_TINHTRANGSINHTRUONG)" +
                    "VALUES(@ID_DONVIHANHCHINH , @TEN_CAYXANH,@THONGTINCHITIET,@TEN_LOAICAYXANH,@BANKINHTANLA , @IS_BOVIEN , " +
                    "@IS_PLANT ,  @IS_ONGNUOC , @CAYXANH , @IS_CAOHONVIAHE , @IS_ONGCONG , @ID_HINHDANGHO , @COUNT , " +
                    "@TENCONGTRINHGANNHAT,@ID_TINHTRANGSINHTRUONG,@LONGITUDE_HOTRONGCAY,@NAMBANGIAOCONGTRINH,@TEN_PHANLOAIQUYHOACH,@IMAGE," +
                    "@DUONGKINHGOC,@ID_LOAICAYXANH,@TEN_CHUNGLOAICAYXANH,@DIENTICHTANLA,@ID_CAYXANH,@CHIEUCAOVUTNGON,@GHICHU," +
                    "@ID_HOTRONGCAY,@DUONGPHO,@ID_KHUVUCCAYXANH, @KHUVUCCAYXANH , @NAMTRONGCAY, @DONVIHANHCHINH,@SOLANCAPNHAT," +
                    "@ID_NHOMNGUYCO,@IS_DAYDIEN,@NGAYTAO,@ID_LOAIBOVIEN,@SONHA,@KICHTHUOCHO , @DIENTICHHO , @IS_BORAO , @TRANGTHAIBOVIEN " +
                    ", @LATITUDE_HOTRONGCAY , @ENDX , @STARTX , @ENDY , @STARTY , @LOAIBOVIEN , @KHOANGCACHHOVIA , " +
                    "@MA_CAYXANH,@ID_TRANGTHAIBOVIEN,@TEN_NHOMNGUYCO,@HINHDANGHO, @IS_CAPNGAM, @ID_NHOMCHUNGLOAI , @MA_HOTRONGCAY, " +
                    "@ID_PHANLOAIQUYHOACH,@TEN_TINHTRANGSINHTRUONG)";
                #endregion
                //var query = $"use {dataName}\n" + "INSERT INTO " + tableName + "(ID_DONVIHANHCHINH ,TEN_CAYXANH,THONGTINCHITIET,TEN_LOAICAYXANH,BANKINHTANLA , IS_BOVIEN , " +
                //    "IS_PLANT ,  IS_ONGNUOC , CAYXANH , IS_CAOHONVIAHE , IS_ONGCONG , ID_HINHDANGHO , COUNT , " +
                //    "TENCONGTRINHGANNHAT,ID_TINHTRANGSINHTRUONG,LONGITUDE_HOTRONGCAY,NAMBANGIAOCONGTRINH,TEN_PHANLOAIQUYHOACH,IMAGE," +
                //    "DUONGKINHGOC,ID_LOAICAYXANH,TEN_CHUNGLOAICAYXANH,DIENTICHTANLA,ID_CAYXANH,CHIEUCAOVUTNGON,GHICHU," +
                //    "ID_HOTRONGCAY,DUONGPHO,ID_KHUVUCCAYXANH, KHUVUCCAYXANH , NAMTRONGCAY, DONVIHANHCHINH,SOLANCAPNHAT," +
                //    "ID_NHOMNGUYCO,IS_DAYDIEN,NGAYTAO,ID_LOAIBOVIEN,SONHA,KICHTHUOCHO , DIENTICHHO , IS_BORAO , TRANGTHAIBOVIEN " +
                //    ", LATITUDE_HOTRONGCAY , ENDX , STARTX , ENDY , STARTY , LOAIBOVIEN , KHOANGCACHHOVIA , " +
                //    "MA_CAYXANH,ID_TRANGTHAIBOVIEN,TEN_NHOMNGUYCO,HINHDANGHO, IS_CAPNGAM, ID_NHOMCHUNGLOAI , MA_HOTRONGCAY, " +
                //    "ID_PHANLOAIQUYHOACH,TEN_TINHTRANGSINHTRUONG)" +
                //    $"VALUES({mapdata.ID_DONVIHANHCHINH} , '{mapdata.TEN_CAYXANH}','{mapdata.THONGTINCHITIET}','{mapdata.TEN_LOAICAYXANH}',{mapdata.BANKINHTANLA }, {(mapdata.IS_BOVIEN?1:0 )}, " +
                //    $"{(mapdata.IS_PLANT?1:0)} ,  {(mapdata.IS_ONGNUOC?1:0)} , '{mapdata.CAYXANH}' , {(mapdata.IS_CAOHONVIAHE?1:0)} , {(mapdata.IS_ONGCONG?1:0)} , '{mapdata.ID_HINHDANGHO}' , {mapdata.COUNT }, " +
                //    $"'{mapdata.TENCONGTRINHGANNHAT}',{mapdata.ID_TINHTRANGSINHTRUONG},{mapdata.LONGITUDE_HOTRONGCAY},{mapdata.NAMBANGIAOCONGTRINH},'{mapdata.TEN_PHANLOAIQUYHOACH}','{mapdata.IMAGE}'," +
                //    $"{mapdata.DUONGKINHGOC},{mapdata.ID_LOAICAYXANH},'{mapdata.TEN_CHUNGLOAICAYXANH}',{mapdata.DIENTICHTANLA},{mapdata.ID_CAYXANH},{mapdata.CHIEUCAOVUTNGON},'{mapdata.GHICHU}'," +
                //    $"{mapdata.ID_HOTRONGCAY},'{mapdata.DUONGPHO}',{mapdata.ID_KHUVUCCAYXANH}, '{mapdata.KHUVUCCAYXANH }', {mapdata.NAMTRONGCAY}, '{mapdata.DONVIHANHCHINH}',{mapdata.SOLANCAPNHAT}," +
                //    $"{mapdata.ID_NHOMNGUYCO},{(mapdata.IS_DAYDIEN?1:0)},{mapdata.NGAYTAO},{mapdata.ID_LOAIBOVIEN},'{mapdata.SONHA}',{mapdata.KICHTHUOCHO }, {mapdata.DIENTICHHO }, {(mapdata.IS_BORAO?1:0) }, '{mapdata.TRANGTHAIBOVIEN}', " +
                //    $" {mapdata.LATITUDE_HOTRONGCAY }, {mapdata.ENDX }, {mapdata.STARTX }, {mapdata.ENDY }, {mapdata.STARTY }, '{mapdata.LOAIBOVIEN }', {mapdata.KHOANGCACHHOVIA }, " +
                //    $"'{mapdata.MA_CAYXANH}',{mapdata.ID_TRANGTHAIBOVIEN},'{mapdata.TEN_NHOMNGUYCO}','{mapdata.HINHDANGHO}', {(mapdata.IS_CAPNGAM?1:0)}, {mapdata.ID_NHOMCHUNGLOAI }, '{mapdata.MA_HOTRONGCAY}', " +
                //    $"{mapdata.ID_PHANLOAIQUYHOACH},'{mapdata.TEN_TINHTRANGSINHTRUONG}')";
                // SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);
                #region addParams
                SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);
                command.Parameters.Add(new SqlParameter("ID_DONVIHANHCHINH", mapdata.ID_DONVIHANHCHINH));
                command.Parameters.Add(new SqlParameter("TEN_CAYXANH", mapdata.TEN_CAYXANH));
                command.Parameters.Add(new SqlParameter("THONGTINCHITIET", mapdata.THONGTINCHITIET));
                command.Parameters.Add(new SqlParameter("TEN_LOAICAYXANH", mapdata.TEN_LOAICAYXANH));
                command.Parameters.Add(new SqlParameter("BANKINHTANLA", mapdata.BANKINHTANLA));
                command.Parameters.Add(new SqlParameter("IS_BOVIEN", mapdata.IS_BOVIEN));
                command.Parameters.Add(new SqlParameter("IS_PLANT", mapdata.IS_PLANT));
                command.Parameters.Add(new SqlParameter("IS_ONGNUOC", mapdata.IS_ONGNUOC));
                command.Parameters.Add(new SqlParameter("CAYXANH", mapdata.CAYXANH));
                command.Parameters.Add(new SqlParameter("IS_CAOHONVIAHE", mapdata.IS_CAOHONVIAHE));
                command.Parameters.Add(new SqlParameter("IS_ONGCONG", mapdata.IS_ONGCONG));
                command.Parameters.Add(new SqlParameter("ID_HINHDANGHO", mapdata.ID_HINHDANGHO));
                command.Parameters.Add(new SqlParameter("COUNT", mapdata.COUNT));
                command.Parameters.Add(new SqlParameter("TENCONGTRINHGANNHAT", mapdata.TENCONGTRINHGANNHAT));
                command.Parameters.Add(new SqlParameter("ID_TINHTRANGSINHTRUONG", mapdata.ID_TINHTRANGSINHTRUONG));
                command.Parameters.Add(new SqlParameter("LONGITUDE_HOTRONGCAY", xyvn2000[1]));
                command.Parameters.Add(new SqlParameter("NAMBANGIAOCONGTRINH", mapdata.NAMBANGIAOCONGTRINH));
                command.Parameters.Add(new SqlParameter("TEN_PHANLOAIQUYHOACH", mapdata.TEN_PHANLOAIQUYHOACH));
                command.Parameters.Add(new SqlParameter("IMAGE", mapdata.IMAGE));
                command.Parameters.Add(new SqlParameter("DUONGKINHGOC", mapdata.DUONGKINHGOC));
                command.Parameters.Add(new SqlParameter("ID_LOAICAYXANH", mapdata.ID_LOAICAYXANH));
                command.Parameters.Add(new SqlParameter("TEN_CHUNGLOAICAYXANH", mapdata.TEN_CHUNGLOAICAYXANH));
                command.Parameters.Add(new SqlParameter("DIENTICHTANLA", mapdata.DIENTICHTANLA));
                command.Parameters.Add(new SqlParameter("ID_CAYXANH", mapdata.ID_CAYXANH));
                command.Parameters.Add(new SqlParameter("CHIEUCAOVUTNGON", mapdata.CHIEUCAOVUTNGON));
                command.Parameters.Add(new SqlParameter("GHICHU", mapdata.GHICHU));
                command.Parameters.Add(new SqlParameter("ID_HOTRONGCAY", mapdata.ID_HOTRONGCAY));
                command.Parameters.Add(new SqlParameter("DUONGPHO", mapdata.DUONGPHO));
                command.Parameters.Add(new SqlParameter("ID_KHUVUCCAYXANH", mapdata.ID_KHUVUCCAYXANH));
                command.Parameters.Add(new SqlParameter("KHUVUCCAYXANH", mapdata.KHUVUCCAYXANH));
                command.Parameters.Add(new SqlParameter("NAMTRONGCAY", mapdata.NAMTRONGCAY));
                command.Parameters.Add(new SqlParameter("DONVIHANHCHINH", mapdata.DONVIHANHCHINH));
                command.Parameters.Add(new SqlParameter("SOLANCAPNHAT", mapdata.SOLANCAPNHAT));
                command.Parameters.Add(new SqlParameter("ID_NHOMNGUYCO", mapdata.ID_NHOMNGUYCO));
                command.Parameters.Add(new SqlParameter("IS_DAYDIEN", mapdata.IS_DAYDIEN));
                command.Parameters.Add(new SqlParameter("NGAYTAO", mapdata.NGAYTAO));
                command.Parameters.Add(new SqlParameter("ID_LOAIBOVIEN", mapdata.ID_LOAIBOVIEN));
                command.Parameters.Add(new SqlParameter("SONHA", mapdata.SONHA));
                command.Parameters.Add(new SqlParameter("KICHTHUOCHO", mapdata.KICHTHUOCHO));
                command.Parameters.Add(new SqlParameter("HINHDANGHO", mapdata.HINHDANGHO));
                command.Parameters.Add(new SqlParameter("DIENTICHHO", mapdata.DIENTICHHO));
                command.Parameters.Add(new SqlParameter("MA_HOTRONGCAY", mapdata.MA_HOTRONGCAY));
                command.Parameters.Add(new SqlParameter("IS_BORAO", mapdata.IS_BORAO));
                command.Parameters.Add(new SqlParameter("TRANGTHAIBOVIEN", mapdata.TRANGTHAIBOVIEN));
                command.Parameters.Add(new SqlParameter("LATITUDE_HOTRONGCAY", xyvn2000[0]));
                command.Parameters.Add(new SqlParameter("ENDX", mapdata.ENDX));
                command.Parameters.Add(new SqlParameter("STARTX", mapdata.STARTX));
                command.Parameters.Add(new SqlParameter("ENDY", mapdata.ENDY));
                command.Parameters.Add(new SqlParameter("STARTY", mapdata.STARTY));
                command.Parameters.Add(new SqlParameter("LOAIBOVIEN", mapdata.LOAIBOVIEN));
                command.Parameters.Add(new SqlParameter("KHOANGCACHHOVIA", mapdata.KHOANGCACHHOVIA));
                command.Parameters.Add(new SqlParameter("MA_CAYXANH", mapdata.MA_CAYXANH));
                command.Parameters.Add(new SqlParameter("ID_TRANGTHAIBOVIEN", mapdata.ID_TRANGTHAIBOVIEN));
                command.Parameters.Add(new SqlParameter("TEN_NHOMNGUYCO", mapdata.TEN_NHOMNGUYCO));
                command.Parameters.Add(new SqlParameter("IS_CAPNGAM", mapdata.IS_CAPNGAM));
                command.Parameters.Add(new SqlParameter("ID_NHOMCHUNGLOAI", mapdata.ID_NHOMCHUNGLOAI));
                command.Parameters.Add(new SqlParameter("ID_PHANLOAIQUYHOACH", mapdata.ID_PHANLOAIQUYHOACH));
                command.Parameters.Add(new SqlParameter("TEN_TINHTRANGSINHTRUONG", mapdata.TEN_TINHTRANGSINHTRUONG));
                #endregion
                r = command.ExecuteNonQuery();
                i++;
            }
            watch.Stop();
            try
            {
                var elapsedMs = watch.ElapsedMilliseconds;
                if (i != 0)
                {
                    var result = new ImportResultViewModel
                    {
                        TimeQuery = elapsedMs,
                        RowQuery = $"Thêm mới thành công {i} dòng"
                    };
                    return result;
                }
                else
                {
                    var result = new ImportResultViewModel
                    {
                        TimeQuery = elapsedMs,
                        RowQuery = $"Lỗi"
                    };
                    return result;
                }

            }
            catch (SqlException e)
            {
                Debug.Write(e);
                return null;
            }
        }

        public IEnumerable<Items> Filter(string dataName, string tableName, double lat, double lng, double bk)
        {
            double latDegree = 0, lngDegree = 0;
            double xDuoi, xTren, yTrai, yPhai;

            //tinh ban kinh ra bao nhieu do? de so sanh
            latDegree = bk / 110.574;
            lngDegree = bk / (111.320 * Math.Cos(latDegree));

            xDuoi = lat - latDegree;
            xTren = lat + latDegree;

            yTrai = lng - lngDegree;
            yPhai = lng + lngDegree;
            List<Items> items = new List<Items>();
            var query = $"use {dataName}\n" +
                $"select  TEN_CAYXANH,LATITUDE_HOTRONGCAY,LONGITUDE_HOTRONGCAY from {tableName} " +
                $"where LATITUDE_HOTRONGCAY <={xTren} and LATITUDE_HOTRONGCAY >= {xDuoi} and LONGITUDE_HOTRONGCAY <={yPhai} and LONGITUDE_HOTRONGCAY >={yTrai}";
            SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);

            SqlDataReader r = command.ExecuteReader();
            while (r.Read())
            {
                string tenCayXanh = r["TEN_CAYXANH"].ToString();
                string latDb = r["LATITUDE_HOTRONGCAY"].ToString();
                string logDb = r["LONGITUDE_HOTRONGCAY"].ToString();
                var model = new Items
                {
                    TenCayXanh = tenCayXanh,
                    Lattitude = double.Parse(latDb),
                    Longtitude = double.Parse(logDb)
                };
                items.Add(model);
            }
            r.Close();
            return items;
        }
        public bool CreateCircleFunction(string dataName, string tableName)
        {
            var query = $"USE {dataName}\n" +
                $" GO \n" +
                $"CREATE FUNCTION FILTERCIRCLE(@LAT_USER FLOAT ,@LNGUSER FLOAT, @LATDATA FLOAT, @LONGDATA FLOAT) \n" +
                $"RETURNS FLOAT \n" +
                $" AS \n" +
                $"BEGIN " +
                $"DECLARE @OUTPUT BIT \n" +
                $"DECLARE @PONIT1X FLOAT \n" +
                $"DECLARE @PONIT1Y FLOAT \n" +
                $"DECLARE @PONIT2X FLOAT \n" +
                $"DECLARE @PONIT2Y FLOAT \n" +
                $"DECLARE @DELTA1 FLOAT \n" +
                $"DECLARE @DELTA2 FLOAT \n" +
                $"DECLARE @SUM FLOAT \n" +
                $"DECLARE @RESULT FLOAT " +
                $"SET @PONIT1X = @LAT_USER * PI() / 180 \n" +
                $"SET @PONIT1Y = @LNGUSER * PI() / 180 \n" +
                $"SET @PONIT2X = @LATDATA * PI() / 180 \n" +
                $"SET @PONIT2Y = @LONGDATA * PI() / 180 \n" +
                $"SET @DELTA1 = @PONIT2X - @PONIT1X \n" +
                $"SET @DELTA2 = @PONIT2Y - @PONIT1Y \n" +
                $"SET @SUM = SIN(@DELTA1 / 2) * SIN(@DELTA1 / 2) + COS((@LAT_USER * PI() / 180)) * COS((@LATDATA * PI() / 180)) * SIN(@DELTA2 / 2) * SIN(@DELTA2 / 2) \n" +
                $"SET @RESULT = 6371 * 2 * ATN2(SQRT(@SUM), SQRT(1 - @SUM)) \n" +
                $"RETURN @RESULT \n" +
                $"END"+
            $" GO \n";
            try
            {

                SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);

                command.ExecuteNonQuery();
                return true;
                //chua tao
            }
            catch (SqlException e)
            {
                //tao roi
                Debug.Write(e.Message);
                return false;
            }
        }
        private bool checkExistFunction()
        {
            var query = "IF EXISTS(SELECT * FROM " +
                "  sys.objects WHERE  object_id = OBJECT_ID(N'[dbo].[FILTERCIRCLE]') AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT' )) " +
                " BEGIN SELECT 'RESULT' = 1 END " +
                " ELSE" +
                " BEGIN " +
                " SELECT " +
                " 'RESULT' = 0 " +
                " END ";
            SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);

            int r = (int)command.ExecuteScalar();
            if (r == 1)
                return false;
            else
                return true;

        }
        public IEnumerable<Items> FilterCircle(string dataName, string tableName, double lat, double lng, double bk)
        {
            var checkExist = checkExistFunction();
            if (checkExist)
            {
                CreateCircleFunction(dataName, tableName);
            }
            List<Items> items = new List<Items>();

            var query = $"use {dataName}\n" +
                $"select  TEN_CAYXANH,LATITUDE_HOTRONGCAY,LONGITUDE_HOTRONGCAY from {tableName} where  dbo.FILTERCIRCLE({lat},{lng},LATITUDE_HOTRONGCAY,LONGITUDE_HOTRONGCAY) <= {bk}";

            SqlCommand command = new SqlCommand(query, ConnectDatabase.connect);
            SqlDataReader r = command.ExecuteReader();
            while (r.Read())
            {
                string tenCayXanh = r["TEN_CAYXANH"].ToString();
                string latDb = r["LATITUDE_HOTRONGCAY"].ToString();
                string logDb = r["LONGITUDE_HOTRONGCAY"].ToString();
                var model = new Items
                {
                    TenCayXanh = tenCayXanh,
                    Lattitude = double.Parse(latDb),
                    Longtitude = double.Parse(logDb)
                };
                items.Add(model);
            }
            r.Close();
            return items;
        }
    }
}