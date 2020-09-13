using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace LogisticsSystem.Tools
{
    public class WebRequestService
    {
        /// <summary>
        /// 根据地名查询经纬度
        /// </summary>
        /// <param name="url">api地址</param>
        /// <param name="key">申请的appKey</param>
        /// <param name="sk">私钥</param>
        /// <param name="address">查询的地址(省市区+具体地址)</param>
        /// <param name="city">查询的城市</param>
        /// <returns></returns>
        public static string QueryGDLocationByName(string url, string key, string sk, string address, string city)
        {
            //计算数字签名
            string sign = AKSNCaculater.GDCaculateSN(sk, new Dictionary<string, string>()
            {
                { "address",address},
                { "city",city},
                { "key",key}
            });

            //发送请求
            string jsonResult = HTTPClientHelper.GetResponseByGet(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("address",address),
                new KeyValuePair<string, string>("city", city),
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("sig", sign),
            }, url);

            //解析JSON
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonResult);
            //JObject jo = JObject.Parse(jsonResult);
            var code = jo.Properties().Where(p => p.Name == "status").Select(p => p.Value.ToString()).FirstOrDefault();
            //jo.Remove();
            //请求成功
            if (code == "1")
            {
                string value = jo.Properties().Where(p => p.Name == "geocodes").Select(p => p.Value.ToString()).FirstOrDefault();
                JArray ja = (JArray)JsonConvert.DeserializeObject(value);
                return ja[0]["location"].ToString();
            }
            else //请求失败
                return null;
        }


        /// 根据地地名进行邮编的模糊查询
        /// </summary>
        /// <param name="url">接口的地址</param>
        /// <param name="key">申请的appkey</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <param name="district">区域</param>
        /// <param name="address">地名关键字</param>
        /// <param name="jsonPath">json文件路径</param>
        /// <returns></returns>
        public static string QueryPostCodeByName(string url, string key, string province, string city, string district, string address, string jsonPath)
        {
            FileInfo file = new FileInfo(jsonPath);
            if (file.Exists)
            {
                FileStream stream = file.OpenRead();
                StreamReader reader = new StreamReader(stream);
                string jsonText = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                reader.Dispose();
                stream.Dispose();

                //解析数据
                JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
                var infos = jo.Properties().Where(p => p.Name == "Coding").Select(p => p.Value.ToString()).FirstOrDefault();
                JArray ja = (JArray)JsonConvert.DeserializeObject(infos);
                //找到该省的数据
                var aimObj = ja.Where(p => p["province"].ToString() == province).FirstOrDefault();
                var provinceId = aimObj["id"].ToString();//省份Id

                var cityInfo = aimObj["city"].Where(p => p["city"].ToString() == city).FirstOrDefault();
                var cityId = cityInfo["id"].ToString();//省份Id

                var districtInfo = cityInfo["district"].Where(p => p["district"].ToString() == district).FirstOrDefault();
                var districtId = districtInfo["id"].ToString();


                string jsonRequest = HTTPClientHelper.GetResponseByGet(new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("pid",provinceId),
                    new KeyValuePair<string, string>("cid",cityId),
                    new KeyValuePair<string, string>("did",districtId),
                    new KeyValuePair<string, string>("q",address),
                    new KeyValuePair<string, string>("key",key)
                }, url);

                //解析请求数据
                JObject postcodeJo = (JObject)JsonConvert.DeserializeObject(jsonRequest);
                var code = postcodeJo.Properties().Where(p => p.Name == "reason").Select(p => p.Value.ToString()).FirstOrDefault();
                if (code == "successed")
                {
                    try
                    {
                        var result = postcodeJo.Properties().Where(p => p.Name == "result").Select(p => p.Value.ToString()).FirstOrDefault();
                        JArray resulytList = (JsonConvert.DeserializeObject(result) is JArray) ?
                            (JArray)JsonConvert.DeserializeObject(result) :
                            (JArray)JsonConvert.DeserializeObject("[" + result + "]");
                        return resulytList[0]["list"][0]["PostNumber"].ToString();
                    }
                    catch (System.Exception)
                    {
                        //address需要详细拆分
                        return "-1";
                    }
                }
                else
                    return null;
            }
            else
                return null;
        }


        public static string QueryPostCodeByName(string url, string key, string province, string city, string district, string jsonPath)
        {
            FileInfo file = new FileInfo(jsonPath);
            if (file.Exists)
            {
                FileStream stream = file.OpenRead();
                StreamReader reader = new StreamReader(stream);
                string jsonText = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                reader.Dispose();
                stream.Dispose();

                //解析数据
                JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
                var infos = jo.Properties().Where(p => p.Name == "Coding").Select(p => p.Value.ToString()).FirstOrDefault();
                JArray ja = (JArray)JsonConvert.DeserializeObject(infos);
                //找到该省的数据
                var aimObj = ja.Where(p => p["province"].ToString() == province).FirstOrDefault();
                var provinceId = aimObj["id"].ToString();//省份Id

                var cityInfo = aimObj["city"].Where(p => p["city"].ToString() == city).FirstOrDefault();
                var cityId = cityInfo["id"].ToString();//省份Id

                var districtInfo = cityInfo["district"].Where(p => p["district"].ToString() == district).FirstOrDefault();
                var districtId = districtInfo["id"].ToString();


                string jsonRequest = HTTPClientHelper.GetResponseByGet(new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("pid",provinceId),
                    new KeyValuePair<string, string>("cid",cityId),
                    new KeyValuePair<string, string>("did",districtId),
                    new KeyValuePair<string, string>("key",key)
                }, url);

                //解析请求数据
                JObject postcodeJo = (JObject)JsonConvert.DeserializeObject(jsonRequest);
                var code = postcodeJo.Properties().Where(p => p.Name == "reason").Select(p => p.Value.ToString()).FirstOrDefault();
                if (code == "successed")
                {
                    try
                    {
                        var result = postcodeJo.Properties().Where(p => p.Name == "result").Select(p => p.Value.ToString()).FirstOrDefault();
                        JArray resulytList = (JsonConvert.DeserializeObject(result) is JArray) ?
                            (JArray)JsonConvert.DeserializeObject(result) :
                            (JArray)JsonConvert.DeserializeObject("[" + result + "]");
                        return resulytList[0]["list"][0]["PostNumber"].ToString();
                    }
                    catch (System.Exception)
                    {
                        //address需要详细拆分
                        return "-1";
                    }
                }
                else
                    return null;
            }
            else
                return null;
        }


        /// <summary>
        /// 计算单个起点到一个终点的距离
        /// </summary>
        /// <param name="orignLocation">起点</param>
        /// <param name="destination">终点</param>
        /// <param name="key"></param>
        /// <param name="sk"></param>
        /// <param name="type">计算的方式，0：直线距离；1：驾车距离</param>
        /// <returns></returns>
        public static string CalcDistance(string orignLocation, string destination, string key, string sk, string type = "0")
        {
            string url = "https://restapi.amap.com/v3/distance";

            string sign = AKSNCaculater.GDCaculateSN(sk, new Dictionary<string, string>()
            {
                { "destination",destination },
                { "key",key},
                { "origins",orignLocation},
                { "type",type }
            });

            string jsonResult = HTTPClientHelper.GetResponseByGet(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("destination",destination),
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("origins", orignLocation),
                new KeyValuePair<string, string>("type", type),
                new KeyValuePair<string, string>("sig", sign),
            }, url);

            //解析JSON
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonResult);
            var code = jo.Properties().Where(p => p.Name == "status").Select(p => p.Value.ToString()).FirstOrDefault();

            //请求成功
            if (code == "1")
            {
                string value = jo.Properties().Where(p => p.Name == "results").Select(p => p.Value.ToString()).FirstOrDefault();
                JArray ja = (JArray)JsonConvert.DeserializeObject(value);
                return ja[0]["distance"].ToString();
            }
            else //请求失败
                return null;
        }


        /// <summary>
        /// 批量计算多个起点到一个终点的距离
        /// </summary>
        /// <param name="orignLocation">起点，多个起点用|分隔</param>
        /// <param name="destination">终点</param>
        /// <param name="key"></param>
        /// <param name="sk"></param>
        /// <param name="type">计算的方式，0：直线距离；1：驾车距离</param>
        /// <returns></returns>
        public static List<string> CalcGroupDistance(string orignLocation, string destination, string key, string sk, string type = "0")
        {
            string url = "https://restapi.amap.com/v3/distance";

            string sign = AKSNCaculater.GDCaculateSN(sk, new Dictionary<string, string>()
            {
                { "destination",destination },
                { "key",key},
                { "origins",orignLocation},
                { "type",type }
            });

            string jsonResult = HTTPClientHelper.GetResponseByGet(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("destination",destination),
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("origins", orignLocation),
                new KeyValuePair<string, string>("type", type),
                new KeyValuePair<string, string>("sig", sign),
            }, url);

            //解析JSON
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonResult);
            var code = jo.Properties().Where(p => p.Name == "status").Select(p => p.Value.ToString()).FirstOrDefault();

            //请求成功
            if (code == "1")
            {
                var distances = new List<string>();
                string value = jo.Properties().Where(p => p.Name == "results").Select(p => p.Value.ToString()).FirstOrDefault();
                JArray ja = (JArray)JsonConvert.DeserializeObject(value);
                foreach (var item in ja)
                    distances.Add(item["distance"].ToString());
                return distances;
            }
            else //请求失败
                return null;
        }


        /// <summary>
        /// 货车路径规划
        /// </summary>
        /// <param name="orign">起点</param>
        /// <param name="wayPoints">途经点</param>
        /// <param name="destination">终点</param>
        /// <param name="strategy">驾车策略</param>
        /// <param name="size">车辆大小</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public string GetTruckPath(string orign, string wayPoints, string destination, string strategy, string size, string key)
        {
            string url = "https://restapi.amap.com/v4/direction/truck";

            string jsonResult = HTTPClientHelper.GetResponseByGet(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("origin",orign),
                new KeyValuePair<string, string>("wayPoints", wayPoints),
                new KeyValuePair<string, string>("destination", destination),
                new KeyValuePair<string, string>("strategy", strategy),
                new KeyValuePair<string, string>("size", size)
            }, url);

            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonResult);

            var code = jo.Properties().Where(p => p.Name == "errcode").Select(p => p.Value.ToString()).FirstOrDefault();

            if (code == "0")
            {

                JObject _data = (JObject)JsonConvert.DeserializeObject(
                    jo.Properties().Where(p => p.Name == "data").Select(p => p.Value.ToString()).FirstOrDefault());

                JObject _route = (JObject)JsonConvert.DeserializeObject(
                    _data.Properties().Where(p => p.Name == "route").Select(p => p.Value.ToString()).FirstOrDefault());

                var steps = _route.Properties().Where(p => p.Name == "paths").Select(p => p.Value.ToString()).FirstOrDefault();

                return steps;
            }
            else
                return jo.Properties().Where(p => p.Name == "errdetail").Select(p => p.Value.ToString()).FirstOrDefault();

        }
    }
}
