import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import * as DataStructures from "./data-structures";
import IRoadmap = DataStructures.IRoadmap;

interface RoadmapState {
    roadmaps: IRoadmap[];
    loading: boolean;
}

export class Roadmaps extends React.Component<RouteComponentProps<{}>, RoadmapState> {
    constructor() {
        super();
        this.state = { roadmaps: [], loading: true };

        fetch('api/Roadmaps/GetAll')
            .then(response => response.json() as Promise<IRoadmap[]>)
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

    private static renderRoadmaps(roadmaps: IRoadmap[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                </tr>
            </thead>
            <tbody>
            {roadmaps.map(roadmap =>
                    <tr key={roadmap.id}>
                        <td><NavLink to={'/roadmap'} exact activeClassName='active'>
                            <span className='glyphicon glyphicon-home'></span> {roadmap.name}
                            </NavLink>
                            <a href={"Roadmap?id=" + roadmap.id}>{roadmap.name}</a></td>
                    <td>{ roadmap.description }</td>
                </tr>
            )}
            </tbody>
        </table>;
    }
}

//interface Roadmap {
//    id:string;
//    name: string;
//    description: string;
//}
