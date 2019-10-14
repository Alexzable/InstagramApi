using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Models;
using System.Configuration;

namespace WebApi.Controllers
{
    public class CallBackController : Controller
    {
        private static string accessToken = "";
        private static User userData = new User();
        private readonly UserContext _context;

        [HttpGet]
        public ActionResult<User> InstaData()
        {
            return userData;
        }
        public async Task<ActionResult> CallBack(string code)
        {
            if (!String.IsNullOrEmpty(code))
            {
                code = code.ToString();
                await GetDataInstagramToken(code);

            }
            SaveData(userData);
            return Redirect(ConfigurationManager.AppSettings["localhost2"]);
        }
        public async Task<String> GetAccessToken(string code)
        {
            try
            {
                var values = new Dictionary<string, string>    {
                      { "client_id",ConfigurationManager.AppSettings["client_id"]},
                      { "client_secret", ConfigurationManager.AppSettings["client_secret"]},
                      { "grant_type", "authorization_code" },
                      { "redirect_uri", ConfigurationManager.AppSettings["redirect_uri"]},
                      { "code", code}
                };
                var content = new FormUrlEncodedContent(values);
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(ConfigurationManager.AppSettings["instagram_token"], content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsResult = (JObject)JsonConvert.DeserializeObject(responseString);
                     accessToken = (string)jsResult["access_token"];
                    string imageurl = (string)jsResult["user"]["profile_picture"];
                    User user2 = new User();
                    user2.ProfePicture = imageurl;
                    string fullname = (string)jsResult["user"]["full_name"];
                    user2.Name = fullname;

                    SaveData(user2);

                    return accessToken;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }
        public async Task GetEaambededDataInstagramToken(string PhotoUrl)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var response = await client.GetAsync("https://api.instagram.com/oembed?url=" + PhotoUrl);
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(responseString);
                    string displayHtml = (string)jObject.SelectToken("html");

                    userData.Html = displayHtml;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public async Task GetDataInstagramToken(string code)
        {
            User userInfo = new User();

            try
            {
                Task accessTokenTask = Task.Run(() => GetAccessToken(code));
                accessTokenTask.Wait();
                using (var client = new HttpClient())
                {
                    //GET SELF OWNER INFORMATION
                    var response = await client.GetAsync(ConfigurationManager.AppSettings["instagram_media"] + accessToken);
                    // GET SELF MEDIA INFORMATION
                    var response2 = await client.GetAsync(ConfigurationManager.AppSettings["instagram_owner"] + accessToken);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var responseString2 = await response2.Content.ReadAsStringAsync();
                    var jsResult = (JObject)JsonConvert.DeserializeObject(responseString);
                    var jsResult2 = (JObject)JsonConvert.DeserializeObject(responseString2);

                    //get HTML CSS AND JS CODE .
                    Task embeededCode = Task.Run(() => GetEaambededDataInstagramToken("https://www.instagram.com/p/B3HYAN2iGoe/"));
                    embeededCode.Wait();
                    double PictureNr = (double)jsResult2["data"]["counts"]["media"];
                    string LinkBio = (string)jsResult2["data"]["bio"];
                    string Location = (string)jsResult2["data"]["website"];

                    double IDInsta = (double)jsResult["data"][0]["user"]["id"];
                    string Name = (string)jsResult["data"][0]["user"]["full_name"];
                    string ProfilePicture = (string)jsResult["data"][0]["user"]["profile_picture"];
                    string Username = (string)jsResult["data"][0]["user"]["username"];

                    double Comments = 0;
                    double Likes = 0;
                    for (int i = 0; i < PictureNr; i++)
                    {
                        Comments = Comments+ (double)jsResult["data"][i]["comments"]["count"];
                        Likes = Likes + (double)jsResult["data"][i]["likes"]["count"];
                    }
                 

                }
         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }

        public void SaveData(User user)
        {
            userData = user;
        }
        public User GetData()
        {
            return userData;
        }

       

    }
}