using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Municorn.Infrastructure.Commands;
using Municorn.Infrastructure.Queries;
using Municorn.Infrastructure.Requests;

namespace Municorn.WebAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
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
        public async Task<ObjectResult> GetNotificationStatusById([FromQuery][Required][StringLength(50)] string id, [FromRoute][Required] int version = 1)
        {
            var query = new GetNotificationStatusQuery(id);
            var response = await _mediator.Send(query);
            return StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Метод для создания и одновременной отправки уведомления
        /// </summary>
        /// <response code="201">Created</response>            
        /// <response code="400">Bad request</response>                                                                                                                      
        /// <response code="500">Internal Server Error</response>
        /// <param name="request">Данные уведомления</param>    
        /// <param name="version">Версия контроллера</param>
        /// <returns>Метод должен возвращать идентификатор уведомления и статус его отправки</returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("android")]
        public async Task<ObjectResult> CreateAndSendNotification([FromBody][Required] CreateAndroidNotificationRequest request, [FromRoute][Required] int version = 1)
        {
            var command = new CreateAndroidNotificationCommand(request);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }

        /// <summary>
        /// Метод для создания и одновременной отправки уведомления
        /// </summary>
        /// <response code="201">Created</response>            
        /// <response code="400">Bad request</response>                                                                                                                      
        /// <response code="500">Internal Server Error</response>
        /// <param name="request">Данные уведомления</param>    
        /// <param name="version">Версия контроллера</param>
        /// <returns>Метод должен возвращать идентификатор уведомления и статус его отправки</returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("iOS")]
        public async Task<ObjectResult> CreateAndSendNotification([FromBody][Required] CreateiOSNotificationRequest request, [FromRoute][Required] int version = 1)
        {
            var command = new CreateiOSNotificationCommand(request);
            var response = await _mediator.Send(command);
            return StatusCode((int)HttpStatusCode.Created, response);
        }
    }
}
