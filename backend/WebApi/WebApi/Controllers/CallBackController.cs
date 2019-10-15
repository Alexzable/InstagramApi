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
        private static User userData;
        [HttpGet]
        public ActionResult<User> InstaData()
        {
            return userData;
        }
        public async Task<ActionResult> CallBack(string code)
        {
            string accessToken = "";
            if (!String.IsNullOrEmpty(code))
            {
                accessToken = await GetAccessToken(code);
                userData = await GetDataInstagram(accessToken);
                userData.Html = await GetEaambededDataInstagram(userData.ChosenPhoto);

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
                    var accessToken = (string)jsResult["access_token"];
                  
                    return accessToken;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }
        public async Task<String> GetEaambededDataInstagram(string PhotoUrl)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var response = await client.GetAsync("https://api.instagram.com/oembed?url=" + PhotoUrl);
                    var responseString = await response.Content.ReadAsStringAsync();
                    JObject jObject = JObject.Parse(responseString);
                    string displayHtml = (string)jObject.SelectToken("html");
               

                    return displayHtml;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public async Task<User> GetDataInstagram(string accessToken)
        {
            try
            {
                User getUserDataInstagram = new User();
               
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

                    getUserDataInstagram.IDInsta = IDInsta;
                    getUserDataInstagram.LinkBio = LinkBio;
                    getUserDataInstagram.Location = Location;
                    getUserDataInstagram.Name = Name;
                    getUserDataInstagram.ProfePicture = ProfilePicture;
                    getUserDataInstagram.Username = Username;
                    getUserDataInstagram.Followers = Follows;
                    getUserDataInstagram.FollowedBy = FollowedBy;
                    getUserDataInstagram.Comments = Comments;
                    getUserDataInstagram.Likes = Likes;
                    getUserDataInstagram.Media = PictureNr;
                    getUserDataInstagram.ClientId = ConfigurationManager.AppSettings["client_id"];
                    getUserDataInstagram.ClientSecret = ConfigurationManager.AppSettings["client_secret"];
                    getUserDataInstagram.ChosenPhoto = linkPhoto;

                    return getUserDataInstagram;
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