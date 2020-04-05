import * as React from 'react';
import { IOrder, IShelf } from '../interfaces/IShelf';

interface IShelfProps {
    shelf: IShelf
}

class Shelf extends React.PureComponent<IShelfProps> {
    renderOrder = (order: IOrder) => {
        return (
            <div key={order.name + "-" + order.value} className='single-order'>
                <h5>{order.name}</h5>
                {order.value}
            </div>
        )
    }
    public render() {
        return (
            <div>
                <div className='shelf-title'>
                    <h1>{this.props.shelf.type}</h1>
                </div>
                {this.props.shelf.orders.map(order => this.renderOrder(order))}
            </div>
        );
    }
};

export default Shelf;