﻿using MediatR;
using NN.POS.Model.Dtos.Currencies;

namespace NN.POS.API.App.Queries.Currencies;

public class GetExchangeRateByIdQuery(int id) : IRequest<ExchangeRateDto>
{
    public int Id => id;
}