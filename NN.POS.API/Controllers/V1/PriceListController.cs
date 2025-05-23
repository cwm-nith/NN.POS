﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using NN.POS.API.App.Commands.PriceLists;
using NN.POS.API.App.Queries.PriceLists;
using NN.POS.Common.Pagination;
using NN.POS.Model.Dtos.PriceLists;

namespace NN.POS.API.Controllers.V1;

public class PriceListController(IMediator mediator) : BaseApiController
{
    #region Price List

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreatePriceListDto body)
    {
        await mediator.Send(new CreatePriceListCommand(body));
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdatePriceListDto body)
    {
        await mediator.Send(new UpdatePriceListCommand
        {
            Id = id,
            Name = body.Name
        });
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await mediator.Send(new DeletePriceListCommand(id));
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<PriceListDto>>> GetPage([FromQuery] GetPagePriceListDto q)
    {
        var query = new GetPagePriceListQuery
        {
            Page = q.Page,
            Results = q.Results,
            Search = q.Search,
            ExcludeId = q.ExcludeId
        };
        var data = await mediator.Send(query);
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PriceListDto>> GetById(int id)
    {
        var query = new GetPriceListByIdQuery(id);
        var data = await mediator.Send(query);
        return Ok(data);
    }

    #endregion

    #region Price list detail

    [HttpPost("detail")]
    public async Task<ActionResult> CreateDetail([FromBody] List<CreatePriceListDetailDto> body)
    {
        var cmd = new CreatePriceListDetailCommand(body);
        await mediator.Send(cmd);
        return Ok();
    }

    [HttpPut("detail/{id:int}")]
    public async Task<ActionResult> UpdateDetail(int id, [FromBody] UpdatePriceListDetailDto body)
    {
        var cmd = new UpdatePriceListDetailCommand(id, body);
        await mediator.Send(cmd);
        return Ok();
    }

    [HttpGet("detail/{id:int}")]
    public async Task<ActionResult> GetDetailById(int id)
    {
        var data = await mediator.Send(new GetPriceListDetailByIdQuery(id));
        return Ok(data);
    }

    [HttpGet("detail")]
    public async Task<ActionResult> GetDetailPage([FromQuery] GetPagePriceListDetailDto dto)
    {
        var data = await mediator.Send(new GetPagePriceListDetailQuery
        {
            Page = dto.Page,
            Results = dto.Results,
            Search = dto.Search,
            PriceListId = dto.PriceListId
        });
        return Ok(data);
    }

    [HttpGet("detail/copy/{priceListId:int}")]
    public async Task<ActionResult<List<PriceListDetailDto>>> GetPriceListCopy(int priceListId,
        [FromQuery] int priceListIdCopyFrom)
    {
        return Ok(await mediator.Send(new GetPriceListCopyQuery(priceListId, priceListIdCopyFrom)));
    }

    #endregion
}