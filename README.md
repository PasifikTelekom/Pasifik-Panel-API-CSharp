# Pasifik-Panel-API-CSharp
This library package provides a variety systems, the simplest way to integrate Pasifik services with your system.

## Requirements
You should add to References `System.Web.Extensions`.

Navigate to `C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5\`.

You can see `System.Web.Extensions.dll`, we use `JavaScriptSerializer` class which Serialize Objects Collection to JSON string.

*3.5* or greater .Net Framework required to build it.

## Installation
Download source code and Unzip package.

Open Solution using Visual Studio 2010.

From Menu > Build > Build **pasifiklib**.

## Usage
After build add reference `pasifiklib.dll` to your project.

in your code.
```csharp
using pasifiklib;
...
string username = "YOUR_USERNAME";
string password = "YOUR_PASSWORD";
string header = "YOUR_COMPANY";
string lang = "tr"; // 'tr': Turkish response, 'en': English response, 'ar': Arabic response.
bool DEBUG = true;
PasifikAPI obj = new PasifikAPI(username, password, lang, DEBUG);
```
## Test Case
Follow `TestCase.cs` TestCase class and replace it with requirement parameters, for test just uncomment the following methods inside `Program.cs`.

```csharp
TestCase test = new TestCase();
//test.send_one_message_to_many_receipients();
//test.send_one_message_to_many_receipients_schedule_delivery();
//test.send_one_message_to_many_receipients_schedule_delivery_with_validity_period();
//test.send_one_message_to_many_receipients_turkish_language();
//test.send_one_message_to_many_receipients_flash_sms();
//test.send_one_message_to_many_receipients_unicode();
//test.send_one_message_to_many_receipients_outside_turkey();
//test.send_many_message_to_many_receipients();
//test.query_multi_general_report();
//test.query_multi_general_report_with_id();
//test.query_detailed_report_with_id();
//test.get_account_settings();
//test.get_authority();
//test.get_cdr_report();
//test.get_cdr_report_range_datetime();
//test.get_cdr_report_with_type();
//test.get_active_calls();
//test.get_disconnect_active_call();
Console.ReadKey();
```
To run code **Menu > Debug > Start Debugging** or press **F5**.

You will see the result on **Command Prompt**.
