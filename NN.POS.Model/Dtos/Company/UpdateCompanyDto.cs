﻿namespace NN.POS.Model.Dtos.Company;

public class UpdateCompanyDto : IBaseDto
{
    public string Name { get; set; } = string.Empty;
    public int PriceListId { get; set; }
    public int SysCcyId { get; set; }
    public int LocalCcyId { get; set; }
    public string? Location { get; set; }
    public string? Address { get; set; }
}