
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Front_End.Controllers
{
    public class AdminController : Controller
    {

        public async Task<int> AllTimeBlogPostCount()
        {
            int blogPostCount = new int();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7250/api/Admin/all-time-blog-post-count"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    blogPostCount = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            return blogPostCount;
        }
        public async Task<int> AllTimeCommentCount()
        {
            int allTimeCommentCount = new int();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7250/api/Admin/all-time-comment-count"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    allTimeCommentCount = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            return allTimeCommentCount;
        }

        public async Task<int> AllTimeDownvoteCount()
        {
            int downVoteCount = new int();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7250/api/Admin/all-time-downvotes-count"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    downVoteCount = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            return downVoteCount;
        }

        public async Task<int> AllTimeUpvoteCount()
        {
            int upVoteCount = new int();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7250/api/Admin/all-time-upvotes-count"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    upVoteCount = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            return upVoteCount;
        }
        public async Task<int> DailyBlogPostCount(DateTime Date)
        {
            int dailyBlogPostCount = new int();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7250/api/Admin/daily-blog-posts-count?date={Date}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dailyBlogPostCount = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            return dailyBlogPostCount;
        }

        public async Task<int> DailyCommentCount(DateTime Date)
        {
            int dailyCommentCount = new int();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7250/api/Admin/daily-comments-count?date={Date}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dailyCommentCount = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            return dailyCommentCount;
        }

        public async Task<int> DailyDownvoteCount(DateTime Date)
        {
            int dailyDownvoteCount = new int();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7250/api/Admin/daily-downvotes?date={Date}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dailyDownvoteCount = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            return dailyDownvoteCount;
        }

        public async Task<int> DailyUpvoteCount(DateTime Date)
        {
            int dailyUpvoteCount = new int();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7250/api/Admin/daily-upvotes-count?date={Date}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    dailyUpvoteCount = JsonConvert.DeserializeObject<int>(apiResponse);
                }
            }
            return dailyUpvoteCount;
        }

        public async Task<List<string>> Top10PopularPosts()
        {
            List<string> popularPosts = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7250/api/Admin/top-10-popular-posts"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    popularPosts = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                }
            }
            return popularPosts;
        }


        public async Task<List<string>> Top10PopularBloggers()
        {
            List<string> popularBloggers = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7250/api/Admin/top-10-popular-bloggers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    popularBloggers = JsonConvert.DeserializeObject<List<string>>(apiResponse);
                }
            }
            return popularBloggers;
        }


        public async Task<IActionResult> index()
        {
            var model = new AdminDashboardViewModel();

            // Fetch all-time stats
            model.AllTimeBlogPostCount = await AllTimeBlogPostCount();
            model.AllTimeCommentCount = await AllTimeCommentCount();
            model.AllTimeDownvoteCount = await AllTimeDownvoteCount();
            model.AllTimeUpvoteCount = await  AllTimeUpvoteCount();

            // Fetch daily stats for today
            model.DailyBlogPostCount = await DailyBlogPostCount(DateTime.Today);
            model.DailyCommentCount = await DailyCommentCount(DateTime.Today);
            model.DailyDownvoteCount = await DailyDownvoteCount(DateTime.Today);
            model.DailyUpvoteCount = await DailyUpvoteCount(DateTime.Today);

            // Fetch top 10 popular posts and bloggers
            model.Top10PopularPosts = await Top10PopularPosts();
            model.Top10PopularBloggers = await Top10PopularBloggers();


            return View(model);
        }

        //        [HttpGet]
        //        public async Task<int> GetAllTimeBlogPostCount()
        //        {
        //            return await GetCountFromApi("https://localhost:7250/api/Admin/all-time-blog-post-count");
        //        }
        //        //[HttpGet]
        //        //public async Task<DailyActivityData> GetDailyActivity(DateTime Date)
        //        //{
        //        //    using (var httpClient = new HttpClient())
        //        //    {
        //        //        try
        //        //        {
        //        //            HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7250/api/Admin/daily-activity?date={Date}");
        //        //            response.EnsureSuccessStatusCode();
        //        //            var dailyActivity = await response.Content.ReadAsAsync<DailyActivityData>();
        //        //            return dailyActivity;
        //        //        }
        //        //        catch (HttpRequestException)
        //        //        {
        //        //            // Handle exception
        //        //            return null;
        //        //        }
        //        //    }
        //        //}


        //        [HttpGet]
        //        public async Task<int> GetAllTimeCommentCount()
        //        {
        //            return await GetCountFromApi("https://localhost:7250/api/Admin/all-time-comment-count");
        //        }

        //        [HttpGet]
        //        public async Task<int> GetAllTimeDownvoteCount()
        //        {
        //            return await GetCountFromApi("https://localhost:7250/api/Admin/all-time-downvotes");
        //        }

        //        [HttpGet]
        //        public async Task<int> GetAllTimeUpvoteCount()
        //        {
        //            return await GetCountFromApi("https://localhost:7250/api/Admin/all-time-upvotes");
        //        }

        //        [HttpGet]
        //        public async Task<int> GetDailyBlogPostCount(DateTime date)
        //        {
        //            return await GetCountFromApi($"https://localhost:7250/api/Admin/daily-blog-posts?date={date}");
        //        }

        //        [HttpGet]
        //        public async Task<int> GetDailyCommentCount(DateTime date)
        //        {
        //            return await GetCountFromApi($"https://localhost:7250/api/Admin/daily-comments?date={date}");
        //        }

        //        [HttpGet]
        //        public async Task<int> GetDailyDownvoteCount(DateTime date)
        //        {
        //            return await GetCountFromApi($"https://localhost:7250/api/Admin/daily-downvotes?date={date}");
        //        }

        //        [HttpGet]
        //        public async Task<int> GetDailyUpvoteCount(DateTime date)
        //        {
        //            return await GetCountFromApi($"https://localhost:7250/api/Admin/daily-upvotes?date={date}");
        //        }

        //        [HttpGet]
        //        public async Task<string[]> GetTop10PopularPosts()
        //        {
        //            return await GetArrayFromApi("https://localhost:7250/api/Admin/top-10-popular-posts");
        //        }

        //        [HttpGet]
        //        public async Task<string[]> GetTop10PopularBloggers()
        //        {
        //            return await GetArrayFromApi("https://localhost:7250/api/Admin/top-10-popular-bloggers");
        //        }
        //        public async Task<string[]> GetTop10PopularBloggersEmail()
        //        {
        //            return await GetArrayFromApi("https://localhost:7250/api/Admin/top-10-popular-bloggers-emails");
        //        }

        //        private async Task<int> GetCountFromApi(string url)
        //        {
        //            using (var httpClient = new HttpClient())
        //            {
        //                try
        //                {
        //                    HttpResponseMessage response = await httpClient.GetAsync(url);
        //                    response.EnsureSuccessStatusCode();
        //                    var count = await response.Content.ReadAsStringAsync();
        //                    if (int.TryParse(count, out int result))
        //                    {
        //                        return result;
        //                    }
        //                    else
        //                    {
        //                        return 0;
        //                    }
        //                }
        //                catch (HttpRequestException)
        //                {
        //                    return 0;
        //                }
        //            }
        //        }

        //        private async Task<string[]> GetArrayFromApi(string url)
        //        {
        //            using (var httpClient = new HttpClient())
        //            {
        //                try
        //                {
        //                    HttpResponseMessage response = await httpClient.GetAsync(url);
        //                    response.EnsureSuccessStatusCode();
        //                    var array = await response.Content.ReadAsAsync<string[]>();
        //                    return array;
        //                }
        //                catch (HttpRequestException)
        //                {
        //                    return new string[0];
        //                }
        //            }
        //        }


    }
}