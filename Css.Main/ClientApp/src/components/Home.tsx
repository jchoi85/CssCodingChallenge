import * as React from 'react';
import MainApi from '../api/api';
import { IShelf } from '../interfaces/IShelf';
import ShelvesContainer from './ShelfContainer';

interface IHomeProps {
}

interface IHomeState {
    shelves: IShelf[],
}

class Home extends React.Component<IHomeProps, IHomeState> {
    constructor(props: IHomeProps) {
        super(props);
        this.state = {
            shelves: []
        }
    }

    componentDidMount = () => {
        let fetchData = this.fetchData;

        setInterval(function() {
            fetchData()
            }, 1000)
    }

    fetchData = () => {
        MainApi.getShelves().then(data => {
            this.setState({
                shelves: data
            })
        })
    }

    startOrders = async () => {
        await MainApi.startOrders();
    }

    stopOrders = async () => {
        await MainApi.stopOrders();
    }

    public render() {
        return (
            <div className="container">
                <div className="row">
                    <div className="buttons">
                        <button className="btn-primary" onClick={this.startOrders}>Start Orders</button>
                        <button className="btn-primary" onClick={this.stopOrders}>Stop Orders</button>
                    </div>
                </div>
                <div className="row">
                    <ShelvesContainer shelves={this.state.shelves} />
                </div>
            </div>
        );
    }
};

export default Home;