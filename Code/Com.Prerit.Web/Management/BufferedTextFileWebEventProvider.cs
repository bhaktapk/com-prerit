using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Management;

namespace Com.Prerit.Web.Management
{
    public class BufferedTextFileWebEventProvider : BufferedWebEventProvider
    {
        #region Properties

        private string LogDirectoryPath
        {
            get;
            set;
        }

        private string LogFileNameFormat
        {
            get;
            set;
        }

        #endregion

        #region Methods

        private string GetFormattedFileName(string fileNameFormat)
        {
            return DateTime.Today.ToString(fileNameFormat);
        }

        private StreamWriter GetLogWriter()
        {
            string filePath = Path.Combine(LogDirectoryPath, GetFormattedFileName(LogFileNameFormat));

            StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8);

            writer.NewLine = "\r\n";

            return writer;
        }

        private string GetValidatedLogDirectoryPath(string name, NameValueCollection config)
        {
            string fullPhysicalPath;

            string logDirectoryPathConfigValue = config[BufferedTextFileWebEventProviderMarkup.LogDirectoryPath];

            if (string.IsNullOrEmpty(logDirectoryPathConfigValue))
            {
                throw new ProviderException(
                    string.Format("The required attribute '{0}' is missing a valid value in the configuration of the '{1}' provider.",
                                  BufferedTextFileWebEventProviderMarkup.LogDirectoryPath,
                                  name));
            }

            if (logDirectoryPathConfigValue.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new ProviderException(
                    string.Format("The attribute '{0}' contains illegal characters in the configuration of the '{1}' provider.",
                                  BufferedTextFileWebEventProviderMarkup.LogDirectoryPath,
                                  name));
            }

            try
            {
                string physicalPath;

                if (IsPathVirtual(logDirectoryPathConfigValue))
                {
                    physicalPath = HostingEnvironment.MapPath(logDirectoryPathConfigValue);
                }
                else
                {
                    physicalPath = logDirectoryPathConfigValue;
                }

                fullPhysicalPath = Path.GetFullPath(physicalPath);
            }
            catch (Exception e)
            {
                throw new ProviderException(
                    string.Format("The attribute '{0}' is not a valid physical or virtual path in the configuration of the '{1}' provider.",
                                  BufferedTextFileWebEventProviderMarkup.LogDirectoryPath,
                                  name),
                    e);
            }

            return fullPhysicalPath;
        }

        private string GetValidatedLogFileNameFormat(string name, NameValueCollection config)
        {
            string formattedFileName;

            string logFileNameFormatConfigValue = config[BufferedTextFileWebEventProviderMarkup.LogFileFormat];

            if (string.IsNullOrEmpty(logFileNameFormatConfigValue))
            {
                throw new ProviderException(
                    string.Format("The required attribute '{0}' is missing a valid value in the configuration of the '{1}' provider.",
                                  BufferedTextFileWebEventProviderMarkup.LogFileFormat,
                                  name));
            }

            if (logFileNameFormatConfigValue.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                throw new ProviderException(
                    string.Format("The attribute '{0}' contains illegal characters in the configuration of the '{1}' provider.",
                                  BufferedTextFileWebEventProviderMarkup.LogFileFormat,
                                  name));
            }

            try
            {
                formattedFileName = GetFormattedFileName(logFileNameFormatConfigValue);
            }
            catch (Exception e)
            {
                throw new ProviderException(
                    string.Format("The attribute '{0}' is not a valid DateTimeFormat pattern in the configuration of the '{1}' provider.",
                                  BufferedTextFileWebEventProviderMarkup.LogFileFormat,
                                  name),
                    e);
            }

            try
            {
                Path.GetFullPath(Path.Combine(LogDirectoryPath, formattedFileName));
            }
            catch (Exception e)
            {
                throw new ProviderException(
                    string.Format("The attribute '{0}' is not a valid file name in the configuration of the '{1}' provider.",
                                  BufferedTextFileWebEventProviderMarkup.LogFileFormat,
                                  name),
                    e);
            }

            return logFileNameFormatConfigValue;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            LogDirectoryPath = GetValidatedLogDirectoryPath(name, config);

            LogFileNameFormat = GetValidatedLogFileNameFormat(name, config);

            config.Remove(BufferedTextFileWebEventProviderMarkup.LogDirectoryPath);

            config.Remove(BufferedTextFileWebEventProviderMarkup.LogFileFormat);

            base.Initialize(name, config);
        }

        private static bool IsPathVirtual(string path)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    //TODO: use regular expressions
                    result = VirtualPathUtility.IsAppRelative(path) || VirtualPathUtility.IsAbsolute(path);
                }
                catch (Exception e)
                {
                    //swallow exception because an "Is" method should never throw an exception
                    Trace.TraceInformation(e.ToString());
                }
            }

            return result;
        }

        private void LogEntries(WebBaseEventCollection eventCollection)
        {
            using (StreamWriter writer = GetLogWriter())
            {
                foreach (WebBaseEvent webEvent in eventCollection)
                {
                    LogEntry(webEvent, writer);
                }
            }
        }

        private void LogEntry(WebBaseEvent webEvent)
        {
            using (StreamWriter writer = GetLogWriter())
            {
                LogEntry(webEvent, writer);
            }
        }

        private void LogEntry(WebBaseEvent webEvent, StreamWriter writer)
        {
            writer.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            writer.WriteLine(ScrubLog(webEvent.ToString(true, true)));
            writer.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            writer.WriteLine();
        }

        public override void ProcessEvent(WebBaseEvent webEvent)
        {
            if (UseBuffering)
            {
                base.ProcessEvent(webEvent);
            }
            else
            {
                LogEntry(webEvent);
            }
        }

        public override void ProcessEventFlush(WebEventBufferFlushInfo flushInfo)
        {
            LogEntries(flushInfo.Events);
        }

        private string ScrubLog(string log)
        {
            return log.Trim().Replace("\n", "\r\n").Replace("\r\r\n", "\r\n");
        }

        public override void Shutdown()
        {
            Flush();

            base.Shutdown();
        }

        #endregion
    }
}