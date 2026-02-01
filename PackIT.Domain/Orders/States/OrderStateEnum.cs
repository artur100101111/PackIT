namespace PackIT.Domain.Orders.States
{
    public enum OrderStateEnum
    {
        New = 0,
        InPacking = 1,
        Packed = 2,
        InDelivery = 3,
        Delivered = 4,
        Canceled = 5
    }
}