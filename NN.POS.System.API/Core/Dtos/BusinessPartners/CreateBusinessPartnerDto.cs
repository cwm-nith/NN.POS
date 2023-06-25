﻿using NN.POS.System.API.Core.Enums;

namespace NN.POS.System.API.Core.Dtos.BusinessPartners;

public class CreateBusinessPartnerDto : IBaseDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public BusinessPartnerEnum.ContactType ContactType { get; set; }

    public BusinessPartnerEnum.BusinessType BusinessType { get; set; }


}