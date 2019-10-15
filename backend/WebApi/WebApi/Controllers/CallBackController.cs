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

                    double IDInsta = (double)jsResult2["data"]["id"];
                    string LinkBio = (string)jsResult2["data"]["bio"];
                    string Location = (string)jsResult2["data"]["website"];
                    string Name = (string)jsResult2["data"]["full_name"];
                    string ProfilePicture = (string)jsResult2["data"]["profile_picture"];
                    string Username = (string)jsResult2["data"]["username"];
                    double PictureNr = (double)jsResult2["data"]["counts"]["media"];
                    double Follows = (double)jsResult2["data"]["counts"]["follows"];
                    double FollowedBy = (double)jsResult2["data"]["counts"]["followed_by"];
                    
                    double Comments = 0;
                    double Likes = 0;
                    string linkPhoto = (string)jsResult["data"][1]["link"];
                    for (int i = 0; i < PictureNr; i++)
                    {
                        Comments = Comments+ (double)jsResult["data"][i]["comments"]["count"];
                        Likes = Likes + (double)jsResult["data"][i]["likes"]["count"];
                    }


                    userData.IDInsta = IDInsta;
                    userData.LinkBio = LinkBio;
                    userData.Location = Location;
                    userData.Name = Name;
                    userData.ProfePicture = ProfilePicture;
                    userData.Username = Username;
                    userData.Followers = Follows;
                    userData.FollowedBy = FollowedBy;
                    userData.Comments = Comments;
                    userData.Likes = Likes;
                    userData.Media = PictureNr;
                    userData.ClientId = ConfigurationManager.AppSettings["client_id"];
                    userData.ClientSecret = ConfigurationManager.AppSettings["client_secret"];


                    //get HTML CSS AND JS CODE .
                    Task embeededCode = Task.Run(() => GetEaambededDataInstagramToken(linkPhoto));
                    embeededCode.Wait();
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