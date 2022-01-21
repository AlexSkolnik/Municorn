using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Municorn.Infrastructure.Commands;
using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Queries;
using Municorn.Infrastructure.Requests;
using Municorn.Infrastructure.Responses;

namespace Municorn.WebAPI.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Метод для получения статуса отправки уведомления по его идентификатору
        /// </summary>
        /// <response code="201">Created</response>            
        /// <response code="400">Bad request</response>                                                                                                                      
        /// <response code="500">Internal Server Error</response>
        /// <param name="id">Идентификатор уведомления</param>   
        /// <param name="version">Версия контроллера</param>
        /// <returns>NotificationStatus</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult<NotificationStatus>> GetNotificationStatusById([FromQuery][Required][StringLength(50)] string id, [FromRoute][Required] int version = 2)
        {
            var query = new GetNotificationStatusQuery(id);
            var response = await _mediator.Send(query);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Метод для создания и одновременной отправки уведомления
        /// </summary>
        /// <remarks>
        /// Пример запроса
        ///  
        ///     {
        ///        "deviceType": "Android",
        ///        "jsonObject": {"deviceToken":"deviceToken","message":"Внимание","title":"Уведомление","condition":"string"}
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Created</response>            
        /// <response code="400">Bad request</response>                                                                                                                      
        /// <response code="500">Internal Server Error</response>
        /// <param name="request">Данные уведомления</param>    
        /// <param name="version">Версия контроллера</param>
        /// <returns>Метод должен возвращать идентификатор уведомления и статус его отправки</returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<CreateNotificationResponse>> CreateAndSendNotification([FromBody] CreateCommonNotificationRequest request, [FromRoute][Required] int version = 2)
        {
            IRequest<CreateNotificationResponse> command;
            var jsonSerializerOptions = new JsonSerializerOptions() 
            {
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            switch (request.DeviceType)
            {
                case DeviceType.Android:
                    var androidData = JsonSerializer.Deserialize<CreateAndroidNotificationRequest>(request.JsonObject.ToString(), jsonSerializerOptions);
                    command = new CreateAndroidNotificationCommand(androidData);
                    break;

                case DeviceType.iOS:
                    var iOSData = JsonSerializer.Deserialize<CreateiOSNotificationRequest>(request.JsonObject.ToString(), jsonSerializerOptions);
                    command = new CreateiOSNotificationCommand(iOSData);
                    break;

                default:
                    return StatusCode((int)HttpStatusCode.BadRequest, "Неизвестный тип устройства");
            }

            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }
    }
}
