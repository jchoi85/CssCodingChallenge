import * as React from 'react';
import { IShelf } from '../interfaces/IShelf';
import Shelf from './Shelf';

interface IShelvesContainerProps {
    shelves: IShelf[]
}

class ShelvesContainer extends React.PureComponent<IShelvesContainerProps> {
    renderShelf = (shelf: IShelf) => {
        return (
            <Shelf
                key={shelf.type}
                shelf={shelf}
            />
        )
    }

    public render() {
        return (
            <div className="shelves-container">
                {
                    this.props.shelves.map(shelf => this.renderShelf(shelf))
                }
            </div>
        );
    }
};

export default ShelvesContainer;