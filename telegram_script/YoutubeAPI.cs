using System;
using System.Collections.Generic;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;


namespace telegram_script
{
    class YoutubeAPI
    {
        public static List<WordCount> get_Channel_from_ID(List<string> videoId)
        {
            List<WordCount> list = new List<WordCount>();
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                //Api Token
                ApiKey = "API_TOKEN",
            });
            var searchListRequest = youtubeService.Videos.List("snippet");
            searchListRequest.Id = videoId; // Replace with your search term.
  
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse =  searchListRequest.Execute();
            if (searchListResponse.Items.Count != 0)
            {
                for (int i = 0; i < searchListResponse.Items.Count; i++)
                {
                    string name = searchListResponse.Items[i].Snippet.ChannelTitle;
                    WordCount.AddList(ref list, name);
                }
            }
            return list;
        }    
    }
}
