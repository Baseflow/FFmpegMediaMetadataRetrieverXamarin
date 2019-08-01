using Android.App;
using Android.Widget;
using Android.OS;
using Wseemann.Media;
using Android.Graphics;

namespace Sample
{
    [Activity(Label = "Sample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { 

                FFmpegMediaMetadataRetriever mmr = new FFmpegMediaMetadataRetriever();
                mmr.SetDataSource("https://file-examples.com/wp-content/uploads/2018/04/file_example_MOV_480_700kB.mov");
                mmr.ExtractMetadata(FFmpegMediaMetadataRetriever.MetadataKeyAlbum);
                mmr.ExtractMetadata(FFmpegMediaMetadataRetriever.MetadataKeyArtist);
                Bitmap b = mmr.GetFrameAtTime(2000000, FFmpegMediaMetadataRetriever.OptionClosest); // frame at 2 seconds
                byte[] artwork = mmr.GetEmbeddedPicture();

                button.Text = string.Format("Album: {0}", mmr.GetMetadata().GetString(FFmpegMediaMetadataRetriever.MetadataKeyAlbum)); 

                mmr.Release();
            };
        }
    }
}

