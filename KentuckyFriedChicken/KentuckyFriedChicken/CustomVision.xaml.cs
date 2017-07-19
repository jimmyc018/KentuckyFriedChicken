using KentuckyFriedChicken.Model;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KentuckyFriedChicken
{
	public partial class CustomVision : ContentPage
	{
		public CustomVision()
		{
			InitializeComponent();
		}

		private async void LoadCamera(object sender, EventArgs e)
		{
			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				await DisplayAlert("No Camera", ":( No camera available.", "OK");
				return;
			}

			MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
			{
				PhotoSize = PhotoSize.Medium,
				Directory = "Sample",
				Name = $"{DateTime.UtcNow}.jpg"
			});

			if (file == null)
				return;

			image.Source = ImageSource.FromStream(() =>
			{
				return file.GetStream();
			});


			await MakePredictionRequest(file);
		}

		static byte[] GetImageAsByteArray(MediaFile file)
		{
			var stream = file.GetStream();
			BinaryReader binaryReader = new BinaryReader(stream);
			return binaryReader.ReadBytes((int)stream.Length);
		}

		async Task MakePredictionRequest(MediaFile file)
		{
			var client = new HttpClient();

			client.DefaultRequestHeaders.Add("Prediction-Key", "13e5167753114b2ba2e67c8ce2ceee3c");

			string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/310d5c7e-8da1-4dd7-a9e6-2bde9561fa93/image?iterationId=9d133bf8-0953-4bcc-bfc3-655df614de14";

			HttpResponseMessage response;

			byte[] byteData = GetImageAsByteArray(file);

			using (var content = new ByteArrayContent(byteData))
			{

				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
				response = await client.PostAsync(url, content);


				if (response.IsSuccessStatusCode)
				{
					var responseString = await response.Content.ReadAsStringAsync();

					EvaluationModel responseModel = JsonConvert.DeserializeObject<EvaluationModel>(responseString);

					double max = responseModel.Predictions.Max(m => m.Probability);

					TagLabel.Text = (max >= 0.5) ? "KFC" : "Not KFC";

				}

				//Get rid of file once we have finished using it
				file.Dispose();
			}
		}
	}
}