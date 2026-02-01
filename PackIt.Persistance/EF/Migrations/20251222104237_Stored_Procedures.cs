using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PackIt.Persistance.EF.Migrations
{
    /// <inheritdoc />
    public partial class Stored_Procedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(
                @"CREATE PROCEDURE [packing].[GetLocationTreeById](@locationId bigint)
                    AS
                    BEGIN
                    SET NOCOUNT ON;
                    
                    WITH RecursiveCTE
                    AS(
                    Select * --, 0 as DepthInTree, PathInTree = '/' + CAST(Id as VARCHAR(4000))
                    FROM [packing].[Locations]
                    WHERE Id = @locationId
                    UNION ALL
                    Select l.* --, r.DepthInTree +1, PathInTree =   r.PathInTree+ '/' + CAST(l.Id AS VARCHAR(20))
        
                    FROM [packing].[Locations] l
                    INNER JOIN RecursiveCTE r ON l.ParentId = r.Id
                    )
                    SELECT * FROM RecursiveCTE option (maxrecursion 50)
                    END
                    "
                );

            migrationBuilder.Sql(
              @"CREATE PROCEDURE [packing].[SPGetLocationAncestorsPath](@locationId bigint)
                    AS
                    BEGIN
                    SET NOCOUNT ON;
                    
                    WITH RecursiveCTE
                    AS(
                    Select *, PathInTree = CONVERT(VARCHAR(4000), Id) 
                    FROM [packing].[Locations]
                    WHERE Id = @locationId
                    UNION ALL
                    Select l.*,PathInTree = CONVERT(VARCHAR(4000), r.PathInTree + cast('/' as varchar(1)) + cast(l.Id as varchar(20)))
                    FROM [packing].[Locations] l
                    INNER JOIN RecursiveCTE r ON l.Id = r.ParentId
                    )
                    SELECT * FROM RecursiveCTE 
                    order by PathInTree DESC
                    option (maxrecursion 50)

                    END
                    "
              );


            //Used in IOrderReadService to check if new Order with the same items and quantity exists.
            migrationBuilder.Sql(
                @"
                    CREATE TYPE [packing].[OrderItemKeys] AS Table
                    (Code Varchar(30),
                    Quantity INT
                )
                "
                );

            migrationBuilder.Sql
               (@"
                    CREATE PROCEDURE [packing].[GetOrdersByStatusAndItems]
                    ( 
                           @OrderStatus nvarchar(30),
                        @RequestedLocationCode VARCHAR(30),  
                        @RequestedDeliveryDate datetime2,
                        @OrderItemKeys OrderItemKeys READONLY)
                    AS
                    BEGIN
                    SET NOCOUNT ON;

                    DECLARE  @OrderItemsCount INT;
                    SELECT @OrderItemsCount = COUNT(*) FROM @OrderItemKeys;

                SELECT CAST
                        (CASE WHEN 
                            EXISTS(
                                SELECT  1 From [packing].[Orders] o
                                WHERE 
                                o.RequestedDeliveryTime =@RequestedDeliveryDate
                                AND
                                o.Order_State = @OrderStatus 
                                AND 
                                o.Requested_Location_Code =@requestedLocationCode
                                AND 
                                (
                                Select COUNT(*) FROM [packing].[OrderItems] i
                                WHERE i.Order_Id = o.Id) = @OrderItemsCount
                                AND 
                                NOT EXISTS
                                (
                                    SELECT 1 FROM [packing].[OrderItems] oi
                                    WHERE oi.Order_Id = o.Id
                                     AND
                                        NOT EXISTS
                                        (
                                            SELECT 1 FROM @OrderItemKeys oik
                                            where oik.Code = oi.Item_Code and oik.Quantity = oi.Quantity
                                
                                            )
                                )
                                ) THEN 1 ELSE 0 END as bit
                    ) AS Result

                    END
                "
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                    drop procedure [packing].[SPGetLocationAncestorsPath];
                    drop procedure [packing].[GetOrdersByStatusAndItems];
                    drop procedure [packing].[GetLocationTreeById];
                    drop type [packing].[OrderItemKeys];
                "
                );
        }
    }
}
