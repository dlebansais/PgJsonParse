namespace PgJsonParse
{
    public enum DownloadState
    {
        NotDownloaded,
        Downloading,      // Ensure all download errors are below this line (cf: IsDownloadFailed property)
        FailedToDownload,
        DownloadCanceled,
        Downloaded,       // Ensure all download errors are above this line (cf: IsDownloadFailed property)
    }
}
