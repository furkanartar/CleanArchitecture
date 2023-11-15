using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListModelQuery getListModelQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListModelListItemDto> response = await Mediator.Send(getListModelQuery);

            return Ok(response);
        }

        [HttpPost("GetList/ByDynamic")] //body'den değer aldığım için post
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery = null)
        {
            /*
             * Örnek body:
             * 
                {
                  "sort": [
                    {
                      "field": "name", //hangi field'ı sıralamada kullanacağız?
                      "dir": "asc" //asc ya da desc
                    }
                  ],
                  "filter": {
                    "field": "name", //hangi alana göre filtreleyeceğiz?
                    "value": "420D", //operatöre göre filtrelenecek değer (örneğin name alanı içerisinde eq operatörünü kullanacak olursak 420D içeren veriler gelir), 
                    "operator": "eq", //eq, neq vb devamı aşağıda
                    "logic": "and", //and ya da or başka filtreleri birleştirmek için...
                    "filters": [ // başka filtrelerde eklemek için
                      "string"
                    ]
                  }
                }


            diğer operatörler: (soldaki değer sizden istenen. Örneğin startswith)
            { "eq", "=" },
            { "neq", "!=" },
            { "lt", "<" },
            { "lte", "<=" },
            { "gt", ">" },
            { "gte", ">=" },
            { "isnull", "== null" },
            { "isnotnull", "!= null" },
            { "startswith", "StartsWith" },
            { "endswith", "EndsWith" },
            { "contains", "Contains" },
            { "doesnotcontain", "Contains" }



            ÖRNEK 2:

            {
              "sort": [
                {
                  "field": "name",
                  "dir": "asc"
                }
              ],
              "filter": {
                "field": "name",
                "value": "420D",
                "operator": "eq",
                "logic": "and",
                "filters": [
                  {
                    "field": "Fuel.name",
                    "value": "iz",
                    "operator": "contains"
                  }
                ]
              }
            }
             
             */

            GetListByDynamicModelQuery getListByDynamicModelQuery = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };
            GetListResponse<GetListByDynamicModelListItemDto> response = await Mediator.Send(getListByDynamicModelQuery);
            return Ok(response);
        }
    }
}
