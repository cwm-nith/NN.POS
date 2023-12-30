﻿using Microsoft.EntityFrameworkCore;
using NN.POS.API.Infra.DbConfigs;
using NN.POS.API.Infra.Tables.BusinessPartners;
using NN.POS.API.Infra.Tables.Company;
using NN.POS.API.Infra.Tables.Currencies;
using NN.POS.API.Infra.Tables.ItemMasters;
using NN.POS.API.Infra.Tables.PriceLists;
using NN.POS.API.Infra.Tables.Roles;
using NN.POS.API.Infra.Tables.Tax;
using NN.POS.API.Infra.Tables.UnitOfMeasures;
using NN.POS.API.Infra.Tables.User;
using NN.POS.API.Infra.Tables.Warehouses;

namespace NN.POS.API.Infra.Tables;

public class DataDbContext(DbContextOptions<DataDbContext> options) : DbContext(options)
{
    public DbSet<UserTable>? Users { get; set; }
    public DbSet<RoleTable>? Roles { get; set; }
    public DbSet<UserRoleTable>? UserRoles { get; set; }
    public DbSet<BusinessPartnerTable>? BusinessPartners { get; set; }
    public DbSet<CustomerGroupTable>? CustomerGroups { get; set; }
    public DbSet<PriceListDetailTable>? PriceListDetails { get; set; }
    public DbSet<PriceListTable>? PriceLists { get; set; }
    public DbSet<UnitOfMeasureDefineTable>? UnitOfMeasureDefines { get; set; }
    public DbSet<UnitOfMeasureGroupTable>? UnitOfMeasureGroups { get; set; }
    public DbSet<UnitOfMeasureTable>? UnitOfMeasures { get; set; }
    public DbSet<ItemMasterDataTable>? ItemMasterData { get; set; }
    public DbSet<CurrencyTable>? Currencies { get; set; }
    public DbSet<WarehouseTable>? Warehouses { get; set; }
    public DbSet<CompanyTable>? Companies { get; set; }
    public DbSet<BranchTable>? Branches { get; set; }
    public DbSet<TaxTable>? Tax { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .AddPriceListTableRelationship()
            .ItemMasterDataTableDbConfig()
            .BusinessPartnerConfig()
            .TaxTableDbConfig();
    }
}