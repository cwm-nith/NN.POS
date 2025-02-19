﻿using NN.POS.Model.Enums;

namespace NN.POS.Model.Dtos.Purchases.PurchaseOrders;

public class CreatePurchaseOrderDto : IBaseDto
{
    public int SupplyId { get; set; }
    public int BranchId { get; set; }
    public int PurCcyId { get; set; }
    public int WarehouseId { get; set; }
    public string InvoiceNo { get; set; } = string.Empty;
    public DateTime? PostingDate { get; set; }
    public DateTime? DocumentDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal SubTotalSys { get; set; }
    public decimal DiscountValue { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TaxValue { get; set; }
    public decimal BalanceDue { get; set; }
    public decimal PurRate { get; set; }
    public decimal BalanceDueSys { get; set; }
    public string Remark { get; set; } = string.Empty;
    public decimal DownPayment { get; set; }
    public decimal AppliedAmount { get; set; }
    public decimal ReturnAmount { get; set; }
    public PurchaseStatus Status { get; set; }
    public List<CreatePurchaseOrderDetailDto> PurchaseOrderDetails { get; set; } = [];
}