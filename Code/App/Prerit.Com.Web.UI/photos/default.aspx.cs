using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

public partial class photos_default : Page
{
	private static readonly object syncRoot = new object();

	protected void Page_Load(object sender, EventArgs e)
	{
		int albumId;

		List<string> photoList;

		int.TryParse(Request.QueryString["albumid"], out albumId);

		switch (albumId)
		{
			default:
			case 0:
				photoAlbumViewMultiView.ActiveViewIndex = 0;
				photoList = GetPithiPhotoList();
				break;
			case 1:
				photoAlbumViewMultiView.ActiveViewIndex = 1;
				photoList = GetWeddingPhotoList();
				break;
			case 2:
				photoAlbumViewMultiView.ActiveViewIndex = 2;
				photoList = GetHoneymoonPhotoList();
				break;
			case 3:
				photoAlbumViewMultiView.ActiveViewIndex = 3;
				photoList = GetChristmasPhotoList();
				break;
		}

		photoRepeater.DataSource = photoList;
		photoRepeater.DataBind();
	}

	private List<string> GetPhotoList(string cacheKey, string folderPath)
	{
		if (Cache[cacheKey] == null)
		{
			lock (syncRoot)
			{
				if (Cache[cacheKey] == null)
				{
					List<string> photoList = new List<string>();

					foreach (string fileName in Directory.GetFiles(MapPath(folderPath)))
					{
						if (Path.GetExtension(fileName) == ".jpg" && !fileName.EndsWith("_thumb.jpg"))
						{
							string baseFileName = Path.GetFileNameWithoutExtension(fileName);

							photoList.Add(ResolveUrl(folderPath + Path.GetFileName(baseFileName)));
						}
					}

					Cache[cacheKey] = photoList;
				}
			}
		}

		return (List<string>) Cache[cacheKey];
	}

	private List<string> GetPithiPhotoList()
	{
		const string cacheKey = "pithiPhotoList";

		string folderPath = "~/photos/pithi/";

		return GetPhotoList(cacheKey, folderPath);
	}

	private List<string> GetWeddingPhotoList()
	{
		const string cacheKey = "weddingPhotoList";

		string folderPath = "~/photos/wedding/";

		return GetPhotoList(cacheKey, folderPath);
	}

	private List<string> GetHoneymoonPhotoList()
	{
		const string cacheKey = "honeymoonPhotoList";

		string folderPath = "~/photos/honeymoon/";

		return GetPhotoList(cacheKey, folderPath);
	}

	private List<string> GetChristmasPhotoList()
	{
		const string cacheKey = "christmasPhotoList";

		string folderPath = "~/photos/christmas/";

		return GetPhotoList(cacheKey, folderPath);
	}
}