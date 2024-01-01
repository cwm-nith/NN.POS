﻿using MediatR;
using NN.POS.Model.Dtos.Currencies;

namespace NN.POS.API.App.Queries.Currencies;

public class GetLocalCurrencyQuery : IRequest<CurrencyDto>
{
}