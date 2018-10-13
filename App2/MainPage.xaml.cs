using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;
using MyToolkit.Multimedia;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using EASendMailRT;
using Windows.Graphics.Imaging;
using Windows.Devices.Enumeration;
using Windows.Globalization;
using Windows.Graphics.Display;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Media.Ocr;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.System.Display;
using Windows.Storage.Streams;
using Windows.Media.SpeechRecognition;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer_time;
        DispatcherTimer timer_3sec;
        DispatcherTimer timer_5sec;
        DispatcherTimer timer_alarm;
        DispatcherTimer timer_Reload2minute;
        DispatcherTimer timer_10secSpeechRec;
        DispatcherTimer timer_novelmicrosec;
        DispatcherTimer timer_1s;

        private SpeechRecognizer speechRecognizer;


        string Lat, Lng;


        // Bitmap holder of currently loaded image.
        private SoftwareBitmap bitmap;
        private Language ocrLanguage = new Language("en");

        private MediaCapture _mediaCapture;


        MediaCapture capture;
        InMemoryRandomAccessStream buffer;
        bool record;
        string filename;
        string audioFile = ".MP3";
        private StorageFile outputFile;





        Windows.Media.Capture.MediaCapture audioCapture;
        MediaCaptureInitializationSettings captureInitSettings;
        Windows.Media.Playback.MediaPlayer mediaPlayer;





        private string contactFeatures;
        private string ExamHour;
        private string ExamMinute;
        private string AudioPath;
        private string ImagePath;
        private string SurahNumber;
        private string QuranLocation;
        private string MenuNumber;
        private string EmergencyAlart;
        private string SongName;
        private string SongCategory;
        private string menuitem;
        private string WriterName;
        private string NctbClass;
        private string NctbSubject;
        private string NctbTopic;
        private string AlarmHours;
        private string ALarmMinutes;
        private int x;
        private int y;
        Double variable1;
        Double variable2;
        private string Operation;
        private string Restart;
        private string tappedkey;


        DisplayRequest _displayRequest = new DisplayRequest();
        StorageFolder _captureFolder;



        public MainPage()
        {


            this.InitializeComponent();


        }


        internal async Task<Uri> GetYoutubeUri(string VideoID)
        {
            YouTubeUri uri = await YouTube.GetVideoUriAsync(VideoID, YouTubeQuality.Quality1080P);
            return uri.Uri;
        }


        public void DispatcherTimerSetup()
        {
            timer_time = new DispatcherTimer();
            timer_time.Tick += timer_time_Tick;
            timer_time.Interval = new TimeSpan(0, 0, 1);


            timer_3sec = new DispatcherTimer();
            timer_3sec.Tick += timer_3sec_Tick;
            timer_3sec.Interval = new TimeSpan(0, 0, 1);

            timer_5sec = new DispatcherTimer();
            timer_5sec.Tick += timer_5sec_Tick;
            timer_5sec.Interval = new TimeSpan(0, 0, 5);

            timer_Reload2minute = new DispatcherTimer();
            timer_Reload2minute.Tick += timer_Reload2minute_Tick;
            timer_Reload2minute.Interval = new TimeSpan(0, 2, 0);



            timer_10secSpeechRec = new DispatcherTimer();
            timer_10secSpeechRec.Tick += timer10secSpeechRec_Tick;
            timer_10secSpeechRec.Interval = new TimeSpan(0, 0, 10);




            timer_1s = new DispatcherTimer();
            timer_1s.Tick += timer_1s_Tick;
            timer_1s.Interval = new TimeSpan(0, 0, 1);

        }



        void timer_1s_Tick(object sender, object e)


        {
            check_tap.IsChecked = false;
            timer_1s.Stop();
            txt_wrd.Text += txt_alp.Text;

            txt_alp.Text = "";



        }


        async void timer10secSpeechRec_Tick(object sender, object e)
        {



            // Create an instance of SpeechRecognizer.
            var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

            // Compile the dictation grammar by default.
            await speechRecognizer.CompileConstraintsAsync();

            speechRecognizer.UIOptions.IsReadBackEnabled = false;
            speechRecognizer.UIOptions.ShowConfirmation = false;
            speechRecognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromDays(2);
            speechRecognizer.Timeouts.BabbleTimeout = TimeSpan.FromDays(2);
            speechRecognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromDays(2);


            // Start recognition.
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();



            txt_speechResult.Text = speechRecognitionResult.Text;


            if (txt_speechResult.Text.Contains("zero") || txt_speechResult.Text.Contains("one") || txt_speechResult.Text.Contains("two") || txt_speechResult.Text.Contains("three") || txt_speechResult.Text.Contains("four") || txt_speechResult.Text.Contains("five") || txt_speechResult.Text.Contains("six") || txt_speechResult.Text.Contains("seven") || txt_speechResult.Text.Contains("eight") || txt_speechResult.Text.Contains("nine"))
            {


                txt_speechResult.Text = txt_speechResult.Text.Replace("zero", "0");
                txt_speechResult.Text = txt_speechResult.Text.Replace("one", "1");
                txt_speechResult.Text = txt_speechResult.Text.Replace("two", "2");
                txt_speechResult.Text = txt_speechResult.Text.Replace("three", "3");
                txt_speechResult.Text = txt_speechResult.Text.Replace("four", "4");
                txt_speechResult.Text = txt_speechResult.Text.Replace("five", "5");
                txt_speechResult.Text = txt_speechResult.Text.Replace("six", "6");
                txt_speechResult.Text = txt_speechResult.Text.Replace("seven", "7");
                txt_speechResult.Text = txt_speechResult.Text.Replace("eight", "8");
                txt_speechResult.Text = txt_speechResult.Text.Replace("nine", "9");






            }



        }

        void timer_time_Tick(object sender, object e)
        {
            txt_time.Text = DateTime.Now.ToString("hh:mm:ss tt");
            txt_date.Text = DateTime.Now.ToString(" dddd, MMMM dd, yyyy");


        }

        async void timer_alarm_Tick(object sender, object e)
        {



            if (check_ExamMode.IsChecked == true)
            {
                check_ExamMode.IsChecked = false;
                check_ExamMode_Copy.IsChecked = false;


                timer_alarm.Stop();



                try
                {

                    try
                    {


                        // The media object for controlling and playing audio.
                        MediaElement mediaElement12 = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("sending");

                        // Send the stream to the media object.
                        mediaElement12.SetSource(stream12, stream12.ContentType);
                        mediaElement12.Play();



                        SmtpMail oMail = new SmtpMail("TryIt");
                        SmtpClient oSmtp = new SmtpClient();

                        // Set your gmail email address
                        oMail.From = new MailAddress("mears.soft@gmail.com");

                        // Add recipient email address, please change it to yours
                        oMail.To.Add(new MailAddress(txt_teacherEmail.Text));

                        // Set email subject and email body text
                        oMail.Subject = "Answer sheet from your student";

                        if (txt_paragraph.Text != "")
                        {
                            oMail.TextBody = "time: " + txt_time.Text + " . " + txt_paragraph.Text;
                        }

                        else
                        {
                            oMail.TextBody = "time: " + txt_time.Text + " . " + txt_sentance.Text;

                        }







                        // Gmail SMTP server
                        SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                        // User and password for ESMTP authentication
                        oServer.User = "mears.soft@gmail.com";
                        oServer.Password = "Abanglalink1";

                        // Use 587 port
                        oServer.Port = 587;
                        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                        await oSmtp.SendMailAsync(oServer, oMail);



                    }

                    finally

                    {

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement12 = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("your answer sheet is send to " + txt_teacherEmail.Text);

                        // Send the stream to the media object.
                        mediaElement12.SetSource(stream12, stream12.ContentType);
                        mediaElement12.Play();


                    }


                }

                catch
                {


                }

            }



       
           else
            {

                timer_alarm.Stop();


                Uri newuri = new Uri("ms-appx:///Assets/Sounds/Alarm.mp3");
                media_Alarm.Source = newuri;
                media_Alarm.Play();


            }

        }

       async void timer_Reload2minute_Tick(object sender, object e)
        {

            try
            {
                Uri url = new Uri("https://app.simplenote.com/publish/N5d7M3");
                WebNewsUrl.Navigate(url);
            }

            catch
            {


            }



            try


            {

                var geoLocator = new Geolocator();
                geoLocator.DesiredAccuracy = PositionAccuracy.High;
                Geoposition pos = await geoLocator.GetGeopositionAsync();



                Lat = pos.Coordinate.Point.Position.Latitude.ToString();
                Lng = pos.Coordinate.Point.Position.Longitude.ToString();

                var data = await App2.Helper.GetWeather(Lat, Lng);
                if (data != null)
                {


                    txt_weatherComment.Text = $"{data.weather[0].description}";
                    txt_humidity.Text = $" {data.main.humidity}%";
                    txt_pressure.Text = $"{data.main.pressure} atm";
                    txt_temp.Text = $"{data.main.temp} °C";
                }

                else

                {

                }

            }

            catch
            {


            }

            try

            {

                // The location to reverse geocode.
                BasicGeoposition location = new BasicGeoposition();
                location.Latitude = Convert.ToDouble(Lat);
                location.Longitude = Convert.ToDouble(Lng);
                Geopoint pointToReverseGeocode = new Geopoint(location);

                MapService.ServiceToken = "Hvk4ZPbMSRLd1FpHq2kV~sTg6aSOVZPtXAFAjddNZug~AvO4iQonznsl6BRt9neCFFlduIMVcSu7Z7zxiKd-abDu5CfLizoy_QAlO4es7bYe";


                // Reverse geocode the specified geographic location.
                MapLocationFinderResult result =
                      await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);

                // If the query returns results, display the name of the town
                // contained in the address of the first result.
                if (result.Status == MapLocationFinderStatus.Success)
                {

                    txt_location.Text = "";
                    txt_location.Text += "Country = " +
                          result.Locations[0].Address.Country;
                    txt_location.Text += "  town = " +
                          result.Locations[0].Address.Town;

                    txt_location.Text += "  Street number = " +
                          result.Locations[0].Address.StreetNumber;


                    txt_location.Text += "  building name = " +
                          result.Locations[0].Address.BuildingName;


                    txt_lat.Text = Lat;
                    txt_lon.Text = Lng;



                }

                else
                {
                    //  tbOutputText.Text += "Failed";

                }

            }


            catch
            {


            }


        }

        void timer_3sec_Tick(object sender, object e)
        {

        }

        void timer_5sec_Tick(object sender, object e)
        {
            try
            {


                ButtonAutomationPeer peer1 = new ButtonAutomationPeer(btn_next);

                IInvokeProvider invokeProv1 = peer1.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv1.Invoke();

            }

            catch
            {

            }

        }


        private void btn_nctb_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btn_weather_Click(object sender, RoutedEventArgs e)
        {

            if (txt_lon.Text == "Longitude")

            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Check your internet connection");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


            }

            else
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Today the weather condition is " + txt_weatherComment.Text + ". Temperature is about " + txt_temp.Text + ", pressure " + txt_pressure.Text + ", humidity " + txt_humidity.Text);

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }

            }

        private async void btn_location_Click(object sender, RoutedEventArgs e)
        {


            if (txt_location.Text == "Location")

            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Check your internet connection");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }

            else
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Your current location is " + txt_location.Text);

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }
            }

        private void btn_show_Click(object sender, RoutedEventArgs e)
        {


        }

        private async void btn_msg_Click(object sender, RoutedEventArgs e)
        {
            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("voice message is recording");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

            await Task.Delay(1500);


            check_msg.IsChecked = true;




            var devices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Devices.Enumeration.DeviceClass.AudioCapture);



            captureInitSettings = new Windows.Media.Capture.MediaCaptureInitializationSettings();
            captureInitSettings.StreamingCaptureMode = Windows.Media.Capture.StreamingCaptureMode.AudioAndVideo;
            audioCapture = new Windows.Media.Capture.MediaCapture();
            await audioCapture.InitializeAsync(captureInitSettings);




            var storageFile = await Windows.Storage.KnownFolders.VideosLibrary.CreateFileAsync("audioOut.mp3", Windows.Storage.CreationCollisionOption.GenerateUniqueName);

            MediaEncodingProfile profile = MediaEncodingProfile.CreateM4a(Windows.Media.MediaProperties.AudioEncodingQuality.Auto); await audioCapture.StartRecordToStorageFileAsync(profile, storageFile);


            AudioPath = storageFile.Path;


        }

        private async void btn_date_Click(object sender, RoutedEventArgs e)
        {
            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Today the date is" + txt_date.Text);

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

        private async void btn_calculator_Click(object sender, RoutedEventArgs e)
        {


            check_calculator.IsChecked = true;
            check_variable1.IsChecked = false;
            check_variable2.IsChecked = false;
            txt_v1.Text = "";
            txt_v2.Text = "";


            variable1 = 0;
            variable2 = 0;

            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(" Use Braille-board to input variable");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();


        }

        private async void btn_1_Click(object sender, RoutedEventArgs e)
        {

            if (check_menu.IsChecked == true)
            {

                Restart += "1";

                if (Restart == "1111111111")

                {
                    check_ExamMode_Copy.IsChecked = false;
                    check_ExamMode.IsChecked = false;

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Hack done. your exam mode is stopped");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();


                }
            }

                    if (check_writting.IsChecked == true && check_email.IsChecked == false)

            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("1");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "1";

                }


                else
                {

                    if (check_writting.IsChecked == true)

                    {

                        if (tappedkey != "1")

                        {

                            txt_wrd.Text += txt_alp.Text;




                            txt_alp.Text = ".";

                            txt_tap.Text = "";
                            txt_tap.Text = "tapped";



                            tappedkey = "1";




                            // The media object for controlling and playing audio.
                            MediaElement mediaElement = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(".");

                            // Send the stream to the media object.
                            mediaElement.SetSource(stream, stream.ContentType);
                            mediaElement.Play();




                        }





                        else if (txt_tap.Text == "tapped" && tappedkey == "1")


                        {

                            if (txt_alp.Text == "")

                            {

                                txt_alp.Text = ".";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                txt_paragraph.Text += txt_sentance.Text + ". ";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("full stop " + txt_sentance.Text);

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == ".")

                            {
                                txt_alp.Text = ",";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("coma");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == ",")

                            {
                                txt_alp.Text = "?";


                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("question sign");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "?")

                            {
                                txt_alp.Text = "!";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Exclamatory");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();
                            }


                            else if (txt_alp.Text == "!")

                            {
                                txt_alp.Text = "(";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("bracket start");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }

                            else if (txt_alp.Text == ")")

                            {
                                txt_alp.Text = ")";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("bracket close");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == ")")

                            {

                                txt_alp.Text = ".";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                txt_paragraph.Text += txt_sentance.Text + ". ";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("full stop " + txt_sentance.Text);

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }

                        }




                    }


                }

            }



            else if (check_writting.IsChecked == true && check_email.IsChecked == true)



            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("1");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "1";

                }


                else
                {

                    if (txt_alp.Text == "")

                    {

                        txt_tap.Text = "";
                        txt_tap.Text = "tapped";

                        txt_alp.Text = "@";

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_alp.Text);

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();



                    }


                    else if (txt_alp.Text == "@")

                    {


                        txt_tap.Text = "";
                        txt_tap.Text = "tapped";

                        txt_alp.Text = ".";

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("dot");

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();



                    }


                    else if (txt_alp.Text == ".")

                    {


                        txt_tap.Text = "";
                        txt_tap.Text = "tapped";

                        txt_alp.Text = "@gmail.com";

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_alp.Text);

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();



                    }


                    else if (txt_alp.Text == "@gmail.com")

                    {

                        txt_tap.Text = "";
                        txt_tap.Text = "tapped";


                        txt_alp.Text = "@yahoo.com";

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_alp.Text);

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();



                    }

                    else if (txt_alp.Text == "@yahoo.com")

                    {


                        txt_tap.Text = "";
                        txt_tap.Text = "tapped";


                        txt_alp.Text = "@hotmail.com";

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_alp.Text);

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();



                    }

                    else if (txt_alp.Text == "@hotmail.com")

                    {

                        txt_tap.Text = "";
                        txt_tap.Text = "tapped";


                        txt_alp.Text = "@";

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_alp.Text);

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();



                    }



                }
            }


            else
            {



                {




                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("1");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                    {
                        check_refresh_number.IsChecked = true;
                        MenuNumber += "1";

                    }

                    else

                    {


                    }

                    if (check_quran.IsChecked == true)

                    {

                        check_quran_number.IsChecked = true;
                        SurahNumber += "1";


                    }



                    if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
                    {

                        AlarmHours += "1";
                    }

                    else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

                    {
                        ALarmMinutes += "1";


                    }

                    else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                    {
                        ExamHour += "1";


                    }

                    else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                    {
                        ExamMinute += "1";


                    }


                    else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                    {

                        txt_v1.Text += "1";

                    }

                    else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                    {

                        txt_v2.Text += "1";

                    }
                }


            }
        }

        private async void btn_2_Click(object sender, RoutedEventArgs e)
        {


            if (check_writting.IsChecked == true)

            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("2");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "2";
                }


                else
                {

                    if (check_writting.IsChecked == true)

                    {

                        if (tappedkey != "2")

                        {

                            txt_wrd.Text += txt_alp.Text;




                            txt_alp.Text = "a";

                            txt_tap.Text = "";
                            txt_tap.Text = "tapped";



                            tappedkey = "2";




                            // The media object for controlling and playing audio.
                            MediaElement mediaElement = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("a");

                            // Send the stream to the media object.
                            mediaElement.SetSource(stream, stream.ContentType);
                            mediaElement.Play();


                        }





                        else if (txt_tap.Text == "tapped" && tappedkey == "2")


                        {

                            if (txt_alp.Text == "")

                            {

                                txt_alp.Text = "a";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("a");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == "a")

                            {
                                txt_alp.Text = "b";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("b");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "b")

                            {
                                txt_alp.Text = "c";


                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("c");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "c")

                            {
                                txt_alp.Text = "a";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("a");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();
                            }




                        }




                    }


                }

            }



            else

            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("2");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();


            if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
            {

                MenuNumber += "2";
                check_refresh_number.IsChecked = true;
            }

            else

            {


            }


            if (check_quran.IsChecked == true)

            {
                check_quran_number.IsChecked = true;

                SurahNumber += "2";


            }



            if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
            {

                AlarmHours += "2";
            }

            else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

            {
                ALarmMinutes += "2";


            }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                {
                    ExamHour += "2";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                {
                    ExamMinute += "2";


                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
            {

                txt_v1.Text += "2";

            }

            else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
            {

                txt_v2.Text += "2";

            }

            }
        }

        private async void btn_3_Click(object sender, RoutedEventArgs e)
        {


            if (check_writting.IsChecked == true)

            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("3");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "3";
                }


                else
                {

                    if (check_writting.IsChecked == true)

                    {

                        if (tappedkey != "3")

                        {

                            txt_wrd.Text += txt_alp.Text;




                            txt_alp.Text = "d";

                            txt_tap.Text = "";
                            txt_tap.Text = "tapped";



                            tappedkey = "3";




                            // The media object for controlling and playing audio.
                            MediaElement mediaElement = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("d");

                            // Send the stream to the media object.
                            mediaElement.SetSource(stream, stream.ContentType);
                            mediaElement.Play();


                        }





                        else if (txt_tap.Text == "tapped" && tappedkey == "3")

                        
                        {

                            if (txt_alp.Text == "")

                            {

                                txt_alp.Text = "d";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("d");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == "d")

                            {
                                txt_alp.Text = "e";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("e");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "e")

                            {
                                txt_alp.Text = "f";


                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("f");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "f")

                            {
                                txt_alp.Text = "d";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("d");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();
                            }




                        }




                    }


                }

            }



            else

            {




                if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                {

                    MenuNumber += "3";
                    check_refresh_number.IsChecked = true;
                }

                else

                {


                }

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("3");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


                if (check_quran.IsChecked == true)

                {
                    check_quran_number.IsChecked = true;

                    SurahNumber += "3";


                }



                if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
                {

                    AlarmHours += "3";
                }

                else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

                {
                    ALarmMinutes += "3";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                {
                    ExamHour += "3";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                {
                    ExamMinute += "3";


                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                {

                    txt_v1.Text += "3";

                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                {

                    txt_v2.Text += "3";

                }
            }

        }

        private async void btn_4_Click(object sender, RoutedEventArgs e)
        {


            if (check_writting.IsChecked == true)

            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("4");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "4";
                }


                else
                {

                    if (check_writting.IsChecked == true)

                    {

                        if (tappedkey != "4")

                        {

                            txt_wrd.Text += txt_alp.Text;




                            txt_alp.Text = "g";

                            txt_tap.Text = "";
                            txt_tap.Text = "tapped";



                            tappedkey = "4";




                            // The media object for controlling and playing audio.
                            MediaElement mediaElement = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("g");

                            // Send the stream to the media object.
                            mediaElement.SetSource(stream, stream.ContentType);
                            mediaElement.Play();


                        }





                        else if (txt_tap.Text == "tapped" && tappedkey == "4")


                        {

                            if (txt_alp.Text == "")

                            {

                                txt_alp.Text = "g";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("g");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == "g")

                            {
                                txt_alp.Text = "h";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("h");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "h")

                            {
                                txt_alp.Text = "i";


                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("i");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "i")

                            {
                                txt_alp.Text = "g";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("g");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();
                            }




                        }




                    }


                }

            }



            else

            {

                if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                {

                    MenuNumber += "4";
                    check_refresh_number.IsChecked = true;
                }

                else

                {


                }


                if (media_music.CurrentState.ToString() == "Playing")
                {

                    media_music.Position -= TimeSpan.FromSeconds(30);



                }

                else
                {


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("4");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    if (check_quran.IsChecked == true)

                    {
                        check_quran_number.IsChecked = true;

                        SurahNumber += "4";


                    }




                    if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
                    {

                        AlarmHours += "4";
                    }

                    else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

                    {
                        ALarmMinutes += "4";


                    }

                    else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                    {
                        ExamHour += "4";


                    }

                    else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                    {
                        ExamMinute += "4";


                    }
                    else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                    {

                        txt_v1.Text += "4";

                    }

                    else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                    {

                        txt_v2.Text += "4";

                    }
                }

            }
        }

        private async void btn_answer_Click(object sender, RoutedEventArgs e)
        {

            if (check_writting.IsChecked == true)

            {

                if (check_numberinput.IsChecked == false)

                {

                    check_numberinput.IsChecked = true;

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("number");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }


                else if (check_numberinput.IsChecked == true)

                {


                    check_numberinput.IsChecked = false;

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("alphatbet");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                }



            }



            else

            {

                if (check_menu.IsChecked == false)

                {

                    check_voicecntrl.IsChecked = true;



                    check_menu.IsChecked = false;
                    check_alarm.IsChecked = false;
                    check_alrmHour.IsChecked = false;
                    check_alrmMinute.IsChecked = false;
                    check_Class.IsChecked = false;
                    check_nctb.IsChecked = false;
                    check_novels.IsChecked = false;
                    check_subject.IsChecked = false;
                    check_topic.IsChecked = false;
                    check_variable1.IsChecked = false;
                    check_variable2.IsChecked = false;
                    check_writerChoosed.IsChecked = false;
                    check_calculator.IsChecked = false;
                    check_calcEquation.IsChecked = false;
                    check_song.IsChecked = false;
                    check_songCategory.IsChecked = false;
                    check_ocr.IsChecked = false;
                    check_quran.IsChecked = false;
                    check_quran_number.IsChecked = false;
                    check_refresh_number.IsChecked = false;


                    txt_math1.Text = "";
                    txt_math2.Text = "";
                    txt_mathAnswer.Text = "";
                    txt_surahNumber.Text = "";
                    txt_speechResult.Text = "";
                    txt_alarmHours.Text = "";
                    txt_alarmMinutes.Text = "";
                    txt1.Text = "";
                    txt_v1.Text = "";
                    txt_v2.Text = "";
                    txt_calAnswer.Text = "";
                    txt_result.Text = "";

                    ExamHour = null;
                    ExamMinute = null;
                    EmergencyAlart = null;
                    variable1 = Convert.ToDouble(null);
                    variable2 = Convert.ToDouble(null);
                    SongCategory = null;
                    SongName = null;
                    menuitem = null;
                    WriterName = null;
                    NctbClass = null;
                    NctbSubject = null;
                    NctbTopic = null;
                    AlarmHours = null;
                    ALarmMinutes = null;
                    MenuNumber = "";
                    SurahNumber = null;
                    Restart = null;
                    ImagePath = null;
                    list_class.SelectedIndex = -1;
                    list_novels.SelectedIndex = -1;
                    list_subject.SelectedIndex = -1;
                    list_topic.SelectedIndex = -1;
                    list_calcEquation.SelectedIndex = -1;
                    list_menuitem.SelectedIndex = -1;
                    list_songBangla.SelectedIndex = -1;
                    list_songCategory.SelectedIndex = -1;
                    list_songEnglish.SelectedIndex = -1;
                    list_songHindi.SelectedIndex = -1;
                    list_quran.SelectedIndex = -1;










                    // Create an instance of SpeechRecognizer.
                    var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

                    // Compile the dictation grammar by default.
                    await speechRecognizer.CompileConstraintsAsync();

                    speechRecognizer.UIOptions.IsReadBackEnabled = false;
                    speechRecognizer.UIOptions.ShowConfirmation = false;
                    speechRecognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromDays(2);
                    speechRecognizer.Timeouts.BabbleTimeout = TimeSpan.FromDays(2);
                    speechRecognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromDays(2);


                    // Start recognition.
                    Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();



                    txt_speechResult.Text = speechRecognitionResult.Text;


                    if (txt_speechResult.Text.Contains("zero") || txt_speechResult.Text.Contains("one") || txt_speechResult.Text.Contains("two") || txt_speechResult.Text.Contains("three") || txt_speechResult.Text.Contains("four") || txt_speechResult.Text.Contains("five") || txt_speechResult.Text.Contains("six") || txt_speechResult.Text.Contains("seven") || txt_speechResult.Text.Contains("eight") || txt_speechResult.Text.Contains("nine"))
                    {


                        txt_speechResult.Text = txt_speechResult.Text.Replace("zero", "0");
                        txt_speechResult.Text = txt_speechResult.Text.Replace("one", "1");
                        txt_speechResult.Text = txt_speechResult.Text.Replace("two", "2");
                        txt_speechResult.Text = txt_speechResult.Text.Replace("three", "3");
                        txt_speechResult.Text = txt_speechResult.Text.Replace("four", "4");
                        txt_speechResult.Text = txt_speechResult.Text.Replace("five", "5");
                        txt_speechResult.Text = txt_speechResult.Text.Replace("six", "6");
                        txt_speechResult.Text = txt_speechResult.Text.Replace("seven", "7");
                        txt_speechResult.Text = txt_speechResult.Text.Replace("eight", "8");
                        txt_speechResult.Text = txt_speechResult.Text.Replace("nine", "9");






                    }

                }


                if (check_calculator.IsChecked == true && check_variable1.IsChecked == true)
                {

                    variable2 = Convert.ToDouble(txt_v2.Text);
                    check_variable2.IsChecked = true;

                    list_calcEquation.SelectedIndex = -1;


                    if (Operation == "+")
                    {
                        txt_calAnswer.Text = Convert.ToString(Convert.ToDouble(variable1) + Convert.ToDouble(variable2));

                        //// / The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(variable1.ToString() + " plus " + variable2.ToString() + " is equals to " + txt_calAnswer.Text);

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();
                    }

                    else if (Operation == "-")
                    {

                        txt_calAnswer.Text = Convert.ToString(Convert.ToDouble(variable1) - Convert.ToDouble(variable2));
                        //// / The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(variable1.ToString() + " minus " + variable2.ToString() + " is equals to " + txt_calAnswer.Text);

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();
                    }

                    else if (Operation == "*")
                    {

                        txt_calAnswer.Text = Convert.ToString(Convert.ToDouble(variable1) * Convert.ToDouble(variable2));


                        //// / The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(variable1.ToString() + " multiply " + variable2.ToString() + " is equals to " + txt_calAnswer.Text);

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();
                    }
                    else if (Operation == "/")
                    {
                        txt_calAnswer.Text = Convert.ToString(Convert.ToDouble(variable1) / Convert.ToDouble(variable2));

                        //// / The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(variable1.ToString() + " divide " + variable2.ToString() + " is equals to " + txt_calAnswer.Text);

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();
                    }

                }

                else

                {


                }


            }


        }





        private async void btn_refresh_Click(object sender, RoutedEventArgs e)
        {


            if (check_ExamMode.IsChecked == true || check_ExamMode_Copy.IsChecked == true)

            {

                check_calculator.IsChecked = false;
                check_variable1.IsChecked = false;
                check_variable2.IsChecked = false;
                check_calcEquation.IsChecked = false;


                txt_v1.Text = "";
                txt_v2.Text = "";

                list_calcEquation.SelectedIndex = -1;

                check_ExamMode.IsChecked = true;
                check_TchrEmail.IsChecked = true;
                check_ExamTimer.IsChecked = true;
                check_ExamHour.IsChecked = true;
                check_ExamMinute.IsChecked = true;

            }

            else
            {

                list_topic.Items.Clear();
                list_subject.Items.Clear();



                check_ExamMode_Copy.IsChecked = false;
                check_ExamMode.IsChecked = false;
                check_TchrEmail.IsChecked = false;
                check_ExamTimer.IsChecked = false;
                check_ExamHour.IsChecked = false;
                check_ExamMinute.IsChecked = false;






                media_novel.Stop();


                //// / The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Refreshed");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


                check_refresh.IsChecked = true;



                if (media_Alarm.CurrentState.ToString() == "Playing")
                {

                    media_Alarm.Stop();
                    timer_alarm.Stop();

                }

                media_speech.Stop();
                media_music.Stop();





                check_msg.IsChecked = false;
                check_contact.IsChecked = false;
                check_contactFeatures.IsChecked = false;
                check_email.IsChecked = false;
                check_newContact.IsChecked = false;
                check_seeExisting.IsChecked = false;
                check_writting.IsChecked = false;
                check_msg.IsChecked = false;
                check_tap.IsChecked = false;
                check_numberinput.IsChecked = false;
                check_voicecntrl.IsChecked = false;
                check_novelChoosed.IsChecked = false;
                check_menu.IsChecked = false;
                check_alarm.IsChecked = false;
                check_alrmHour.IsChecked = false;
                check_alrmMinute.IsChecked = false;
                check_Class.IsChecked = false;
                check_nctb.IsChecked = false;
                check_novels.IsChecked = false;
                check_subject.IsChecked = false;
                check_topic.IsChecked = false;
                check_variable1.IsChecked = false;
                check_variable2.IsChecked = false;
                check_writerChoosed.IsChecked = false;
                check_calculator.IsChecked = false;
                check_calcEquation.IsChecked = false;
                check_song.IsChecked = false;
                check_songCategory.IsChecked = false;
                check_ocr.IsChecked = false;
                check_quran.IsChecked = false;
                check_quran_number.IsChecked = false;
                check_refresh_number.IsChecked = false;
                check_notesRecordings.IsChecked = false;
                check_notesFeatures.IsChecked = false;



                check_savedNotes.IsChecked = false;
                check_notesSelect.IsChecked = false;
                check_notetxtcomplete.IsChecked = false;
                check_noteName.IsChecked = false;
                check_notesFeatures.IsChecked = false;
                check_savedNotesNameSelect.IsChecked = false;
                check_rcdName.IsChecked = false;
                check_rcdSelect.IsChecked = false;
                check_rcdStrt.IsChecked = false;
                check_savedRecordings.IsChecked = false;




                txt_math1.Text = "";
                txt_math2.Text = "";
                txt_mathAnswer.Text = "";
                txt_surahNumber.Text = "";
                txt_speechResult.Text = "";
                txt_alarmHours.Text = "";
                txt_alarmMinutes.Text = "";
                txt1.Text = "";
                txt_v1.Text = "";
                txt_v2.Text = "";
                txt_calAnswer.Text = "";
                txt_result.Text = "";
                txt_wrd.Text = "";
                txt_sentance.Text = "";
                txt_alp.Text = "";
                txt_number.Text = "";



                AlarmHours = null;
                ALarmMinutes = null;
                EmergencyAlart = null;
                variable1 = Convert.ToDouble(null);
                variable2 = Convert.ToDouble(null);
                SongCategory = null;
                SongName = null;
                menuitem = null;
                WriterName = null;
                NctbClass = null;
                NctbSubject = null;
                NctbTopic = null;
                AlarmHours = null;
                ALarmMinutes = null;
                MenuNumber = "";
                SurahNumber = null;
                Restart = null;
                ImagePath = null;
                list_class.SelectedIndex = -1;
                list_novels.SelectedIndex = -1;
                list_subject.SelectedIndex = -1;
                list_topic.SelectedIndex = -1;
                list_calcEquation.SelectedIndex = -1;
                list_menuitem.SelectedIndex = -1;
                list_songBangla.SelectedIndex = -1;
                list_songCategory.SelectedIndex = -1;
                list_songEnglish.SelectedIndex = -1;
                list_songHindi.SelectedIndex = -1;
                list_quran.SelectedIndex = -1;
                list_emailaddress.SelectedIndex = -1;
                list_contactFeatures.SelectedIndex = -1;
                list_notes.SelectedIndex = -1;
                list_notesFeatures.SelectedIndex = -1;
                list_recordings.SelectedIndex = -1;


            }

        }

        private async void btn_next_Click(object sender, RoutedEventArgs e)
        {


           if(check_ExamMode.IsChecked == true && check_TchrEmail.IsChecked == true )

            {
                try
                {
                    list_examMode.SelectedIndex = list_examMode.SelectedIndex + 1;
                }

                catch
                {
                

                    list_examMode.SelectedIndex = 0;

                }

            }

          

           else if(check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == false)

            {
                try
                {
                    list_notesFeatures.SelectedIndex = list_notesFeatures.SelectedIndex + 1;
                }

              catch
                {
                    try
                    {
                        list_notesFeatures.SelectedIndex = 0;

                    }

                    catch
                    {


                    }
                }


            }

         

            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_savedNotes.IsChecked == true)

            {
                try
                {
                    list_notes.SelectedIndex = list_notes.SelectedIndex + 1;
                }

                catch
                {
                    try
                    {
                        list_notes.SelectedIndex = 0;
                    }
                    catch
                    {

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("No notes is found");

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();
                    }
                }

               // break;

            }

           
            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_savedRecordings.IsChecked == true)

            {
                try
                {
                    list_recordings.SelectedIndex = list_recordings.SelectedIndex + 1;
                }

                catch
                {
                    try
                    {
                        list_recordings.SelectedIndex = 0;
                    }
                    catch
                    {
                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("No recording is found");

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();
                    }
                }



            }




            else if(check_email.IsChecked == true && check_msg.IsChecked == true)

            {
                try
                {
                    list_emailaddress.SelectedIndex = list_emailaddress.SelectedIndex + 1;
                }

                catch
                {
                    try
                    {
                        list_emailaddress.SelectedIndex = 0;


                    }

                    catch
                    {

                    }

                }

            }



            else if(check_contact.IsChecked == true && check_contactFeatures.IsChecked == false)

            {

                try
                {

                    list_contactFeatures.SelectedIndex = list_contactFeatures.SelectedIndex + 1;
                }

                catch
                {

                }
            }

            else if (check_contact.IsChecked == true && check_contactFeatures.IsChecked == true && check_seeExisting.IsChecked == true)

            {
              
                    try
                    {
                        list_emailaddress.SelectedIndex = list_emailaddress.SelectedIndex + 1;
                    }

                    catch

                    {
                        try
                        {
                            list_emailaddress.SelectedIndex = 0;
                        }
                        catch
                        {


                        }

                    }

                


            }

            else if (check_novels.IsChecked == true && check_writerChoosed.IsChecked == false)
            {
                                                  
                try
                {
                    list_novels.SelectedIndex = list_novels.SelectedIndex + 1;

                }

                catch

                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("The list is ended");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                }



            }

            else if (check_novels.IsChecked == true && check_writerChoosed.IsChecked == true)
            {
                try
                {
                    txt1.Text = Convert.ToString(Convert.ToDouble(txt1.Text) + Convert.ToDouble("1"));


                    Uri newuri = new Uri("ms-appx:///Assets/Novels/" + WriterName + "/" + txt1.Text + ".mp3");
                    media_novel.Source = newuri;

                    media_novel.Play();

                }

                catch
                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("The list is ended");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }

            }


            else if (check_quran.IsChecked == true)

            {

                try
                {

                    list_quran.SelectedIndex = list_quran.SelectedIndex + 1;


                    Uri newuri = new Uri(QuranLocation + list_quran.SelectedValue.ToString());
                    media_music.Source = newuri;

                    media_music.Play();

                }

                catch

                {
                    list_subject.SelectedIndex = 0;

                    try
                    {



                        Uri newuri = new Uri(QuranLocation + list_quran.SelectedValue.ToString());
                        media_music.Source = newuri;

                        media_music.Play();

                    }

                    catch

                    {




                    }


                }

            }

            else if (check_nctb.IsChecked == true && check_Class.IsChecked == false && check_subject.IsChecked == false && check_topic.IsChecked == false)
            {

                try
                {
                    list_class.SelectedIndex = list_class.SelectedIndex + 1;
                }

                catch
                {
                    list_class.SelectedIndex = 0;
                }

            }
            else if (check_nctb.IsChecked == true && check_Class.IsChecked == true && check_subject.IsChecked == false && check_topic.IsChecked == false)
            {
                try
                {

                    list_subject.SelectedIndex = list_subject.SelectedIndex + 1;

                }

                catch

                {
                    list_subject.SelectedIndex = 0;


                }


            }
            else if (check_nctb.IsChecked == true && check_Class.IsChecked == true && check_subject.IsChecked == true && check_topic.IsChecked == false)
            {

                try
                {
                    list_topic.SelectedIndex = list_topic.SelectedIndex + 1;
                }

                catch

                {
                    list_topic.SelectedIndex = 0;


                }


            }
            else if (check_nctb.IsChecked == true && check_Class.IsChecked == true && check_subject.IsChecked == true && check_topic.IsChecked == true)
            {


                txt1.Text = Convert.ToString(Convert.ToDouble(txt1.Text) + Convert.ToDouble("1"));



                Uri newuri = new Uri("ms-appx:///Assets/NCTB/" + NctbClass + "/" + NctbSubject + "/" + NctbTopic + "/" + txt1.Text + ".mp3");
                media_music.Source = newuri;

                media_music.Play();



            }

            else if (check_refresh.IsChecked == true && check_song.IsChecked == false && check_ExamMode.IsChecked == false && check_ExamMode_Copy.IsChecked == false && check_menu.IsChecked == true && check_alarm.IsChecked == false && check_ocr.IsChecked == false && check_nctb.IsChecked == false && check_novels.IsChecked == false && check_calculator.IsChecked == false)

            {

                try
                {
                    list_menuitem.SelectedIndex = list_menuitem.SelectedIndex + 1;
                }

                catch

                {
                    list_menuitem.SelectedIndex = 0;


                }




            }


            else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_calcEquation.IsChecked == false && check_variable2.IsChecked == false)
            {
                try
                {
                    list_calcEquation.SelectedIndex = list_calcEquation.SelectedIndex + 1;
                }

                catch

                {
                    list_calcEquation.SelectedIndex = 0;


                }

            }

            else if (check_song.IsChecked == true && check_songCategory.IsChecked == false && SongCategory == null)

            {

                try
                {
                    list_songCategory.SelectedIndex = list_songCategory.SelectedIndex + 1;
                }

                catch

                {
                    list_songCategory.SelectedIndex = 0;


                }
            }

            else if (check_song.IsChecked == true && check_songCategory.IsChecked == true && SongCategory != null)

            {
                if (SongCategory == "Bangla Song")
                {


                    try
                    {
                        list_songBangla.SelectedIndex = list_songBangla.SelectedIndex + 1;
                    }

                    catch

                    {
                        list_songBangla.SelectedIndex = 0;


                    }


                }

                else if (SongCategory == "Hindi Song")
                {


                    try
                    {
                        list_songHindi.SelectedIndex = list_songHindi.SelectedIndex + 1;
                    }

                    catch

                    {
                        list_songHindi.SelectedIndex = 0;


                    }


                }

                else if (SongCategory == "English Song")
                {


                    try
                    {
                        list_songEnglish.SelectedIndex = list_songEnglish.SelectedIndex + 1;
                    }

                    catch

                    {
                        list_songEnglish.SelectedIndex = 0;


                    }


                }

                else if (SongCategory == "Top rated Song")
                {


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("This features is Under development. Click refresh and menu button to access other feathers.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }

            }



        }
        private async void btn_bigButton_Click(object sender, RoutedEventArgs e)
        {


            
             if (check_refresh.IsChecked == true && check_refresh_number.IsChecked == false && check_menu.IsChecked == false && check_alarm.IsChecked == false && check_ocr.IsChecked == false && check_msg.IsChecked == false && check_notesRecordings.IsChecked == false && check_ExamMode.IsChecked == false  && check_nctb.IsChecked == false && check_novels.IsChecked == false && check_calculator.IsChecked == false)

            {


                check_menu.IsChecked = true;



            }


            if (check_ExamMode.IsChecked == true && check_TchrEmail.IsChecked == false && check_ExamTimer.IsChecked == false && check_calculator.IsChecked == false)

            {
                if (txt_wrd.Text.Contains("@"))
                {
                    check_TchrEmail.IsChecked = true;


                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("TeacherEmail.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file, txt_wrd.Text);
                    ApplicationData.Current.LocalSettings.Values["Teacher Email Saved"] = true;


                    txt_teacherEmail.Text = txt_wrd.Text;


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Continuing with the email " + txt_teacherEmail.Text + ". Now set the duration of your exam. first set hours");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                }

                else if (txt_teacherEmail.Text.Contains("@"))
                {

                    check_TchrEmail.IsChecked = true;


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Continuing with the email " + txt_teacherEmail.Text + ". Now set the duration of your exam. first set hours");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                }

                else
                {


                }

                check_email.IsChecked = false;
                check_writting.IsChecked = false;

            }


            else if (check_ExamMode.IsChecked == true && check_TchrEmail.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)


            {
                check_ExamHour.IsChecked = true;


                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(ExamHour + " hour added. Now set minutes.");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }

            else if (check_ExamMode.IsChecked == true && check_TchrEmail.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)


            {

                check_ExamMinute.IsChecked = true;
                check_writting.IsChecked = true;


                int y = Convert.ToInt32(ExamHour);
                int x = Convert.ToInt32(ExamMinute);


                timer_alarm = new DispatcherTimer();
                timer_alarm.Tick += timer_alarm_Tick;
                timer_alarm.Interval = new TimeSpan(y, x, 1);


                timer_alarm.Start();


                check_ExamTimer.IsChecked = true;




                // The media object for controlling and playing audio.
                MediaElement mediaElement12 = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("your exam time is set for " + ExamHour + " hours and " + ExamMinute + " minutes");

                // Send the stream to the media object.
                mediaElement12.SetSource(stream12, stream12.ContentType);
                mediaElement12.Play();


            }

            else if( check_ExamMode.IsChecked == true && check_TchrEmail.IsChecked == true &&check_ExamTimer.IsChecked == true)


            {
                if(list_examMode.SelectedIndex != -1)

                {
                    if (list_examMode.SelectedValue.ToString() == "Time")
                    {


                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_time);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();


                    }

                    else if (list_examMode.SelectedValue.ToString() == "Calculator")
                    {


                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_calculator);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                        check_writting.IsChecked = false;
                        check_numberinput.IsChecked = false;

                        check_ExamMode.IsChecked = false;
                        check_ExamMode_Copy.IsChecked = true;
                        check_ExamHour.IsChecked = false;
                        check_ExamMinute.IsChecked = false;
                        check_ExamTimer.IsChecked = false;
                        check_TchrEmail.IsChecked = false;
                      


                    }

                    else if (list_examMode.SelectedValue.ToString() == "Answer Sheet")

                    {
                        check_variable1.IsChecked = false;
                        check_variable2.IsChecked = false;
                        check_calcEquation.IsChecked = false;
                        check_calculator.IsChecked = false;


                        check_writting.IsChecked = true;
                        txt_v1.Text = "";
                        txt_v2.Text = "";
                        txt_calAnswer.Text = "";


                        check_ExamMode.IsChecked = true;
                        check_ExamMode_Copy.IsChecked = false;
                        check_ExamHour.IsChecked = true;
                        check_ExamMinute.IsChecked = true;
                        check_ExamTimer.IsChecked = true;
                        check_TchrEmail.IsChecked = true;




                        // The media object for controlling and playing audio.
                        MediaElement mediaElement12 = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("switch to answer sheet");

                        // Send the stream to the media object.
                        mediaElement12.SetSource(stream12, stream12.ContentType);
                        mediaElement12.Play();


                    }


                    else if (list_examMode.SelectedValue.ToString() == "Send Answer Sheet")
                    {

                        try
                        {

                            try
                            {
                                check_ExamMode.IsChecked = false;
                                check_ExamMode_Copy.IsChecked = false;



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement12 = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("sending");

                                // Send the stream to the media object.
                                mediaElement12.SetSource(stream12, stream12.ContentType);
                                mediaElement12.Play();



                                SmtpMail oMail = new SmtpMail("TryIt");
                                SmtpClient oSmtp = new SmtpClient();

                                // Set your gmail email address
                                oMail.From = new MailAddress("mears.soft@gmail.com");

                                // Add recipient email address, please change it to yours
                                oMail.To.Add(new MailAddress(txt_teacherEmail.Text));

                                // Set email subject and email body text
                                oMail.Subject = "Answer sheet from your student";

                                if (txt_paragraph.Text != "")
                                {
                                    oMail.TextBody = "time: " + txt_time.Text + " . " + txt_paragraph.Text;
                                }

                                else
                                {
                                    oMail.TextBody = "time: " + txt_time.Text + " . " + txt_sentance.Text;

                                }







                                // Gmail SMTP server
                                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                                // User and password for ESMTP authentication
                                oServer.User = "mears.soft@gmail.com";
                                oServer.Password = "Abanglalink1";

                                // Use 587 port
                                oServer.Port = 587;
                                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                                await oSmtp.SendMailAsync(oServer, oMail);



                            }

                            finally

                            {

                                // The media object for controlling and playing audio.
                                MediaElement mediaElement12 = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("your answer sheet is send to " + txt_teacherEmail.Text);

                                // Send the stream to the media object.
                                mediaElement12.SetSource(stream12, stream12.ContentType);
                                mediaElement12.Play();


                            }


                        }

                        catch
                        {


                        }

                    }

                    else
                    {


                    }

                  
                }



            }




            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == false)

            {
                check_notesFeatures.IsChecked = true;


                if (list_notesFeatures.SelectedValue.ToString() == "New Notes")

                {
                    check_notesSelect.IsChecked = true;
                    check_writting.IsChecked = true;

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Use braille board to write notes");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                }

                else if (list_notesFeatures.SelectedValue.ToString() == "Saved Notes")

                {
                    check_savedNotes.IsChecked = true;

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Press next button to access saved notes.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }

                else if (list_notesFeatures.SelectedValue.ToString() == "New Recordings")

                {
                    check_rcdSelect.IsChecked = true;
                    check_writting.IsChecked = true;

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement1 = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("first type the name of your  record");

                    // Send the stream to the media object.
                    mediaElement1.SetSource(stream1, stream1.ContentType);
                    mediaElement1.Play();

                }

                else if (list_notesFeatures.SelectedValue.ToString() == "Saved Recordings")

                {
                    check_savedRecordings.IsChecked = true;

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement1 = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("Click next button to access saved recordings");

                    // Send the stream to the media object.
                    mediaElement1.SetSource(stream1, stream1.ContentType);
                    mediaElement1.Play();

                }



            }

            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_notesSelect.IsChecked == true && check_notetxtcomplete.IsChecked == false && check_noteName.IsChecked == false)

            {
                check_notetxtcomplete.IsChecked = true;

                if (txt_paragraph.Text != "")
                {
                    txt_notesnew.Text = txt_paragraph.Text;

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("write a name to save your note.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                    txt_paragraph.Text = "";
                    txt_sentance.Text = "";
                    txt_wrd.Text = "";
                    txt_alp.Text = "";



                }

                else if (txt_sentance.Text != "")


                {
                    txt_notesnew.Text = txt_sentance.Text;

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("write a name to save your note.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                    txt_paragraph.Text = "";
                    txt_sentance.Text = "";
                    txt_wrd.Text = "";
                    txt_alp.Text = "";


                }

                else if (txt_sentance.Text == "" && txt_paragraph.Text == "")
                {


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement1 = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("your note is empty");

                    // Send the stream to the media object.
                    mediaElement1.SetSource(stream1, stream1.ContentType);
                    mediaElement1.Play();




                }




            }

            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_notesSelect.IsChecked == true && check_notetxtcomplete.IsChecked == true && check_noteName.IsChecked == false)

            {


                if (txt_wrd.Text != "")
                {

                    check_noteName.IsChecked = true;

                    StorageFile file = await Windows.Storage.KnownFolders.MusicLibrary.CreateFileAsync(txt_wrd.Text + ".txt", CreationCollisionOption.OpenIfExists);
                    await FileIO.WriteTextAsync(file, txt_notesnew.Text);

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement1 = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("your note is saved by the name " + txt_wrd.Text);

                    // Send the stream to the media object.
                    mediaElement1.SetSource(stream1, stream1.ContentType);
                    mediaElement1.Play();


                    if (ApplicationData.Current.LocalSettings.Values.ContainsKey("notesName"))
                    {
                        StorageFile file1 = await ApplicationData.Current.LocalFolder.GetFileAsync("NotesName.txt");
                        txt_number.Text = await FileIO.ReadTextAsync(file1);

                        txt_number.Text += "\n" + txt_wrd.Text;

                        StorageFile file2 = await ApplicationData.Current.LocalFolder.CreateFileAsync("NotesName.txt", CreationCollisionOption.ReplaceExisting);
                        await FileIO.WriteTextAsync(file2, txt_number.Text);
                        ApplicationData.Current.LocalSettings.Values["notesName"] = true;

                        list_notes.Items.Add(txt_wrd.Text);
                    }
                    else
                    {
                        StorageFile file2 = await ApplicationData.Current.LocalFolder.CreateFileAsync("NotesName.txt", CreationCollisionOption.OpenIfExists);
                        await FileIO.WriteTextAsync(file2, txt_wrd.Text);
                        ApplicationData.Current.LocalSettings.Values["notesName"] = true;
                        list_notes.Items.Add(txt_wrd.Text);
                    }
                }

                else if (txt_sentance.Text != "")
                {
                    check_noteName.IsChecked = true;

                    StorageFile file = await Windows.Storage.KnownFolders.MusicLibrary.CreateFileAsync(txt_sentance.Text, CreationCollisionOption.OpenIfExists);
                    await FileIO.WriteTextAsync(file, txt_notesnew.Text);


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement1 = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("your note is saved by the name " + txt_sentance.Text);

                    // Send the stream to the media object.
                    mediaElement1.SetSource(stream1, stream1.ContentType);
                    mediaElement1.Play();

                    if (ApplicationData.Current.LocalSettings.Values.ContainsKey("notesName"))
                    {
                        StorageFile file1 = await ApplicationData.Current.LocalFolder.GetFileAsync("NotesName.txt");
                        txt_number.Text = await FileIO.ReadTextAsync(file1);

                        txt_number.Text += "\n" + txt_sentance.Text;

                        StorageFile file2 = await ApplicationData.Current.LocalFolder.CreateFileAsync("NotesName.txt", CreationCollisionOption.ReplaceExisting);
                        await FileIO.WriteTextAsync(file2, txt_number.Text);
                        ApplicationData.Current.LocalSettings.Values["notesName"] = true;

                        list_notes.Items.Add(txt_sentance.Text);
                    }
                    else
                    {
                        StorageFile file2 = await ApplicationData.Current.LocalFolder.CreateFileAsync("NotesName.txt", CreationCollisionOption.OpenIfExists);
                        await FileIO.WriteTextAsync(file2, txt_sentance.Text);
                        ApplicationData.Current.LocalSettings.Values["notesName"] = true;
                        list_notes.Items.Add(txt_sentance.Text);
                    }

                }

                else
                {


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement1 = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("type a name for your note");

                    // Send the stream to the media object.
                    mediaElement1.SetSource(stream1, stream1.ContentType);
                    mediaElement1.Play();




                }
            }

            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_notesSelect.IsChecked == true && check_notetxtcomplete.IsChecked == true && check_noteName.IsChecked == true)

            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement1 = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("you can access your saved notes by next button");

                // Send the stream to the media object.
                mediaElement1.SetSource(stream1, stream1.ContentType);
                mediaElement1.Play();





                check_notesFeatures.IsChecked = false;
                check_notesSelect.IsChecked = false;
                list_notesFeatures.SelectedIndex = -1;
                check_noteName.IsChecked = false;
                check_notetxtcomplete.IsChecked = false;





            }

            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_savedNotes.IsChecked == true && check_savedNotesNameSelect.IsChecked == false)

            {



                try
                {

                    if (list_notes.SelectedValue.ToString() != "")
                    {

                        try
                        {

                            StorageFile file1 = await Windows.Storage.KnownFolders.MusicLibrary.GetFileAsync(list_notes.SelectedValue.ToString() + ".txt");
                            txt_paragraph.Text = await FileIO.ReadTextAsync(file1);

                            // The media object for controlling and playing audio.
                            MediaElement mediaElement1 = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("your note. " + txt_paragraph.Text);

                            // Send the stream to the media object.
                            mediaElement1.SetSource(stream1, stream1.ContentType);
                            mediaElement1.Play();


                            //   check_savedNotesNameSelect.IsChecked = true;
                        }

                        catch
                        {

                            txt1.Text = "error on loading note file";
                        }
                    }

                    else
                    {
                        // The media object for controlling and playing audio.
                        MediaElement mediaElement1 = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync(" ");

                        // Send the stream to the media object.
                        mediaElement1.SetSource(stream1, stream1.ContentType);
                        mediaElement1.Play();

                    }


                }

                catch
                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement1 = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("press next button to select saved notes");

                    // Send the stream to the media object.
                    mediaElement1.SetSource(stream1, stream1.ContentType);
                    mediaElement1.Play();

                }

            }



            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_rcdSelect.IsChecked == true && check_rcdStrt.IsChecked == false && check_rcdName.IsChecked == false)

            {



                // The media object for controlling and playing audio.
                MediaElement mediaElement1 = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("start recording");

                // Send the stream to the media object.
                mediaElement1.SetSource(stream1, stream1.ContentType);
                mediaElement1.Play();

                check_rcdName.IsChecked = true;
                check_writting.IsChecked = true;


                check_rcdStrt.IsChecked = true;


                var devices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Devices.Enumeration.DeviceClass.AudioCapture);



                captureInitSettings = new Windows.Media.Capture.MediaCaptureInitializationSettings();
                captureInitSettings.StreamingCaptureMode = Windows.Media.Capture.StreamingCaptureMode.AudioAndVideo;
                audioCapture = new Windows.Media.Capture.MediaCapture();
                await audioCapture.InitializeAsync(captureInitSettings);



                var storageFile = await Windows.Storage.KnownFolders.MusicLibrary.CreateFileAsync(txt_wrd.Text + ".mp3", Windows.Storage.CreationCollisionOption.GenerateUniqueName);

                MediaEncodingProfile profile = MediaEncodingProfile.CreateM4a(Windows.Media.MediaProperties.AudioEncodingQuality.Auto); await audioCapture.StartRecordToStorageFileAsync(profile, storageFile);

                txt1.Text = storageFile.Path;



            }

            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_rcdSelect.IsChecked == true && check_rcdStrt.IsChecked == true && check_rcdName.IsChecked == true)

            {

                await audioCapture.StopRecordAsync();


                // The media object for controlling and playing audio.
                MediaElement mediaElement12 = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("stopped and saved audio by the name " + txt_wrd.Text);

                // Send the stream to the media object.
                mediaElement12.SetSource(stream12, stream12.ContentType);
                mediaElement12.Play();

                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("SaveRecordings"))
                {


                    StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("SaveRecordingsName.txt");
                    txt_number.Text = await FileIO.ReadTextAsync(file);

                    txt_number.Text += "\n" + txt_wrd.Text;


                    StorageFile file1 = await ApplicationData.Current.LocalFolder.CreateFileAsync("SaveRecordingsName.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file1, txt_number.Text);

                    list_recordings.Items.Add(txt_wrd.Text);
                }

                else
                {
                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("SaveRecordingsName.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file, txt_wrd.Text);
                    ApplicationData.Current.LocalSettings.Values["SaveRecordings"] = true;

                    list_recordings.Items.Add(txt_wrd.Text);

                }





            }

          else  if(check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_savedRecordings.IsChecked == true)
            {
               try
                {

                    var audioFile = await KnownFolders.MusicLibrary.GetFileAsync(list_recordings.SelectedValue.ToString() + ".mp3");
                    var stream = await audioFile.OpenAsync(FileAccessMode.ReadWrite);
                    media_music.SetSource(stream, audioFile.ContentType);
                    
                    media_music.Play();


                }

                catch
                {

                }

            }




            else if (check_refresh.IsChecked == true && check_refresh_number.IsChecked == false && check_alarm.IsChecked == false && check_calculator.IsChecked == false && check_nctb.IsChecked == false && check_novels.IsChecked == false)


            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Enter");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


            }




           else   if(check_msg.IsChecked == true && check_email.IsChecked == false)



            {



               
                await audioCapture.StopRecordAsync();


                // The media object for controlling and playing audio.
                MediaElement mediaElement12 = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("stopped. Now enter the address or select from contact.");

                // Send the stream to the media object.
                mediaElement12.SetSource(stream12, stream12.ContentType);
                mediaElement12.Play();

                check_email.IsChecked = true;
                check_writting.IsChecked = true;

                //1666

                return;

            }


              else if(check_msg.IsChecked == true && check_email.IsChecked == true)
            {


                try
                {
                    if (txt_wrd.Text.Contains("@"))

                    {
                        try
                        {


                            // The media object for controlling and playing audio.
                            MediaElement mediaElement12 = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("sending");

                            // Send the stream to the media object.
                            mediaElement12.SetSource(stream12, stream12.ContentType);
                            mediaElement12.Play();



                            SmtpMail oMail = new SmtpMail("TryIt");
                            SmtpClient oSmtp = new SmtpClient();

                            // Set your gmail email address
                            oMail.From = new MailAddress("mears.soft@gmail.com");

                            // Add recipient email address, please change it to yours
                            oMail.To.Add(new MailAddress(txt_wrd.Text));

                            // Set email subject and email body text
                            oMail.Subject = "Voice message from your blind friend";
                            oMail.TextBody = "Your friend sent a voice message to you.";




                            Attachment oAttachment = await oMail.AddAttachmentAsync(AudioPath);



                            // Gmail SMTP server
                            SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                            // User and password for ESMTP authentication
                            oServer.User = "mears.soft@gmail.com";
                            oServer.Password = "Abanglalink1";

                            // Use 587 port
                            oServer.Port = 587;
                            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                            await oSmtp.SendMailAsync(oServer, oMail);



                            check_email.IsChecked = true;
                            check_writting.IsChecked = true;


                        }

                        finally

                        {

                            // The media object for controlling and playing audio.
                            MediaElement mediaElement12 = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("your message is sent to " + txt_wrd.Text);

                            // Send the stream to the media object.
                            mediaElement12.SetSource(stream12, stream12.ContentType);
                            mediaElement12.Play();

                            check_email.IsChecked = true;
                            check_writting.IsChecked = true;


                        }

                    }

                    else if (txt_wrd.Text == "" && list_emailaddress.SelectedValue.ToString() != "")
                    {

                        try
                        {


                            // The media object for controlling and playing audio.
                            MediaElement mediaElement12 = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("sending");

                            // Send the stream to the media object.
                            mediaElement12.SetSource(stream12, stream12.ContentType);
                            mediaElement12.Play();



                            SmtpMail oMail = new SmtpMail("TryIt");
                            SmtpClient oSmtp = new SmtpClient();

                            // Set your gmail email address
                            oMail.From = new MailAddress("mears.soft@gmail.com");

                            // Add recipient email address, please change it to yours
                            oMail.To.Add(new MailAddress(list_emailaddress.SelectedValue.ToString()));

                            // Set email subject and email body text
                            oMail.Subject = "Voice message from your blind friend";
                            oMail.TextBody = "Your friend sent a voice message to you.";




                            Attachment oAttachment = await oMail.AddAttachmentAsync(AudioPath);



                            // Gmail SMTP server
                            SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                            // User and password for ESMTP authentication
                            oServer.User = "mears.soft@gmail.com";
                            oServer.Password = "Abanglalink1";

                            // Use 587 port
                            oServer.Port = 587;
                            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                            await oSmtp.SendMailAsync(oServer, oMail);



                            check_email.IsChecked = true;
                            check_writting.IsChecked = true;


                        }

                        finally

                        {

                            // The media object for controlling and playing audio.
                            MediaElement mediaElement12 = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("your message is sent to " + list_emailaddress.SelectedValue.ToString());

                            // Send the stream to the media object.
                            mediaElement12.SetSource(stream12, stream12.ContentType);
                            mediaElement12.Play();

                            check_email.IsChecked = true;
                            check_writting.IsChecked = true;


                        }

                    }

                    else if (txt_wrd.Text == "" && list_emailaddress.SelectedValue.ToString() == "")
                    {
                        // The media object for controlling and playing audio.
                        MediaElement mediaElement12 = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("add new contact or press next button to access contact list");

                        // Send the stream to the media object.
                        mediaElement12.SetSource(stream12, stream12.ContentType);
                        mediaElement12.Play();

                        check_email.IsChecked = true;
                        check_writting.IsChecked = true;


                    }

                }

                catch

                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement12 = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("Error.");

                    // Send the stream to the media object.
                    mediaElement12.SetSource(stream12, stream12.ContentType);
                    mediaElement12.Play();

                    check_email.IsChecked = true;
                    check_writting.IsChecked = true;

                }





                return;

            }



              else if(check_contact.IsChecked == true && check_contactFeatures.IsChecked == false)

            {
                check_contactFeatures.IsChecked = true;

               
                    if(list_contactFeatures.SelectedValue.ToString() == "Add new contact")
                    {
                        check_writting.IsChecked = true;
                        check_email.IsChecked = true;
                        check_newContact.IsChecked = true;
                    }

                    else if(list_contactFeatures.SelectedValue.ToString() == "See existing")

                    {

                        check_seeExisting.IsChecked = true;

                    }
                

            }

            else if (check_contact.IsChecked == true && check_contactFeatures.IsChecked == true && check_newContact.IsChecked == true)

            {

               

                 


                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("text"))
                {
                    StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("MyText.txt");
                    txt_number.Text = await FileIO.ReadTextAsync(file);
                }



                    txt_number.Text += "\n" + txt_wrd.Text;

                    StorageFile file1 = await ApplicationData.Current.LocalFolder.CreateFileAsync("MyText.txt", CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file1, txt_number.Text);
                    ApplicationData.Current.LocalSettings.Values["text"] = true;


             
                    list_emailaddress.Items.Clear();


                    string root = ApplicationData.Current.LocalFolder.Path;
                    string path = root + @"\MyText.txt";

                txt1.Text = path;

                    var items = new List<string>();
                    using (var stream = File.OpenRead(path))  // open file
                    using (var reader = new StreamReader(stream))   // read the stream with TextReader
                    {
                        string line;

                        // read until no more lines are present
                        while ((line = reader.ReadLine()) != null)
                        {
                            list_emailaddress.Items.Add(line);
                        }
                    }


                
            }



           else if (check_novels.IsChecked == true && check_writerChoosed.IsChecked == false)

            {




                try
                {
                    check_writerChoosed.IsChecked = true;


                    ///4567

                    WriterName = list_novels.SelectedValue.ToString();

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(WriterName + " is choosed.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();


                    await Task.Delay(2500);

                    txt1.Text = "1";


                    Uri newuri = new Uri("ms-appx:///Assets/Novels/" + WriterName + "/" + txt1.Text + ".mp3");
                    media_novel.Source = newuri;

                    media_novel.Play();


                    if (check_voicecntrl.IsChecked == true)

                    {

                      


                        
                    }

                    else
                    {

                    }

                }

                catch

                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("The list is ended");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                }


            }

            else if (check_novels.IsChecked == true && check_writerChoosed.IsChecked == true && check_voicecntrl.IsChecked == true)

            {
                try
                {
                    check_novelChoosed.IsChecked = true;

                }

                catch
                {

                }

            }

            else if (check_nctb.IsChecked == true && check_Class.IsChecked == false && check_subject.IsChecked == false && check_topic.IsChecked == false)
            {
                //3344

                NctbClass = list_class.SelectedValue.ToString();

                check_Class.IsChecked = true;


                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(NctbClass + " is selected. Now select a subject by next button.");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                if (NctbClass == "Class 5")

                {



                    list_subject.Items.Add("Amar Bangla Boi");
                    list_subject.Items.Add("Prathomic Gonit");
                    list_subject.Items.Add("English For Today");
                    list_subject.Items.Add("Bangladesh And Bisho");
                    list_subject.Items.Add("Prathomic Biggan");
                    list_subject.Items.Add("Islam Religion");
                    list_subject.Items.Add("Hindu Religion");


                }

                else if (NctbClass == "Class 6")
                {

                    list_subject.Items.Add("Ananda Path");
                    list_subject.Items.Add("Bangla Grammer");
                    list_subject.Items.Add("English For Today");
                    list_subject.Items.Add("Krishi ShiKkha");
                    list_subject.Items.Add("Garostho Biggan");
                    list_subject.Items.Add("Sharerik Shikkha");
                    list_subject.Items.Add("Tottho Projokti");
                    list_subject.Items.Add("Kormo shikka");
                    list_subject.Items.Add("Biggan");
                    list_subject.Items.Add("Bangladesh And Bisho");
                    list_subject.Items.Add("Charupath");
                    list_subject.Items.Add("Vasha O Songkriti");
                    list_subject.Items.Add("Islam Religion");
                    list_subject.Items.Add("Hindu Religion");




                }

                else if (NctbClass == "Class 7")
                {
                    list_subject.Items.Add("Ananda Path");
                    list_subject.Items.Add("Bangla Grammer");
                    list_subject.Items.Add("English For Today");
                    list_subject.Items.Add("Krishi ShiKkha");
                    list_subject.Items.Add("Garostho Biggan");
                    list_subject.Items.Add("Sharerik Shikkha");
                    list_subject.Items.Add("Tottho Projokti");
                    list_subject.Items.Add("Kormo shikka");
                    list_subject.Items.Add("Biggan");
                    list_subject.Items.Add("Bangladesh And Bisho");
                    list_subject.Items.Add("Charupath");
                    list_subject.Items.Add("Vasha O Songkriti");
                    list_subject.Items.Add("Islam Religion");
                    list_subject.Items.Add("Hindu Religion");



                }

                else if (NctbClass == "Class 8")
                {
                    list_subject.Items.Add("Ananda Path");
                    list_subject.Items.Add("English for Today");
                    list_subject.Items.Add("Shahitto Konoka");
                    list_subject.Items.Add("Bangla Grammer");
                    list_subject.Items.Add("Gonit");
                    list_subject.Items.Add("Krishi Shikka");
                    list_subject.Items.Add("Garostha Biggan");
                    list_subject.Items.Add("Bangladesh O Bissho");
                    list_subject.Items.Add("Sharirek Shikka");
                    list_subject.Items.Add("Tottho O Jogajog");
                    list_subject.Items.Add("Kormo O Jibonmuki");
                    list_subject.Items.Add("Charu O Karukala");
                    list_subject.Items.Add("Islam Religion");
                    list_subject.Items.Add("Hindu Religion");



                }

                else if (NctbClass == "Class 9-10")
                {

                    list_subject.Items.Add("Bangla Sahitto");
                    list_subject.Items.Add("Bangla Grammer");
                    list_subject.Items.Add("Bangla Sohpath");
                    list_subject.Items.Add("English For Today");
                    list_subject.Items.Add("Compositions");
                    list_subject.Items.Add("Gonit");
                    list_subject.Items.Add("Art and Crafts");
                    list_subject.Items.Add("ICT");
                    list_subject.Items.Add("Career Education");
                    list_subject.Items.Add("Islam Religion");
                    list_subject.Items.Add("Hindu Religion");
                    list_subject.Items.Add("Bangladesh And Bisho");
                    list_subject.Items.Add("Physics");
                    list_subject.Items.Add("Chemistry");
                    list_subject.Items.Add("Biology");
                    list_subject.Items.Add("Higher Mthematics");
                    list_subject.Items.Add("Economics");
                    list_subject.Items.Add("Agriculture studies");
                    list_subject.Items.Add("Home Science");
                    list_subject.Items.Add("Bebshay Uddag");
                    list_subject.Items.Add("Computer Study");
                    list_subject.Items.Add("Civics and Citizenship");
                    list_subject.Items.Add("Accounting");
                    list_subject.Items.Add("Finance and Banking");
                    list_subject.Items.Add("History of Bangladesh and World");
                    list_subject.Items.Add("Geography and Environment");



                }



            }
            else if (check_nctb.IsChecked == true && check_Class.IsChecked == true && check_subject.IsChecked == false && check_topic.IsChecked == false)
            {
                try
                {
                    check_subject.IsChecked = true;

                    NctbSubject = list_subject.SelectedValue.ToString();

                    string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                    string path = root + @"\Assets\NCTB\" + NctbClass + @"\" + NctbSubject;

                    DirectoryInfo dirinfo = new DirectoryInfo(path);
                    FileInfo[] files = dirinfo.GetFiles("*.mp3");

                    foreach (FileInfo file in files)
                    {
                        list_topic.Items.Add(file.Name);
                    }


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(NctbSubject + " is selected. Now select a Topic by next button.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }


                catch
                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(NctbClass + " "+NctbSubject + " audio book is currently unavailable in demo version");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }

            }


            else if (check_nctb.IsChecked == true && check_Class.IsChecked == true && check_subject.IsChecked == true && check_topic.IsChecked == false)
            {

                check_topic.IsChecked = true;
                txt1.Text = list_topic.SelectedValue.ToString();
                txt1.Text = txt1.Text.Replace(".mp3", "");

                NctbTopic = txt1.Text;


                txt1.Text = "1";


                Uri newuri = new Uri("ms-appx:///Assets/NCTB/" + NctbClass + "/" + NctbSubject + "/" + NctbTopic + "/" + txt1.Text + ".mp3");
                media_music.Source = newuri;

                media_music.Play();





            }



            else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
            {

                check_alrmHour.IsChecked = true;

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Now select minutes.");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


            }

            else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

            {

                check_alrmMinute.IsChecked = true;



                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Your alarm is set for " + AlarmHours + " hours and " + ALarmMinutes + " minutes.");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


                int y = Convert.ToInt32(AlarmHours);
                int x = Convert.ToInt32(ALarmMinutes);


                timer_alarm = new DispatcherTimer();
                timer_alarm.Tick += timer_alarm_Tick;
                timer_alarm.Interval = new TimeSpan(y, x, 1);


                timer_alarm.Start();



            }

            else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_calcEquation.IsChecked == false && check_variable2.IsChecked == false)
            {
                ///01718

                

                variable1 = Convert.ToDouble(txt_v1.Text);
                variable2 = 0;

                check_variable1.IsChecked = true;


                /// The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(variable1 + " is typed. Now select operation");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


            }

            else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_calcEquation.IsChecked == false && check_variable2.IsChecked == false)
            {



                check_calcEquation.IsChecked = true;


                if (list_calcEquation.SelectedValue.ToString() == "plus")
                {

                    Operation = "+";


                    /// The media object for controlling and playing audio.
                    MediaElement mediaElement1 = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("Plus operation choosed. Now enter another variable.");

                    // Send the stream to the media object.
                    mediaElement1.SetSource(stream1, stream1.ContentType);
                    mediaElement1.Play();



                }

                else if (list_calcEquation.SelectedValue.ToString() == "minus")

                {
                    Operation = "-";


                    /// The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Minus operation choosed. Now enter another variable.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();


                }

                else if (list_calcEquation.SelectedValue.ToString() == "multiply")

                {

                    Operation = "*";


                    /// The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Multiply operation choosed. Now enter another variable.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                }

                else if (list_calcEquation.SelectedValue.ToString() == "division")

                {
                    Operation = "/";


                    /// The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Division operation choosed. Now enter another variable.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();


                }


            }

            else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_calcEquation.IsChecked == true && check_variable2.IsChecked == true)

            {

                txt_v1.Text = txt_calAnswer.Text;
                txt_v2.Text = "";
                txt_calAnswer.Text = "";


                variable1 = Convert.ToDouble(txt_v1.Text);
                variable2 = 0;



                check_calcEquation.IsChecked = false;
                check_variable2.IsChecked = false;

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(variable1 + " is entered. Now select operation.");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


            }


            else if (check_song.IsChecked == true && check_songCategory.IsChecked == false)

            {

                SongCategory = list_songCategory.SelectedValue.ToString();

                check_songCategory.IsChecked = true;

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(SongCategory + " is selected. Now enjoy music.");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }


            else if (check_refresh.IsChecked == true && check_menu.IsChecked == true && check_ocr.IsChecked == true)

            {
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();



                ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_ocr);

                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();



            }


             if (check_refresh.IsChecked == true && check_ExamMode.IsChecked == false && check_notesRecordings.IsChecked == false && check_menu.IsChecked == true && check_quran.IsChecked == false && check_contact.IsChecked == false &&check_alarm.IsChecked == false && check_ocr.IsChecked == false && check_nctb.IsChecked == false && check_novels.IsChecked == false && check_calculator.IsChecked == false)

            {
                try
                {
                    menuitem = list_menuitem.SelectedValue.ToString();
                }


                catch
                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("First Choose a menuitem by next and previous button.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }

                try
                {


                    if (menuitem == "1 Todays Time")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_time);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }

                    else if (menuitem == "16 Exam Mode")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_ExamMode);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }


                    else if (menuitem == "15 Notes and Recordings")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_notes);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();


                    }

                    else if (menuitem == "14 Contact list")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_contact);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();


                    }



                    else if (menuitem == "2 Todays Date")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_date);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();


                    }
                    else if (menuitem == "6 Calculator")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_calculator);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }
                    else if (menuitem == "7 Alarm Clock")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_alarm);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }
                    else if (menuitem == "8 Novels")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_novels);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }
                    else if (menuitem == "9 Nctb Books")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_nctb);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }
                    else if (menuitem == "12 Songs")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_song);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }

                    else if (menuitem == "13 The Holy Quran")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_Quran);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }

                    else if (menuitem == "10 Live Reader")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_ocr);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }
                    else if (menuitem == "3 Todays Weather")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_weather);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }

                    else if (menuitem == "4 Todays News Update")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_News);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }

                    else if (menuitem == "5 Your current Location")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_location);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }
                    else if (menuitem == "11 Send and receive messages")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_msg);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }


                }

                catch

                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Something is wrong. Click refresh button and try again.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }


            }


            ////////WARNING////////
            ///SOB ENTER CODE ETAR UPORE HOBE. NAILE DOUBLE ENTER PRESS HOAR CODE ENTRY HOY AND VEJAL HOY.



            if (check_quran.IsChecked == true )

            {



                if (media_music.CurrentState.ToString() == "Playing")

                {
                    check_quran_number.IsChecked = false;
                    SurahNumber = null;

                    media_music.Stop();


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Enter Surah number and press enter.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();


                }
                else if( check_quran_number.IsChecked == true)

                {

                    try
                    {

                        check_quran_number.IsChecked = false;

                        txt1.Text = SurahNumber;

                        txt1.Text = Convert.ToString(Convert.ToDouble(txt1.Text) - Convert.ToDouble("1"));

                        int serial = Convert.ToInt32(txt1.Text);

                        list_quran.SelectedIndex = serial;

                        Uri newuri = new Uri(QuranLocation + list_quran.SelectedValue.ToString());
                        media_music.Source = newuri;

                        media_music.Play();


                    }

                    catch
                    {
                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Something is wrong. Click refresh button and try again.");

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();

                    }


                }


            }





            if (check_refresh.IsChecked == true && check_refresh_number.IsChecked == true && check_menu.IsChecked == false)
            {


                check_menu.IsChecked = true;



                try
                {


                    if (MenuNumber == "1")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_time);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }

                    else if (MenuNumber == "16")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_ExamMode);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }



                    else if (MenuNumber == "14")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_contact);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();


                    }



                    else if (MenuNumber == "15")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_notes);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();


                    }


                    else if (MenuNumber == "2")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_date);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();


                    }
                    else if (MenuNumber == "6")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_calculator);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }
                    else if (MenuNumber == "7")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_alarm);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();


                    }
                    else if (MenuNumber == "8")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_novels);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }
                    else if (MenuNumber == "9")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_nctb);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }
                    else if (MenuNumber == "12")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_song);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }
                    else if (MenuNumber == "10")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_ocr);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }
                    else if (MenuNumber == "3")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_weather);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }

                    else if (MenuNumber == "4")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_News);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }

                    else if (MenuNumber == "5")

                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_location);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                    }
                    else if (MenuNumber == "11")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_msg);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }
                    else if (MenuNumber == "13")

                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_Quran);

                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();
                    }

                }

                catch

                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Something is wrong. Click refresh button and try again.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }


            }





           



        }


        private async void btn_back_Click(object sender, RoutedEventArgs e)
        {




            if (check_ExamMode.IsChecked == true && check_TchrEmail.IsChecked == true)

            {
                try
                {
                    list_examMode.SelectedIndex = list_examMode.SelectedIndex + 1;
                }

                catch
                {


                    list_examMode.SelectedIndex = 0;

                }

            }



            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == false)

            {
                try
                {
                    list_notesFeatures.SelectedIndex = list_notesFeatures.SelectedIndex - 1;
                }

                catch
                {
                    try
                    {
                        list_notesFeatures.SelectedIndex = 0;

                    }

                    catch
                    {


                    }
                }


            }



            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_savedNotes.IsChecked == true)

            {
                try
                {
                    list_notes.SelectedIndex = list_notes.SelectedIndex - 1;
                }

                catch
                {
                    try
                    {
                        list_notes.SelectedIndex = 0;
                    }
                    catch
                    {

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("No notes is found");

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();
                    }
                }

                // break;

            }


            else if (check_notesRecordings.IsChecked == true && check_notesFeatures.IsChecked == true && check_savedRecordings.IsChecked == true)

            {
                try
                {
                    list_recordings.SelectedIndex = list_recordings.SelectedIndex - 1;
                }

                catch
                {
                    try
                    {
                        list_recordings.SelectedIndex = 0;
                    }
                    catch
                    {
                        // The media object for controlling and playing audio.
                        MediaElement mediaElement = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("No recording is found");

                        // Send the stream to the media object.
                        mediaElement.SetSource(stream, stream.ContentType);
                        mediaElement.Play();
                    }
                }



            }




            else if (check_email.IsChecked == true && check_msg.IsChecked == true)

            {
                try
                {
                    list_emailaddress.SelectedIndex = list_emailaddress.SelectedIndex - 1;
                }

                catch
                {
                    try
                    {
                        list_emailaddress.SelectedIndex = 0;


                    }

                    catch
                    {

                    }

                }

            }



            else if (check_contact.IsChecked == true && check_contactFeatures.IsChecked == false)

            {

                try
                {

                    list_contactFeatures.SelectedIndex = list_contactFeatures.SelectedIndex - 1;
                }

                catch
                {

                }
            }

            else if (check_contact.IsChecked == true && check_contactFeatures.IsChecked == true && check_seeExisting.IsChecked == true)

            {

                try
                {
                    list_emailaddress.SelectedIndex = list_emailaddress.SelectedIndex - 1;
                }

                catch

                {
                    try
                    {
                        list_emailaddress.SelectedIndex = 0;
                    }
                    catch
                    {


                    }

                }




            }


            else if (check_novels.IsChecked == true && check_writerChoosed.IsChecked == false)
            {

                try
                {
                    list_novels.SelectedIndex = list_novels.SelectedIndex - 1;

                }

                catch
                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }

            }

            else if (check_novels.IsChecked == true && check_writerChoosed.IsChecked == true)
            {
                try
                {
                    txt1.Text = Convert.ToString(Convert.ToDouble(txt1.Text) - Convert.ToDouble("1"));


                    Uri newuri = new Uri("ms-appx:///Assets/Novels/" + WriterName + "/" + txt1.Text + ".mp3");
                    media_novel.Source = newuri;

                    media_novel.Play();

                }

                catch
                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }

            }


            else if (check_quran.IsChecked == true)

            {


                try
                {

                    list_quran.SelectedIndex = list_quran.SelectedIndex + 1;


                    Uri newuri = new Uri(QuranLocation + list_quran.SelectedValue.ToString());
                    media_music.Source = newuri;

                    media_music.Play();

                }

                catch

                {
                    list_subject.SelectedIndex = 0;

                    try
                    {



                        Uri newuri = new Uri(QuranLocation + list_quran.SelectedValue.ToString());
                        media_music.Source = newuri;

                        media_music.Play();

                    }

                    catch

                    {




                    }


                }


            }



            else if (check_nctb.IsChecked == true && check_Class.IsChecked == false && check_subject.IsChecked == false && check_topic.IsChecked == false)
            {
                try
                {
                    list_class.SelectedIndex = list_class.SelectedIndex - 1;
                }

                catch
                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }


            }
            else if (check_nctb.IsChecked == true && check_Class.IsChecked == true && check_subject.IsChecked == false && check_topic.IsChecked == false)
            {
                try

                {
                    list_subject.SelectedIndex = list_subject.SelectedIndex - 1;
                }

                catch
                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }


            }
            else if (check_nctb.IsChecked == true && check_Class.IsChecked == true && check_subject.IsChecked == true && check_topic.IsChecked == false)
            {
                try
                {

                    list_topic.SelectedIndex = list_topic.SelectedIndex - 1;
                }

                catch
                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }




            }
            else if (check_nctb.IsChecked == true && check_Class.IsChecked == true && check_subject.IsChecked == true && check_topic.IsChecked == true)
            {
                try

                {

                    txt1.Text = Convert.ToString(Convert.ToDouble(txt1.Text) - Convert.ToDouble("1"));



                    Uri newuri = new Uri("ms-appx:///Assets/NCTB/" + NctbClass + "/" + NctbSubject + "/" + NctbTopic + "/" + txt1.Text + ".mp3");
                    media_music.Source = newuri;

                    media_music.Play();

                }


                catch
                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }



            }

            else if (check_refresh.IsChecked == true && check_menu.IsChecked == true && check_ocr.IsChecked == false && check_song.IsChecked == false && check_alarm.IsChecked == false && check_nctb.IsChecked == false && check_novels.IsChecked == false && check_calculator.IsChecked == false)

            {

                try
                {
                    list_menuitem.SelectedIndex = list_menuitem.SelectedIndex - 1;
                }

                catch

                {
                    list_menuitem.SelectedIndex = 0;


                }




            }


            else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_calcEquation.IsChecked == false && check_variable2.IsChecked == false)
            {
                try
                {
                    list_calcEquation.SelectedIndex = list_calcEquation.SelectedIndex - 1;
                }

                catch

                {
                    list_calcEquation.SelectedIndex = 0;


                }

            }


            else if (check_song.IsChecked == true && check_songCategory.IsChecked == false && SongCategory == null)

            {

                try
                {
                    list_songCategory.SelectedIndex = list_songCategory.SelectedIndex - 1;
                }

                catch

                {
                    list_songCategory.SelectedIndex = 0;


                }
            }

            else if (check_song.IsChecked == true && check_songCategory.IsChecked == true && SongCategory != null)

            {
                if (SongCategory == "Bangla Song")
                {


                    try
                    {
                        list_songBangla.SelectedIndex = list_songBangla.SelectedIndex - 1;
                    }

                    catch

                    {
                        list_songBangla.SelectedIndex = 0;


                    }


                }

                else if (SongCategory == "Hindi Song")
                {


                    try
                    {
                        list_songHindi.SelectedIndex = list_songHindi.SelectedIndex - 1;
                    }

                    catch

                    {
                        list_songHindi.SelectedIndex = 0;


                    }


                }

                else if (SongCategory == "English Song")
                {


                    try
                    {
                        list_songEnglish.SelectedIndex = list_songEnglish.SelectedIndex - 1;
                    }

                    catch

                    {
                        list_songEnglish.SelectedIndex = 0;


                    }


                }

                else if (SongCategory == "Top rated Song")
                {


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("This feather is Under development. Click refresh and menu button to access other feathers.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }

            }



        }

        

        private void btn_parsentage_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btn_dot_Click(object sender, RoutedEventArgs e)
        {


            if (check_writting.IsChecked == true)

            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_wrd.Text);

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                txt_sentance.Text += " " + txt_wrd.Text;

                txt_wrd.Text = "";
                txt_alp.Text = "";


            }

            else

            {



                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("dot");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


                if (check_menu.IsChecked == false)
                {

                    Restart += "1";

                    if (Restart == "11111")

                    {

                        // The media object for controlling and playing audio.
                        MediaElement mediaElement1 = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("Restarting");

                        // Send the stream to the media object.
                        mediaElement1.SetSource(stream1, stream1.ContentType);
                        mediaElement1.Play();


                        AppRestartFailureReason result = await CoreApplication.RequestRestartAsync("Restart");



                    }




                }


                if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                {

                    txt_v1.Text += ".";

                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                {

                    txt_v2.Text += ".";

                }

            }

        }

        private async void btn_0_Click(object sender, RoutedEventArgs e)
        {


            if (check_writting.IsChecked == true && check_numberinput.IsChecked == false)

            {

                try
                {

                    if (txt_wrd.Text != "")
                    {
                        txt_wrd.Text = txt_wrd.Text.Substring(0, txt_wrd.Text.Length - 1);



                        // The media object for controlling and playing audio.
                        MediaElement mediaElement1 = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("undo " + txt_wrd.Text) ;

                        // Send the stream to the media object.
                        mediaElement1.SetSource(stream1, stream1.ContentType);
                        mediaElement1.Play();



                    }

                    else if (txt_wrd.Text == "" && txt_sentance.Text != "")
                    {

                        txt_sentance.Text = txt_sentance.Text.Substring(0, txt_sentance.Text.Length - 1);



                        // The media object for controlling and playing audio.
                        MediaElement mediaElement1 = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("undo " + txt_sentance.Text);

                        // Send the stream to the media object.
                        mediaElement1.SetSource(stream1, stream1.ContentType);
                        mediaElement1.Play();

                    }

                    else if (txt_wrd.Text == "" && txt_sentance.Text == "")
                    {

                        txt_paragraph.Text = txt_paragraph.Text.Substring(0, txt_paragraph.Text.Length - 1);



                        // The media object for controlling and playing audio.
                        MediaElement mediaElement1 = this.media_speech;

                        // The object for controlling the speech synthesis engine (voice).
                        var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                        // Generate the audio stream from plain text.
                        SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("undo " + txt_paragraph.Text );

                        // Send the stream to the media object.
                        mediaElement1.SetSource(stream1, stream1.ContentType);
                        mediaElement1.Play();


                    }
                }

                catch
                {


                }
                

            }


            else if (check_writting.IsChecked == true && check_numberinput.IsChecked == true)

            {
                txt_wrd.Text += "0";

                // The media object for controlling and playing audio.
                MediaElement mediaElement1 = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("0");

                // Send the stream to the media object.
                mediaElement1.SetSource(stream1, stream1.ContentType);
                mediaElement1.Play();
            }


            else

            {



                if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                {

                    MenuNumber += "0";
                    check_refresh_number.IsChecked = true;
                }

                else

                {


                }

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("0");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                if (check_quran.IsChecked == true)

                {
                    check_quran_number.IsChecked = true;

                    SurahNumber += "0";


                }



                if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
                {

                    AlarmHours += "0";
                }

                else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

                {
                    ALarmMinutes += "0";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                {
                    ExamHour += "0";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                {
                    ExamMinute += "0";


                }
                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                {

                    txt_v1.Text += "0";

                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                {

                    txt_v2.Text += "0";

                }

            }

        }

        private async Task<bool> RecordProcess()
        {
            if (buffer != null)
            {
                buffer.Dispose();
            }
            buffer = new InMemoryRandomAccessStream();
            if (capture != null)
            {
                capture.Dispose();
            }
            try
            {
                MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings
                {
                    StreamingCaptureMode = StreamingCaptureMode.Audio
                };
                capture = new MediaCapture();
                await capture.InitializeAsync(settings);
                capture.RecordLimitationExceeded += (MediaCapture sender) =>
                {
                    //Stop
                    //   await capture.StopRecordAsync();
                    record = false;
                    throw new Exception("Record Limitation Exceeded ");
                };
                capture.Failed += (MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs) =>
                {
                    record = false;
                    throw new Exception(string.Format("Code: {0}. {1}", errorEventArgs.Code, errorEventArgs.Message));
                };
            }
            catch
            {


            }

            return true;
        }

        private async Task InitMediaCapture()
        {
            audioCapture = null;
            audioCapture = new Windows.Media.Capture.MediaCapture();

            // for dispose purpose
            (App.Current as App).MediaCapture = audioCapture;
            await audioCapture.InitializeAsync(captureInitSettings);

        }

        private async void btn_9_Click(object sender, RoutedEventArgs e)
        {



            if (check_writting.IsChecked == true)

            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("9");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "9";
                }


                else
                {

                    if (check_writting.IsChecked == true)

                    {

                        if (tappedkey != "9")

                        {

                            txt_wrd.Text += txt_alp.Text;




                            txt_alp.Text = "w";

                            txt_tap.Text = "";
                            txt_tap.Text = "tapped";



                            tappedkey = "9";




                            // The media object for controlling and playing audio.
                            MediaElement mediaElement = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("w");

                            // Send the stream to the media object.
                            mediaElement.SetSource(stream, stream.ContentType);
                            mediaElement.Play();


                        }





                        else if (txt_tap.Text == "tapped" && tappedkey == "9")


                        {

                            if (txt_alp.Text == "")

                            {

                                txt_alp.Text = "w";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("w");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == "w")

                            {
                                txt_alp.Text = "x";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("x");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "x")

                            {
                                txt_alp.Text = "y";


                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("y");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "y")

                            {
                                txt_alp.Text = "z";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("z");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();
                            }


                            else if (txt_alp.Text == "z")

                            {
                                txt_alp.Text = "w";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("w");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }



                        }




                    }


                }

            }



            else

            {


                if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                {

                    MenuNumber += "9";
                    check_refresh_number.IsChecked = true;
                }

                else

                {


                }

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("9");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


                if (check_quran.IsChecked == true)

                {

                    check_quran_number.IsChecked = true;
                    SurahNumber += "9";


                }



                if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
                {

                    AlarmHours += "9";
                }

                else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

                {
                    ALarmMinutes += "9";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                {
                    ExamHour += "9";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                {
                    ExamMinute += "9";


                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                {

                    txt_v1.Text += "9";

                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                {

                    txt_v2.Text += "9";

                }

                else if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                {
                    EmergencyAlart += 9;

                    if (EmergencyAlart == "999")
                    {


                        try
                        {


                            try
                            {
                                // The media object for controlling and playing audio.
                                MediaElement mediaElement1 = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("recording start");

                                // Send the stream to the media object.
                                mediaElement1.SetSource(stream1, stream1.ContentType);
                                mediaElement1.Play();

                                await Task.Delay(1000);


                                var devices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(Windows.Devices.Enumeration.DeviceClass.AudioCapture);



                                captureInitSettings = new Windows.Media.Capture.MediaCaptureInitializationSettings();
                                captureInitSettings.StreamingCaptureMode = Windows.Media.Capture.StreamingCaptureMode.AudioAndVideo;
                                audioCapture = new Windows.Media.Capture.MediaCapture();
                                await audioCapture.InitializeAsync(captureInitSettings);




                                var storageFile = await Windows.Storage.KnownFolders.VideosLibrary.CreateFileAsync("audioOut.mp3", Windows.Storage.CreationCollisionOption.GenerateUniqueName);

                                MediaEncodingProfile profile = MediaEncodingProfile.CreateM4a(Windows.Media.MediaProperties.AudioEncodingQuality.Auto); await audioCapture.StartRecordToStorageFileAsync(profile, storageFile);


                                await Task.Delay(10000);


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement12 = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth12 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream12 = await synth12.SynthesizeTextToStreamAsync("stopped");

                                // Send the stream to the media object.
                                mediaElement12.SetSource(stream12, stream12.ContentType);
                                mediaElement12.Play();

                                await audioCapture.StopRecordAsync();

                                AudioPath = storageFile.Path;


                            }

                            finally

                            {


                                SmtpMail oMail = new SmtpMail("TryIt");
                                SmtpClient oSmtp = new SmtpClient();

                                // Set your gmail email address
                                oMail.From = new MailAddress("mears.soft@gmail.com");

                                // Add recipient email address, please change it to yours
                                oMail.To.Add(new MailAddress("mailbox.toonooy@gmail.com"));

                                // Set email subject and email body text
                                oMail.Subject = "EMERGENCY!!!!!";
                                oMail.TextBody = "ID " + txt_ID.Text + " is in emergency. See his/her device recorded audio.";


                                // Windows.Storage.StorageFile file = await Windows.Storage.KnownFolders.PicturesLibrary.GetFileAsync("PreviewFrame.jpg");

                                // string attfile = file.Path;



                                Attachment oAttachment = await oMail.AddAttachmentAsync(AudioPath);



                                // Gmail SMTP server
                                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                                // User and password for ESMTP authentication
                                oServer.User = "mears.soft@gmail.com";
                                oServer.Password = "Abanglalink1";

                                // Use 587 port
                                oServer.Port = 587;
                                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                                await oSmtp.SendMailAsync(oServer, oMail);




                                // The media object for controlling and playing audio.
                                MediaElement mediaElement1 = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("Device recorded audio sent to Control Centre.");

                                // Send the stream to the media object.
                                mediaElement1.SetSource(stream1, stream1.ContentType);
                                mediaElement1.Play();


                            }
                        }

                        catch

                        {
                            // The media object for controlling and playing audio.
                            MediaElement mediaElement1 = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("Device recorded audio cannot not sent to control centre.");

                            // Send the stream to the media object.
                            mediaElement1.SetSource(stream1, stream1.ContentType);
                            mediaElement1.Play();

                        }




                        try
                        {



                            SmtpMail oMail = new SmtpMail("TryIt");
                            SmtpClient oSmtp = new SmtpClient();

                            // Set your gmail email address
                            oMail.From = new MailAddress("mears.soft@gmail.com");

                            // Add recipient email address, please change it to yours
                            oMail.To.Add(new MailAddress("mailbox.toonooy@gmail.com"));

                            // Set email subject and email body text
                            oMail.Subject = "EMERGENCY!!!!!";
                            oMail.TextBody = "ID " + txt_ID.Text + " is in emergency. His/her location is (" + txt_lat.Text + ", " + txt_lon.Text + ")";







                            // Gmail SMTP server
                            SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                            // User and password for ESMTP authentication
                            oServer.User = "mears.soft@gmail.com";
                            oServer.Password = "Abanglalink1";

                            // Use 587 port
                            oServer.Port = 587;
                            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                            await oSmtp.SendMailAsync(oServer, oMail);


                            // The media object for controlling and playing audio.
                            MediaElement mediaElement1 = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("Your location sent to control centre.");

                            // Send the stream to the media object.
                            mediaElement1.SetSource(stream1, stream1.ContentType);
                            mediaElement1.Play();


                        }

                        catch
                        {
                            // The media object for controlling and playing audio.
                            MediaElement mediaElement1 = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("Something went wrong. Your location can't sent to control centre.");

                            // Send the stream to the media object.
                            mediaElement1.SetSource(stream1, stream1.ContentType);
                            mediaElement1.Play();





                        }









                        try
                        {

                            _mediaCapture = new MediaCapture();
                            var cameraDevice = await FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel.Back);
                            var settings = new MediaCaptureInitializationSettings { VideoDeviceId = cameraDevice.Id };
                            await _mediaCapture.InitializeAsync(settings);
                            _displayRequest.RequestActive();
                            PreviewControl.Source = _mediaCapture;
                            await _mediaCapture.StartPreviewAsync();

                            var picturesLibrary = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
                            _captureFolder = picturesLibrary.SaveFolder ?? ApplicationData.Current.LocalFolder;

                            await Task.Delay(500);
                            var storeFile = await _captureFolder.CreateFileAsync("PreviewFrame.jpg", CreationCollisionOption.GenerateUniqueName);
                            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();
                            await _mediaCapture.CapturePhotoToStorageFileAsync(imgFormat, storeFile);
                            await _mediaCapture.StopPreviewAsync();

                            ImagePath = storeFile.Path;

                        }

                        finally

                        {


                            try
                            {





                                SmtpMail oMail = new SmtpMail("TryIt");
                                SmtpClient oSmtp = new SmtpClient();

                                // Set your gmail email address
                                oMail.From = new MailAddress("mears.soft@gmail.com");

                                // Add recipient email address, please change it to yours
                                oMail.To.Add(new MailAddress("mailbox.toonooy@gmail.com"));

                                // Set email subject and email body text
                                oMail.Subject = "EMERGENCY!!!!!";
                                oMail.TextBody = "ID " + txt_ID.Text + " is in emergency. See his/her device captured image.";


                                // Windows.Storage.StorageFile file = await Windows.Storage.KnownFolders.PicturesLibrary.GetFileAsync("PreviewFrame.jpg");

                                // string attfile = file.Path;



                                Attachment oAttachment = await oMail.AddAttachmentAsync(ImagePath);



                                // Gmail SMTP server
                                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                                // User and password for ESMTP authentication
                                oServer.User = "mears.soft@gmail.com";
                                oServer.Password = "Abanglalink1";

                                // Use 587 port
                                oServer.Port = 587;
                                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                                await oSmtp.SendMailAsync(oServer, oMail);




                                // The media object for controlling and playing audio.
                                MediaElement mediaElement1 = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("Device captured image sent to Control Centre.");

                                // Send the stream to the media object.
                                mediaElement1.SetSource(stream1, stream1.ContentType);
                                mediaElement1.Play();


                            }
                            catch
                            {
                                // The media object for controlling and playing audio.
                                MediaElement mediaElement1 = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth1 = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream1 = await synth1.SynthesizeTextToStreamAsync("Device captured image can't sent to control centre.");

                                // Send the stream to the media object.
                                mediaElement1.SetSource(stream1, stream1.ContentType);
                                mediaElement1.Play();
                            }


                        }




                    }



                }




            }
        }

    
        private async void btn_8_Click(object sender, RoutedEventArgs e)
        {


            if (check_writting.IsChecked == true)

            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("8");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "8";
                }


                else
                {

                    if (check_writting.IsChecked == true)

                    {

                        if (tappedkey != "8")

                        {

                            txt_wrd.Text += txt_alp.Text;




                            txt_alp.Text = "t";

                            txt_tap.Text = "";
                            txt_tap.Text = "tapped";



                            tappedkey = "8";




                            // The media object for controlling and playing audio.
                            MediaElement mediaElement = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("t");

                            // Send the stream to the media object.
                            mediaElement.SetSource(stream, stream.ContentType);
                            mediaElement.Play();


                        }





                        else if (txt_tap.Text == "tapped" && tappedkey == "8")


                        {

                            if (txt_alp.Text == "")

                            {

                                txt_alp.Text = "t";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("t");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == "t")

                            {
                                txt_alp.Text = "u";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("u");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "u")

                            {
                                txt_alp.Text = "v";


                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("v");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "v")

                            {
                                txt_alp.Text = "t";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("t");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();
                            }




                        }




                    }


                }

            }



            else

            {



                if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                {

                    MenuNumber += "8";
                    check_refresh_number.IsChecked = true;
                }

                else

                {


                }


                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("8");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                if (check_quran.IsChecked == true)

                {

                    check_quran_number.IsChecked = true;
                    SurahNumber += "8";


                }


                if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
                {

                    AlarmHours += "8";
                }

                else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

                {
                    ALarmMinutes += "8";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                {
                    ExamHour += "8";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                {
                    ExamMinute += "8";


                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                {

                    txt_v1.Text += "8";

                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                {

                    txt_v2.Text += "8";

                }


            }
        }

        private async void btn_7_Click(object sender, RoutedEventArgs e)
        {



            if (check_writting.IsChecked == true)

            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("7");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "7";
                }


                else
                {

                    if (check_writting.IsChecked == true)

                    {

                        if (tappedkey != "7")

                        {

                            txt_wrd.Text += txt_alp.Text;




                            txt_alp.Text = "p";

                            txt_tap.Text = "";
                            txt_tap.Text = "tapped";



                            tappedkey = "7";




                            // The media object for controlling and playing audio.
                            MediaElement mediaElement = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("p");

                            // Send the stream to the media object.
                            mediaElement.SetSource(stream, stream.ContentType);
                            mediaElement.Play();


                        }





                        else if (txt_tap.Text == "tapped" && tappedkey == "7")


                        {

                            if (txt_alp.Text == "")

                            {

                                txt_alp.Text = "p";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("p");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == "p")

                            {
                                txt_alp.Text = "q";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("q");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "q")

                            {
                                txt_alp.Text = "r";


                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("r");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "r")

                            {
                                txt_alp.Text = "s";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("s");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();
                            }


                            else if (txt_alp.Text == "s")

                            {
                                txt_alp.Text = "p";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("p");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }



                        }




                    }


                }

            }



            else

            {


                if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                {

                    MenuNumber += "7";
                    check_refresh_number.IsChecked = true;
                }

                else

                {


                }

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("7");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                if (check_quran.IsChecked == true)

                {
                    check_quran_number.IsChecked = true;

                    SurahNumber += "7";


                }


                if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
                {

                    AlarmHours += "7";
                }

                else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

                {
                    ALarmMinutes += "7";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                {
                    ExamHour += "7";


                }

                else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                {
                    ExamMinute += "7";


                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                {

                    txt_v1.Text += "7";

                }

                else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                {

                    txt_v2.Text += "7";

                }


            }

        }

        private async void btn_6_Click(object sender, RoutedEventArgs e)
        {

            if (check_writting.IsChecked == true)

            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("6");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "6";
                }


                else
                {

                    if (check_writting.IsChecked == true)

                    {

                        if (tappedkey != "6")

                        {

                            txt_wrd.Text += txt_alp.Text;




                            txt_alp.Text = "m";

                            txt_tap.Text = "";
                            txt_tap.Text = "tapped";



                            tappedkey = "6";




                            // The media object for controlling and playing audio.
                            MediaElement mediaElement = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("m");

                            // Send the stream to the media object.
                            mediaElement.SetSource(stream, stream.ContentType);
                            mediaElement.Play();


                        }





                        else if (txt_tap.Text == "tapped" && tappedkey == "6")


                        {

                            if (txt_alp.Text == "")

                            {

                                txt_alp.Text = "m";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("m");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == "m")

                            {
                                txt_alp.Text = "n";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("n");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "n")

                            {
                                txt_alp.Text = "o";


                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("o");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "o")

                            {
                                txt_alp.Text = "m";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("m");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();
                            }




                        }




                    }


                }

            }



            else

            {

                if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                {

                    MenuNumber += "6";
                    check_refresh_number.IsChecked = true;
                }

                else

                {


                }

                if (media_music.CurrentState.ToString() == "Playing")
                {

                    media_music.Position += TimeSpan.FromSeconds(30);



                }


                else

                {





                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("6");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();


                    if (check_quran.IsChecked == true)

                    {
                        check_quran_number.IsChecked = true;

                        SurahNumber += "6";


                    }



                    if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
                    {

                        AlarmHours += "6";
                    }

                    else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

                    {
                        ALarmMinutes += "6";


                    }

                    else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                    {
                        ExamHour += "6";


                    }

                    else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                    {
                        ExamMinute += "6";


                    }
                    else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                    {

                        txt_v1.Text += "6";

                    }

                    else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                    {

                        txt_v2.Text += "6";

                    }

                }

            }

        }

        private async void btn_5_Click(object sender, RoutedEventArgs e)
        {


            if (check_writting.IsChecked == true)

            {

                if (check_numberinput.IsChecked == true)

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("5");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                    txt_wrd.Text += "5";
                }


                else
                {

                    if (check_writting.IsChecked == true)

                    {

                        if (tappedkey != "5")

                        {

                            txt_wrd.Text += txt_alp.Text;




                            txt_alp.Text = "j";

                            txt_tap.Text = "";
                            txt_tap.Text = "tapped";



                            tappedkey = "5";




                            // The media object for controlling and playing audio.
                            MediaElement mediaElement = this.media_speech;

                            // The object for controlling the speech synthesis engine (voice).
                            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                            // Generate the audio stream from plain text.
                            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("j");

                            // Send the stream to the media object.
                            mediaElement.SetSource(stream, stream.ContentType);
                            mediaElement.Play();


                        }





                        else if (txt_tap.Text == "tapped" && tappedkey == "5")


                        {

                            if (txt_alp.Text == "")

                            {

                                txt_alp.Text = "j";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("j");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();


                            }


                            else if (txt_alp.Text == "j")

                            {
                                txt_alp.Text = "k";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("k");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "k")

                            {
                                txt_alp.Text = "l";


                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";


                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("l");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();

                            }

                            else if (txt_alp.Text == "l")

                            {
                                txt_alp.Text = "j";

                                txt_tap.Text = "";
                                txt_tap.Text = "tapped";



                                // The media object for controlling and playing audio.
                                MediaElement mediaElement = this.media_speech;

                                // The object for controlling the speech synthesis engine (voice).
                                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                                // Generate the audio stream from plain text.
                                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("j");

                                // Send the stream to the media object.
                                mediaElement.SetSource(stream, stream.ContentType);
                                mediaElement.Play();
                            }




                        }




                    }


                }

            }



            else

            {


                if (check_refresh.IsChecked == true && check_menu.IsChecked == false)
                {

                    MenuNumber += "5";
                    check_refresh_number.IsChecked = true;
                }

                else

                {


                }




                if (media_music.CurrentState.ToString() == "Playing")
                {

                    media_music.Pause();

                }

                else if (media_music.CurrentState.ToString() == "Paused")
                {

                    media_music.Play();

                }

                else

                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("5");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();


                    if (check_quran.IsChecked == true)

                    {

                        check_quran_number.IsChecked = true;
                        SurahNumber += "5";


                    }



                    if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == false && check_alrmMinute.IsChecked == false)
                    {

                        AlarmHours += "5";
                    }

                    else if (check_alarm.IsChecked == true && check_alrmHour.IsChecked == true && check_alrmMinute.IsChecked == false)

                    {
                        ALarmMinutes += "5";


                    }

                    else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == false && check_ExamMinute.IsChecked == false)

                    {
                        ExamHour += "5";


                    }

                    else if (check_ExamMode.IsChecked == true && check_ExamHour.IsChecked == true && check_ExamMinute.IsChecked == false)

                    {
                        ExamMinute += "5";


                    }
                    else if (check_calculator.IsChecked == true && check_variable1.IsChecked == false && check_variable2.IsChecked == false)
                    {

                        txt_v1.Text += "5";

                    }

                    else if (check_calculator.IsChecked == true && check_variable1.IsChecked == true && check_variable2.IsChecked == false)
                    {

                        txt_v2.Text += "5";

                    }


                }


            }



        }


     



        private async void btn_time_Click(object sender, RoutedEventArgs e)
        {
            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Now it is " + txt_time.Text);

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }


        private static async Task<DeviceInformation> FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel desiredPanel)
        {
            // Get available devices for capturing pictures
            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            // Get the desired camera by panel
            DeviceInformation desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == desiredPanel);

            // If there is no device mounted on the desired panel, return the first device found
            return desiredDevice ?? allVideoDevices.FirstOrDefault();
        }

        private async void btn_ocr_Click(object sender, RoutedEventArgs e)
        {

            check_ocr.IsChecked = true;

            _mediaCapture = new MediaCapture();
            var cameraDevice = await FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel.Back);
            var settings = new MediaCaptureInitializationSettings { VideoDeviceId = cameraDevice.Id };
            await _mediaCapture.InitializeAsync(settings);
            PreviewControl.Source = _mediaCapture;
            await _mediaCapture.StartPreviewAsync();



            await Task.Delay(3000);




            txt_result.Text = "";



            //Get information about the preview.
            var previewProperties1 = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview) as VideoEncodingProperties;
            int videoFrameWidth1 = (int)previewProperties1.Width;
            int videoFrameHeight1 = (int)previewProperties1.Height;

            // In portrait modes, the width and height must be swapped for the VideoFrame to have the correct aspect ratio and avoid letterboxing / black bars.

            videoFrameWidth1 = (int)previewProperties1.Height;
            videoFrameHeight1 = (int)previewProperties1.Width;


            // Create the video frame to request a SoftwareBitmap preview frame.
            var videoFrame1 = new VideoFrame(BitmapPixelFormat.Bgra8, videoFrameWidth1, videoFrameHeight1);

            using (var currentFrame = await _mediaCapture.GetPreviewFrameAsync(videoFrame1))
            {
                // Collect the resulting frame.
                SoftwareBitmap bitmap = currentFrame.SoftwareBitmap;

                // OcrEngine ocrEngine = OcrEngine.TryCreateFromLanguage(ocrLanguage);

                var ocrEngine = OcrEngine.TryCreateFromLanguage(new Language("en"));


                var imgSource = new WriteableBitmap(bitmap.PixelWidth, bitmap.PixelHeight);
                bitmap.CopyToBuffer(imgSource.PixelBuffer);
                PreviewImage.Source = imgSource;

                var ocrResult = await ocrEngine.RecognizeAsync(bitmap);



                txt_result.Text = ocrResult.Text;



                if (txt_result.Text != "")
                {
                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_result.Text + " is  found in front of camera");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();
                }

                else
                {

                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Nothing found on camera. Place the camera in front of element & press enter.");

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();


                }




            }
        }

        private async void btn_alarm_Click(object sender, RoutedEventArgs e)
        {
            check_alarm.IsChecked = true;
            check_alrmHour.IsChecked = false;
            check_alrmMinute.IsChecked = false;

            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("To set alarm. First select the number of hours after you need alarm.");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

        }

        private async void btn_novels_Click(object sender, RoutedEventArgs e)
        {
            check_novels.IsChecked = true;
            check_writerChoosed.IsChecked = false;
            list_novels.SelectedIndex = -1;

            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("First choose a writer to read his book");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

        }

        private async void btn_nctb_Click(object sender, RoutedEventArgs e)
        {


            check_nctb.IsChecked = true;
            check_Class.IsChecked = false;
            check_subject.IsChecked = false;
            check_topic.IsChecked = false;

            list_class.SelectedIndex = -1;



            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("First choose your class to read your book.");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

        }


        private async void btn_song_Click(object sender, RoutedEventArgs e)
        {
            check_song.IsChecked = true;
            SongCategory = null;


            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("First select a category of song.");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();


        }



        private async void list_subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_subject.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }

            catch
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }
        }

        private async void list_novels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_novels.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }

            catch
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }
        }


        private async void list_class_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_class.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }

            catch
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }
        }



        private async void list_topic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try

            {

                Uri newuri = new Uri("ms-appx:///Assets/NCTB/" + NctbClass + "/" + NctbSubject + "/" + list_topic.SelectedValue.ToString());
                media_music.Source = newuri;

                media_music.Play();
            }

            catch
            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }

        }



        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            list_contactFeatures.Items.Add("Add new contact");
            list_contactFeatures.Items.Add("See existing");


            list_notesFeatures.Items.Add("New Notes");
            list_notesFeatures.Items.Add("Saved Notes");
            list_notesFeatures.Items.Add("New Recordings");
            list_notesFeatures.Items.Add("Saved Recordings");


            list_examMode.Items.Add("Answer Sheet");
            list_examMode.Items.Add("Time");
            list_examMode.Items.Add("Calculator");
            list_examMode.Items.Add("Send Answer Sheet");
           




            DispatcherTimerSetup();
            timer_time.Start();

            timer_Reload2minute.Start();


            QuranLocation = "ms-appx:///Assets/Quran/";


            check_refresh.IsChecked = true;


            try
            {
                Uri url = new Uri("https://app.simplenote.com/publish/N5d7M3");
                WebNewsUrl.Navigate(url);
            }

            catch
            {


            }



            try
            {



                list_emailaddress.Items.Clear();


                string root1 = ApplicationData.Current.LocalFolder.Path;
                string path1 = root1 + @"\MyText.txt";


                var items = new List<string>();
                using (var stream = File.OpenRead(path1))  // open file
                using (var reader = new StreamReader(stream))   // read the stream with TextReader
                {
                    string line;

                    // read until no more lines are present
                    while ((line = reader.ReadLine()) != null)
                    {
                        list_emailaddress.Items.Add(line);
                    }
                }


            }

            catch
            {
                txt1.Text += "cant load address";

            }


            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("notesName"))
            {
                try
                {



                    list_notes.Items.Clear();


                    string root1 = ApplicationData.Current.LocalFolder.Path;
                    string path1 = root1 + @"\notesname.txt";


                    var items = new List<string>();
                    using (var stream = File.OpenRead(path1))  // open file
                    using (var reader = new StreamReader(stream))   // read the stream with TextReader
                    {
                        string line;

                        // read until no more lines are present
                        while ((line = reader.ReadLine()) != null)
                        {
                            list_notes.Items.Add(line);
                            txt1.Text += "loaded";
                        }
                    }


                }
                
                catch
                {
                    txt1.Text += "cant load notes";

                }
            }

            else
            {

                txt1.Text += "cant load notes and found also";
            }



            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Teacher Email Saved"))
            {

            

                     StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("TeacherEmail.txt");
                     txt_teacherEmail.Text = await FileIO.ReadTextAsync(file);


            }





                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("SaveRecordings"))
            {
                try
                {



                    list_recordings.Items.Clear();


                    string root1 = ApplicationData.Current.LocalFolder.Path;
                    string path1 = root1 + @"\SaveRecordingsName.txt";


                    var items = new List<string>();
                    using (var stream = File.OpenRead(path1))  // open file
                    using (var reader = new StreamReader(stream))   // read the stream with TextReader
                    {
                        string line;

                        // read until no more lines are present
                        while ((line = reader.ReadLine()) != null)
                        {
                            list_recordings.Items.Add(line);
                            txt1.Text += "loaded";
                        }
                    }


                }

                catch
                {
                    txt1.Text += "cant load notes";

                }
            }

            else
            {

                txt1.Text += "cant load notes and found also";
            }



            Uri newuri = new Uri("ms-appx:///Assets/Sounds/intro-music.mp3");
            media_SystemSound.Source = newuri;

            media_SystemSound.Play();



            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Quran\";

         
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            FileInfo[] files = dirinfo.GetFiles("*.mp3");

            foreach (FileInfo file in files)
            {
                list_quran.Items.Add(file.Name);
            }



            list_calcEquation.Items.Add("plus");
            list_calcEquation.Items.Add("minus");
            list_calcEquation.Items.Add("multiply");
            list_calcEquation.Items.Add("division");





            list_novels.Items.Add("Humayun Ahmed");
            list_novels.Items.Add("Bibhutibhushan Bandyopadhyay");
            list_novels.Items.Add("Sarat Chandra Chattopadhyay");
            list_novels.Items.Add("Satyajit Ray");
            list_novels.Items.Add("Md. Jafor Iqbal");
            list_novels.Items.Add("Rabindranath Tagore");
            list_novels.Items.Add("Kazi Nazrul Islam");
            list_novels.Items.Add("Masud Rana");
            list_novels.Items.Add("Rokib Hasan");
            list_novels.Items.Add("Taslima Nasrin");
            list_novels.Items.Add("Anisul Hoque");
            list_novels.Items.Add("Tarasankar Bandyopadhyay");
            list_novels.Items.Add("Samaresh Majumdar");
            list_novels.Items.Add("Shirshendu Mukhopadhay");
           
            list_novels.Items.Add("Humayun Azad");
            list_novels.Items.Add("Sunil ganapathy");
            list_novels.Items.Add("Famous Short Stories");
            list_novels.Items.Add("Famous Novels");


            list_class.Items.Add("Class 5");
            list_class.Items.Add("Class 6");
            list_class.Items.Add("Class 7");
            list_class.Items.Add("Class 8");
            list_class.Items.Add("Class 9-10");


            list_songCategory.Items.Add("Bangla Song");
            list_songCategory.Items.Add("Hindi Song");
            list_songCategory.Items.Add("English Song");
            list_songCategory.Items.Add("Top rated Song");



            list_menuitem.Items.Add("1 Todays Time");
            list_menuitem.Items.Add("2 Todays Date");
            list_menuitem.Items.Add("3 Todays Weather");
            list_menuitem.Items.Add("4 Todays News Update");
            list_menuitem.Items.Add("5 Your current Location");
            list_menuitem.Items.Add("6 Calculator");
            list_menuitem.Items.Add("7 Alarm Clock");
            list_menuitem.Items.Add("8 Novels");
            list_menuitem.Items.Add("9 Nctb Books");
            list_menuitem.Items.Add("10 Live Reader");
            list_menuitem.Items.Add("11 Send and receive messages");
            list_menuitem.Items.Add("12 Songs");
            list_menuitem.Items.Add("13 The Holy Quran");
            list_menuitem.Items.Add("14 Contact list");
            list_menuitem.Items.Add("15 Notes and Recordings");
            list_menuitem.Items.Add("16 Exam Mode");






            try
            {

                string root3 = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                string path3 = root3 + @"\Assets\Songs\Bangla Song\";

                DirectoryInfo dirinfo3 = new DirectoryInfo(path3);
                FileInfo[] files3 = dirinfo3.GetFiles("*.mp3");

                foreach (FileInfo file in files3)
                {
                    list_songBangla.Items.Add(file.Name);
                }


                
                string path1 = root + @"\Assets\Songs\Hindi Song\";

                DirectoryInfo dirinfo1 = new DirectoryInfo(path1);
                FileInfo[] files1 = dirinfo1.GetFiles("*.mp3");

                foreach (FileInfo file in files1)
                {
                    list_songHindi.Items.Add(file.Name);
                }


                string path2 = root + @"\Assets\Songs\English Song\";

                DirectoryInfo dirinfo2 = new DirectoryInfo(path2);
                FileInfo[] files2 = dirinfo2.GetFiles("*.mp3");

                foreach (FileInfo file in files2)
                {
                    list_songEnglish.Items.Add(file.Name);
                }





            }

            catch
            {

            }



            try


            { 

            var geoLocator = new Geolocator();
            geoLocator.DesiredAccuracy = PositionAccuracy.High;
            Geoposition pos = await geoLocator.GetGeopositionAsync();


          
                Lat = pos.Coordinate.Point.Position.Latitude.ToString();
                Lng = pos.Coordinate.Point.Position.Longitude.ToString();

                var data = await App2.Helper.GetWeather(Lat, Lng);
                if (data != null)
                {


                    txt_weatherComment.Text = $"{data.weather[0].description}";
                    txt_humidity.Text = $" {data.main.humidity}%";
                    txt_pressure.Text = $"{data.main.pressure} atm";
                    txt_temp.Text = $"{data.main.temp} °C";
                }

                else

                {

                }

            }

            catch
            {


            }

            try

            {

                // The location to reverse geocode.
                BasicGeoposition location = new BasicGeoposition();
                location.Latitude = Convert.ToDouble(Lat);
                location.Longitude = Convert.ToDouble(Lng);
                Geopoint pointToReverseGeocode = new Geopoint(location);

                MapService.ServiceToken = "Hvk4ZPbMSRLd1FpHq2kV~sTg6aSOVZPtXAFAjddNZug~AvO4iQonznsl6BRt9neCFFlduIMVcSu7Z7zxiKd-abDu5CfLizoy_QAlO4es7bYe";


                // Reverse geocode the specified geographic location.
                MapLocationFinderResult result =
                      await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);

                // If the query returns results, display the name of the town
                // contained in the address of the first result.
                if (result.Status == MapLocationFinderStatus.Success)
                {

                    txt_location.Text = "";
                    txt_location.Text += "Country = " +
                          result.Locations[0].Address.Country;
                    txt_location.Text += "  town = " +
                          result.Locations[0].Address.Town;
                 
                    txt_location.Text += "  Street number = " +
                          result.Locations[0].Address.StreetNumber;
                   
                  
                    txt_location.Text += "  building name = " +
                          result.Locations[0].Address.BuildingName;
                   

                    txt_lat.Text = Lat;
                    txt_lon.Text = Lng;



                }

                else
                {
                    //  tbOutputText.Text += "Failed";

                }

            }


            catch
            {


            }

        }

            private void check_alrmHour_Checked(object sender, RoutedEventArgs e)
            {

            }

            private void check_alarm_Checked(object sender, RoutedEventArgs e)
            {

            }

            private void txt_time_SelectionChanged(object sender, RoutedEventArgs e)
            {

            }

        private async void list_menuitem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_menuitem.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }
            catch

            {
                list_menuitem.SelectedItem = -1;

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }
        }

        private async void list_calcEquation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_calcEquation.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }

            catch

            {
               

            }

        }

        private void list_songBangla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try

            {

                SongName = list_songBangla.SelectedValue.ToString();

                Uri newuri = new Uri("ms-appx:///Assets/Songs/Bangla Song/" + SongName);
                media_music.Source = newuri;
                media_music.Play();
            }

            catch
            {


            }

            }

        private void list_songEnglish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SongName = list_songEnglish.SelectedValue.ToString();

            Uri newuri = new Uri("ms-appx:///Assets/Songs/English Song/"+ SongName );
            media_music.Source = newuri;
            media_music.Play();
        }

        private void list_songHindi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SongName = list_songHindi.SelectedValue.ToString();

                Uri newuri = new Uri("ms-appx:///Assets/Songs/Hindi Song/" + SongName);
                media_music.Source = newuri;
                media_music.Play();
            }

            catch
            {


            }

            }

        private async void list_songCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (list_songCategory.SelectedValue != null)

            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_songCategory.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }

            else
            {

                list_songCategory.SelectedIndex = -1;
               
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }




            }

        private void WebNewsUrl_LoadCompleted(object sender, NavigationEventArgs e)
        {
            txt_NewsUrl.Text = WebNewsUrl.DocumentTitle;
        }

      
        private async void btn_News_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Uri _videoUri = await GetYoutubeUri(txt_NewsUrl.Text);
                if (_videoUri != null)
                {
                    media_music.Source = _videoUri;
                    media_music.Play();
                }

            }


            catch
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Check your internet connection");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private async void list_quran_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try

            {

                txt1.Text = list_quran.SelectedValue.ToString();

                txt1.Text = txt1.Text.Replace(".mp3", "");



                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt1.Text);

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

            }

            catch

            {



            }

            }

        private async void btn_Quran_Click(object sender, RoutedEventArgs e)
        {
            check_quran.IsChecked = true;


            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Click next button or enter surah number and press answer button.");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

           

        }

        private async void txt_speechResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            //4444

            if (txt_speechResult.Text.Contains("today's time") || txt_speechResult.Text.Contains("what is the current time") || txt_speechResult.Text.Contains("about time"))
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_time);

                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();


                txt_speechResult.Text = "";
            }



            if (txt_speechResult.Text.Contains("what is the current location") || txt_speechResult.Text.Contains("track my location") || txt_speechResult.Text.Contains("location"))
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_location);

                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();


                txt_speechResult.Text = "";
            }


            else if (txt_speechResult.Text.Contains("today's date") || txt_speechResult.Text.Contains("what is the current date") || txt_speechResult.Text.Contains("about date"))
            {

                ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_date);

                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();

                txt_speechResult.Text = "";
            }

            else if (txt_speechResult.Text.Contains("today's weather update") || txt_speechResult.Text.Contains("what is the current weather") || txt_speechResult.Text.Contains("weather forcast"))
            {

                ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_weather);

                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();

                txt_speechResult.Text = "";
            }

            else if (txt_speechResult.Text.Contains("today's news update") || txt_speechResult.Text.Contains("today's news") || txt_speechResult.Text.Contains("current news"))
            {

                ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_News);

                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
                txt_speechResult.Text = "";
            }

            else if (txt_speechResult.Text.Contains("alarm") || txt_speechResult.Text.Contains("wake up"))
            {

                string str = txt_speechResult.Text;
                decimal d;
                string n = str.Split(' ').Where(s1 => decimal.TryParse(s1, out d)).FirstOrDefault();
                txt_alarmHours.Text = decimal.Parse(n).ToString();





                int y = Convert.ToInt32(txt_alarmHours.Text);




                string str1 = txt_speechResult.Text;
                decimal d1;
                string n1 = str1.Split(' ').Where(s => decimal.TryParse(s, out d1)).LastOrDefault();
                txt_alarmMinutes.Text = decimal.Parse(n1).ToString();

                int x = Convert.ToInt32(txt_alarmMinutes.Text);





                timer_alarm = new DispatcherTimer();
                timer_alarm.Tick += timer_alarm_Tick;
                timer_alarm.Interval = new TimeSpan(y, x, 1);


                timer_alarm.Start();

                txt_speechResult.Text = "";



                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("alarm set for " + txt_alarmHours.Text + " hours and " + txt_alarmMinutes.Text + " minutes.");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();



                txt_alarmHours.Text = "";
                txt_alarmMinutes.Text = "";
            }

            else if (txt_speechResult.Text.Contains("textbook") || txt_speechResult.Text.Contains("government books") || txt_speechResult.Text.Contains("text books"))


            {



                ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_nctb);

                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();



            }


            else if (txt_speechResult.Text.Contains("+") || txt_speechResult.Text.Contains("-") || txt_speechResult.Text.Contains("multiply") || txt_speechResult.Text.Contains("into") || txt_speechResult.Text.Contains("divided by") || txt_speechResult.Text.Contains("division") || txt_speechResult.Text.Contains("devide") || txt_speechResult.Text.Contains("/"))


            {

                if (txt_speechResult.Text.Contains("+"))

                {
                    string str = txt_speechResult.Text;
                    decimal d;
                    string n = str.Split(' ').Where(s1 => decimal.TryParse(s1, out d)).FirstOrDefault();
                    txt_math1.Text = decimal.Parse(n).ToString();

                    string str1 = txt_speechResult.Text;
                    decimal d1;
                    string n1 = str1.Split(' ').Where(s => decimal.TryParse(s, out d1)).LastOrDefault();
                    txt_math2.Text = decimal.Parse(n1).ToString();




                    txt_mathAnswer.Text = Convert.ToString(Convert.ToDouble(txt_math1.Text) + Convert.ToDouble(txt_math2.Text));


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_math1.Text + " plus " + txt_math2.Text + " equals to " + txt_mathAnswer.Text);

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }




                else if (txt_speechResult.Text.Contains("-"))

                {
                    string str = txt_speechResult.Text;
                    decimal d;
                    string n = str.Split(' ').Where(s1 => decimal.TryParse(s1, out d)).FirstOrDefault();
                    txt_math1.Text = decimal.Parse(n).ToString();

                    string str1 = txt_speechResult.Text;
                    decimal d1;
                    string n1 = str1.Split(' ').Where(s => decimal.TryParse(s, out d1)).LastOrDefault();
                    txt_math2.Text = decimal.Parse(n1).ToString();


                    txt_mathAnswer.Text = Convert.ToString(Convert.ToDouble(txt_math1.Text) - Convert.ToDouble(txt_math2.Text));


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_math1.Text + " minus " + txt_math2.Text + " equals to " + txt_mathAnswer.Text);

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }

                else if (txt_speechResult.Text.Contains("multiply") || txt_speechResult.Text.Contains("into"))

                {
                    string str = txt_speechResult.Text;
                    decimal d;
                    string n = str.Split(' ').Where(s1 => decimal.TryParse(s1, out d)).FirstOrDefault();
                    txt_math1.Text = decimal.Parse(n).ToString();

                    string str1 = txt_speechResult.Text;
                    decimal d1;
                    string n1 = str1.Split(' ').Where(s => decimal.TryParse(s, out d1)).LastOrDefault();
                    txt_math2.Text = decimal.Parse(n1).ToString();


                    txt_mathAnswer.Text = Convert.ToString(Convert.ToDouble(txt_math1.Text) * Convert.ToDouble(txt_math2.Text));


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_math1.Text + " multiply " + txt_math2.Text + " equals to " + txt_mathAnswer.Text);

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }


                else if (txt_speechResult.Text.Contains("/") || txt_speechResult.Text.Contains("divided"))

                {
                    string str = txt_speechResult.Text;
                    decimal d;
                    string n = str.Split(' ').Where(s1 => decimal.TryParse(s1, out d)).FirstOrDefault();
                    txt_math1.Text = decimal.Parse(n).ToString();

                    string str1 = txt_speechResult.Text;
                    decimal d1;
                    string n1 = str1.Split(' ').Where(s => decimal.TryParse(s, out d1)).LastOrDefault();
                    txt_math2.Text = decimal.Parse(n1).ToString();


                    txt_mathAnswer.Text = Convert.ToString(Convert.ToDouble(txt_math1.Text) / Convert.ToDouble(txt_math2.Text));


                    // The media object for controlling and playing audio.
                    MediaElement mediaElement = this.media_speech;

                    // The object for controlling the speech synthesis engine (voice).
                    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                    // Generate the audio stream from plain text.
                    SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_math1.Text + " divided by " + txt_math2.Text + " equals to " + txt_mathAnswer.Text);

                    // Send the stream to the media object.
                    mediaElement.SetSource(stream, stream.ContentType);
                    mediaElement.Play();

                }




            }

            else if (txt_speechResult.Text.Contains("Al-Quran") || txt_speechResult.Text.Contains("surah"))

            {


                try
                {
                    string str = txt_speechResult.Text;
                    decimal d;
                    string n = str.Split(' ').Where(s1 => decimal.TryParse(s1, out d)).FirstOrDefault();
                    txt_surahNumber.Text = decimal.Parse(n).ToString();



                    txt_surahNumber.Text = Convert.ToString(Convert.ToDouble(txt_surahNumber.Text) - Convert.ToDouble("1"));

                    int serial = Convert.ToInt32(txt_surahNumber.Text);

                    list_quran.SelectedIndex = serial;

                    Uri newuri = new Uri(QuranLocation + list_quran.SelectedValue.ToString());
                    media_music.Source = newuri;

                    media_music.Play();


                    check_quran.IsChecked = true;



                }

                catch

                {



                }
            }

            else if (txt_speechResult.Text.Contains("songs") || txt_speechResult.Text.Contains("song"))

            {
                if (txt_speechResult.Text.Contains("bangla"))

                {

                    SongCategory = "Bangla Song";
                    check_song.IsChecked = true;
                    check_songCategory.IsChecked = true;



                    list_songBangla.SelectedIndex = 0;




                }

                else if (txt_speechResult.Text.Contains("english"))

                {

                    SongCategory = "English Song";
                    check_song.IsChecked = true;
                    check_songCategory.IsChecked = true;



                    list_songEnglish.SelectedIndex = 0;

                }

                else if (txt_speechResult.Text.Contains("hindi"))

                {

                    SongCategory = "Hindi Song";
                    check_song.IsChecked = true;
                    check_songCategory.IsChecked = true;



                    list_songHindi.SelectedIndex = 0;
                }

            }


            else if (txt_speechResult.Text.Contains("novel") || txt_speechResult.Text.Contains("story books") || txt_speechResult.Text.Contains("storybook"))

            {

                ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_novels);

                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();


                await Task.Delay(3000);



                ButtonAutomationPeer peer1 = new ButtonAutomationPeer(btn_next);

                IInvokeProvider invokeProv1 = peer1.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv1.Invoke();





              




            }



            }


        private async void media_music_opened(object sender, RoutedEventArgs e)
        {

            if (check_novelChoosed.IsChecked == true)
            {
             
            }

            else

            {

                await Task.Delay(4000);

                ButtonAutomationPeer peer = new ButtonAutomationPeer(btn_next);

                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();

            }

        }

       

           

        private  void btn1212_Click(object sender, RoutedEventArgs e)
        {

            


        }

        private void txt_tap_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                timer_1s.Stop();
            }

            catch
            {


            }
            try
            {
                timer_1s.Start();
            }

            catch
            {


            }
        }

        private void check_tap_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void txt_tap_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                timer_1s.Stop();
            }

            catch
            {


            }
            try
            {
                timer_1s.Start();
            }

            catch
            {


            }
        }

        private async void txt_wrd_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txt_wrd.Text.Contains(".") && check_email.IsChecked == false)

            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(txt_sentance.Text);

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                txt_paragraph.Text += txt_sentance.Text + ". ";



                txt_alp.Text = "";
                txt_wrd.Text = "";
                txt_sentance.Text = "";

            }

        }

        private async void btn_contact_Click(object sender, RoutedEventArgs e)
        {


            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("You can add and access your contacts here");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

            check_contact.IsChecked = true;

        
        }

        private async void list_contactFeatures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_contactFeatures.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();


            }

            catch
            {


            }
        }

        private async void list_emailaddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try

            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_emailaddress.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }

            catch
            {

            }
        }

        private async void btn_notes_Click(object sender, RoutedEventArgs e)
        {
            check_notesRecordings.IsChecked = true;

            // The media object for controlling and playing audio.
            MediaElement mediaElement = this.media_speech;

            // The object for controlling the speech synthesis engine (voice).
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

            // Generate the audio stream from plain text.
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("You can add or access notes and recordings");

            // Send the stream to the media object.
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

        }

        private async void list_notes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_notes.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }

            catch
            {

            }

            }

        private async void list_recordings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_recordings.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }
            catch
            {


            }

        


    }

        private async void list_notesFeatures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_notesFeatures.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }
            catch
            {


            }

            }

        private async void list_examMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(list_examMode.SelectedValue.ToString());

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }
            catch
            {


            }

        }

        private void btn_notes_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_notes_Copy_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private async void btn_ExamMode_Click(object sender, RoutedEventArgs e)
        {
            check_ExamMode.IsChecked = true;


            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Teacher Email Saved"))
            {

                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("In Exam mode, do you want to continue with the teacher email " + txt_teacherEmail.Text + "? If not then type new email address to continue." );

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                check_writting.IsChecked = true;
                check_email.IsChecked = true;

            }

            else
            {
                // The media object for controlling and playing audio.
                MediaElement mediaElement = this.media_speech;

                // The object for controlling the speech synthesis engine (voice).
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

                // Generate the audio stream from plain text.
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("First add your teachers email address.");

                // Send the stream to the media object.
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                check_writting.IsChecked = true;

            }


        }

        private void txt_v1_TextChanged(object sender, TextChangedEventArgs e)
            {




            }
        }
    }



