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
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    //[Route("[controller]")]
    //[ApiController]
    public class CallBackController : Controller
    {
        private static User userData;
        private static string accessTokenG = "";
        
        public void ConnectToInstagram()
        {
            var client_id = ConfigurationManager.AppSettings["client_id"];
            var redirect_url = ConfigurationManager.AppSettings["redirect_uri"];
             Response.Redirect(ConfigurationManager.AppSettings["instagram_auth"] + client_id +
               "&redirect_uri=" + redirect_url + "&response_type=code");

        }
       
        //Step2 Receiving the redirect from Instagram
        [HttpGet]
        public async Task<ActionResult<string>> Authorization(string code)
        {

            if (String.IsNullOrEmpty(code))
            {
                //Step1  Send post method to instagram for requiring the code;

                return RedirectToAction("Authorization", "CallBack");
            }
            else if (!String.IsNullOrEmpty(code))
            {
                //Step3  Request the access_token
                accessTokenG = await GetAccessToken(code);
                System.Diagnostics.Debug.WriteLine(accessTokenG);
            }
            if(accessTokenG!="")
                return Redirect(ConfigurationManager.AppSettings["localhost2"]);

            return Redirect(ConfigurationManager.AppSettings["localhost"]);
        }
        [HttpPost]
        public async Task<String> GetAccessToken(string code)
        {
            System.Diagnostics.Debug.WriteLine(code);
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

                    System.Diagnostics.Debug.WriteLine(accessToken);
                    return accessToken;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        [HttpGet]
        public async Task<ActionResult<User>> PassDataToAngular(string username, string password, string chosenphoto)
        {
            //Username and Password normaly checked from the databasea
            if(username== "trollfornothingbutmoney" && password =="pacolino" && chosenphoto != "")
            {
                userData = await GetDataInstagram(accessTokenG, chosenphoto);
                userData.Html = await GetEmbededDataInstagram(userData.ChosenPhoto);
            }
            return userData;
        }
     
        public async Task<String> GetEmbededDataInstagram(string PhotoUrl)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var response = await client.GetAsync(ConfigurationManager.AppSettings["instagram_embeded"] + PhotoUrl);
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
       
        public async Task<User> GetDataInstagram(string accessToken, string chosenphoto)
        {


            System.Diagnostics.Debug.WriteLine(accessToken);
            try
            {

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
                    int nrChosenPhoto = Convert.ToInt32(chosenphoto);
                    string linkPhoto = (string)jsResult["data"][nrChosenPhoto]["link"];
                    for (int i = 0; i < PictureNr; i++)
                    {
                        Comments = Comments+ (double)jsResult["data"][i]["comments"]["count"];
                        Likes = Likes + (double)jsResult["data"][i]["likes"]["count"];
                    }
                    User getUserDataInstagram = new User(ConfigurationManager.AppSettings["client_id"], ConfigurationManager.AppSettings["client_secret"],
                        Name, ProfilePicture, LinkBio, Comments, Likes, IDInsta, Location, "", PictureNr, Follows, FollowedBy, linkPhoto);
    

                    return getUserDataInstagram;
                }
         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    
    }
}