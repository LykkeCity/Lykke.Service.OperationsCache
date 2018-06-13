using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Lykke.Service.OperationsCache.Core.Domain;
using Lykke.Service.OperationsCache.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.OperationsCache.Controllers
{
    [Route("api/[controller]")]
    public class OperationsHistoryController : Controller
    {
        private readonly IHistoryCache _historyCache;

        public OperationsHistoryController(IHistoryCache historyCache)
        {
            _historyCache = historyCache ?? throw new ArgumentNullException(nameof(historyCache));
        }

        /// <summary>
        /// Get operations history.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("GetHistory")]
        [ProducesResponseType(typeof(IEnumerable<HistoryEntry>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetHistory(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                return BadRequest($"{nameof(clientId)} is empty");

            var history = await _historyCache.GetRecordsByClient(clientId);
            return Ok(history);
        }

        /// <summary>
        /// Get operations history for a particular asset.
        /// </summary>
        [HttpGet("assethistory")]
        [SwaggerOperation("GetAssetHistory")]
        [ProducesResponseType(typeof(IEnumerable<HistoryEntry>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAssetHistory(string clientId, string assetId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                return BadRequest($"{nameof(clientId)} is empty");

            if (string.IsNullOrWhiteSpace(assetId))
                return BadRequest($"{nameof(assetId)} is empty");

            var history = await _historyCache.GetRecordsForAssetByClientAsync(clientId, assetId);
            return Ok(history);
        }

        /// <summary>
        /// Get operations history for a set of assets.
        /// </summary>
        [HttpGet("assetshistory")]
        [SwaggerOperation("GetAssetsHistory")]
        [ProducesResponseType(typeof(IEnumerable<HistoryEntry>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAssetHistory(string clientId, string[] assetIds)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                return BadRequest($"{nameof(clientId)} is empty");

            if (assetIds == null || assetIds.Length == 0)
                return BadRequest($"{nameof(assetIds)} is empty");

            var history = await _historyCache.GetRecordsForAssetsByClientAsync(clientId, assetIds);
            return Ok(history);
        }
    }
}
