namespace API.Controllers
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using API.Interfaces;
    using API.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PriceController> _logger;

        public PriceController(IHttpClientFactory httpClientFactory, ILogger<PriceController> logger, ICacheService cacheService)
        {
            _httpClientFactory = httpClientFactory;
            _cacheService = cacheService;
            _logger = logger;
        }

        [Route("{overviewType}/{league}/{type}/{language}")]
        [HttpGet]
        public async Task<IActionResult> GetPrices(NinjaOverviewTypeEnum overviewType, string league, string type, string language)
        {
            var cacheKey = BuildCacheKey(overviewType, league, type, language);
            var prices = await _cacheService.Get(cacheKey);
            if (prices == null)
            {
                var url = BuildUrl(overviewType, league, type, language);
                var priceModel = await GetPricesFromNinja(url);
                await _cacheService.Add(cacheKey, priceModel.Data, priceModel.Expires);
                prices = priceModel.Data;
            }

            return Ok(prices);
        }

        private string BuildCacheKey(NinjaOverviewTypeEnum overviewType, string league, string type, string language)
        {
            return $"{overviewType}{league}{type}{language}";
        }

        private string BuildUrl(NinjaOverviewTypeEnum overviewType, string league, string type, string language)
        {
            var url = "https://poe.ninja/api/data/";
            switch (overviewType)
            {
                case NinjaOverviewTypeEnum.Currency:
                    url += "ItemOverview/";
                    break;
                case NinjaOverviewTypeEnum.Item:
                    url += "CurrencyOverview/";
                    break;
            }

            url += $"?league={league}&type={type}&language={language}";
            return url;
        }

        private async Task<PriceResponseModel> GetPricesFromNinja(string url)
        {
            using var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            //Default expiry
            var absoluteExpireTime = DateTime.UtcNow.AddMinutes(1);

            if (response.Headers.TryGetValues("age", out var ageHeader) &&
                response.Headers.TryGetValues("cache-control", out var cacheControlHeader))
            {
                var age = ageHeader.First();
                var cacheControl = cacheControlHeader.First();
                var maxAge = cacheControl[(cacheControl.LastIndexOf("=") + 1)..];

                if (int.TryParse(age, out var ageInSeconds) && int.TryParse(maxAge, out var maxAgeInSeconds))
                {
                    var expiresInSeconds = maxAgeInSeconds - ageInSeconds;
                    absoluteExpireTime = DateTime.UtcNow.AddSeconds(30);
                }
            }

            var prices = await response.Content.ReadAsStringAsync();

            return new PriceResponseModel
            {
                Expires = absoluteExpireTime,
                Data = prices
            };
        }
    }
}
