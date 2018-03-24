import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface RoadmapState {
    roadmaps: Roadmap[];
    loading: boolean;
}

export class Roadmaps extends React.Component<RouteComponentProps<{}>, RoadmapState> {
    constructor() {
        super();
        this.state = { roadmaps: [], loading: true };

        fetch('api/Roadmap/Roadmaps')
            .then(response => response.json() as Promise<Roadmap[]>)
            .then(data => {
                this.setState({ roadmaps: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading!...</em></p>
            : Roadmaps.renderRoadmaps(this.state.roadmaps);

        return <div>
            <h1>Roadmaps</h1>
            <p>This component demonstrates fetching roadmaps from the server.</p>
            { contents }
        </div>;
    }

    private static renderRoadmaps(roadmaps: Roadmap[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Start</th>
                    <th>End</th>
                </tr>
            </thead>
            <tbody>
            {roadmaps.map(roadmap =>
                <tr key={ roadmap.id}>
                    <td>{ roadmap.name }</td>
                    <td>{ roadmap.description }</td>
                    <td>{ roadmap.startDate }</td>
                    <td>{ roadmap.endDate }</td>
                </tr>
            )}
            </tbody>
        </table>;
    }
}

interface Roadmap {
    id:string;
    name: string;
    description: string;
    startDate: string;
    endDate: string;

}
