import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import 'query-string';

interface IState {
    roadmap: IRoadmap | null;
    loading: boolean;
}

export class Roadmap extends React.Component<RouteComponentProps<{}>, IState> {
    constructor() {
        super();
        this.state = { roadmap: null, loading: true };
        console.log(this.props.location.search);

        fetch('api/Roadmap/GetRoadmap')
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
                <tr key={ roadmap.id}>
                    <td>{ roadmap.name }</td>
                    <td>{ roadmap.description }</td>
                </tr>
            </tbody>
        </table>;
    }
}

interface IRoadmap {
    id:string;
    name: string;
    description: string;
    swimlanes: ISwimlane[];
    milestones: IMilestone[];
}

interface ISwimlane {
    id: string;
    name: string;
    deliverables: IDeliverable[];
}

interface IMilestone {
    id: string;
    name: string;
    eventDate: string;
}

interface IDeliverable {
    id: string;
    name: string;
    description: string;
    startDate: string;
    endDate: string;
    category: ICategory;
}

interface ICategory {
    id: string;
    name: string;
    colourIndex: number;
}