var interval = 5000;
var intervalId = setInterval('PageMethods.ArePhotoAlbumsLoading(updateProgressIndicator);', interval);

function updateProgressIndicator(isLoading)
{
    if (!isLoading)
    {
        clearInterval(intervalId);

        $get('inProgressIndicator').style.display = 'none';
        $get('completedIndicator').style.display = 'block';
    }
}
