var interval = 5000;
var intervalId = setInterval('PageMethods.GetLoaderServiceState(updateProgressIndicator);', interval);
var LoadingStatus = { InProgress : 1, FailedLoad : 2, Completed : 3 };

function updateProgressIndicator(loadingStatus)
{
    switch (loadingStatus)
    {
        case LoadingStatus.Completed:
            clearInterval(intervalId);

            $get('inProgressIndicator').style.display = 'none';
            $get('completedIndicator').style.display = 'block';
            $get('failedLoadIndicator').style.display = 'none';

            break;
        case LoadingStatus.FailedLoad:
            clearInterval(intervalId);

            $get('inProgressIndicator').style.display = 'none';
            $get('completedIndicator').style.display = 'none';
            $get('failedLoadIndicator').style.display = 'block';

            break;
    }
}
