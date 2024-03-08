﻿using NN.POS.Model.Dtos.Purchases.PurchaseCreditMemo;
using NN.POS.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NN.POS.API.Infra.Tables.Purchases.PurchaseCreditMemo;

[Table("purchase_credit_memo")]
public class PurchaseCreditMemoTable : BaseTable
{
    [Column("supply_id")]
    public int SupplyId { get; set; }

    [Column("copy_from_id")]
    public int CopyFromId { get; set; }

    [Column("base_on"), StringLength(20)]
    public string BaseOn { get; set; } = string.Empty;

    [Column("base_on_id")]
    public int BaseOnId { get; set; }

    [Column("branch_id")]
    public int BranchId { get; set; }

    [Column("pur_ccy_id")]
    public int PurCcyId { get; set; }

    [Column("sys_ccy_id")]
    public int SysCcyId { get; set; }

    [Column("warehouse_id")]
    public int WarehouseId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("invoice_no"), StringLength(20)]
    public string InvoiceNo { get; set; } = string.Empty;

    [Column("posting_date")]
    public DateTime PostingDate { get; set; }

    [Column("document_date")]
    public DateTime DocumentDate { get; set; }

    [Column("due_date")]
    public DateTime DueDate { get; set; }

    [Column("sub_total", TypeName = "decimal(18,3)")]
    public decimal SubTotal { get; set; }

    [Column("sub_total_sys", TypeName = "decimal(18,3)")]
    public decimal SubTotalSys { get; set; }

    [Column("discount_value", TypeName = "decimal(18,3)")]
    public decimal DiscountValue { get; set; }

    [Column("discount_type")]
    public DiscountType DiscountType { get; set; }

    [Column("tax_rate", TypeName = "decimal(18,3)")]
    public decimal TaxRate { get; set; }

    [Column("tax_value", TypeName = "decimal(18,3)")]
    public decimal TaxValue { get; set; }

    [Column("balance_due", TypeName = "decimal(18,3)")]
    public decimal BalanceDue { get; set; }

    [Column("pur_rate", TypeName = "decimal(18,3)")]
    public decimal PurRate { get; set; }

    [Column("balance_due_sys", TypeName = "decimal(18,3)")]
    public decimal BalanceDueSys { get; set; }

    [Column("remark"), StringLength(500)]
    public string Remark { get; set; } = string.Empty;

    [Column("down_payment", TypeName = "decimal(18,3)")]
    public decimal DownPayment { get; set; }

    [Column("applied_amount", TypeName = "decimal(18,3)")]
    public decimal AppliedAmount { get; set; }

    [Column("return_amount", TypeName = "decimal(18,3)")]
    public decimal ReturnAmount { get; set; }

    [Column("status")]
    public PurchaseStatus Status { get; set; }

    [Column("local_set_rate", TypeName = "decimal(18,3)")]
    public decimal LocalSetRate { get; set; }

    [Column("local_ccy_id")]
    public int LocalCcyId { get; set; }

    public List<PurchaseCreditMemoDetailTable> PurchaseCreditMemoDetails { get; set; } = [];
}

public static class PurchaseCreditMemoExtensions
{
    public static PurchaseCreditMemoTable ToTable(this PurchaseCreditMemoDto p) => new()
    {
        AppliedAmount = p.AppliedAmount,
        BalanceDueSys = p.BalanceDueSys,
        BalanceDue = p.BalanceDue,
        BaseOn = p.BaseOn,
        BaseOnId = p.BaseOnId,
        BranchId = p.BranchId,
        CopyFromId = p.CopyFromId,
        CreatedAt = p.CreatedAt,
        DiscountType = p.DiscountType,
        DiscountValue = p.DiscountValue,
        DocumentDate = p.DocumentDate,
        DownPayment = p.DownPayment,
        DueDate = p.DueDate,
        Id = p.Id,
        InvoiceNo = p.InvoiceNo,
        LocalCcyId = p.LocalCcyId,
        LocalSetRate = p.LocalSetRate,
        PostingDate = p.PostingDate,
        PurCcyId = p.PurCcyId,
        PurchaseCreditMemoDetails = p.PurchaseCreditMemoDetails.Select(i => i.ToTable()).ToList(),
        PurRate = p.PurRate,
        Remark = p.Remark,
        ReturnAmount = p.ReturnAmount,
        Status = p.Status,
        SubTotal = p.SubTotal,
        SubTotalSys = p.SubTotalSys,
        SupplyId = p.SupplyId,
        SysCcyId = p.SysCcyId,
        TaxRate = p.TaxRate,
        TaxValue = p.TaxValue,
        UserId = p.UserId,
        WarehouseId = p.WarehouseId,
    };

    public static PurchaseCreditMemoDto ToDto(this PurchaseCreditMemoTable p, string? branchName = null,
        string? sysCcyName = null,
        string? localCcyName = null,
        string? wsName = null,
        string? purCcyName = null,
        string? supplyName = null,
        string? userName = null) => new()
        {
            AppliedAmount = p.AppliedAmount,
            BalanceDue = p.BalanceDue,
            BalanceDueSys = p.BalanceDueSys,
            BranchId = p.BranchId,
            BranchName = branchName,
            CreatedAt = p.CreatedAt,
            DiscountType = p.DiscountType,
            DiscountValue = p.DiscountValue,
            DocumentDate = p.DocumentDate,
            DownPayment = p.DownPayment,
            DueDate = p.DueDate,
            Id = p.Id,
            CopyFromId = p.CopyFromId,
            InvoiceNo = p.InvoiceNo,
            LocalCcyId = p.LocalCcyId,
            LocalCcyName = localCcyName,
            LocalSetRate = p.LocalSetRate,
            PostingDate = p.PostingDate,
            PurCcyId = p.PurCcyId,
            PurCcyName = purCcyName,
            PurchaseCreditMemoDetails = p.PurchaseCreditMemoDetails.Select(i => i.ToDto()).ToList(),
            PurRate = p.PurRate,
            Remark = p.Remark,
            ReturnAmount = p.ReturnAmount,
            Status = p.Status,
            SubTotal = p.SubTotal,
            SubTotalSys = p.SubTotalSys,
            SupplyId = p.SupplyId,
            SupplyName = supplyName,
            SysCcyId = p.SysCcyId,
            SysCcyName = sysCcyName,
            TaxRate = p.TaxRate,
            TaxValue = p.TaxValue,
            UserId = p.UserId,
            UserName = userName,
            WarehouseId = p.WarehouseId,
            WarehouseName = wsName,
            BaseOn = p.BaseOn,
            BaseOnId = p.BaseOnId
        };
}