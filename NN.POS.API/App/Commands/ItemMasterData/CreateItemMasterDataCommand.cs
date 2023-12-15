﻿using MediatR;
using NN.POS.Model.Dtos.ItemMasters;

namespace NN.POS.API.App.Commands.ItemMasterData;

public class CreateItemMasterDataCommand(CreateItemMasterDataDto dto) : IRequest
{
    public CreateItemMasterDataDto Dto => dto;
}