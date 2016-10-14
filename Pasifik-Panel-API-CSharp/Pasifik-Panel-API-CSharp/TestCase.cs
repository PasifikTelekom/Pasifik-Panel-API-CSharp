using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pasifiklib;

namespace Pasifik_Panel_API_CSharp
{
    public class TestCase
    {
        private string header;
        private PasifikAPI obj;
        public TestCase()
        {
            string username = "YOUR_USERNAME";
            string password = "YOUR_PASSWORD";
            this.header = "YOUR_COMPANY";
            string lang = "tr"; // 'tr': Turkish response, 'en': English response, 'ar': Arabic response.
            bool DEBUG = true;
            this.obj = new PasifikAPI(username, password, lang, DEBUG);
        }
        public void send_one_message_to_many_receipients()
        {
            string from = this.header;
            string to = "905999999998,905999999999";
            string text = "SMS Test";
            string result = this.obj.Submit(from, to, text);
        }
        public void send_one_message_to_many_receipients_schedule_delivery()
        {
            string from = this.header;
            string to = "905999999998,905999999999";
            string text = "SMS Test";
            string scheduled_delivery_time = "2016-09-28T09:30:00Z";// "%Y-%m-%dT%H:%M:%SZ" format e.g "2016-07-23T21:54:02Z" in UTC Timezone.
            string result = this.obj.Submit(from, to, text, false, "Default", scheduled_delivery_time);
        }
        public void send_one_message_to_many_receipients_schedule_delivery_with_validity_period()
        {
            string from = this.header;
            string to = "905999999998,905999999999";
            string text = "SMS Test";
            string scheduled_delivery_time = "2016-09-28T09:30:00Z";// "%Y-%m-%dT%H:%M:%SZ" format e.g "2016-07-23T21:54:02Z" in UTC Timezone.
            int period = 1440; // minutes number e.g 1440 minutes for 24 hours
            string result = this.obj.Submit(from, to, text, false, "Default", scheduled_delivery_time, period);
        }
        public void send_one_message_to_many_receipients_turkish_language()
        {
            string from = this.header;
            string to = "905999999998,905999999999";
            string text = "Artık Ulusal Dil Tanımlayıcısı ile Türkçe karakterli smslerinizi rahatlıkla iletebilirsiniz.";
            string alphabet = "TurkishSingleShift";
            string result = this.obj.Submit(from, to, text, false, alphabet);
        }
        public void send_one_message_to_many_receipients_flash_sms()
        {
            string from = this.header;
            string to = "905999999998,905999999999";
            string text = "My first Flash SMS, It will be temporary on your phone.";
            string alphabet = "DefaultMclass0";
            string result = this.obj.Submit(from, to, text, false, alphabet);
        }
        public void send_one_message_to_many_receipients_unicode()
        {
            string from = this.header;
            string to = "905999999998,905999999999";
            string text = "メッセージありがとうございます";
            string alphabet = "UCS2";
            string result = this.obj.Submit(from, to, text, false, alphabet);
        }
        public void send_one_message_to_many_receipients_outside_turkey()
        {
            string from = this.header;
            string to = "+435999999998,+435999999999";// '+' required e.g '+43' for Germany mobile prefix number
            string text = "SMS Test";
            bool universal = true; // true: it means send sms outside Turkey
            string result = this.obj.Submit(from, to, text, universal);
        }
        public void send_many_message_to_many_receipients()
        {
            string from = this.header;
            List<Dictionary<string, string>> envelopes = new List<Dictionary<string, string>>();
            Dictionary<string, string> envelope = new Dictionary<string, string>() { { "to", "905999999998" }, { "text", "test 1" } };
            envelopes.Add(envelope);
            envelopes.Add(new Dictionary<string, string>() { { "to", "905999999999" }, { "text", "test 2" } });
            string result = this.obj.SubmitMulti(from, envelopes);
        }
        public void query_multi_general_report()
        {
            string start_date = "01.03.2016"; // formated as turkish date time format "%d.%m.%Y"
		    string end_date = "01.03.2016"; // formated as turkish date time format "%d.%m.%Y"
            string result = this.obj.QueryMulti(start_date, end_date);
        }
        public void query_multi_general_report_with_id()
        {
            string sms_id = "123456";
            string result = this.obj.QueryMultiId(sms_id);
        }
        public void query_detailed_report_with_id()
        {
            string sms_id = "123456";
            string result = this.obj.Query(sms_id);
        }
        public void get_account_settings()
        {
            string result = this.obj.GetSettings();
        }
        public void get_authority()
        {
            bool encode = false;
            string result = this.obj.Authorization(encode);
        }
        public void get_cdr_report()
        {
            int i_account = 123456;
            string result = this.obj.CallHistory(i_account);
        }
        public void get_cdr_report_range_datetime()
        {
            int i_account = 123456;
            string start_date = "2016-08-31T10:12:45Z";
		    string end_date = "2016-09-01T10:12:45Z";
		    string cli = "";
		    string cld = "";
		    int offset = 0;
		    int limit = 100;
            string result = this.obj.CallHistory(i_account, start_date, end_date, cli, cld, offset, limit);
        }
        public void get_cdr_report_with_type()
        {
            int i_account = 123456;
            string start_date = "2016-08-31T10:12:45Z";
            string end_date = "2016-09-01T10:12:45Z";
            string cli = "";
            string cld = "";
            int offset = 0;
            int limit = 100;
            string[] type_flag = new string[] { "non_zero_and_errors", "non_zero", "all", "complete", "incomplete", "errors" };
            string type = type_flag[0];
            string result = this.obj.CallHistory(i_account, start_date, end_date, cli, cld, offset, limit, type);
        }
        public void get_active_calls()
        {
            int[] i_account_list = new int[] { 123, 456 };
            string result = this.obj.CallActive(i_account_list);
        }
        public void get_disconnect_active_call()
        {
            int id = 123456;
            string result = this.obj.CallActiveDisconnect(id);
        }
    }
}
