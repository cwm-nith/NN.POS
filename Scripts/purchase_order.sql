USE [NN.POS.System]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[purchase_orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[supply_id] [int] NOT NULL,
  [branch_id] [int] NOT NULL,
	[pur_ccy_id] [int] NOT NULL,
	[sys_ccy_id] [int] NULL,
	[warehouse_id] [int] NOT NULL,
  [user_id] [int] NOT NULL,
  [invoice_no] [nvarchar](20) NOT NULL UNIQUE,
  [posting_date] [datetime] NOT NULL,
  [document_date] [datetime] NOT NULL,
  [delivery_date] [datetime] NOT NULL,
  [sub_total] [decimal](18,3) NOT NULL,
  [sub_total_sys] [decimal](18,3) NOT NULL,
	[discount_value] [decimal](18, 3) NOT NULL,
  [discount_rate] [decimal](18,3) NOT NULL,
	[discount_type] [nvarchar](20) NOT NULL,
  [tax_rate] [decimal](18,3) NOT NULL,
  [tax_value] [decimal](18,3) NOT NULL,
  [balance_due] [decimal](18,3) NOT NULL,
  [pur_rate] [decimal](18,3) NOT NULL,
  [balance_due_sys] [decimal](18,3) NOT NULL,
  [remark] [nvarchar](500) NULL,
  [down_payment] [decimal](18, 3) NOT NULL,
  [applied_amount] [decimal](18, 3) NOT NULL,
  [return_amount] [decimal](18, 3) NOT NULL,
  [status] [nvarchar](20) NOT NULL,
	[local_ccy_id] [int] NOT NULL,
	[local_set_rate] [decimal](18, 3) NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_purchase_orders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE INDEX invoice_no_purchase_orders
ON [dbo].[purchase_orders] ([invoice_no]);

GO

ALTER TABLE [dbo].[purchase_order_details]  WITH CHECK ADD  CONSTRAINT [FK_purchase_order_details_price_lists] FOREIGN KEY([purchase_order_id])
REFERENCES [dbo].[purchase_orders]([id])
GO

ALTER TABLE [dbo].[purchase_order_details] CHECK CONSTRAINT [FK_purchase_order_details_price_lists]
GO



