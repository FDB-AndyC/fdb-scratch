export interface IRoadmap {
    id: string;
    name: string;
    description: string;
    swimlanes: ISwimlane[];
    milestones: IMilestone[];
}

export interface ISwimlane {
    id: string;
    name: string;
    deliverables: IDeliverable[];
}

export interface IMilestone {
    id: string;
    name: string;
    eventDate: string;
}

export interface IDeliverable {
    id: string;
    name: string;
    description: string;
    startDate: string;
    endDate: string;
    category: ICategory;
}

export interface ICategory {
    id: string;
    name: string;
    colourIndex: number;
}