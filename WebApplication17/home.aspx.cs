using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using SurveyMonkey;
using SurveyMonkey.Containers;
using SurveyMonkey.RequestSettings;

namespace WebApplication17
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Delete_Click(object sender, EventArgs e)
        {

        }

        protected void Send_Click(object sender, EventArgs e)
        {


            using (var api = new SurveyMonkeyApi("FUJTCAkWTMKHpIz93MGonA", "7zF8BOnG4G7RzWhxfQxo2a7GL8GCNMI.NkUuZ8eAG7K7qG9WhoRP3iAEkwfbTYwUZ7oUHeu8iqd-XU5h9AXRqmWEs5XP5Q7SQxS-EiQy5iXIYuCrIa6kBDs-3eov8ubt"))
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //Get the id of our survey (or store this locally to avoid the api call)
                var surveys = api.GetUserDetails();
                long surveyId = 0;// surveys.First(s => s.Title == "Customer Feedback Survey").Id.Value;

                //Remember when we last did a check
                DateTime lastCheck = DateTime.UtcNow;

                while (true)
                {
                    //Wait 1 hour
                    System.Threading.Thread.Sleep(60 * 60 * 1000);

                    //Create a settings object to get only responses added since our last check
                    var settings = new GetResponseListSettings { StartCreatedAt = lastCheck };

                    //Remember the current time for filtering the next run
                    lastCheck = DateTime.UtcNow;

                    //Get any new responses
                    List<Response> newResponses = api.GetSurveyResponseDetailsList(surveyId, settings);

                    //Do something with any new responses
                    foreach (Response response in newResponses)
                    {
                        Console.WriteLine("New response from {0} -  view at {1}", response.IpAddress, response.AnalyzeUrl);
                    }

                    //Go again
                }
            }
        }
    }
}

//sadfsafsadfsadfsdf