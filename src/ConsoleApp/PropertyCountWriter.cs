using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FundaApiClient;
using FundaApiClient.Model;

namespace ConsoleApp
{
    public sealed class PropertyCountWriter
    {
        readonly IFetchProperties _fetcher;
        readonly Func<IEnumerable<Property>, IEnumerable<Counted<RealEstateAgent>>> _groupFunc;
        readonly Encoding _encoding;

        public PropertyCountWriter(
            IFetchProperties fetcher,
            Func<IEnumerable<Property>, IEnumerable<Counted<RealEstateAgent>>> groupFunc,
            Encoding encoding = null)
        {
            _fetcher = fetcher;
            _groupFunc = groupFunc;
            _encoding = encoding ?? Encoding.UTF8;
        }

        public string SearchText { get; private set; }

        public void SetSearchText(string searchText)
        {
            SearchText = searchText ?? throw new ArgumentNullException(nameof(searchText));
        }

        public async Task WriteToAsync(Stream @out)
        {
            const string crlf = "\r\n";

            var properties = await _fetcher.GetPropertiesAsync(SearchText);

            var result = _groupFunc(properties);

            foreach (var g in result)
            {
                string line = string.Concat(g.ToString(), crlf);

                byte[] lineAsBytes = _encoding.GetBytes(line);

                @out.Write(lineAsBytes, 0, lineAsBytes.Length);
            }
        }
    }
}
