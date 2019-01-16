using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary2
{
    public class DataBase
    {
        public MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                string path = "/public/DBinfo.json";
                string result = new StreamReader(File.OpenRead(path)).ReadToEnd();
                JObject jo = JsonConvert.DeserializeObject<JObject>(result);
                Hashtable map = new Hashtable();
                foreach (JProperty jp in jo.Properties())
                {
                    map.Add(jp.Name, jp.Value);
                    Console.WriteLine("{0} , {1}", jp.Name, jp.Value);
                }

                conn.ConnectionString = string.Format("server={0};user={1};password={2};database={3}", map["server"].ToString(), map["user"].ToString(), map["password"].ToString(), map["database"].ToString());//strConnection1;
                //Console.WriteLine("접속디비정보 ==> "+conn.ConnectionString);
                conn.Open();

                return conn;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
