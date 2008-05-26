var interval = 5000;
var intervalId = setInterval('PageMethods.GetLoaderAsyncServiceStatus(updateProgressIndicator);', interval);
var LoaderAsyncServiceStatus = { Idle : 0, Loading : 1, FailedLoad : 2, Completed : 3 };

function updateProgressIndicator(loaderAsyncServiceStatus)
{
    switch (loaderAsyncServiceStatus)
    {
        case LoaderAsyncServiceStatus.Completed:
            clearInterval(intervalId);

            $get('inProgressIndicator').style.display = 'none';
            $get('completedIndicator').style.display = 'block';
            $get('failedLoadIndicator').style.display = 'none';

            break;
        case LoaderAsyncServiceStatus.FailedLoad:
            clearInterval(intervalId);

            $get('inProgressIndicator').style.display = 'none';
            $get('completedIndicator').style.display = 'none';
            $get('failedLoadIndicator').style.display = 'block';

            break;
    }
}
