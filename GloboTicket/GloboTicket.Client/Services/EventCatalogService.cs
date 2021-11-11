using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GloboTicket.Web.Extensions;
using GloboTicket.Web.Models.Api;
using Microsoft.Extensions.Logging;

namespace GloboTicket.Web.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient client;
        private readonly ILogger<EventCatalogService> _logger;

        public EventCatalogService(HttpClient client,
            ILogger<EventCatalogService> logger)
        {
            this.client = client;
            this._logger = logger;
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            _logger.LogInformation("before /api/events call!!!");
            var response = await client.GetAsync("/api/events");
            _logger.LogInformation("after /api/events called!!!");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<IEnumerable<Event>> GetByCategoryId(Guid categoryid)
        {
            _logger.LogInformation( $" before /api/events/?categoryId={categoryid} call!!!");
            var response = await client.GetAsync($"/api/events/?categoryId={categoryid}");
            _logger.LogInformation($" after /api/events/?categoryId={categoryid} called!!!");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<Event> GetEvent(Guid id)
        {
            var response = await client.GetAsync($"/api/events/{id}");
            return await response.ReadContentAs<Event>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var response = await client.GetAsync("/api/categories");
            return await response.ReadContentAs<List<Category>>();
        }

    }
}
