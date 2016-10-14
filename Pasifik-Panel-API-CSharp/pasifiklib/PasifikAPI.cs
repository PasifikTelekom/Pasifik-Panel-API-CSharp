using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace pasifiklib
{
    public class PasifikAPI
    {
        private string authorization;
        private bool DEBUG;
        private string base_url;
        public bool Is_Passed { get; set; }
        public PasifikAPI(string username, string password, string lang = "tr", bool DEBUG = false)
        {
            this.authorization = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password)));
            this.base_url = string.Format("https://oim.pasifiktelekom.com.tr/{0}/api/", lang);
            this.DEBUG = DEBUG;
        }
        public string Submit(string from, string to, string text, bool universal = false, string alphabet="Default", string scheduled_delivery_time="", int period=0)
        {
            Dictionary<string, object> dicto = new Dictionary<string, object>();
            dicto.Add("from", from);
            dicto.Add("to", to);
            dicto.Add("text", text);
            dicto.Add("universal", universal);
            dicto.Add("alphabet", alphabet);
            if (scheduled_delivery_time.Length > 0) { dicto.Add("scheduled_delivery_time", scheduled_delivery_time); }
            if (period > 0) { dicto.Add("period", period); }
            string json = new JavaScriptSerializer().Serialize((object) dicto);
            return this._post("sms/submit/", json);
        }
        public string SubmitMulti(string from, List<Dictionary<string, string>> envelopes, bool universal = false, string alphabet = "Default", string scheduled_delivery_time = "", int period = 0)
        {
            Dictionary<string, object> dicto = new Dictionary<string, object>();
            dicto.Add("from", from);
            dicto.Add("envelopes", envelopes);
            dicto.Add("universal", universal);
            dicto.Add("alphabet", alphabet);
            if (scheduled_delivery_time.Length > 0) { dicto.Add("scheduled_delivery_time", scheduled_delivery_time); }
            if (period > 0) { dicto.Add("period", period); }
            string json = new JavaScriptSerializer().Serialize((object)dicto);
            return this._post("sms/submit/multi/", json);
        }
        public string QueryMulti(string start_date, string end_date)
        {
            Dictionary<string, object> dicto = new Dictionary<string, object>();
            dicto.Add("start_date", start_date);
            dicto.Add("end_date", end_date);
            string json = new JavaScriptSerializer().Serialize((object)dicto);
            return this._post("sms/query/multi/", json);
        }
        public string QueryMultiId(string sms_id)
        {
            Dictionary<string, object> dicto = new Dictionary<string, object>();
            dicto.Add("sms_id", sms_id);
            string json = new JavaScriptSerializer().Serialize((object)dicto);
            return this._post("sms/query/multi/id/", json);
        }
        public string Query(string sms_id)
        {
            Dictionary<string, object> dicto = new Dictionary<string, object>();
            dicto.Add("sms_id", sms_id);
            string json = new JavaScriptSerializer().Serialize((object)dicto);
            return this._post("sms/query/", json);
        }
        public string GetSettings()
        {
            return this._post("user/getsettings/", "{}");
        }
        public string Authorization(bool encode = false)
        {
            string auth = encode ? this.authorization : System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(this.authorization));
            if (this.DEBUG)
            {
                Console.WriteLine(auth);
            }
            return auth;
        }
        public string CallHistory(int i_account, string start_date = "", string end_date = "", string cli = "", string cld = "", int offset = 0, int limit = 0, string type = "")
        {
            Dictionary<string, object> dicto = new Dictionary<string,object>(){
                {"i_account", i_account},
                {"start_date", start_date},
                {"end_date", end_date},
                {"cli", cli},
                {"cld", cld},
                {"offset", offset},
                {"limit", limit},
                {"type", type},
            };
            string json = new JavaScriptSerializer().Serialize((object)dicto);
            return this._post("tel/history/", json);
        }
        public string CallActive(int[] i_account_list){
            Dictionary<string, object> dicto = new Dictionary<string, object>() { { "i_account_list", i_account_list } };
            string json = new JavaScriptSerializer().Serialize((object)dicto);
            return this._post("tel/live/", json);
        }
        public string CallActiveDisconnect(int id)
        {
            Dictionary<string, object> dicto = new Dictionary<string, object>() { { "id", id } };
            string json = new JavaScriptSerializer().Serialize((object)dicto);
            return this._post("tel/live/disconnect/", json);
        }
        private string _post(string resource, string json)
        {
            string data = "";
            var headers = "";
            try
            {
                string url = string.Format("{0}{1}", this.base_url, resource);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json; charset=UTF=8";
                request.Accept = "application/json";
                request.Method = "POST";
                request.Headers.Add("Authorization", this.authorization);
                headers = request.Headers.ToString(); // For DEBUG
                Stream requestStream = request.GetRequestStream();
                byte[] sss = Encoding.UTF8.GetBytes(json);
                requestStream.Write(sss, 0, sss.Length);
                requestStream.Close();
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    data = new StreamReader(responseStream).ReadToEnd();
                    this.Is_Passed = true;
                }
            }
            catch (WebException e)
            {
                using (HttpWebResponse response = (HttpWebResponse)e.Response)
                {
                    Stream responseStream = response.GetResponseStream();
                    data = new StreamReader(responseStream).ReadToEnd();
                    this.Is_Passed = false;
                }
            }
            if (this.DEBUG)
            {
                Console.WriteLine(string.Format("REQUEST:\n{0}", json));
                Console.WriteLine(string.Format("HEADER:\n{0}", headers));
                Console.WriteLine(string.Format("RESPONSE:\n{0}", data));
            }
            return data;
        }
    }
}
