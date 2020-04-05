export interface IShelf {
    type: string,
    orders: IOrder[]
}

export interface IOrder {
    name: string,
    value: number
}