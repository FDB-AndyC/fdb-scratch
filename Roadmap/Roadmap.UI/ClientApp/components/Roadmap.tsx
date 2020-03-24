import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import 'query-string';
import * as DataStructures from "./data-structures";
import IRoadmap = DataStructures.IRoadmap;

interface IState {
    roadmap: IRoadmap | null;
    loading: boolean;
}

export class Roadmap extends React.Component<RouteComponentProps<{}>, IState> {
    constructor() {
        super();
        this.state = { roadmap: null, loading: true };
        // console.log(this.props.location.search);
        console.log('props:' + this.props);
        //console.log('roadmap id: ' + this.props.roadmap.id);

        fetch('api/Roadmaps/GetRoadmap')
            .then(response => response.json() as Promise<IRoadmap>)
            .then(data => {
                this.setState({ roadmap: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading || (this.state.roadmap == null)
            ? <p><em>Loading roadmap...</em></p>
            : Roadmap.renderRoadmap(this.state.roadmap as IRoadmap);

        return <div>
            <h1>Roadmaps</h1>
            <p>This component demonstrates fetching a single roadmap from the server.</p>
            { contents }
        </div>;
    }

    private static renderRoadmap(roadmap: IRoadmap) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                </tr>
            </thead>
            <tbody>
                <tr key={ roadmap.id }>
                    <td>{ roadmap.name }</td>
                    <td>{ roadmap.description }</td>
                </tr>
            </tbody>
        </table>;
    }
}
