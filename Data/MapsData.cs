using Microsoft.AspNetCore.Components;

namespace LearningMaps.Data
{
    public class MapsData
    {
        public event Action OnChange;

        public MapData[]? MapsDatas;
      
        public MapData? SelectedMap;

        public List<MapData> FiltredMapsData { get; set; }

        private readonly NavigationManager navigationManager;
        public MapsData(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }



        public void FilterMaps(string Tag)
        {
            if (Tag == "default")
            {
                FiltredMapsData = MapsDatas.Where(t => t.tags.Contains(Tag)).OrderByDescending(m => m.map_ID).ToList();

            }
            else
            {
                FiltredMapsData = MapsDatas.Where(t => t.tags.Contains(Tag)).ToList();

            }

            if (navigationManager.Uri.Contains("map"))
            {
                navigationManager.NavigateTo(navigationManager.BaseUri);
            }



            NotifyStateChanged();
        }


        public void NotifyStateChanged() => OnChange?.Invoke();


    }


   
    public class MapData
    {
        public int map_ID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string down_file { get; set; }
        public MapImages[] images { get; set; }
        public string tags { get; set; }
    }

    public class MapImages
    {
        public string hashcode { get; set; } 
        public int Width { get; set; } = 200;
        public int Height { get; set; } = 200;
        public bool ZoomOut { get; set; }
        public string bigImg
        {
            get
            {
                return $"https://i.imgur.com/{hashcode}.jpg";
            }
        }
        public string thumbnail { get {
                return bigImg.Replace(".jpg", "m.jpg");
            }  }
    }
}
