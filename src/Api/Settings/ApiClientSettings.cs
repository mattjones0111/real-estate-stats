using System;

namespace FundaApiClient.Settings
{
    public sealed class ApiClientSettings
    {
        static readonly Lazy<ApiClientSettings> Loader =
            new Lazy<ApiClientSettings>(Load);

        private static ApiClientSettings Load()
        {
            return new ApiClientSettings();
        }

        public static ApiClientSettings Instance = Loader.Value;

        string _apiKey;

        public string ApiKey
        {
            get
            {
                if (string.IsNullOrEmpty(_apiKey))
                {
                    throw new InvalidOperationException(
                        "Funda Api Client settings are not set. " +
                        "Use ApiClientSettings to set the Api key.");
                }

                return _apiKey;
            }
            set => _apiKey = value;
        }

        int _defaultPageSize = 25;

        public int DefaultPageSize
        {
            get => _defaultPageSize;
            set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentException(
                        "Default page size must be between 1 and 100.");
                }

                _defaultPageSize = value;
            }
        }

        int _retryCounts = 5;

        public int RetryCounts
        {
            get => _retryCounts;
            set
            {
                if (_retryCounts < 1 || _retryCounts > 20)
                {
                    throw new ArgumentException(
                        "Retry counts must be between 1 and 20.");
                }

                _retryCounts = value;
            }
        }
    }
}
